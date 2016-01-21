using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Dothan.DzHelpers;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;

namespace Dothan.ExportData
{
    /// <summary>
    /// 被管理数据对Project级别数据的回调接口。
    /// </summary>
    public interface IProject
    {
        /// <summary>
        /// 当前DBF路径。
        /// </summary>
        string DbfPath { get; }

        /// <summary>
        /// 数据导入操作的目标日期。
        /// </summary>
        DateTime TargetDate { get; }

        /// <summary>
        /// 是否已经停止数据的导入操作；
        /// </summary>
        bool HasStop { get; }

        /// <summary>
        /// 数据的导入状态发生了变化。
        /// </summary>
        void OnImportStateChanged();

        /// <summary>
        /// 判断当前是否正在进行数据的采集操作。
        /// </summary>
        bool IsImporting { get; }

        /// <summary>
        /// 当目标日期发生变化的时候会触发改事件。
        /// </summary>
        event DateChangedEventHandler TargetDateChanged;

        void RaiseTargetDateChanged(DateTime oldDate, DateTime newDate);

        IOutput Output { get; }

        ILog Log { get; }
    }

    public partial class ExportManager : NotificationObject, IProject
    {
        #region Tag常量字符串。

        public const string Tag_ConfigFile = "config.ini";
        public const string Tag_Section_Default = "TARGET_FILE";
        public const string Tag_Section_Properties = "PROPERTIES";
        public const string Tag_Section_IgnoreItems = "IGNORE_ITEMS";

        /// <summary>
        /// 用于获取上次操作DBF的文件夹名称的Tag常量。
        /// </summary>
        public const string Tag_TargetDate = "targetFolderName";

        public const string Tag_StartDetectTime = "startDetectTime";

        public const string Tag_AutoImportNext = "autoImportNext";

        public const string Tag_AutoDetectData = "autoDetectData";

        #endregion

        #region Life Cycle

        public ExportManager(IOutput output)
        {
            this.Output = output;
            this.Log = new MyLog(this.Output);
            this.DbfManager = new DBFManager(this);
            this.InitExportList();
            this.LoadIniFile();
            this.DbfManager.PropertyChanged += this.OnPropertyDataChanged;
        }

        #endregion

        #region ExportList

        public ObservableCollection<ImportItem> ExportList
        {
            get { return _ExportList; }
            set
            {
                if (value != this._ExportList)
                {
                    _ExportList = value;
                    this.RaisePropertyChanged("ExportList");
                }
            }
        }
        private ObservableCollection<ImportItem> _ExportList;

        private void InitExportList()
        {
            this.ExportList = new ObservableCollection<ImportItem>();
            this.ExportList.Add(new OrderRowService_LH(this));
            this.ExportList.Add(new TradeInfo_O32Service(this));

            this.ExportList.Add(new JSMXService_SH(this));
            this.ExportList.Add(new JSMXService_SZ(this, 0));
            this.ExportList.Add(new JSMXService_SZ(this, 1));
            this.ExportList.Add(new JSMXService_SZ(this, 2));
            this.ExportList.Add(new CILService(this));

            this.ExportList.Add(new AShareEODPricesTable(this));
            //this.ExportList.Add(new ChinaETFPchRedmListTable(this));
            this.ExportList.Add(new IndexFuturesEODPricesTable(this));
            this.ExportList.Add(new BondFuturesEODPricesTable(this));
            this.ExportList.Add(new CommodityFuturesEODPricesTable(this));

            foreach (ImportItem item in this.ExportList)
            {
                item.PropertyChanged += this.OnPropertyDataChanged;
            }
        }

        public void StopToImport()
        {
            if (!this.HasStop)
            {
                this.Output.WriteLine("已取消所有的导入操作!");
                this.HasStop = true;
            }
        }

        public bool HasStop
        {
            get { return _HasStop; }
            set { _HasStop = value; }
        }
        private bool _HasStop;

        /// <summary>
        /// 判断当前是否正在进行文件导入操作；
        /// </summary>
        public bool IsImporting
        {
            get { return this._IsImporting; }
            set
            {
                if (this._IsImporting != value)
                {
                    this._IsImporting = value;
                    this.RaisePropertyChanged("IsImporting");
                    this.UpdateCommand();
                }
            }
        }
        private bool _IsImporting;

        public void OnImportStateChanged()
        {
            EImportStatus state = this.ImportState;
            if ((state & EImportStatus.Importing) != 0/* || (state & EImportStatus.WaitForImport) != 0*/)
                this.IsImporting = true;
            else
                this.IsImporting = false;

            // 如果用户点击停止导入操作后，可用导入状态来判断是否已经全部停止，
            // 如果导入操作已经全部停止，则需要回复可导入状态，否则之后就无法进行数据的导入操作了；
            if (!this.IsImporting)
                this.HasStop = false;
        }

        public EImportStatus ImportState
        {
            get
            {
                if (this.ExportList == null || this.ExportList.Count <= 0)
                    return EImportStatus.NotDetected;

                EImportStatus state = EImportStatus.Init;

                foreach (ImportItem item in this.ExportList)
                {
                    state |= item.ImportState;
                }

                return state;
            }
        }

        /// <summary>
        /// 触发命令状态改变事件，事件的注册者可以更新UI中的菜单状态。
        /// </summary>
        public void UpdateCommand()
        {
            this.RaiseCommandStateChanged();
        }

        #endregion

        #region DbfManager

        public DBFManager DbfManager
        {
            get { return _DbfManager; }
            set
            {
                if (this._DbfManager != value)
                {
                    _DbfManager = value;
                    this.RaisePropertyChanged("DbfManager");
                }
            }
        }
        private DBFManager _DbfManager;

        public DateTime TargetDate
        {
            get { return this.DbfManager.TargetDate; }
            set { this.DbfManager.TargetDate = value; }
        }

        public string DbfPath
        {
            get { return this.DbfManager.DbfPath; }
        }

        #endregion

        #region AutoDetect

        /// <summary>
        /// 当天数据全部导入完成后是否需要自动检测并导入下一天的数据。
        /// </summary>
        public bool AutoImportNext
        {
            get { return _AutoImportNext; }
            set
            {
                if (this._AutoImportNext != value)
                {
                    this._AutoImportNext = value;
                    this.RaisePropertyChanged("AutoImportNext");
                }
            }
        }
        private bool _AutoImportNext;

        /// <summary>
        /// 到达设定的时间后自动进行数据的检测与导入操作。
        /// true： 表示启用定时器；
        /// false：表示禁用定时器；
        /// </summary>
        public bool AutoDetectData
        {
            get { return _AutoDetectData; }
            set
            {
                _AutoDetectData = value;
                this.RaisePropertyChanged("AutoDetectData");
            }
        }
        private bool _AutoDetectData = true;

        public DateTime StartDetectTime
        {
            get { return this._StartDetectTime; }
            set
            {
                if (this._StartDetectTime != value)
                {
                    this._StartDetectTime = value;
                    this.RaisePropertyChanged("StartDetectTime");
                }
            }
        }
        private DateTime _StartDetectTime = DBFManager.ParseDateTime(defaultStartDetectTime, "HH:mm:ss");
        public const String defaultStartDetectTime = "17:30:00";

        #endregion

        #region 文件检查定时器

        private DispatcherTimer detectTimer;

        public bool HasStartAutoImportDate
        {
            get
            {
                if (this.detectTimer != null && this.detectTimer.IsEnabled)
                    return true;

                return false;
            }
        }

        /// <summary>
        /// 开始进行文件的自动检测，如果检测到DBF文件，则将文件导出到数据库。
        /// </summary>
        public void StartAutoImportData()
        {
            this.CheckAndExport();
            // 启动定时器；
            this.StartAutoCheckTimer();
        }

        public bool CanStop
        {
            get
            {
                if (this.IsImporting)
                    return false;
                if (this.detectTimer == null || !this.detectTimer.IsEnabled)
                    return false;

                return true;
            }
        }

        public void StopAutoImportData()
        {
            this.StopAutoCheckTimer();
        }

        /// <summary>
        /// 开启定时器：
        ///     如果检测到新的文件夹，则更新操作的目标文件夹；
        ///     如果未检测到新的文件夹，则更新文件列表以及文件的导入状态；
        /// </summary>
        protected void StartAutoCheckTimer()
        {
            this.StopAutoCheckTimer();

            if (this.detectTimer == null)
            {
                this.detectTimer = new DispatcherTimer();
                this.detectTimer.Interval = new TimeSpan(0, 5, 0);
                this.detectTimer.Tick += new EventHandler(AutoCheckTimer_Tick);
            }

            this.detectTimer.Start();

            this.Output.WriteLine("开启DBF文件自动检测操作！" + DateTime.Now);
        }

        /// <summary>
        /// 停止DBF文件自动检测定时器。
        /// </summary>
        protected void StopAutoCheckTimer()
        {
            if (this.detectTimer == null)
                return;

            if (this.detectTimer.IsEnabled)
            {
                this.Output.WriteLine("停止DBF文件自动检测操作！" + DateTime.Now);
                this.detectTimer.Stop();
            }
        }

        /// <summary>
        /// 通过定时器来不断地检测并更新DBF文件路径。
        /// </summary>
        protected void AutoCheckTimer_Tick(object sender, EventArgs e)
        {
            this.CheckAndExport();
        }

        #endregion

        #region Open/Save Project

        /// <summary>
        /// Ini文件。
        /// </summary>
        public string IniFile
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Tag_ConfigFile); }
        }

        private void LoadIniFile()
        {
            if (string.IsNullOrEmpty(this.IniFile) || !File.Exists(this.IniFile))
                return;

            this.LoadIniFile(this.IniFile);
        }

        /// <summary>
        /// 判断当前是否正在加载文件，当正在加载文件的时候，部分功能是无法操作的。
        /// </summary>
        public bool IsLoading { get; set; }

        private void LoadIniFile(string file)
        {
            this.IsLoading = true;
            IniFile iniFile = new IniFile(file);

            string date = iniFile.GetValue(Tag_Section_Default, Tag_TargetDate, string.Empty);
            if (string.IsNullOrEmpty(date))
                TargetDate = this.DbfManager.GetDefaultDateTime();
            else
                TargetDate = DBFManager.ParseDateName(date);

            // timer
            string timerTime = iniFile.GetValue(Tag_Section_Default, Tag_StartDetectTime, ExportManager.defaultStartDetectTime);
            this.StartDetectTime = DBFManager.ParseDateTime(timerTime, "HH:mm:ss");

            this.AutoImportNext = iniFile.GetValue(Tag_Section_Properties, Tag_AutoImportNext, false);

            //
            foreach (ImportItem item in this.ExportList)
            {
                item.Ignore = iniFile.GetValue(Tag_Section_IgnoreItems, item.Name, false);
            }

            this.IsLoading = false;
        }

        private void SaveIniFile()
        {
            if (!string.IsNullOrEmpty(this.IniFile))
                this.SaveIniFile(this.IniFile);
        }

        private void SaveIniFile(string file)
        {
            IniFile iniFile = new IniFile(file);

            iniFile.SetValue(Tag_Section_Default, Tag_TargetDate, this.DbfManager.TargetDateName);
            iniFile.SetValue(Tag_Section_Default, Tag_StartDetectTime, this.StartDetectTime.ToString("HH:mm:ss"));
            iniFile.SetValue(Tag_Section_Properties, Tag_AutoImportNext, this.AutoImportNext);
            iniFile.SetValue(Tag_Section_Properties, Tag_AutoDetectData, this.AutoDetectData);

            foreach (ImportItem item in this.ExportList)
            {
                iniFile.SetValue(Tag_Section_IgnoreItems, item.Name, item.Ignore);
            }
        }

        #endregion

        #region PropertyChanged

        private void OnPropertyDataChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("TargetDate") || e.PropertyName.Equals("StartDetectTime"))
            {
                this.SaveIniFile();
            }
            else if (sender is ImportItem && e.PropertyName.Equals("Ignore"))
            {
                if (!this.IsLoading) this.SaveIniFile();
            }
        }

        #endregion

        #region Output

        public IOutput Output { get; set; }

        public ILog Log { get; protected set; }

        #endregion
    }

    /// <summary>
    /// 输出Log信息接口，如果需要将Log信息输出到本地文件，或者其他地方，只需要提供一个实现了该接口的类即可实现；
    /// </summary>
    public interface IOutput
    {
        void Write(string msg);

        void WriteLine(string msg);

        void WriteLine();
    }
}
