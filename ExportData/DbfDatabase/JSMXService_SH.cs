using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;
using System.Data.Common;

using SettleSysCoding;
using Dothan.DzHelpers;
using System.Diagnostics;

namespace Dothan.ExportData
{
    public class JSMXService_SH : DbfImportItem, IExport2TradeTable
    {
        #region Life Cycle

        public JSMXService_SH(IProject project)
            : base(project)
        {
        }

        #endregion

        #region TableName

        public override string Name
        {
            get { return "JSMXJS559"; }
        }

        #endregion

        #region Export

        public override void DoExport(IExportCallback callback)
        {
            string sql = string.Format("select * from {0} order by '{1}'", this.TableName, JsmxRow_SH.C_ZQDM1);
            DbDataReader reader = this.ExecuteReader(sql);
            base.Export2TradeTable(reader, callback, this);
            this.Close();
        }

        protected JsmxRow_SH GetJsmxRow_SH(DbDataReader reader)
        {
            JsmxRow_SH row = new JsmxRow_SH();

            row.SCDM = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_SCDM)));
            row.JLLX = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_JLLX)));
            row.JYFS = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_JYFS)));
            row.JSFS = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_JSFS)));
            row.YWLX = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_YWLX)));
            row.QSBZ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_QSBZ)));
            row.GHLX = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_GHLX)));
            row.JSBH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_JSBH)));
            row.CJBH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_CJBH)));
            row.SQBH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_SQBH)));
            row.WTBH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_WTBH)));
            row.JYRQ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_JYRQ)));
            row.QSRQ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_QSRQ)));
            row.JSRQ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_JSRQ)));
            row.QTRQ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_QTRQ)));
            row.WTSJ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_WTSJ)));
            row.CJSJ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_CJSJ)));
            row.XWH1 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_XWH1)));
            row.XWH2 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_XWH2)));
            row.XWHY = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_XWHY)));
            row.JSHY = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_JSHY)));
            row.TGHY = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_TGHY)));
            row.ZQZH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_ZQZH)));
            row.ZQDM1 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_ZQDM1)));
            row.ZQDM2 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_ZQDM2)));
            row.ZQLB = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_ZQLB)));
            row.LTLX = this.GetChar(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_LTLX)));
            row.QYLB = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_QYLB)));
            row.GPNF = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_GPNF)));
            row.MMBZ = this.GetChar(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_MMBZ)));
            row.SL = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_SL)));
            row.CJSL = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_CJSL)));
            row.ZJZH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_ZJZH)));
            row.BZ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_BZ)));
            row.JG1 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_JG1)));
            row.JG2 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_JG2)));
            row.QSJE = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_QSJE)));
            row.YHS = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_YHS)));
            row.JSF = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_JSF)));
            row.GHF = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_GHF)));
            row.ZGF = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_ZGF)));
            row.SXF = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_SXF)));
            row.QTJE1 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_QTJE1)));
            row.QTJE2 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_QTJE2)));
            row.QTJE3 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_QTJE3)));
            row.SJSF = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_SJSF)));
            row.JGDM = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_JGDM)));
            row.FJSM = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SH.C_FJSM)));

            return row;
        }

        #endregion

        #region IExport2TradeTable

        //public override void Export2TradeTable(DbDataReader reader, IExportCallback callback, IExport2TradeTable tradeService)
        //{
        //    if (reader == null)
        //        return;
        //    TradeTable tradeTable = new TradeTable();
        //    if (!tradeTable.Open())
        //        return;

        //    OrderRowService_LH lhService = new OrderRowService_LH(this.TheProject);
        //    TradeInfo_O32Service o32Service = new TradeInfo_O32Service(this.TheProject);
        //    if (!lhService.Open() || !o32Service.Open())
        //    {
        //        tradeTable.Close();
        //        lhService.Close();
        //        o32Service.Close();

        //        return;
        //    }

        //    // 特别说明：
        //    // 在加载结算明细表中的时候的时候需要从量化系统和O32系统中读取相关的数据进行比较，
        //    // 在读取其他系统数据的时候会出现两个问题：
        //    // 1、如果一次性把其他系统中的数据全部加载到内存，如果数据量过大，则会撑爆内存；
        //    // 2、如果在根据需要实时的从其他系统相关数据表中读取数据，效率太低，导入速度太慢了；
        //    // 3、所以采用一个折中的办法，设置一个临界值，如果大于该临界值，则采用实时加载的方法，否则一次性读取依赖数据；
        //    bool bufferAll_lh = false;
        //    bool bufferAll_o32 = false;
        //    List<OrderRow_LH> buffer_lh = null;
        //    List<TradeInfo_O32> buffer_o32 = null;
        //    List<OrderRow_LH> lhDataList = null;
        //    List<TradeInfo_O32> o32DataList = null;
        //    string securityId = string.Empty;

        //    lhService.SyncImportState2Local();
        //    if (lhService.TotalCount < this.MaxBufferSize)
        //    {
        //        bufferAll_lh = true;
        //        buffer_lh = lhService.GetTradeItems(EMarketType.SH);
        //    }

        //    o32Service.SyncImportState2Local();
        //    if (o32Service.TotalCount < this.MaxBufferSize)
        //    {
        //        bufferAll_o32 = true;
        //        buffer_o32 = o32Service.GetTradeItems(EMarketType.SH);
        //    }

        //    while (reader.Read())
        //    {
        //        if (this.TheProject.HasStop)
        //        {
        //            tradeService.ImportState = EImportStatus.Interrupt;
        //            break;
        //        }

        //        JsmxRow_SH row = this.GetJsmxRow_SH(reader);
        //        if (securityId != row.ZQDM1)
        //        {
        //            securityId = row.ZQDM1;
        //            if (bufferAll_lh)
        //                lhDataList = buffer_lh.Where(o => o.zqdm.Equals(securityId)).ToList();
        //            else
        //                lhDataList = lhService.GetTradeItems(row.ZQDM1, EMarketType.SH);
        //            if (bufferAll_o32)
        //                o32DataList = buffer_o32.Where(o => o.VC_REPORT_CODE.StartsWith(securityId)).ToList();
        //            else
        //                o32DataList = o32Service.GetTradeItems(row.ZQDM1, EMarketType.SH);
        //        }

        //        //ESecurityType securityType = this.GetSecurityType(row);
        //        EEntrustType entrustType = this.GetEntrustType(row);
        //        if (lhDataList.Where(o => entrustType == OrderRowService_LH.GetEntrustType(o)).FirstOrDefault() == null &&
        //            o32DataList.Where(o => entrustType == TradeInfo_O32Service.GetEntrustType(o)).FirstOrDefault() == null)
        //        {
        //            tradeTable.Add(this.GetTradeRow(row));
        //            callback.ValidIndex++;
        //        }

        //        callback.CurrentIndex++;
        //    }

        //    tradeTable.Close();
        //    lhService.Close();
        //    o32Service.Close();
        //}

        public TradeRow GetTradeRow(DbDataReader reader)
        {
            return this.GetTradeRow(this.GetJsmxRow_SH(reader));
        }

        protected TradeRow GetTradeRow(JsmxRow_SH row)
        {
            TradeRow trade = new TradeRow();

            // 2、成交日期；
            trade.Trade_Date = string.Empty;
            // 3、成交编号；
            trade.Trade_Id = string.Empty;
            // 4、证券代码以证券代码1为准；为了防止与深交所证券代码混淆，需要以加后缀".SH"
            trade.Security_Id = this.ConvertSecurityId(row.ZQDM1);
            // 5、方向需要根据买卖编号进行转换(B：表示Buy；S：表示Sell)；
            trade.Direction = this.GetDirectionByBS(row.MMBZ);
            // 6、数量暂时以成交数量为准；
            trade.Volume = this.GetDouble(row.CJSL);
            // 7、合约乘数为1
            trade.Multiplier = 1;
            // 8、多头空头；
            trade.Side = this.GetSideFlag(row);
            // 9、价格以价格1为准；
            trade.Price = this.GetDouble(row.JG1);
            // 10、金额以清算金额为准；
            trade.Balance = this.GetDouble(row.QSJE);
            // 11、9:30之前为集合竞价；9:30之后为连续竞价；默认为2。
            trade.Trade_Type = this.GetTradeTypeByTradeTime(row.CJSJ, 2);
            // 12、基金公司/内部融券标志位；1表示基金公司；2表示内部融券；
            trade.Source_Type = 2;
            // 13、基金代码/账户信息；
            trade.Source = string.Empty;
            // 14、成交额；
            trade.Turn_Over = trade.Balance;
            // 15、前置编号；
            trade.Front_Id = 0;
            // 16、回话编号；
            trade.Session_Id = 0;
            // 17、本地委托编号；
            trade.Local_Order_Id = string.Empty;
            // 18、项目代码；
            trade.Xmdm = 0;
            // 19、资产单元；
            trade.Zcdy = string.Empty;
            // 20、组合代码；
            trade.Zhdm = string.Empty;
            // 21、策略号；
            trade.Policy_Id = 0;
            // 22、委托日期；
            trade.Order_Date = string.Empty;
            // 23、委托编号；
            trade.Order_Id = string.Empty;
            // 24、交易通道；
            trade.Trade_Route = string.Empty;
            // 25、经纪商席位代码；
            trade.Seat_Id = string.Empty;
            // 26、投资者客户代码；
            trade.Client_Id = string.Empty;
            // 27、系统成交日期；
            trade.Sys_Trade_Date = row.JYRQ;
            // 28、系统成交结算编号；
            trade.Sys_Trade_Settlement_Id = 0;
            trade.Sys_Trade_Id = row.CJBH;
            // 29、系统成交日期；
            trade.Sys_Order_Date = row.JYRQ;
            // 30、系统委托结算编号；
            trade.Sys_Order_Settlement_Id = 0;
            // 31、系统委托编号；
            trade.Sys_Order_Id = row.WTBH;
            // 32、成交时间；
            trade.Trade_Time = row.CJSJ;

            return trade;
        }

        #endregion

        #region 数据转换

        protected override string ConvertSecurityId(string oldId)
        {
            return string.Format("{0}.SH", oldId != null ? oldId.Trim() : string.Empty);
        }

        public ESecurityType GetSecurityType(JsmxRow_SH row)
        {
            return ESecurityType.股票;
        }

        public EEntrustType GetEntrustType(JsmxRow_SH row)
        {
            EEntrustType type = EEntrustType.无;

            if (row.MMBZ == 'B')
                type = EEntrustType.买入;
            else if (row.MMBZ == 'S')
                type = EEntrustType.卖出;

            return type;
        }

        /// <summary>
        /// 获取多头空头状态；
        /// 0=多头；1=空头
        /// </summary>
        private int GetSideFlag(JsmxRow_SH row)
        {
            if (row.MMBZ == 'B')
                return 0;
            else
                return 1;
        }

        #endregion

    }
}
