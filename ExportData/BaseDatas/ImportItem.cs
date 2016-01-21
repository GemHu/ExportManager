using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Data;
using SettleSysCoding;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public enum EImportStatus
    {
        Init = 0x00,            ///< 数据尚未准备好
        NotDetected = 0x01,     ///< 未检测到文件；
        WaitForImport = 0x02,   ///< 检测到了文件，但是还未开始处理；
        Importing = 0x04,       ///< 文件正在导入中；
        Interrupt = 0x08,       ///< 文件导入被中断；
        Imported = 0x10,        ///< 文件已经导入过；
        Exception = 0x20,       ///< 文件导出异常；
    }

    /// <summary>
    /// 数据源类型。
    /// </summary>
    public enum ESourceType
    {
        UnKnown = 0x0000,     ///< 未知数据类型；
        DBF = 0x0001,         ///< DBF文件；
        Mysql = 0x0002,       ///< Mysql数据库；
        SqlServer = 0x0003,   ///< SqlServer数据库；
        Oracle = 0x0004       ///< Oracle数据库；
    }

    public interface IExport
    {
        string TableName { get; set; }

        ESourceType SourceType { get; set; }

        EImportStatus ImportState { get; set; }
    }

    public interface IExportCallback
    {
        int TotalCount { get; set; }

        int CurrentIndex { get; set; }

        int ValidIndex { get; set; }
    }

    public abstract partial class ImportItem : IExport, IExportCallback, INotifyPropertyChanged, IOutput
    {
        #region Life Cycle

        public ImportItem(IProject project, DBHelper dbHelper)
        {
            this._TheProject = project;
            this.DBHelper = dbHelper;

            this.TheProject.TargetDateChanged += this.OnDateChanged;
        }

        protected IProject TheProject
        {
            get { return this._TheProject; }
        }
        private IProject _TheProject;

        /// <summary>
        /// 重置相关数据。
        /// </summary>
        public void Reset()
        {
            this.CurrentIndex = 0;
            this.TotalCount = 1;
            this.ImportState = EImportStatus.Init;
        }

        #endregion

        #region TableName

        public string TableName
        {
            get { return _TableName; }
            set
            {
                if (value != this._TableName)
                {
                    _TableName = value;
                    this.RaisePropertyChanged("TableName");
                }
            }
        }
        private string _TableName;

        /// <summary>
        /// 不同日期导入项的名字相同，同一天每条导入数据的名字不应该相同；
        /// 如果导入项为数据表，则名字应为数据表的名称，如果导入想为DBF文件，则名字为DBF文件的前缀。
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 不同的子类显示的名称会有所不同。
        /// </summary>
        public abstract string ShownName { get; }

        #endregion

        #region Date

        public DateTime Date
        {
            get { return this.TheProject.TargetDate; }
        }

        public string DateName
        {
            get { return this.Date.ToString("yyyyMMdd"); }
        }

        protected virtual void OnDateChanged(object sender, DateChangedEventArgs e)
        {
            this.CurrentIndex = 0;
            this.ValidIndex = 0;
            this.TotalCount = 1;
            this.ImportState = EImportStatus.Init;
        }

        #endregion

        #region SourceType

        public abstract string DataSource { get; }

        public ESourceType SourceType
        {
            get { return this._SourceType; }
            set
            {
                if (value != this._SourceType)
                {
                    this._SourceType = value;
                    this.RaisePropertyChanged("SourceType");
                }
            }
        }
        private ESourceType _SourceType;

        #endregion

        #region ImportState

        public virtual EImportStatus ImportState
        {
            get { return this._ImportState; }
            set
            {
                if (this._ImportState != value)
                {
                    this._ImportState = value;
                    this.RaisePropertyChanged("ImportState");
                    this.TheProject.OnImportStateChanged();
                }
            }
        }
        private EImportStatus _ImportState = EImportStatus.Init;

        /// <summary>
        /// 用于判断在数据导入操作的时候需不需要跳过该项。
        /// </summary>
        public bool Ignore
        {
            get { return _Ignore; }
            set 
            {
                if (this._Ignore != value)
                {
                    this._Ignore = value;
                    this.RaisePropertyChanged("Ignore");
                }
            }
        }
        private bool _Ignore;

        #endregion

        #region TipInfo

        /// <summary>
        /// 导入项提示信息。
        /// </summary>
        public virtual string TipInfo
        {
            get { return this.DataSource; }
        }

        #endregion

        #region IExportCallback Members

        public int TotalCount
        {
            get { return this._TotalCount; }
            set
            {
                if (this._TotalCount != value)
                {
                    this._TotalCount = value;
                    this.RaisePropertyChanged("TotalCount");
                }
            }
        }
        private int _TotalCount = 1;

        public int CurrentIndex
        {
            get { return this._CurrentIndex; }
            set
            {
                if (this._CurrentIndex != value)
                {
                    this._CurrentIndex = value;
                    this.RaisePropertyChanged("CurrentIndex");

                    if (this.TotalCount != 0 && value < this.TotalCount)
                    {
                        this.ProgressRate = value * 100 / this.TotalCount;
                    }
                }
            }
        }
        private int _CurrentIndex;

        public int ValidIndex
        {
            get { return this._ValidIndex; }
            set 
            {
                if (this._ValidIndex != value)
                {
                    this._ValidIndex = value;
                    this.RaisePropertyChanged("ValidIndex");
                }
            }
        }
        private int _ValidIndex;

        /// <summary>
        /// 数据导入进度，0到100。
        /// </summary>
        public int ProgressRate
        {
            get { return _ProgressRate; }
            set
            {
                if (this._ProgressRate != value)
                {
                    this._ProgressRate = value;
                    this.RaisePropertyChanged("ProgressRate");
                }
            }
        }
        private int _ProgressRate;

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Import

        /// <summary>
        /// 在读取数据表中数据的时候的最大缓存数量，如果小于该数值，在操作的时候可以一次向的将数据全部读取到内存中；
        /// </summary>
        public int MaxBufferSize { get { return 10000; } }

        /// <summary>
        /// 同步文件导入状态到本地。
        /// </summary>
        /// <returns>true:表示父类已经同步测完毕，子类不用再进行相关的同步操作</returns>
        public virtual bool SyncImportState2Local()
        {
            if (this.Date == null || string.IsNullOrEmpty(this.TableName))
            {
                this.ImportState = EImportStatus.NotDetected;
                return true;
            }

            ImportRecordTable service = new ImportRecordTable();
            ImportRecordRow info = service.GetImportRecordRow(this.DateName, this.TableName);

            // 交给子类处理；
            if (info == null || info.ImportState == EImportStatus.Init || info.ImportState == EImportStatus.NotDetected || info.ImportState == EImportStatus.Exception)
            {
                return false;
            }

            // 条件不具备，过滤掉；
            this.ImportState = info.ImportState;
            this.TotalCount = info.TotalCount;
            this.CurrentIndex = info.ImportCount;
            return true;
        }

        /// <summary>
        /// 将本地的文件导入状态同步到数据库。
        /// </summary>
        public virtual void SyncImportState2Remote()
        {
            if (string.IsNullOrEmpty(this.DateName) || string.IsNullOrEmpty(this.TableName))
                return;

            ImportRecordTable service = new ImportRecordTable();
            if (!service.Open())
                return;

            ImportRecordRow info = service.GetImportRecordRow(this.DateName, this.TableName);
            if (info == null)
            {
                info = new ImportRecordRow();
                info.TradeDate = DateName;
                info.TableName = this.TableName;
                info.SourceType = this.SourceType;
                info.ImportState = this.ImportState;
                info.ImportDate = System.DateTime.Now;
                service.Add(info);
            }
            else
            {
                //service.UpdateImportStatus(info.Id, this.ImportState);
                service.Update(info.Id, this.ImportState, this.CurrentIndex, this.TotalCount);
            }

            service.Close();
        }

        /// <summary>
        /// 初始化数据导入状态；
        /// </summary>
        public void InitImportState()
        {
            this.ImportState = EImportStatus.Init;
            this.SyncImportState2Remote();
        }

        public void DoExport()
        {
            this.DoExport(this);
        }

        /// <summary>
        /// 开始执行数据的导出操作。
        /// </summary>
        public abstract void DoExport(IExportCallback ballback);

        /// <summary>
        /// 将数据导出到委托表(OrderTable)中。
        /// </summary>
        public void Export2OrderTable(DbDataReader reader, IExportCallback callback, IExport2OrderTable orderService)
        {
            if (reader == null)
                return;
            OrderTable orderTable = new OrderTable();
            if (!orderTable.Open())
                return;

            while (reader.Read())
            {
                if (this.TheProject.HasStop)
                {
                    orderService.ImportState = EImportStatus.Interrupt;
                    break;
                }

                OrderRow data = orderService.GetOrderRow(reader);
                orderTable.Add(data);
                callback.CurrentIndex++;
            }

            orderTable.Close();
        }

        /// <summary>
        /// 将数据导出到成交表（TradeTable）中。
        /// </summary>
        public virtual void Export2TradeTable(DbDataReader reader, IExportCallback callback, IExport2TradeTable tradeService)
        {
            if (reader == null)
                return;
            TradeTable tradeTable = new TradeTable();
            if (!tradeTable.Open())
                return;

            while (reader.Read())
            {
                if (this.TheProject.HasStop)
                {
                    tradeService.ImportState = EImportStatus.Interrupt;
                    break;
                }

                TradeRow data = tradeService.GetTradeRow(reader);
                tradeTable.Add(data);
                callback.CurrentIndex++;
            }

            tradeTable.Close();
        }

        /// <summary>
        /// 将数据导出到 MarketTable中。
        /// </summary>
        public void Export2MarketTable(DbDataReader reader, IExportCallback callback, IExport2MarketTable marketService)
        {
            if (reader == null)
                return;
            MarketTable marketTable = new MarketTable();
            if (!marketTable.Open())
                return;

            while (reader.Read())
            {
                if (this.TheProject.HasStop)
                {
                    marketService.ImportState = EImportStatus.Interrupt;
                    break;
                }

                MarketRow row = marketService.GetMarketRow(reader);
                marketTable.Add(row);
                callback.CurrentIndex++;
                callback.ValidIndex++;
            }

            marketTable.Close();
        }

        #endregion

        #region Data Convert

        protected virtual string GetString(object obj)
        {
            if (obj == null)
                return string.Empty;
            else if (obj is string)
                return (obj as string).Trim();
            else
                return Convert.ToString(obj);
        }

        protected char GetChar(object obj)
        {
            try { return Convert.ToChar(obj); }
            catch (Exception) { return '\0'; }
        }

        protected int GetInt(object obj)
        {
            try { return Convert.ToInt32(obj); }
            catch (Exception) { return 0; }
        }

        protected long GetLong(object obj)
        {
            try { return Convert.ToInt64(obj); }
            catch (Exception) { return 0; }
        }

        protected virtual double GetDouble(object obj)
        {
            if (obj == null)
                return 0.0;

            try { return Convert.ToDouble(obj); }
            catch (Exception) { return 0.0; }
        }

        protected virtual DateTime? GetDateTime(object obj)
        {
            if (obj == null)
                return null;

            try { return Convert.ToDateTime(obj); } catch (Exception)
            { return null; }
        }

        /// <summary>
        /// 根据给定的时间，获取对应的成交类型。
        /// (9:30之前为集合竞价；9:30之后为连续竞价；默认为2：连续竞价。)
        ///     1=做市商成交; 
        ///     2=连续竞价成交; 
        ///     3=集合竞价成交; 
        ///     4=协议成交
        /// </summary>
        /// <param name="tradeTime">成交时间</param>
        /// <param name="defaultType">时间解析失败后的默认成交方式</param>
        /// <returns>成交方式</returns>
        public int GetTradeTypeByTradeTime(string tradeTime, int defaultType)
        {
            if (string.IsNullOrEmpty(tradeTime))
                return defaultType;

            string timeStr = tradeTime.PadLeft(6, '0');
            TimeSpan timeTS;

            if (TimeSpan.TryParseExact(timeStr, "hhmmss", null, out timeTS))
                return this.GetTradeTypeByTradeTime(timeTS);
            else
                return defaultType;
        }

        /// <summary>
        /// 根据给定的时间，获取对应的成交类型。
        /// (9:30之前为集合竞价；9:30之后为连续竞价；默认为2：连续竞价。)
        ///     1=做市商成交; 
        ///     2=连续竞价成交; 
        ///     3=集合竞价成交; 
        ///     4=协议成交
        /// </summary>
        public int GetTradeTypeByTradeTime(TimeSpan tradeTime)
        {
            TimeSpan referTime = new TimeSpan(09, 30, 00);
            if (tradeTime > referTime)
                return 2;
            else
                return 3;
        }

        protected virtual string ConvertSecurityId(string oldId)
        {
            return oldId;
        }

        //public string GetSecurityId_SH(string oldId)
        //{
        //    if (string.IsNullOrEmpty(oldId))
        //        return oldId;
        //    if (oldId.IndexOf('.') > 0)
        //        return oldId;

        //    return string.Format("{0}.SH", oldId);
        //}

        //public string GetSecurityId_SZ(string oldId)
        //{
        //    if (string.IsNullOrEmpty(oldId))
        //        return oldId;
        //    if (oldId.IndexOf('.') > 0)
        //        return oldId;

        //    return string.Format("{0}.SZ", oldId);
        //}

        public string GetSecurityIdWithoutExtension(string oldId)
        {
            if (string.IsNullOrEmpty(oldId))
                return string.Empty;

            int index = oldId.LastIndexOf('.');
            if (index > 0)
                return oldId.Substring(0, index);
            else
                return oldId;
        }

        /// <summary>
        /// 将制定的Object对象转换为对应的Direction，用于子类的重载。
        /// </summary>
        protected virtual int GetDirection(object obj)
        {
            return 0;
        }

        /// <summary>
        /// 如果当前交易是股票，则根据股票的买卖标志转换为对应的Direction。
        /// </summary>
        protected virtual int GetDirectionByBS(char bs)
        {
            if (bs == 'B' || bs == 'b')
                return DirectionCoding.GetDirectionCode(TradeType.TRADE_BUY);
            else if (bs == 'S' || bs == 's')
                return DirectionCoding.GetDirectionCode(TradeType.TRADE_SELL);

            return 0;
        }

        /// <summary>
        /// 如果是期货，则需要根据开平藏来获取对应的Direction。
        /// </summary>
        protected virtual int GetDirectionByOC(char oc)
        {
            if (oc == 'O' || oc == 'o')
                return DirectionCoding.GetDirectionCode(TradeType.TRADE_FUT_OPEN);
            else if (oc == 'C' || oc == 'c')
                return DirectionCoding.GetDirectionCode(TradeType.TRADE_FUT_CLOSE);

            return 0;
        }

        #endregion

        #region IOutput

        public void Write(string msg)
        {
            if (this.TheProject != null && this.TheProject.Output != null)
                this.TheProject.Output.Write(msg);
        }

        public void WriteLine(string msg)
        {
            if (this.TheProject != null && this.TheProject.Output != null)
            {
                string preFix = string.Format("'{0}/{1}' ", this.DateName, this.TableName);
                string sufFix = string.Format(" DateTime : {0}", DateTime.Now.ToString());

                this.TheProject.Output.WriteLine(preFix + msg + sufFix);
            }
        }

        public void WriteLine()
        {
            if (this.TheProject != null && this.TheProject.Output != null)
            {
                this.TheProject.Output.WriteLine();
            }
        }

        #endregion
    }

}
