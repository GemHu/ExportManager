using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Dothan.DzHelpers;
using SettleSysCoding;
using System.Diagnostics;

namespace Dothan.ExportData
{
    public class JSMXService_SZ : DbfImportItem, IExport2TradeTable
    {
        #region Life Cycle

        public JSMXService_SZ(IProject project)
            : this(project, 1)
        {
        }

        public JSMXService_SZ(IProject project, int batchNum)
            : base(project)
        {
            this.BatchNum = batchNum;
            this.UpdateFileName();
        }

        #endregion

        #region FileName

        public override string Name
        {
            get { return "SJSMX" + this.BatchNum; }
        }

        public int BatchNum
        {
            get { return this._BatchNum; }
            set
            {
                if (value >= 0 && value <= 2)
                    this._BatchNum = value;
            }
        }
        private int _BatchNum = 1;

        #endregion

        #region Export

        public override void DoExport(IExportCallback callback)
        {
            string sql = string.Format("select * from {0} order by '{1}'", this.TableName, JsmxRow_SZ.C_MXZQDM);
            DbDataReader reader = this.ExecuteReader(sql);
            base.Export2TradeTable(reader, callback, this);
            this.Close();
        }

        protected JsmxRow_SZ GetJsmxRow_SZ(DbDataReader reader)
        {
            JsmxRow_SZ row = new JsmxRow_SZ();

            row.MXJSZH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXJSZH)));
            row.MXBFZH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXBFZH)));
            row.MXSJLX = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXSJLX)));
            row.MXYWLB = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXYWLB)));
            row.MXZQDM = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXZQDM)));
            row.MXJYDY = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXJYDY)));
            row.MXTGDY = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXTGDY)));
            row.MXZQZH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXZQZH)));
            row.MXDDBH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXDDBH)));
            row.MXYYB = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXYYB)));
            row.MXZXBH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXZXBH)));
            row.MXYWLSH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXYWLSH)));
            row.MXCJSL = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXCJSL)));
            row.MXQSSL = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXQSSL)));
            row.MXCJJG = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXCJJG)));
            row.MXQSJG = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXQSJG)));
            row.MXXYJY = this.GetChar(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXXYJY)));
            row.MXPCBS = this.GetChar(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXPCBS)));
            row.MXZQLB = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXZQLB)));
            row.MXZQZL = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXZQZL)));
            row.MXGFXZ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXGFXZ)));
            row.MXJSFS = this.GetChar(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXJSFS)));
            row.MXHBDH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXHBDH)));
            row.MXQSBJ = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXQSBJ)));
            row.MXYHS = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXYHS)));
            row.MXJYJSF = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXJYJSF)));
            row.MXJGGF = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXJGGF)));
            row.MXGHF = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXGHF)));
            row.MXJSF = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXJSF)));
            row.MXSXF = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXSXF)));
            row.MXQSYJ = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXQSYJ)));
            row.MXQTFY = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXQTFY)));
            row.MXZJJE = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXZJJE)));
            row.MXSFJE = this.GetDouble(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXSFJE)));
            row.MXCJRQ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXCJRQ)));
            row.MXQSRQ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXQSRQ)));
            row.MXJSRQ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXJSRQ)));
            row.MXFSRQ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXFSRQ)));
            row.MXQTRQ = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXQTRQ)));
            row.MXSCDM = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXSCDM)));
            row.MXJYFS = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXJYFS)));
            row.MXZQDM2 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXZQDM2)));
            row.MXTGDY2 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXTGDY2)));
            row.MXDDBH2 = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXDDBH2)));
            row.MXCWDH = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXCWDH)));
            row.MXPPHM = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXPPHM)));
            row.MXFJSM = this.GetString(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXFJSM)));
            row.MXBYBZ = this.GetChar(reader.GetValue(reader.GetOrdinal(JsmxRow_SZ.C_MXBYBZ)));

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
        //        buffer_lh = lhService.GetTradeItems(EMarketType.SZ);
        //    }

        //    o32Service.SyncImportState2Local();
        //    if (o32Service.TotalCount < this.MaxBufferSize)
        //    {
        //        bufferAll_o32 = true;
        //        buffer_o32 = o32Service.GetTradeItems(EMarketType.SZ);
        //    }

        //    while (reader.Read())
        //    {
        //        if (this.TheProject.HasStop)
        //        {
        //            tradeService.ImportState = EImportStatus.Interrupt;
        //            break;
        //        }

        //        JsmxRow_SZ row = this.GetJsmxRow_SZ(reader);
        //        if (securityId != row.MXZQDM)
        //        {
        //            securityId = row.MXZQDM;
        //            if (bufferAll_lh)
        //                lhDataList = buffer_lh.Where(o => o.zqdm.Equals(securityId)).ToList();
        //            else
        //                lhDataList = lhService.GetTradeItems(row.MXZQDM, EMarketType.SZ);
        //            if (bufferAll_o32)
        //                o32DataList = buffer_o32.Where(o => o.VC_REPORT_CODE.StartsWith(securityId)).ToList();
        //            else
        //                o32DataList = o32Service.GetTradeItems(row.MXZQDM, EMarketType.SZ);
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
            return this.GetTradeRow(this.GetJsmxRow_SZ(reader));
        }

        protected TradeRow GetTradeRow(JsmxRow_SZ row)
        {
            TradeRow data = new TradeRow();

            // 02、成交日期：成交日期；
            data.Trade_Date = string.Empty;
            // 03、成交ID：可能为订单编号/申请单号、执行编号，对于其他渠道申请的委托，执行编号可能为空；
            data.Trade_Id = string.Empty;
            // 04、证券代码：深交所证券代码需要添加后缀".SZ"；
            data.Security_Id = this.ConvertSecurityId(row.MXZQDM);
            // 05、方向标志位：
            data.Direction = this.GetDirection(row);
            // 06、数量：数量暂时以成交数量为准；
            data.Volume = row.MXCJSL;
            // 07、合约乘数：
            data.Multiplier = 1;
            // 08、多头空头；
            data.Side = this.GetSideFlag(row);
            // 09、价格：
            data.Price = row.MXCJJG;
            // 10、金额：
            data.Balance = row.MXQSBJ;
            // 11、成交类型：9:30之前为集合竞价；9:30之后为连续竞价；默认为2；
            data.Trade_Type = 2;
            // 12、基金代码或内部融券标志位；
            data.Source_Type = 2;
            // 13、基金代码/账户信息；
            data.Source = row.MXZQZH;
            // 14、成交额：
            data.Turn_Over = data.Balance;
            // 15、前置编号；
            data.Front_Id = 0;
            // 16、回话编号；
            data.Session_Id = 0;
            // 17、本地委托编号；
            data.Local_Order_Id = string.Empty;
            // 18、项目代码；
            data.Xmdm = 0;
            // 19、资产单元；
            data.Zcdy = string.Empty;
            // 20、组合代码；
            data.Zhdm = string.Empty;
            // 21、策略号；
            data.Policy_Id = 0;
            // 22、委托日期：没有托管日期；
            data.Order_Date = string.Empty;
            // 23、委托编号：对应于结算明细表中的订单编号；
            data.Order_Id = string.Empty;
            // 24、交易通道：
            data.Trade_Route = string.Empty;
            // 25、经纪商席位代码；
            data.Seat_Id = string.Empty;
            // 26、投资者客户代码；
            data.Client_Id = string.Empty;
            // 27、系统成交日期；
            data.Sys_Trade_Date = row.MXCJRQ;
            // 28、系统成交结算编号；
            data.Sys_Trade_Settlement_Id = 0;
            // 29、系统成交ID：同成交ID，清算表中的数据均为系统生成的，但是在成交表中需要的是成交ID，所以将成交信息与系统成交信息设为相同的数据；
            data.Sys_Trade_Id = row.MXYWLSH;
            // 30、系统委托日期与系统成交日期为同一天；
            data.Sys_Order_Date = row.MXCJRQ;
            // 31、系统委托结算编号：
            data.Sys_Order_Settlement_Id = 0;
            // 32、系统委托日期；
            data.Sys_Order_Id = row.MXDDBH;
            // 33、成交时间：清算表中没有与时间相关的字段；
            data.Trade_Time = string.Empty;

            return data;
        }

        #endregion

        #region Data Convert

        protected override string ConvertSecurityId(string oldId)
        {
            return string.Format("{0}.SZ", oldId != null ? oldId.Trim() : string.Empty);
        }

        public int GetDirection(JsmxRow_SZ row)
        {
            if (row.MXQSSL > 0)
                return DirectionCoding.GetDirectionCode(TradeType.TRADE_BUY);
            else
                return DirectionCoding.GetDirectionCode(TradeType.TRADE_SELL);
        }

        public EEntrustType GetEntrustType(JsmxRow_SZ row)
        {
            if (row.MXQSSL >= 0)
                return EEntrustType.买入;
            else
                return EEntrustType.卖出;
        }

        /// <summary>
        /// 获取多头空头状态；
        /// 0=多头；1=空头
        /// </summary>
        private int GetSideFlag(JsmxRow_SZ row)
        {
            if (row.MXQSSL > 0)
                return 0;
            else
                return 1;
        }

        #endregion
    }
}
