using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using SettleSysCoding;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;

namespace Dothan.ExportData
{
    /// <summary>
    /// ETF退补款数据处理服务类。
    /// </summary>
    public class CILService : DbfImportItem, IExport2TradeTable
    {
        #region Life Cycle

        public CILService(IProject project)
            : base(project)
        {
        }

        #endregion

        #region Property

        public override string Name
        {
            get { return "CIL"; }
        }

        protected override void UpdateFileName()
        {
            if (!Directory.Exists(this.TablePath))
                return;

            foreach (string item in Directory.GetFiles(this.TablePath))
            {
                // 接口文件名： 	YFDddddddxxxCILyyyymmdd.dbf
                // 其中dddddd为ETF基金一级市场代码，xxx为销售代理机构开放式基金代销机构代码；
                // YFD代表易方达基金；CIL表示现金替代款；yyyymmdd表示清算日期，为交收日的前一个上交所交易日。

                string itemName = Path.GetFileName(item);
                if (itemName.StartsWith("YFD", StringComparison.OrdinalIgnoreCase) && itemName.Substring(12).StartsWith(this.Name, StringComparison.OrdinalIgnoreCase))
                {
                    this.FileName = item;
                    return;
                }
            }

            this.FileName = string.Empty;
        }

        #endregion

        #region Export

        public override void DoExport(IExportCallback callback)
        {
            string sql = string.Format("select * from {0}", this.TableName);
            DbDataReader reader = this.ExecuteReader(sql);
            base.Export2TradeTable(reader, callback, this);
            this.Close();
        }

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
        //        buffer_lh = lhService.GetTradeItems(EMarketType.ALL);
        //    }

        //    o32Service.SyncImportState2Local();
        //    if (o32Service.TotalCount < this.MaxBufferSize)
        //    {
        //        bufferAll_o32 = true;
        //        buffer_o32 = o32Service.GetTradeItems(EMarketType.ALL);
        //    }

        //    while (reader.Read())
        //    {
        //        if (this.TheProject.HasStop)
        //        {
        //            tradeService.ImportState = EImportStatus.Interrupt;
        //            break;
        //        }

        //        CilRow row = this.GetCilRow(reader);
        //        if (securityId != row.ZQDM)
        //        {
        //            securityId = row.ZQDM;
        //            if (bufferAll_lh)
        //                lhDataList = buffer_lh.Where(o => o.zqdm == securityId).ToList();
        //            else
        //                lhDataList = lhService.GetTradeItems(row.ZQDM, EMarketType.ALL);
        //            if (bufferAll_o32)
        //                o32DataList = buffer_o32.Where(o => o.VC_REPORT_CODE == securityId).ToList();
        //            else
        //                o32DataList = o32Service.GetTradeItems(row.ZQDM, EMarketType.ALL);
        //        }

        //        //EEntrustType entrustType = this.GetEntrustType(row);
        //        if (lhDataList.Count <= 0 && o32DataList.Count <= 0)
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

        protected CilRow GetCilRow(DbDataReader reader)
        {
            CilRow row = new CilRow();

            row.JJDM = this.GetString(reader.GetValue(reader.GetOrdinal(CilRow.C_JJDM)));
            row.ZJLX = this.GetString(reader.GetValue(reader.GetOrdinal(CilRow.C_ZJLX)));
            row.ZQZH = this.GetString(reader.GetValue(reader.GetOrdinal(CilRow.C_ZQZH)));
            row.JYXW = this.GetString(reader.GetValue(reader.GetOrdinal(CilRow.C_JYXW)));
            row.XWHY = this.GetString(reader.GetValue(reader.GetOrdinal(CilRow.C_XWHY)));
            row.TKJE = this.GetString(reader.GetValue(reader.GetOrdinal(CilRow.C_TKJE)));
            row.BKJE = this.GetString(reader.GetValue(reader.GetOrdinal(CilRow.C_BKJE)));
            row.JSRQ = this.GetString(reader.GetValue(reader.GetOrdinal(CilRow.C_JSRQ)));
            row.JYRQ = this.GetString(reader.GetValue(reader.GetOrdinal(CilRow.C_JYRQ)));
            row.ZQDM = this.GetString(reader.GetValue(reader.GetOrdinal(CilRow.C_ZQDM)));

            return row;
        }

        #endregion

        #region IExport2TradeTable

        public TradeRow GetTradeRow(DbDataReader reader)
        {
            return this.GetTradeRow(this.GetCilRow(reader));
        }

        protected TradeRow GetTradeRow(CilRow row)
        {
            TradeRow data = new TradeRow();

            // 02、成交日期；
            data.Trade_Date = "";
            // 03、成交编号；
            data.Trade_Id = "";
            // 04、证券代码以证券代码1为准；为了防止与深交所证券代码混淆，需要以加后缀".SH"
            data.Security_Id = this.ConvertSecurityId(row.ZQDM);

            // 02：现金替代退补款；04：赎回替代款；
            if (row.ZJLX.Equals("02"))
            {
                // 05、方向；
                data.Direction = DirectionCoding.GetDirectionCode(TradeType.TRADE_ETF_CASH_REPLACEMENT_DRAWBACK);
                // 10、成交金额；
                data.Balance = this.GetDouble(row.BKJE);
                // 14、成交额；
                data.Turn_Over = this.GetDouble(row.BKJE);
            }
            else
            {
                data.Direction = DirectionCoding.GetDirectionCode(TradeType.TRADE_ETF_RED_REPLACEMENT_DRAWBACK);
                data.Balance = this.GetDouble(row.TKJE);
                data.Turn_Over = this.GetDouble(row.TKJE);
            }

            // 06、数量暂时以成交数量为准；
            data.Volume = 1.0;
            // 07、上海合约乘数为1
            data.Multiplier = 1;
            // 08、多头空头（0表示多头，1表示空头）；
            data.Side = 0;
            // 09、价格以价格1为准；
            data.Price = 0.0;
            // 11、成交类型：9:30之前为集合竞价；9:30之后为连续竞价；默认为2。
            data.Trade_Type = 2;
            // 12、基金公司/内部融券（1表示基金公司，2表示内部融券）；
            data.Source_Type = 0;
            // 13、基金代码/账户信息；
            data.Source = string.Empty;
            // 15、前置编号；
            data.Front_Id = 0;
            // 16、会话编号；
            data.Session_Id = 0;
            // 17、本地委托编号；
            data.Local_Order_Id = "";
            // 18、项目代码；
            data.Xmdm = 0;
            // 19、资产单元；
            data.Zcdy = "";
            // 20、组合代码；
            data.Zhdm = "";
            // 21、策略号；
            data.Policy_Id = 0;
            // 22、委托日期；
            data.Order_Date = "";
            // 23、委托编号；
            data.Order_Id = "";
            // 24、交易通道；
            data.Trade_Route = "";
            // 25、经纪商席位代码；
            data.Seat_Id = row.JYXW;
            // 26、投资者客户代码；
            data.Client_Id = "";
            // 27、系统成交日期；
            data.Sys_Trade_Date = row.JYRQ;
            // 28、系统成交结算编号；
            data.Sys_Trade_Settlement_Id = 0;
            // 29、系统成交编号；
            data.Sys_Trade_Id = "";
            // 30、系统委托日期；
            data.Sys_Order_Date = row.JYRQ;
            // 31、系统委托结算编号；
            data.Sys_Order_Settlement_Id = 0;
            // 32、系统委托编号；
            data.Sys_Order_Id = "";
            // 33、成交时间；
            data.Trade_Time = "";

            return data;
        }

        #endregion

        #region 数据转换

        protected override string ConvertSecurityId(string oldId)
        {
            return string.Format("{0}.SH", oldId);
        }

        //public ESecurityType GetSecurityType(CilRow row)
        //{

        //}

        //public EEntrustType GetEntrustType(CilRow row)
        //{

        //}

        #endregion
    }
}
