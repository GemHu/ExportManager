using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using SettleSysCoding;
using System.Diagnostics;

namespace Dothan.ExportData
{
    public class TradeInfo_O32Service : O32DBImportItem, IExport2TradeTable
    {
        #region Life Cycle

        public TradeInfo_O32Service(IProject project)
            : base(project, "TradeInfo_O32")
        {

        }

        #endregion

        #region Export

        public override bool SyncImportState2Local()
        {
            if (base.SyncImportState2Local())
                return true;

            int count = this.GetDataCount(this.DateName);

            if (count <= 0)
                this.ImportState = EImportStatus.NotDetected;
            else
                this.ImportState = EImportStatus.WaitForImport;

            this.TotalCount = count;

            return true;
        }

        public override void DoExport(IExportCallback callback)
        {
            DbDataReader reader = this.GetTradeInfos();
            if (reader == null)
                return;

            this.Export2TradeTable(reader, callback, this);
            //this.WriteColumns2File(reader, @"D:\TradeInfoColumns_O32.txt");
            this.Close();
        }

        public void WriteColumns2File(DbDataReader reader, string file)
        {
            if (reader == null)
                return;

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(file))
            {
                for (int i = 0; i < reader.VisibleFieldCount; i++)
                {
                    string index = (i + 1).ToString("D2");
                    string columnName = reader.GetName(i).ToUpper();
                    string type = reader.GetFieldType(i).Name;
                    string comment = "说明";
                    writer.WriteLine(string.Format("{0}\t|\t{1}\t|\t{2}\t|\t{3}", index, columnName, type, comment));
                }
            }
        }

        private string GetTradeTableSql(string date)
        {
            return string.Format(
            "select \n" +
            "	d.vc_stock_name, nvl(et.vc_third_reff,'0') vc_third_reff, a.c_busin_class, a.l_entrust_serial_no, a.vc_deal_no, to_char(a.l_date) l_date, \n" +
            "	a.l_fund_id, b.vc_combi_no, b.vc_combi_name, a.L_BUSINESS_TIME, a.c_entrust_direction, c.vc_report_direction, c.vc_entrustdir_name, \n" +
            "	a.l_deal_amount, a.en_deal_price, \n" +
            "	(a.l_deal_amount*decode(a.c_entrust_direction, '5', 100, '6', 100, '7', 100, '8', 100, a.en_deal_price) * decode(d.c_asset_class,'a', o.l_multiplier_unit,sf_future_multiple(a.vc_inter_code))) en_deal_balance, \n" +
            "	a.vc_report_code, a.vc_stockholder_id, a.vc_report_seat,vc_operator_name, \n" +
            "	(select \n" +
            "		a.vc_currency_name \n" +
            "	from \n" +
            "		TRADE.tcurrencyinfo a \n" +
            "	where \n" +
            "		a.vc_currency_no = d.vc_currency_no) vc_currency_name, \n" +
            "	et.c_invest_type, e.vc_fund_name,e.vc_currency_no vc_currency_no_bb, \n" +
            "	a.l_deal_amount \n" +
            "		* decode(a.c_entrust_direction, '5', 100, '6', 100, '7', 100, '8', 100, a.en_deal_price) \n" +
            "		* decode(d.c_asset_class,'a',o.l_multiplier_unit,sf_future_multiple(a.vc_inter_code)) \n" +
            "		* sf_currency_rate(a.l_date, d.vc_currency_no, e.vc_currency_no,e.l_fund_id,e.l_set_no) en_deal_balance_bb, \n" +
            "	TRUNC(decode(nvl(v.en_fund_value, 0), 0, 0,  a.l_deal_amount \n" +
            "		* decode(a.c_entrust_direction, '5', 100, '6', 100, '7', 100, '8', 100, a.en_deal_price) \n" +
            "		* decode(d.c_asset_class,'a',o.l_multiplier_unit,sf_future_multiple(a.vc_inter_code)) \n" +
            "		*  sf_currency_rate(a.l_date, d.vc_currency_no, e.vc_currency_no,e.l_fund_id,e.l_set_no) \n" +
            "		/ v.en_fund_value \n" +
            "		* 100), 8) en_deal_ratio, \n" +
            "	a.vc_broker_no,tb.vc_broker_name , ' ' vc_grouptype_name, ' ' vc_group_name, d.vc_international_code,d.vc_ric_code,d.vc_sedol_code,d.vc_cusip_code,d.vc_bloomberg_code,a.l_report_serial_no,a.c_market_no, \n" +
            "	case \n" +
            "		when c.c_entrust_direction = 'X' then 1 \n" +
            "		when c.c_entrust_direction = 'Y' then -1 \n" +
            "		else decode(c.c_fund_direction, '1', 1, '2', -1, 0) end \n" +
            "			* a.l_Deal_Amount \n" +
            "			*(CASE \n" +
            "				WHEN a.c_entrust_direction IN ('5', '6', '7', '8') THEN 100 \n" +
            "				ELSE a.en_deal_price END + decode(h.c_net_price,0,0,Nvl(h.en_real_interest,0))) \n" +
            "			* decode(d.c_asset_class,'a',o.l_multiplier_unit,sf_future_multiple(a.vc_inter_code))  en_full_balance, \n" +
            "	ti.l_direct_operator l_direct_operator ,ti.l_Direct_Instead_Operator,  decode(nvl(a.c_margined_out,''), '0','否','1','是', '') c_margined_out \n" +
            "from \n" +
            "	TRADE.THISREALDEAL a, TRADE.vhisfundasset v, TRADE.TCOMBI b, TRADE.TENTRUSTDIRECTION c, TRADE.thisoptionproperty o, TRADE.toperator op, \n" +
            "	TRADE.thisentrusts et, TRADE.thisstockinfo d, TRADE.tfundinfo e, TRADE.tbroker tb, TRADE.thisbondproperty h, TRADE.thisinstruction ti \n" +
            "where \n" +
            "	op.l_operator_no (+) = et.l_operator_no \n" +
            "	and a.l_entrust_serial_no = et.l_entrust_serial_no (+) \n" +
            "	and a.l_fund_id = e.l_fund_id \n" +
            "	and a.l_date = v.l_date(+) \n" +
            "	and a.l_fund_id = v.l_fund_id(+) \n" +
            "	and a.c_valid <> '2' \n" +
            "	and a.l_date = o.l_date(+) \n" +
            "	and a.vc_inter_code = o.vc_inter_code(+) \n" +
            "	and a.l_basecombi_id = b.l_combi_id \n" +
            "	and a.c_entrust_direction = c.c_entrust_direction \n" +
            "	and a.c_market_no=c.c_market_no \n" +
            "	and a.l_date = et.l_date(+) \n" +
            "	and a.l_date = d.l_date \n" +
            "	and a.vc_inter_code = d.vc_inter_code \n" +
            "	and a.vc_broker_no=tb.vc_broker_no(+) \n" +
            "	and a.l_date = h.l_date(+) \n" +
            "	and a.vc_inter_code = h.vc_inter_code(+) \n" +
            "	and a.l_date = ti.l_date(+) \n" +
            "	and a.l_daily_instruction_no = ti.l_daily_instruction_no(+) \n" +
            "	and a.l_index_daily_modify = ti.l_index_daily_modify(+) \n" +
            "	and b.c_combi_status not in ('2','3') \n" +
            "	and nvl(et.c_cancel_flag,'0') = '0' \n" +
            "	and a.l_date >= {0} \n" +
            "	and a.l_date <= {0} \n" +
            "	and ( \n" +
            "		select count(*) from TRADE.TOPFUNDRIGHT \n" +
            "		where TOPFUNDRIGHT.l_basecombi_id=a.l_basecombi_id \n" +
            "			and TOPFUNDRIGHT.c_layer='3' \n" +
            "			and TOPFUNDRIGHT.l_operator_no=1000 \n" +
            "			and instr(TOPFUNDRIGHT.vc_rights, '1')>0 ) > 0 \n" +
            "	and  exists ( \n" +
            "		select 1 from TRADE.toperator \n" +
            "		where \n" +
            "			toperator.L_OPERATOR_NO = 1000 \n" +
            "			and ( instr(toperator.VC_MANAGE_RIGHT,d.c_stock_type)>0 or trim(toperator.VC_MANAGE_RIGHT) is null)) \n" +
            "	and rownum<=50000 \n", date);
        }

        private string GetTradeInfoCountSql(string date)
        {
            string tableSql = this.GetTradeTableSql(date);

            return string.Format("select count(*) from ({0})", tableSql);

        }

        private string GetTradeInfosSql(string date)
        {
            return this.GetTradeTableSql(date);
        }

        /// <summary>
        /// 获取制定日期的数据条数。
        /// </summary>
        protected int GetDataCount(string dateName)
        {
            object ret = this.ExecuteScalar(this.GetTradeInfoCountSql(DateName));

            return Convert.ToInt32(ret);
        }

        private DbDataReader GetTradeInfos()
        {
            return this.ExecuteReader(this.GetTradeInfosSql(this.DateName));
        }

        #endregion

        #region IExport2TradeTable

        public override void Export2TradeTable(DbDataReader reader, IExportCallback callback, IExport2TradeTable tradeService)
        {
            if (reader == null)
                return;
            TradeTable tradeTable = new TradeTable();
            if (!tradeTable.Open())
                return;

            OrderRowService_LH lhService = new OrderRowService_LH(this.TheProject);
            if (!lhService.Open())
            {
                tradeTable.Close();
                this.ImportState = EImportStatus.Exception;
                this.TheProject.Output.WriteLine("量化平台数据库打开失败！");
                return;
            }

            ProjectInfoService_LH projInfoService = new ProjectInfoService_LH();
            projInfoService.DB = lhService;
            List<ProjectInfo_LH> projInfos = projInfoService.GetProjectInfos(true);

            bool bufferAll_lh = false;
            List<OrderRow_LH> buffer_lh = null;
            List<OrderRow_LH> lhDataList = null;
            string securityId = string.Empty;

            lhService.SyncImportState2Local();
            if (lhService.TotalCount < this.MaxBufferSize)
            {
                bufferAll_lh = true;
                buffer_lh = lhService.GetTradeItems(EMarketType.ALL);
            }

            while (reader.Read())
            {
                if (this.TheProject.HasStop)
                {
                    tradeService.ImportState = EImportStatus.Interrupt;
                    break;
                }

                TradeInfo_O32 row = this.GetTradeInfo(reader);
                // 过滤掉无效的zmdm
                ProjectInfo_LH projInfo = projInfos.Where(o => o.Zmdm.ToString() == row.VC_COMBI_NO).FirstOrDefault();
                if (projInfo == null)
                {
                    callback.CurrentIndex++;
                    continue;
                }

                string interCode = row.VC_REPORT_CODE;
                if (securityId != interCode)
                {
                    securityId = interCode;
                    if (bufferAll_lh)
                        lhDataList = buffer_lh.Where(o => o.zqdm.Equals(interCode)).ToList();   // 从内存中筛选，速度快
                    else
                        lhDataList = lhService.GetTradeItems(interCode, EMarketType.ALL);  // 直接从数据库查询，比较耗时
                }

                if (lhDataList.Where(o => o.zmdm.ToString() == row.VC_COMBI_NO).FirstOrDefault() == null)
                {
                    TradeRow tradeInfo = this.GetTradeRow(row);
                    tradeInfo.Xmdm = projInfo.Zmdm;
                    tradeTable.Add(tradeInfo);
                    callback.ValidIndex++;
                }

                callback.CurrentIndex++;
            }

            lhService.Close();
            tradeTable.Close();
        }

        public TradeInfo_O32 GetTradeInfo(DbDataReader reader)
        {
            TradeInfo_O32 row = new TradeInfo_O32();

            //1-10
            row.VC_STOCK_NAME = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_STOCK_NAME)));
            row.VC_THIRD_REFF = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_THIRD_REFF)));
            row.C_BUSIN_CLASS = this.GetChar(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_C_BUSIN_CLASS)));
            row.L_ENTRUST_SERIAL_NO = this.GetLong(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_L_ENTRUST_SERIAL_NO)));
            row.VC_DEAL_NO = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_DEAL_NO)));
            row.L_DATE = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_L_DATE)));
            row.L_FUND_ID = this.GetInt(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_L_FUND_ID)));
            row.VC_COMBI_NO = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_COMBI_NO)));
            row.VC_COMBI_NAME = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_COMBI_NAME)));
            row.L_BUSINESS_TIME = this.GetInt(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_L_BUSINESS_TIME)));

            // 11-20
            row.C_ENTRUST_DIRECTION = this.GetChar(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_C_ENTRUST_DIRECTION)));
            row.VC_REPORT_DIRECTION = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_REPORT_DIRECTION)));
            row.VC_ENTRUSTDIR_NAME = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_ENTRUSTDIR_NAME)));
            row.L_DEAL_AMOUNT = this.GetInt(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_L_DEAL_AMOUNT)));
            row.EN_DEAL_PRICE = this.GetDouble(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_EN_DEAL_PRICE)));
            row.EN_DEAL_BALANCE = this.GetDouble(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_EN_DEAL_BALANCE)));
            row.VC_REPORT_CODE = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_REPORT_CODE)));
            row.VC_STOCKHOLDER_ID = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_STOCKHOLDER_ID)));
            row.VC_REPORT_SEAT = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_REPORT_SEAT)));
            row.VC_OPERATOR_NAME = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_OPERATOR_NAME)));

            // 21-30
            row.VC_CURRENCY_NAME = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_CURRENCY_NAME)));
            row.C_INVEST_TYPE = this.GetChar(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_C_INVEST_TYPE)));
            row.VC_FUND_NAME = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_FUND_NAME)));
            row.VC_CURRENCY_NO_BB = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_CURRENCY_NO_BB)));
            row.EN_DEAL_BALANCE_BB = this.GetDouble(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_EN_DEAL_BALANCE_BB)));
            row.EN_DEAL_RATIO = this.GetDouble(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_EN_DEAL_RATIO)));
            row.VC_BROKER_NO = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_BROKER_NO)));
            row.VC_BROKER_NAME = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_BROKER_NAME)));
            row.VC_GROUPTYPE_NAME = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_GROUPTYPE_NAME)));
            row.VC_GROUP_NAME = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_GROUP_NAME)));


            // 31-41
            row.VC_INTERNATIONAL_CODE = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_INTERNATIONAL_CODE)));
            row.VC_RIC_CODE = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_RIC_CODE)));
            row.VC_SEDOL_CODE = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_SEDOL_CODE)));
            row.VC_CUSIP_CODE = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_CUSIP_CODE)));
            row.VC_BLOOMBERG_CODE = this.GetString(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_VC_BLOOMBERG_CODE)));
            row.L_REPORT_SERIAL_NO = this.GetLong(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_L_REPORT_SERIAL_NO)));
            row.C_MARKET_NO = this.GetChar(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_C_MARKET_NO)));
            row.EN_FULL_BALANCE = this.GetDouble(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_EN_FULL_BALANCE)));
            row.L_DIRECT_OPERATOR = this.GetInt(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_L_DIRECT_OPERATOR)));
            row.L_DIRECT_INSTEAD_OPERATOR = this.GetInt(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_L_DIRECT_INSTEAD_OPERATOR)));
            row.C_MARGINED_OUT = this.GetChar(reader.GetValue(reader.GetOrdinal(TradeInfo_O32.C_C_MARGINED_OUT)));

            return row;
        }

        public TradeRow GetTradeRow(DbDataReader reader)
        {
            return this.GetTradeRow(this.GetTradeInfo(reader));
        }

        protected TradeRow GetTradeRow(TradeInfo_O32 row)
        {
            TradeRow info = new TradeRow();

            // 02、成交日期：
            info.Trade_Date = row.L_DATE;
            // 03、成交ID：
            info.Trade_Id = row.VC_DEAL_NO;
            // 04、证券代码：
            info.Security_Id = this.ConvertSecurityId(row);
            // 05、方向标志位：可根据平仓类型转换；
            info.Direction = this.GetDirection(row);
            // 06、数量：
            info.Volume = this.GetDouble(row.L_DEAL_AMOUNT);
            // 07、合约乘数：
            info.Multiplier = 1.0;
            // 08、多头空头状态
            info.Side = this.GetSideState(row);
            // 09、价格：
            info.Price = row.EN_DEAL_PRICE;
            // 10、金额：
            info.Balance = row.EN_DEAL_BALANCE;
            // 11、成交类型：
            info.Trade_Type = GetTradeTypeByTradeTime(row.L_BUSINESS_TIME.ToString(), 2);
            // 12、基金公司/内部融券类型
            info.Source_Type = 0;
            // 13、基金代码/账户信息
            info.Source = "";
            // 14、成交额：
            info.Turn_Over = row.EN_DEAL_BALANCE;
            // 15、前置编号：
            info.Front_Id = 0;
            // 16、会话编号：
            info.Session_Id = 0;
            // 17、本地委托编号：
            info.Local_Order_Id = "";
            // 18、项目代码：
            info.Xmdm = 0;
            // 19、资产单元：
            info.Zcdy = "";
            // 20、组合代码：
            info.Zhdm = row.VC_COMBI_NO;
            // 21、策略号：
            info.Policy_Id = 0;
            // 22、委托日期：
            info.Order_Date = row.L_DATE;
            // 23、委托编号：
            info.Order_Id = row.L_ENTRUST_SERIAL_NO != 0 ? row.L_ENTRUST_SERIAL_NO.ToString() : string.Empty;
            // 24、交易通道：
            info.Trade_Route = "";
            // 25 、经纪商席位代码：
            info.Seat_Id = row.VC_REPORT_SEAT;
            // 26、投资者客户代码：
            info.Client_Id = row.VC_STOCKHOLDER_ID;
            // 27、系统成交日期：
            info.Sys_Trade_Date = "";
            // 28、系统成交结算编号：
            info.Sys_Trade_Settlement_Id = 0;
            // 29、系统成交编号：
            info.Sys_Trade_Id = "";
            // 30、系统委托日期：
            info.Sys_Order_Date = "";
            // 31、系统委托结算编号：
            info.Sys_Order_Settlement_Id = 0;
            // 32、系统委托编号：
            info.Sys_Order_Id = "";
            // 33、成交时间：
            info.Trade_Time = row.L_BUSINESS_TIME.ToString();

            return info;
        }

        #endregion

        #region DataConvert

        protected string ConvertSecurityId(TradeInfo_O32 row)
        {
            if (row.C_MARKET_NO == '1')
                return string.Format("{0}.SH", row.VC_REPORT_CODE);
            else if (row.C_MARKET_NO == '2')
                return string.Format("{0}.SZ", row.VC_REPORT_CODE);
            else if (row.C_MARKET_NO == '7')
                return string.Format("{0}.CFE", row.VC_REPORT_CODE);
            else
                return row.VC_REPORT_CODE;
        }

        protected int GetDirection(TradeInfo_O32 row)
        {
            switch (row.C_ENTRUST_DIRECTION)
            {
                case 'X':
                case 'V': 
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_FUT_OPEN);
                case 'Y':
                case 'W':
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_FUT_CLOSE);
                case 'P':   // ETF基金申购
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_ETF_SUBSCRIPTION);
                case 'Q':   // ETF基金赎回
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_ETF_REDUMPTION);
                case 'm':   // 基金分拆
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_FUND_SPLIT);
                case 'n':   // 基金合并
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_FUND_MERGE);
                case 'p':   // 开基申购
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_FUND_SUBSCRIPTION);
                case 'q':   // 开基赎回
                    return DirectionCoding.GetDirectionCode(TradeType.TRADE_FUND_REDUMPTION);
                default:
                    {
                        if (row.VC_REPORT_DIRECTION == "B" || row.VC_REPORT_DIRECTION == "0B")
                            return DirectionCoding.GetDirectionCode(TradeType.TRADE_BUY);
                        else if (row.VC_REPORT_DIRECTION == "S" || row.VC_REPORT_DIRECTION == "0S")
                            return DirectionCoding.GetDirectionCode(TradeType.TRADE_SELL);
                        else
                            return 0;
                    }
            }
        }

        /// <summary>
        /// 获取多头空头状态，0：表示多头，1：表示空头；
        /// </summary>
        private int GetSideState(TradeInfo_O32 row)
        {
            switch (row.C_ENTRUST_DIRECTION)
            {
                case 'X':   // 卖出开仓，看跌
                case 'Y':   // 买入平仓，看跌
                    return 1;
                case 'V':   // 买入开仓，看涨
                case 'W':   // 卖出平仓，看涨
                    return 0;
                default:
                    return 0;
            }
        }

        //internal static EEntrustType GetEntrustType(TradeInfo_O32 o)
        //{
        //    switch (o.C_ENTRUST_DIRECTION)
        //    {
        //        case 'X':
        //            return EEntrustType.卖出开仓;
        //        case 'Y':
        //            return EEntrustType.买入平仓;
        //        case 'V':
        //            return EEntrustType.买入开仓;
        //        case 'W':
        //            return EEntrustType.卖出平仓;
        //        default:
        //            if (o.VC_REPORT_DIRECTION == "B" || o.VC_REPORT_DIRECTION == "0B")
        //                return EEntrustType.买入;
        //            else
        //                return EEntrustType.卖出;
        //    }
        //}

        //public EEntrustType GetEntrustType(TradeInfo_O32 row)
        //{
        //    EEntrustType type = EEntrustType.无;
        //    switch (row.C_ENTRUST_DIRECTION)
        //    {
        //        case '0':
        //            type = EEntrustType.议案投票;
        //            break;
        //        case '1':
        //            type = EEntrustType.买入;
        //            break;
        //        case '2':
        //            type = EEntrustType.卖出;
        //            break;
        //        case '3':
        //            type = EEntrustType.债券买入;
        //            break;
        //        case '4':
        //            type = EEntrustType.债券卖出;
        //            break;
        //        case '5':
        //            type = EEntrustType.融资回购_折入;
        //            break;
        //        case '6':
        //            type = EEntrustType.融券回购_折出;
        //            break;
        //        case '7':
        //            type = EEntrustType.融资购回;
        //            break;
        //        case '8':
        //            type = EEntrustType.融券购回;
        //            break;
        //        case '9':
        //            type = EEntrustType.配股认购;
        //            break;
        //        case 'A':
        //            type = EEntrustType.转股;
        //            break;
        //        case 'B':
        //            type = EEntrustType.回售;
        //            break;
        //        case 'C':
        //            type = EEntrustType.申购;
        //            break;
        //        case 'D':
        //            type = EEntrustType.基金认购;
        //            break;
        //        case 'E':
        //            type = EEntrustType.基金转换;
        //            break;
        //        case 'F':
        //            type = EEntrustType.存款;
        //            break;
        //        case 'G':
        //            type = EEntrustType.转托管;
        //            break;
        //        case 'H':
        //            type = EEntrustType.指定交易;
        //            break;
        //        case 'I':
        //            type = EEntrustType.撤销指定;
        //            break;
        //        case 'J':
        //            type = EEntrustType.承销买入;
        //            break;
        //        case 'K':
        //            type = EEntrustType.分销买入;
        //            break;
        //        case 'L':
        //            type = EEntrustType.分销卖出;
        //            break;
        //        case 'M':
        //            break;
        //        case 'N':
        //            type = EEntrustType.债券投标;
        //            break;
        //        case 'O':
        //            type = EEntrustType.预受要约;
        //            break;
        //        case 'P':
        //            type = EEntrustType.ETF基金申购;
        //            break;
        //        case 'Q':
        //            type = EEntrustType.ETF基金赎回;
        //            break;
        //        case 'R':
        //            type = EEntrustType.行权认购;
        //            break;
        //        case 'S':
        //            type = EEntrustType.行权认沽;
        //            break;
        //        case 'T':
        //            type = EEntrustType.提交质押;
        //            break;
        //        case 'U':
        //            type = EEntrustType.转回质押;
        //            break;
        //        case 'V':
        //            type = EEntrustType.买入开仓;
        //            break;
        //        case 'W':
        //            type = EEntrustType.卖出平仓;
        //            break;
        //        case 'X':
        //            type = EEntrustType.卖出开仓;
        //            break;
        //        case 'Y':
        //            type = EEntrustType.买入平仓;
        //            break;
        //        case 'Z':
        //            type = EEntrustType.盈亏结转;
        //            break;
        //        case 'a':
        //            type = EEntrustType.项目投资;
        //            break;
        //        case 'b':
        //            type = EEntrustType.预受要约撤销;
        //            break;
        //        case 'c':
        //            type = EEntrustType.债券远期买入;
        //            break;
        //        case 'd':
        //            type = EEntrustType.债券远期卖出;
        //            break;
        //        case 'e':
        //            break;
        //        case 'f':
        //            break;
        //        case 'g':
        //            type = EEntrustType.跨市场转托管;
        //            break;
        //        case 'h':
        //            break;
        //        case 'i':
        //            type = EEntrustType.存款支取;
        //            break;
        //        case 'j':
        //            break;
        //        case 'k':
        //            break;
        //        case 'l':
        //            break;
        //        case 'm':
        //            type = EEntrustType.基金分拆;
        //            break;
        //        case 'n':
        //            type = EEntrustType.基金合并;
        //            break;
        //        case 'o':
        //            type = EEntrustType.基金冲账;
        //            break;
        //        case 'p':
        //            type = EEntrustType.开基申购;
        //            break;
        //        case 'q':
        //            type = EEntrustType.开基赎回;
        //            break;
        //        case 'r':
        //            type = EEntrustType.认购;
        //            break;
        //        case 's':
        //            type = EEntrustType.约定购回;
        //            break;
        //        case 't':
        //            break;
        //        case 'u':
        //            break;
        //        case 'v':
        //            break;
        //        case 'w':
        //            break;
        //        case 'x':
        //            break;
        //        case 'y':
        //            break;
        //        case 'z':
        //            type = EEntrustType.分红设置;
        //            break;
        //        case '(':
        //            type = EEntrustType.担保券交存;
        //            break;
        //        case ')':
        //            type = EEntrustType.担保券提取;
        //            break;
        //        case '{':
        //            type = EEntrustType.融资买入;
        //            break;
        //        case '}':
        //            type = EEntrustType.卖券还款;
        //            break;
        //        case '[':
        //            type = EEntrustType.融券卖出;
        //            break;
        //        case ']':
        //            type = EEntrustType.买券还券;
        //            break;
        //        case '!':
        //            type = EEntrustType.无;
        //            break;
        //        case '+':
        //            type = EEntrustType.债券融出到期;
        //            break;
        //        case '-':
        //            type = EEntrustType.债券融出;
        //            break;
        //        case '*':
        //            type = EEntrustType.债券融入到期;
        //            break;
        //        case '~':
        //            type = EEntrustType.直接还券;
        //            break;
        //        case '?':
        //            type = EEntrustType.债券融入;
        //            break;
        //        case '`':
        //            type = EEntrustType.直接还款;
        //            break;
        //        default:
        //            break;
        //    }

        //    return type;
        //}

        #endregion

        /// <summary>
        /// 获取当前表中的所有数据。
        /// </summary>
        internal List<TradeInfo_O32> GetTradeItems(EMarketType type)
        {
            List<TradeInfo_O32> datas = new List<TradeInfo_O32>();
            char mt = this.GetMarketType(type);

            //DateTime startTime = System.DateTime.Now;
            using (DbDataReader reader = this.GetTradeInfos())
            {
                while (reader.Read())
                {
                    TradeInfo_O32 row = this.GetTradeInfo(reader);

                    if (type == EMarketType.ALL)
                    {
                        datas.Add(row);
                    }
                    else
                    {
                        if (row.C_MARKET_NO == mt)
                            datas.Add(row);
                    }
                }
            }

            //TimeSpan span = DateTime.Now - startTime;
            //Trace.WriteLine("");
            //Trace.WriteLine("#### EMarketType = " + type.ToString());
            //Trace.WriteLine("#### 时间间隔    = " + span.ToString());
            //Trace.WriteLine("");

            return datas;
        }

        internal List<TradeInfo_O32> GetTradeItems(string secutityId, EMarketType market)
        {
            List<TradeInfo_O32> dataList = new List<TradeInfo_O32>();
            if (string.IsNullOrEmpty(secutityId))
                return dataList;

            List<TradeInfo_O32> datas = this.GetTradeItems(market);
            dataList = datas.Where(o => o.VC_REPORT_CODE == secutityId).ToList();

            return dataList;
        }

        private char GetMarketType(EMarketType type)
        {
            switch (type)
            {
                case EMarketType.SH:
                    return '1';
                case EMarketType.SZ:
                    return '2';
                case EMarketType.JJ:
                    return '7';
                default:
                    return '\0';
            }
        }
    }
}
