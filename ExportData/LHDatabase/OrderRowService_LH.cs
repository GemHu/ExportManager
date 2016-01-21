using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SettleSysCoding;

namespace Dothan.ExportData
{
    public class OrderRowService_LH : LHDBImportItem, IExport2OrderTable, IExport2TradeTable
    {
        #region Life Cycle

        public OrderRowService_LH(IProject project)
            : base(project, "tb_OrderTable")
        {
        }

        #endregion

        #region Export

        public override bool SyncImportState2Local()
        {
            if (base.SyncImportState2Local())
                return true;

            int count = 0;
            if (this.Date.Date == System.DateTime.Now.Date)
                count = this.getOrderTableCount();
            else
                count = this.getOrderTableHisCount();

            if (count <= 0)
            {
                this.ImportState = EImportStatus.NotDetected;
            }
            else
            {
                this.ImportState = EImportStatus.WaitForImport;
                this.TotalCount = count;
            }

            return true;
        }

        public override void DoExport(IExportCallback callback)
        {
            DbDataReader reader = this.GetOrderItems(false, EMarketType.ALL);
            this.Export2OrderAndTradeTable(reader, callback);
            this.Close();
        }

        private DbDataReader GetOrderItems(bool tradeOnly, EMarketType market)
        {
            string sql = null;
            char mt = this.GetMarketType(market);

            if (tradeOnly)
            {
                if (market == EMarketType.ALL)
                {
                    if (this.Date.Date == System.DateTime.Now.Date)
                        sql = string.Format("SELECT * FROM dbo.tb_OrderTable WHERE SUBSTRING(CONVERT(CHAR(10), CrtDate, 112), 1, 8) = '{0}' AND CjNum > 0 ORDER BY CrtDate", this.DateName);
                    else
                        sql = string.Format("SELECT * FROM dbo.tb_OrderTable_his WHERE SUBSTRING(CONVERT(CHAR(10), CrtDate, 112), 1, 8) = '{0}' AND CjNum > 0 ORDER BY CrtDate", this.DateName);
                }
                else
                {
                    if (this.Date.Date == System.DateTime.Now.Date)
                        sql = string.Format("SELECT * FROM dbo.tb_OrderTable WHERE SUBSTRING(CONVERT(CHAR(10), CrtDate, 112), 1, 8) = '{0}' AND CjNum > 0 AND {1} = '{2}' ORDER BY CrtDate", this.DateName, OrderRow_LH.C_sc, mt);
                    else
                        sql = string.Format("SELECT * FROM dbo.tb_OrderTable_his WHERE SUBSTRING(CONVERT(CHAR(10), CrtDate, 112), 1, 8) = '{0}' AND CjNum > 0 AND {1} = '{2}' ORDER BY CrtDate", this.DateName, OrderRow_LH.C_sc, mt);
                }
            }
            else
            {
                if (market == EMarketType.ALL)
                {
                    if (this.Date.Date == System.DateTime.Now.Date)
                        sql = string.Format("SELECT * FROM dbo.tb_OrderTable WHERE SUBSTRING(CONVERT(CHAR(10), CrtDate, 112), 1, 8) = '{0}' ORDER BY CrtDate", this.DateName);
                    else
                        sql = string.Format("SELECT * FROM dbo.tb_OrderTable_his WHERE SUBSTRING(CONVERT(CHAR(10), CrtDate, 112), 1, 8) = '{0}' ORDER BY CrtDate", this.DateName);
                }
                else
                {
                    if (this.Date.Date == System.DateTime.Now.Date)
                        sql = string.Format("SELECT * FROM dbo.tb_OrderTable WHERE SUBSTRING(CONVERT(CHAR(10), CrtDate, 112), 1, 8) = '{0}' AND {1} = '{2}' ORDER BY CrtDate", this.DateName, OrderRow_LH.C_sc, mt);
                    else
                        sql = string.Format("SELECT * FROM dbo.tb_OrderTable_his WHERE SUBSTRING(CONVERT(CHAR(10), CrtDate, 112), 1, 8) = '{0}' AND {1} = '{2}' ORDER BY CrtDate", this.DateName, OrderRow_LH.C_sc, mt);
                }
            }

            return this.ExecuteReader(sql);
        }

        private int getOrderTableCount()
        {
            string sql = string.Format("SELECT COUNT(*) FROM dbo.tb_OrderTable");
            object obj = this.ExecuteScalar(sql);
            if (obj == null)
                return -1;

            return (int)obj;
        }

        private int getOrderTableHisCount()
        {
            //string sql = string.Format("SELECT COUNT(*) FROM dbo.tb_OrderTable_his WHERE CONVERT(VARCHAR(10), CrtDate, 120) = '{0}';", this.Date.ToShortDateString());
            string sql = string.Format("SELECT COUNT(*) FROM dbo.tb_OrderTable_his WHERE SUBSTRING(CONVERT(CHAR(10), CrtDate, 112), 1, 8) = '{0}'", this.DateName);
            object obj = this.ExecuteScalar(sql);
            if (obj == null)
                return -1;

            return (int)obj;
        }

        protected void Export2OrderAndTradeTable(DbDataReader reader, IExportCallback callBack)
        {
            OrderTable orderTable = new OrderTable();
            TradeTable tradeTable = new TradeTable();
            if (!orderTable.Open() || !tradeTable.Open())
            {
                orderTable.Close();
                tradeTable.Close();

                return;
            }

            while (reader.Read())
            {
                if (this.TheProject.HasStop)
                {
                    this.ImportState = EImportStatus.Interrupt;
                    break;
                }

                OrderRow_LH row = this.GetOrderRow_LH(reader as SqlDataReader);
                orderTable.Add(this.GetOrderRow(row));

                // 成交信息
                if (row.CjNum > 0)
                {
                    tradeTable.Add(GetTradeRow(row));
                    callBack.ValidIndex++;
                }

                callBack.CurrentIndex++;
            }

            orderTable.Close();
            tradeTable.Close();
        }

        protected OrderRow_LH GetOrderRow_LH(DbDataReader reader)
        {
            OrderRow_LH order = new OrderRow_LH();
            order.BS = Convert.ToChar(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_BS)));
            order.cancelnum = Convert.ToDouble(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_cancelnum)));
            order.Cancelorderno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_Cancelorderno)));
            order.cidx = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_cidx)));
            order.CjBal = Convert.ToDouble(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_CjBal)));
            order.CjNum = Convert.ToDouble(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_CjNum)));
            order.CjTimeJys = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_CjTimeJys)));
            order.CrtDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_CrtDate)));
            order.CustomInfo1 = Convert.ToString(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_CustomInfo1)));
            order.CustomInfo2 = Convert.ToString(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_CustomInfo2)));
            order.CustomInfo3 = Convert.ToString(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_CustomInfo3)));
            order.DebugInfo = Convert.ToString(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_DebugInfo)));
            order.FirtCjTime = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_FirtCjTime)));
            order.FroseBal = Convert.ToDouble(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_FroseBal)));
            order.FroseStore = Convert.ToDouble(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_FroseStore)));
            order.FroseStoreETF = Convert.ToDouble(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_FroseStoreETF)));
            order.gddm = Convert.ToString(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_gddm)));
            order.InstruId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_InstruId)));
            order.LastCjTime = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_LastCjTime)));
            order.mUnitnum = Convert.ToDouble(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_mUnitnum)));
            order.OpenOrClose = Convert.ToChar(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_OpenOrClose)));
            order.Oper = Convert.ToString(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_Oper)));
            order.orderno = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_orderno)));
            order.OrderNum = Convert.ToDouble(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_OrderNum)));
            order.OrderPr = Convert.ToDouble(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_OrderPr)));
            order.OrderTime = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_OrderTime)));
            order.OrderType = Convert.ToChar(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_OrderType)));
            order.PolicyId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_PolicyId)));
            order.reportno = Convert.ToString(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_reportno)));
            order.ReportTime = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_ReportTime)));
            order.sc = Convert.ToChar(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_sc)));
            order.status = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_status)));
            order.xmdm = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_xmdm)));
            order.zjdm = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_zjdm)));
            order.zmdm = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_zmdm)));
            order.zqdm = Convert.ToString(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_zqdm)));
            order.zqlb = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(OrderRow_LH.C_zqlb)));

            return order;
        }

        #endregion

        #region IExport2OrderTable

        public OrderRow GetOrderRow(DbDataReader reader)
        {
            return this.GetOrderRow(this.GetOrderRow_LH(reader as SqlDataReader));
        }

        public OrderRow GetOrderRow(OrderRow_LH row)
        {
            OrderRow order = new OrderRow();

            order.Trade_Date = row.CrtDate != null ? row.CrtDate.Value.ToString("yyyyMMdd") : null;
            order.Order_Id = row.orderno.ToString();
            order.Security_Id = this.ConvertSecurityId(row);
            order.Direction = this.GetDirection(row);
            order.Volume = row.OrderNum;
            order.Multiplier = row.mUnitnum;
            order.Side = this.GetSideState(row);
            order.Price = row.OrderPr;
            order.Order_Type = row.OpenOrClose;
            //order.Order_Status = ;
            //order.Order_Status_Msg;
            //order.Original_Volume;
            //order.Turn_Over;
            //order.Front_Id;
            //order.Session_Id = ;
            //order.Local_Order_Id;
            order.Xmdm = row.xmdm;
            //order.Zcdy = ;
            //order.Zhdm = row.zmdm.ToString();
            order.Policy_Id = row.PolicyId;
            //order.Trading_Route;
            //order.Seat_Id;
            //order.Client_Id;
            //order.Sys_Order_Date;
            //order.Sys_Order_Settlement_Id;
            //order.Sys_Order_Id;
            //order.Create_User_Id;
            //order.Create_Iime;
            //order.Update_User_Id;
            //order.Update_Time;

            return order;
        }

        #endregion

        #region IExport2TradeTable

        public TradeRow GetTradeRow(DbDataReader reader)
        {
            return this.GetTradeRow(this.GetOrderRow_LH(reader as SqlDataReader));
        }

        public TradeRow GetTradeRow(OrderRow_LH row)
        {
            TradeRow trade = new TradeRow();
            trade.Trade_Date = row.CrtDate != null ? row.CrtDate.Value.ToString("yyyyMMdd") : null;
            //trade.Trade_Id;
            trade.Security_Id = this.ConvertSecurityId(row);
            trade.Direction = this.GetDirection(row);
            trade.Volume = row.CjNum;
            trade.Multiplier = row.mUnitnum;
            trade.Side = this.GetSideState(row);
            trade.Price = row.OrderPr;
            trade.Balance = row.CjBal;
            //trade.Trade_Type;
            //trade.Source_Type;
            //trade.Source
            trade.Turn_Over = row.CjBal;
            //trade.Front_Id;
            //trade.Session_Id;
            //trade.Local_Order_Id;
            trade.Xmdm = row.xmdm;
            //trade.Zcdy;
            trade.Zhdm = row.zmdm.ToString();
            trade.Policy_Id = row.PolicyId;
            trade.Order_Date = row.CrtDate != null ? row.CrtDate.Value.ToString("yyyyMMdd") : null;
            trade.Order_Id = row.orderno.ToString();
            //trade.Trade_Route;
            //trade.Seat_Id;
            //trade.Client_Id;
            //trade.Sys_Trade_Date;
            //trade.Sys_Trade_Settlement_Id;
            //trade.Sys_Trade_Id;
            //trade.Sys_Order_Date;
            //trade.Sys_Order_Settlement_Id;
            //trade.Sys_Order_Id;
            trade.Trade_Time = row.CjTimeJys.ToString();

            return trade;
        }

        #endregion

        #region Data Convert

        protected int GetDirection(OrderRow_LH order)
        {
            if (order.sc == 'S' || order.sc == 'H')
            {
                if (order.BS == 'B')
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_BUY);
                else if (order.BS == 'S')
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_SELL);
            }
            else if (order.sc == 'F')
            {
                if (order.OpenOrClose == 'O')
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_FUT_OPEN);
                else if (order.OpenOrClose == 'C')
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_FUT_CLOSE);
            }

            return 0;
        }

        protected string ConvertSecurityId(OrderRow_LH order)
        {
            if (string.IsNullOrEmpty(order.zqdm))
                return string.Empty;

            if (order.sc == 'S' || order.sc == 's')
                return string.Format("{0}.SZ", order.zqdm);
            else if (order.sc == 'H' || order.sc == 'h')
                return string.Format("{0}.SH", order.zqdm);
            else if (order.sc == 'F' || order.sc == 'f')
                return string.Format("{0}.CFE", order.zqdm);

            return order.zqdm;
        }

        /// <summary>
        /// 获取多头空头状态。
        /// </summary>
        protected int GetSideState(OrderRow_LH order, int defaultValue = 0)
        {
            if (order.OpenOrClose == 'C' || order.OpenOrClose == 'c')
            {
                if (order.BS == 'B' || order.BS == 'b')
                    return 1;
                else if (order.BS == 'S' || order.BS == 's')
                    return 0;
            }
            else if (order.OpenOrClose == 'O' || order.OpenOrClose == 'o')
            {
                if (order.BS == 'B' || order.BS == 'b')
                    return 0;
                else if (order.BS == 'S' || order.BS == 's')
                    return 1;
            }

            return defaultValue;
        }

        public static ESecurityType GetSecurityType(OrderRow_LH row)
        {
            return GetSecurityType(row.zqlb);
        }

        public static ESecurityType GetSecurityType(int zqlb)
        {
            ESecurityType type = ESecurityType.其他;
            // 1: 'STOCK':股票
            if (zqlb >= 100 && zqlb < 200)
            {
                type = ESecurityType.投票;
            }
            // 2: 'BOND':债券
            else if (zqlb >= 200 && zqlb < 300)
            {
                if (zqlb == 201)                    // 201 国债
                    type = ESecurityType.国债;
                else if (zqlb == 202)               // 202 地方债
                    type = ESecurityType.地方债;
                else if (zqlb == 204)               // 204 可转债
                    type = ESecurityType.可转债;
                else if (zqlb == 205)               // 205 公司债
                    type = ESecurityType.公司债;
                else if (zqlb == 249)               // 249 债转股代码
                    type = ESecurityType.债转股;
                else if (zqlb == 250)               // 250 债券出入库
                    type = ESecurityType.债券质押;
            }
            // 3: 'fund'基金
            else if (zqlb >= 300 && zqlb < 400)
            {
                if (zqlb == 300)                    // 300 契约型封闭式基金
                    type = ESecurityType.封闭式基金;
                //else if (zqlb == 310)             // 310 交易型开放式指数证券投资基金ETF
                //    type = ESecurityType.开放式基金;
                //else if (zqlb == 311)               // 311 国债ETF
                //    type = ESecurityType.开放式基金;
                //else if (zqlb == 319)                // 319 开放式基金申赎
                //    type = ESecurityType.开放式基金;
                //else if (zqlb == 321)                // 321 开放式基金认购
                //    type = ESecurityType.开放式基金;
                //else if (zqlb == 322)                // 322 开放式基金跨市场转托管
                //    type = ESecurityType.开放式基金;
                //else if (zqlb == 323)                // 323 开放式基金分红
                //    type = ESecurityType.开放式基金;
                //else if (zqlb == 324)                // 324 开放式基金基金转换
                //    type = ESecurityType.开放式基金;
                else
                    type = ESecurityType.开放式基金;
            }
            // 4: 'warrants'权证
            else if (zqlb >= 400 && zqlb < 500)
            {
                // TODO:
                if (zqlb == 401)                    // 401 权证(含股改权证、公司权证)
                    type = ESecurityType.认购权证;
                else if (zqlb == 482)               // 482 权证行权
                    type = ESecurityType.认购行权;
            }
            // 5: 股指期货
            else if (zqlb >= 500 && zqlb < 600)
            {
                // 501 沪深300期货合约
                // 502 沪深300期货合约
                // 503 沪深300期货合约
                // 504 沪深300期货远月合约
                // 511 沪深50期货近月
                // 512 沪深50期货下月
                // 513 沪深50期货季月
                // 514 沪深50期远月合约
                // 521 中证500期货近月
                // 522 中证500期货下月
                // 523 中证500期货季月
                // 524 中证500期远月合约
                return ESecurityType.股指期货;
            }
            // 6: 回购
            else if (zqlb >= 600 && zqlb < 700)
            {
                // 601 国债回购（席位托管方式）
                // 602 企业债回购
                // 603 国债买断式回购
                // 604 债券质押式回购（账户托管方式）
                // 605 债券质押式报价回购
                type = ESecurityType.债券回购;
            }
            // 7: 配股
            else if (zqlb >= 700 && zqlb < 800)
            {
                type = ESecurityType.配股;
            }
            // 8: 申购(新股，增发等)
            else if (zqlb >= 800 && zqlb < 900)
            {
                // TODO:
                // 801 申购、增发（对应600***）
                // 801 持股增发（对应600***
                // 802 可转债申购（对应600***）
                // 803 基金申购
                // 804 国债券分销
                type = ESecurityType.申购;
            }
            // 9: B股业务
            else if (zqlb >= 900 && zqlb < 1000)
            {
                // 900 B股
                type = ESecurityType.股票;
            }
            // 10: 指数认购期权
            else if (zqlb >= 1000 && zqlb < 1100)
            {
                // 1001 沪深300指数期权合约
                type = ESecurityType.认购期权;
            }
            // 11: 指数认沽期权
            else if (zqlb >= 1100 && zqlb < 1200)
            {
                // 1101:个股期权
                type = ESecurityType.认沽期权;
            }
            // 12: 个股认购期权
            else if (zqlb >= 1200 && zqlb < 1300)
            {
                // 1201:个股认购期权
                type = ESecurityType.认购期权;
            }
            // 13: 个股认沽期权
            else if (zqlb >= 1300 && zqlb < 1400)
            {
                // 1301:个股认沽期权
                type = ESecurityType.认沽期权;
            }
            // 14: 不存在
            // else if (zqlb >= 1400 && zqlb < 1500) { }
            // 15: 国债期货
            else if (zqlb >= 1500 && zqlb < 1600)
            {
                //               1501:国债期货近月
                //               1502:国债期货季月
                //               1503:国债期货下季月
                type = ESecurityType.国债期货;
            }
            // 16: 大连商品期货，目前不细分
            else if (zqlb >= 1600 && zqlb < 1700)
            {
                type = ESecurityType.商品期货;
            }
            // 17: 郑州商品期货，目前不细分
            else if (zqlb >= 1700 && zqlb < 1800)
            {
                type = ESecurityType.商品期货;
            }
            // 18: 上海商品期货，目前不细分
            else if (zqlb >= 1800 && zqlb < 1900)
            {
                type = ESecurityType.商品期货;
            }
            // 98: 指数类代码:
            else if (zqlb >= 9800 && zqlb < 9900)
            {
                type = ESecurityType.指数;
            }
            // 99: 其他': Other:以后扩充
            else if (zqlb >= 9900 && zqlb < 10000)
            {
                type = ESecurityType.其他;
            }

            return type;
        }

        public static EEntrustType GetEntrustType(OrderRow_LH order)
        {
            EEntrustType direction = EEntrustType.无;
            if (order.sc == 'S' || order.sc == 'H')
            {
                if (order.BS == 'B')
                    direction = EEntrustType.买入;
                else if (order.BS == 'S')
                    direction = EEntrustType.卖出;
            }
            else if (order.sc == 'F')
            {
                if (order.BS == 'B')
                {
                    if (order.OpenOrClose == 'O')
                        direction = EEntrustType.买入开仓;
                    else if (order.OpenOrClose == 'C')
                        direction = EEntrustType.买入平仓;
                    else
                        direction = EEntrustType.买入;
                }
                else if (order.BS == 'S')
                {
                    if (order.OpenOrClose == 'O')
                        direction = EEntrustType.卖出开仓;
                    else if (order.OpenOrClose == 'C')
                        direction = EEntrustType.卖出平仓;
                    else
                        direction = EEntrustType.卖出;
                }
            }

            return direction;
        }

        #endregion

        #region Other

        public List<OrderRow_LH> GetTradeItems(EMarketType type)
        {
            List<OrderRow_LH> list = new List<OrderRow_LH>();

            using (DbDataReader reader = this.GetOrderItems(true, type))
            {
                while (reader.Read())
                {
                    list.Add(this.GetOrderRow_LH(reader));
                }
            }
            this.Close();

            return list;
        }

        /// <summary>
        /// 获取当天所有给定证券代码的数据。
        /// </summary>
        public List<OrderRow_LH> GetTradeItems(string securityId, EMarketType type)
        {
            int index = securityId.IndexOf('.');
            if (index > 0)
                securityId = securityId.Substring(0, index);

            if (this.ImportState == EImportStatus.Init)
                this.SyncImportState2Local();

            List<OrderRow_LH> dataList = new List<OrderRow_LH>();
            if (this.ImportState == EImportStatus.NotDetected)
                return dataList;

            using (DbDataReader reader = this.GetOrderItems(true, type))
            {
                while (reader.Read())
                {
                    OrderRow_LH row = this.GetOrderRow_LH(reader);
                    if (row.zqdm == securityId)
                        dataList.Add(this.GetOrderRow_LH(reader));
                }
            }

            return dataList;
        }

        private char GetMarketType(EMarketType type)
        {
            switch (type)
            {
                case EMarketType.SH:
                    return 'H';
                case EMarketType.SZ:
                    return 'S';
                case EMarketType.JJ:
                    return 'F';
                default:
                    return '\0';
            }
        }
        
        #endregion
    }
}
