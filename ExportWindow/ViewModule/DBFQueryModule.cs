using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;
using System.Data;
using Dothan.ExportData;

namespace Dothan.ExportWindow
{
    public class DBFQueryModule : NotificationObject
    {
        #region Life Cycle

        public DBFQueryModule()
        {

        }

        public DBFQueryModule(DBFManager manager)
        {
            this.DbfManager = manager;
        }

        public DBFManager DbfManager
        {
            get;
            set;
        }

        #endregion

        #region CommandText

        public string CommandText
        {
            get { return _CommandText; }
            set
            {
                _CommandText = value;
                this.RaisePropertyChanged("CommandText");
            }
        }
        private string _CommandText;

        #endregion

        #region ExecuteCommand

        /// <summary>
        /// 是否可以执行命令。
        /// </summary>
        public bool CanExecute
        {
            get { return _CanExecute; }
            set
            {
                if (this._CanExecute != value)
                {
                    _CanExecute = value;
                    this.RaisePropertyChanged("CanExecute");
                }
            }
        }
        private bool _CanExecute;

        public void CheckCanExecute()
        {
            if (this.DbfManager == null)
                this.CanExecute = false;
            else if (this.IsRuning)
                this.CanExecute = false;
            else if (string.IsNullOrEmpty(this.CommandText))
                this.CanExecute = false;
            else
                this.CanExecute = true;
        }

        /// <summary>
        /// 命令是否正在运行中。
        /// </summary>
        public bool IsRuning
        {
            get { return _IsRuning; }
            set
            {
                _IsRuning = value;
                this.RaisePropertyChanged("IsRuning");
            }
        }
        private bool _IsRuning;

        public void ExecuteCommand()
        {
            this.IsRuning = true;
            this.CheckCanExecute();

            string[] commands = this.CommandText.Split(';');
            foreach (string command in commands)
            {
                if (string.IsNullOrEmpty(command))
                    continue;

                // 暂时只支持查询；
                if (command.StartsWith("select ", StringComparison.OrdinalIgnoreCase))
                {
                    string msg = string.Empty;
                    this.ReaultData = DbfDBHelper.ExecuteCommand(this.DbfManager.DbfPath, command, ref msg);
                    if (!string.IsNullOrEmpty(msg))
                        this.DbfManager.TheProject.Output.WriteLine(msg);
                }
            }

            this.IsRuning = false;
            this.CheckCanExecute();
        }

        public void StopExecuteCommand()
        {
        }

        #endregion

        #region ReaultData

        /// <summary>
        /// 数据查询结果。
        /// </summary>
        public DataTable ReaultData
        {
            get { return _ReaultData; }
            set
            {
                _ReaultData = value;
                this.RaisePropertyChanged("ReaultData");
            }
        }
        private DataTable _ReaultData;

        #endregion
    }

    public interface IDBFQueryCanExecuteListener
    {

    }
}
