using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public abstract class DbfImportItem : ImportItem
    {
        #region Life Cycle

        public DbfImportItem(IProject project)
            : base(project, null)
        {
            this.DBHelper = new DbfDBHelper(this.TablePath);
            this.SourceType = ESourceType.DBF;
            this.UpdateFileName();
        }

        #endregion

        #region DBFSource

        public string DBFSource
        {
            get { return this.TablePath; }
        }

        public override string DataSource
        {
            get { return this.TablePath; }
        }

        #endregion

        #region FileName

        /// <summary>
        /// 数据库表名前缀，由于每天的数据库文件名称不一定完全相同，所以用数据库表名的前缀来区分不同的数据库文件。
        /// </summary>
        public abstract override string Name { get; }

        public override string ShownName
        {
            get
            {
                if (string.IsNullOrEmpty(base.TableName))
                    return this.Name;

                return base.TableName;
            }
        }

        public string TablePath
        {
            get
            {
                string connStr = ConfigurationHelper.ConnectionStrings[DbfDBHelper.Tag_ConnName].ConnectionString;
                
                if (string.IsNullOrEmpty(connStr) || this.Date == null)
                    return string.Empty;

                return Path.Combine(connStr, base.DateName);
            }
        }

        public String FileName
        {
            get
            {
                return this._FileName;
            }
            protected set
            {
                if (this._FileName != value)
                {
                    this._FileName = value;
                    this.RaisePropertyChanged("FileName");

                    this.TableName = Path.GetFileName(this.FileName);
                }
            }
        }
        private string _FileName;

        protected virtual void UpdateFileName()
        {
            if (!Directory.Exists(this.TablePath))
                return;

            foreach (string item in Directory.GetFiles(this.TablePath))
            {
                if (Path.GetFileName(item).StartsWith(this.Name))
                {
                    this.FileName = item;
                    return;
                }
            }

            this.FileName = string.Empty;
        }

        public override string TipInfo
        {
            get
            {
                if (string.IsNullOrEmpty(this.FileName))
                    return "未检测到DBF文件！";
                else
                    return this.FileName;
            }
        }

        public override EImportStatus ImportState
        {
            get { return base.ImportState; }
            set
            {
                base.ImportState = value;

                this.RaisePropertyChanged("ShownName");
                this.RaisePropertyChanged("TipInfo");
            }
        }

        #endregion

        #region DBHelper

        protected new DbfDBHelper DBHelper
        {
            get { return base.DBHelper as DbfDBHelper; }
            set { base.DBHelper = value; }
        }

        protected override void OnDateChanged(object sender, DateChangedEventArgs e)
        {
            base.OnDateChanged(sender, e);
            this.DBHelper.TablePath = this.TablePath;
            this.UpdateFileName();

            this.RaisePropertyChanged("TablePath");
            this.RaisePropertyChanged("FileName");
            this.RaisePropertyChanged("ShownName");
        }

        #endregion

        #region Import Methord

        public override bool SyncImportState2Local()
        {
            if (base.SyncImportState2Local())
                return true;

            if (File.Exists(this.FileName))
            {
                this.TotalCount = this.GetItemsCount();
                if (this.TotalCount > 0)
                    this.ImportState = EImportStatus.WaitForImport;
                else
                    this.ImportState = EImportStatus.NotDetected;
            }
            else
            {
                this.ImportState = EImportStatus.NotDetected;
            }

            return true;
        }

        public override void SyncImportState2Remote()
        {
            if (string.IsNullOrEmpty(this.FileName) && !File.Exists(this.FileName))
                return;

            base.SyncImportState2Remote();
        }

        protected int GetItemsCount()
        {
            string sql = string.Format("SELECT COUNT(*) FROM {0}", TableName);
            object obj = this.ExecuteScalar(sql);

            return Convert.ToInt32(obj);
        }

        public override abstract void DoExport(IExportCallback callback);

        #endregion

    }

}
