using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Collections.ObjectModel;

namespace Dothan.ExportData
{
    public class DBFManager : NotificationObject
    {
        #region Life Cycle

        public DBFManager(IProject project)
        {
            this.TheProject = project;
        }

        #endregion

        #region TheProject

        public IProject TheProject
        {
            get { return _TheProject; }
            set { _TheProject = value; }
        }
        private IProject _TheProject;

        #endregion

        #region DBFPath

        /// <summary>
        /// DBF文件自动检测文件夹，DBFPathName为给文件夹下的子文件夹。
        /// </summary>
        public string DetectFolder
        {
            get { return ConfigurationHelper.ConnectionStrings[DbfDBHelper.Tag_ConnName].ConnectionString; }
        }

        /// <summary>
        /// 当前DBF文件夹。
        /// </summary>
        public string DbfPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.DetectFolder))
                    return string.Empty;

                return Path.Combine(this.DetectFolder, this.TargetDateName);
            }
        }

        public bool checkDBFFileExists(string filePath, DateTime date)
        {
            return Directory.Exists(Path.Combine(filePath, date.ToString("yyyyMMdd")));
        }

        private bool checkDBFPathExist()
        {
            string dbfPath = this.DbfPath;
            if (string.IsNullOrEmpty(dbfPath))
            {
                this.TheProject.Output.WriteLine("请现在配置文件中指定DBF文件夹所在的位置!");
                this.TheProject.Output.WriteLine();
                return false;
            }

            if (!Directory.Exists(dbfPath))
            {
                this.TheProject.Output.WriteLine("未检测到文件夹：" + dbfPath);
                this.TheProject.Output.WriteLine("请确认配置文件中指定的目标文件夹是否存在！");
                this.TheProject.Output.WriteLine();
                return false;
            }

            return true;
        }

        #endregion

        #region TargetDate

        public DateTime TargetDate
        {
            get { return this._TargetDate; }
            set
            {
                if (this._TargetDate.Date != value.Date)
                {
                    DateTime oldDate = this._TargetDate;
                    this._TargetDate = value;
                    this.RaisePropertyChanged("TargetDate");
                    this.UpdateDbfFileList();
                    this.TheProject.RaiseTargetDateChanged(oldDate, value);
                }
            }
        }
        private DateTime _TargetDate = DateTime.Now;
        public DateTime GetDefaultDateTime()
        {
            DateTime target = DateTime.Now;
            string detectPath = this.DetectFolder;
            if (string.IsNullOrEmpty(detectPath))
                return target;

            // 连续检查7天。
            for (int i = 0; i < 7; i++)
            {
                if (this.checkDBFFileExists(detectPath, target))
                    return target;

                target = target.AddDays(-1);
            }

            return DateTime.Now;
        }

        /// <summary>
        /// 日期格式："yyyyMMdd",eg:20151029;
        /// </summary>
        public string TargetDateName
        {
            get { return GetDateName(this.TargetDate); }
        }

        /// <summary>
        /// 根据给定的日期，获取对应的文件夹名称。
        /// </summary>
        public static string GetDateName(DateTime dt)
        {
            return dt.ToString("yyyyMMdd");
        }

        /// <summary>
        /// 将给定的文件夹名称转换为对应的日期。
        /// </summary>
        public static DateTime ParseDateName(string pathName)
        {
            return ParseDateTime(pathName, "yyyyMMdd");
        }

        public static DateTime ParseDateTime(string dateTime, string format)
        {
            return DateTime.ParseExact(dateTime, format, DateTimeFormatInfo.CurrentInfo);
        }

        #endregion

        #region FileExtension 文件扩展名

        /// <summary>
        /// DBF 动态扩展名，根据当天日期所自动生成的扩展名，大写显示。
        /// </summary>
        public string DExt
        {
            get { return GetExtension(this.TargetDate); }
        }

        public string GetExtension(DateTime dt)
        {
            return string.Format(".{0}{1}", dt.Month.ToString("x"), dt.ToString("dd")).ToUpper();
        }

        /// <summary>
        /// DBF 静态扩展名，大写显示。
        /// </summary>
        public static string SExt
        {
            get { return ".DBF"; }
        }

        #endregion

        #region DBFFileList

        public ObservableCollection<DbfFileInfo> DbfFileList
        {
            get { return this._DbfFileList; }
            set
            {
                if (this._DbfFileList != value)
                {
                    this._DbfFileList = value;
                    this.RaisePropertyChanged("DbfFileList");
                }
            }
        }
        private ObservableCollection<DbfFileInfo> _DbfFileList;

        public DbfFileInfo SelectedFile
        {
            get { return this._SelectedFile; }
            set
            {
                if (this._SelectedFile != value)
                {
                    this._SelectedFile = value;
                    this.RaisePropertyChanged("SelectedFile");
                }
            }
        }
        private DbfFileInfo _SelectedFile;

        public void UpdateDbfFileList()
        {
            if (!this.checkDBFPathExist())
                return;

            if (this.DbfFileList == null)
                this.DbfFileList = new ObservableCollection<DbfFileInfo>();

            string[] files = Directory.GetFiles(this.DbfPath);
            // 1、删除无效的文件；
            foreach (DbfFileInfo item in this.DbfFileList.ToList())
            {
                if (files.Where(o => o.Equals(item.FileName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null)
                    continue;
                this.DbfFileList.Remove(item);
            }
            // 2、添加新的文件；
            foreach (string file in files)
            {
                if (!file.EndsWith(DBFManager.SExt, StringComparison.OrdinalIgnoreCase) && !file.EndsWith(this.DExt, StringComparison.OrdinalIgnoreCase))
                    continue;
                if (this.DbfFileList.Where(o => o.FileName.Equals(file, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null)
                    continue;

                this.DbfFileList.SortAdd<DbfFileInfo>(new DbfFileInfo(file));
            }
        }

        #endregion

        #region SwitchTargetDate

        public bool CanSwitch2Next()
        {
            if (this.TheProject.IsImporting)
                return false;
            if (this.DateIsLatest(this.TargetDate))
                return false;

            return true;
        }

        public bool Switch2Next()
        {
            DateTime date = this.TargetDate;
            while (!this.DateIsLatest(date))
            {
                date = date.AddDays(1);
                string dbfPath = Path.Combine(this.DetectFolder, GetDateName(date));
                if (Directory.Exists(dbfPath))
                {
                    this.TargetDate = date;
                    return true;
                }
            }

            return false;
        }

        public bool Switch2Prev()
        {
            DateTime date = this.TargetDate;
            int index = 0;
            int maxIndex = 30;
            // 检测30天时间，如果未检测则跳出
            while (index < maxIndex)
            {
                date = date.AddDays(-1);
                string dbfPath = Path.Combine(this.DetectFolder, GetDateName(date));
                if (Directory.Exists(dbfPath))
                {
                    this.TargetDate = date;
                    return true;
                }

                index++;
            }

            return false;
        }

        /// <summary>
        /// 判断给定的文件夹名称是不是最新的文件夹。
        /// </summary>
        public bool DateIsLatest(DateTime targetDate)
        {
            DateTime dt = targetDate;
            while (dt.Date < System.DateTime.Now.Date)
            {
                dt = dt.AddDays(1);
                string folderPath = Path.Combine(DetectFolder, GetDateName(dt));
                if (Directory.Exists(folderPath))
                    return false;
            }

            return true;
        }

        #endregion
    }
}
