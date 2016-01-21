using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{

    public class DbfFileInfo : NotificationObject, IComparable<DbfFileInfo>
    {
        #region Life Cycle

        public DbfFileInfo(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
                return;

            this.DBFInfo = new FileInfo(fileName);
        }

        public FileInfo DBFInfo
        {
            get { return this._DBFInfo; }
            set
            {
                if (value != this._DBFInfo)
                {
                    this._DBFInfo = value;

                    this.RaisePropertyChanged("FileName");
                    this.RaisePropertyChanged("Name");
                    this.RaisePropertyChanged("ModifyTime");
                    this.RaisePropertyChanged("FileSize");
                    this.RaisePropertyChanged("FileType");
                }
            }
        }
        private FileInfo _DBFInfo;

        #endregion

        public string FileName
        {
            get 
            {
                if (this.DBFInfo == null)
                    return string.Empty;

                return this.DBFInfo.FullName;
            }
        }

        public string Name
        {
            get 
            {
                if (this.DBFInfo == null)
                    return string.Empty;

                return this.DBFInfo.Name;
            }
        }

        public string ModifyTime
        {
            get
            {
                if (this.DBFInfo == null)
                    return string.Empty;

                return this.DBFInfo.LastWriteTime.ToString("yyyy/MM/dd HH:mm");
            }
        }

        public string FileType
        {
            get
            {
                if (this.DBFInfo == null)
                    return string.Empty;

                return Path.GetExtension(this.DBFInfo.FullName).Trim(new char[]{'.'}).ToUpper() + " 文件";
            }
        }

        public string FileSize
        {
            get
            {
                if (this.DBFInfo == null)
                    return string.Empty;
                
                return Convert.ToString(this.DBFInfo.Length / 1024) + "KB";
            }
        }

        #region CompareTo

        public int CompareTo(DbfFileInfo other)
        {
            return this.Name.CompareTo(other.Name);
        }

        #endregion
    }
}
