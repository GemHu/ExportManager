using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.Common;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public interface IExport2MarketTable : IExport
    {
        MarketRow GetMarketRow(DbDataReader reader);
    }

    public class MarketTable : SettleDBHelper
    {
        public const string TableName = "tb_market";
        
        #region Life Cycle

        public MarketTable()
        {
        }

        #endregion

        #region CURD

        public bool Add(MarketRow row)
        {
            string sql = string.Format("insert into {0}({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}) VALUES(@{1}, @{2}, @{3}, @{4}, @{5}, @{6}, @{7}, @{8}, @{9}, @{10}, @{11}, @{12}, @{13}, @{14}, @{15}, @{16}, @{17}, @{18}, @{19}, @{20}, @{21}, @{22}, @{23}, @{24}, @{25}, @{26}, @{27}, @{28}, @{29}, @{30}, @{31});", TableName,
                MarketRow.C_trade_date,
                MarketRow.C_security_id,
                MarketRow.C_last_price, 
                MarketRow.C_change_rate, 
                MarketRow.C_volume,
                MarketRow.C_trade_count,
                MarketRow.C_turn_over,
                MarketRow.C_pre_close_price, 
                MarketRow.C_pre_open_interest, 
                MarketRow.C_pre_settlement_price, 
                MarketRow.C_open_price, 
                MarketRow.C_highest_price, 
                MarketRow.C_lowerst_price, 
                MarketRow.C_close_price, 
                MarketRow.C_open_interest, 
                MarketRow.C_settlement_price, 
                MarketRow.C_up_limit_price, 
                MarketRow.C_lower_limit_price, 
                MarketRow.C_currency,
                MarketRow.C_epsilon,
                MarketRow.C_multiplier, 
                MarketRow.C_rule,
                MarketRow.C_ipov,
                MarketRow.C_ytm, 
                MarketRow.C_syl1, 
                MarketRow.C_syl2, 
                MarketRow.C_pre_delta, 
                MarketRow.C_curr_delta, 
                MarketRow.C_avg_price, 
                MarketRow.C_sys_update_time, 
                MarketRow.C_update_time);
            
            MySqlParameter[] parameters = new MySqlParameter[]{
                new MySqlParameter(string.Format("@{0}", MarketRow.C_trade_date), row.Trade_Date), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_security_id), row.Security_Id), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_last_price), row.Last_Price), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_change_rate), row.Change_Rate), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_volume), row.Volume), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_trade_count), row.Trade_Count), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_turn_over), row.Turn_Over), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_pre_close_price), row.Pre_Close_Price), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_pre_open_interest), row.Pre_Open_Interest), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_pre_settlement_price), row.Pre_Settlement_Price), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_open_price), row.Open_Price), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_highest_price), row.Highest_Price), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_lowerst_price), row.Lowerst_Price), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_close_price), row.Close_Price), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_open_interest), row.Open_Interest), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_settlement_price), row.Settlement_Price), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_up_limit_price), row.Up_Limit_Price), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_lower_limit_price), row.Lower_Limit_Price), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_currency), row.Currency), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_epsilon), row.Epsilon), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_multiplier), row.Multiplier), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_rule), row.Rule), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_ipov), row.Ipov), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_ytm), row.Ytm), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_syl1), row.Syl1), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_syl2), row.Syl2), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_pre_delta), row.Pre_Delta), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_curr_delta), row.Curr_Delta), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_avg_price), row.Avg_Price), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_sys_update_time), row.Sys_Update_Time), 
                new MySqlParameter(string.Format("@{0}", MarketRow.C_update_time), row.Update_Time)
            };

            return this.ExecuteNonQuery(sql, parameters) > 0;
        }

        /// <summary>
        /// 清除指定日期的数据；
        /// </summary>
        /// <param name="date">操作的目标日期，日期格式为：yyyyMMdd,eg：20151225</param>
        public int RemoveByDate(string date)
        {
            string sql = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TableName, MarketRow.C_trade_date, date);

            return this.ExecuteNonQuery(sql);
        }

        #endregion

    }

    public class MarketRow
    {
        #region 数据库表字段常量

        /// <summary>
        /// 01、主键; 字段类型 = bigint auto_increment; 
        /// </summary>
        public const string C_idx = "idx";

        /// <summary>
        /// 02、交易日; 字段类型 = char(8); 
        /// </summary>
        public const string C_trade_date = "trade_date";

        /// <summary>
        /// 03、证券代码; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_security_id = "security_id";

        /// <summary>
        /// 04、最新价; 字段类型 = double; 
        /// </summary>
        public const string C_last_price = "last_price";

        /// <summary>
        /// 05、涨跌; 字段类型 = double; 
        /// </summary>
        public const string C_change_rate = "change_rate";

        /// <summary>
        /// 06、成交量; 字段类型 = double; 
        /// </summary>
        public const string C_volume = "volume";

        /// <summary>
        /// 07、成交笔数; 字段类型 = double; 
        /// </summary>
        public const string C_trade_count = "trade_count";

        /// <summary>
        /// 08、成交额; 字段类型 = double; 
        /// </summary>
        public const string C_turn_over = "turn_over";

        /// <summary>
        /// 09、前收盘价; 字段类型 = double; 
        /// </summary>
        public const string C_pre_close_price = "pre_close_price";

        /// <summary>
        /// 10、前持仓量; 字段类型 = double; 
        /// </summary>
        public const string C_pre_open_interest = "pre_open_interest";

        /// <summary>
        /// 11、前结算价; 字段类型 = double; 
        /// </summary>
        public const string C_pre_settlement_price = "pre_settlement_price";

        /// <summary>
        /// 12、开盘价; 字段类型 = double; 
        /// </summary>
        public const string C_open_price = "open_price";

        /// <summary>
        /// 13、最高价; 字段类型 = double; 
        /// </summary>
        public const string C_highest_price = "highest_price";

        /// <summary>
        /// 14、最低价; 字段类型 = double; 
        /// </summary>
        public const string C_lowerst_price = "lowerst_price";

        /// <summary>
        /// 15、收盘价; 字段类型 = double; 
        /// </summary>
        public const string C_close_price = "close_price";

        /// <summary>
        /// 16、持仓量; 字段类型 = double; 
        /// </summary>
        public const string C_open_interest = "open_interest";

        /// <summary>
        /// 17、结算价; 字段类型 = double; 
        /// </summary>
        public const string C_settlement_price = "settlement_price";

        /// <summary>
        /// 18、涨停价; 字段类型 = double; 
        /// </summary>
        public const string C_up_limit_price = "up_limit_price";

        /// <summary>
        /// 19、跌停价; 字段类型 = double; 
        /// </summary>
        public const string C_lower_limit_price = "lower_limit_price";

        /// <summary>
        /// 20、货币; 字段类型 = varchar(10); 	CNY=人民币; USD=美元; EUR=欧元; HKD=港元
        /// </summary>
        public const string C_currency = "currency";

        /// <summary>
        /// 21、最小变价; 字段类型 = double; 
        /// </summary>
        public const string C_epsilon = "epsilon";

        /// <summary>
        /// 22、合约乘数; 字段类型 = double; 
        /// </summary>
        public const string C_multiplier = "multiplier";

        /// <summary>
        /// 23、交易规则; 字段类型 = int; 	1=做市商交易; 2=竞价交易(两价取先); 3=竞价交易(三价取中); 4=协议交易
        /// </summary>
        public const string C_rule = "rule";

        /// <summary>
        /// 24、基金份额参考净值; 字段类型 = double; 
        /// </summary>
        public const string C_ipov = "ipov";

        /// <summary>
        /// 25、到期收益率; 字段类型 = double; 
        /// </summary>
        public const string C_ytm = "ytm";

        /// <summary>
        /// 26、市盈率1; 字段类型 = double; 
        /// </summary>
        public const string C_syl1 = "syl1";

        /// <summary>
        /// 27、市盈率2; 字段类型 = double; 
        /// </summary>
        public const string C_syl2 = "syl2";

        /// <summary>
        /// 28、前虚实度; 字段类型 = double; 
        /// </summary>
        public const string C_pre_delta = "pre_delta";

        /// <summary>
        /// 29、虚实度; 字段类型 = double; 
        /// </summary>
        public const string C_curr_delta = "curr_delta";

        /// <summary>
        /// 30、平均价; 字段类型 = double; 
        /// </summary>
        public const string C_avg_price = "avg_price";

        /// <summary>
        /// 31、系统更新时间; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_sys_update_time = "sys_update_time";

        /// <summary>
        /// 32、更新时间; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_update_time = "update_time";

        #endregion

        #region 数据库表字段对应属性

        /// <summary>
        /// 01、主键; ; 字段类型 = bigint auto_increment
        /// </summary>
        public long Idx { get; set; }

        /// <summary>
        /// 02、交易日; ; 字段类型 = char(8)
        /// </summary>
        public string Trade_Date { get; set; }

        /// <summary>
        /// 03、证券代码; ; 字段类型 = varchar(20)
        /// </summary>
        public string Security_Id { get; set; }

        /// <summary>
        /// 04、最新价; ; 字段类型 = double
        /// </summary>
        public double Last_Price { get; set; }

        /// <summary>
        /// 05、涨跌; ; 字段类型 = double
        /// </summary>
        public double Change_Rate { get; set; }

        /// <summary>
        /// 06、成交量; ; 字段类型 = double
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// 07、成交笔数; ; 字段类型 = double
        /// </summary>
        public double Trade_Count { get; set; }

        /// <summary>
        /// 08、成交额; ; 字段类型 = double
        /// </summary>
        public double Turn_Over { get; set; }

        /// <summary>
        /// 09、前收盘价; ; 字段类型 = double
        /// </summary>
        public double Pre_Close_Price { get; set; }

        /// <summary>
        /// 10、前持仓量; ; 字段类型 = double
        /// </summary>
        public double Pre_Open_Interest { get; set; }

        /// <summary>
        /// 11、前结算价; ; 字段类型 = double
        /// </summary>
        public double Pre_Settlement_Price { get; set; }

        /// <summary>
        /// 12、开盘价; ; 字段类型 = double
        /// </summary>
        public double Open_Price { get; set; }

        /// <summary>
        /// 13、最高价; ; 字段类型 = double
        /// </summary>
        public double Highest_Price { get; set; }

        /// <summary>
        /// 14、最低价; ; 字段类型 = double
        /// </summary>
        public double Lowerst_Price { get; set; }

        /// <summary>
        /// 15、收盘价; ; 字段类型 = double
        /// </summary>
        public double Close_Price { get; set; }

        /// <summary>
        /// 16、持仓量; ; 字段类型 = double
        /// </summary>
        public double Open_Interest { get; set; }

        /// <summary>
        /// 17、结算价; ; 字段类型 = double
        /// </summary>
        public double Settlement_Price { get; set; }

        /// <summary>
        /// 18、涨停价; ; 字段类型 = double
        /// </summary>
        public double Up_Limit_Price { get; set; }

        /// <summary>
        /// 19、跌停价; ; 字段类型 = double
        /// </summary>
        public double Lower_Limit_Price { get; set; }

        /// <summary>
        /// 20、货币; 	CNY=人民币; USD=美元; EUR=欧元; HKD=港元; 字段类型 = varchar(10)
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 21、最小变价; ; 字段类型 = double
        /// </summary>
        public double Epsilon { get; set; }

        /// <summary>
        /// 22、合约乘数; ; 字段类型 = double
        /// </summary>
        public double Multiplier { get; set; }

        /// <summary>
        /// 23、交易规则; 	1=做市商交易; 2=竞价交易(两价取先); 3=竞价交易(三价取中); 4=协议交易; 字段类型 = int
        /// </summary>
        public int Rule { get; set; }

        /// <summary>
        /// 24、基金份额参考净值; ; 字段类型 = double
        /// </summary>
        public double Ipov { get; set; }

        /// <summary>
        /// 25、到期收益率; ; 字段类型 = double
        /// </summary>
        public double Ytm { get; set; }

        /// <summary>
        /// 26、市盈率1; ; 字段类型 = double
        /// </summary>
        public double Syl1 { get; set; }

        /// <summary>
        /// 27、市盈率2; ; 字段类型 = double
        /// </summary>
        public double Syl2 { get; set; }

        /// <summary>
        /// 28、前虚实度; ; 字段类型 = double
        /// </summary>
        public double Pre_Delta { get; set; }

        /// <summary>
        /// 29、虚实度; ; 字段类型 = double
        /// </summary>
        public double Curr_Delta { get; set; }

        /// <summary>
        /// 30、平均价; ; 字段类型 = double
        /// </summary>
        public double Avg_Price { get; set; }

        /// <summary>
        /// 31、系统更新时间; ; 字段类型 = varchar(50)
        /// </summary>
        public string Sys_Update_Time { get; set; }

        /// <summary>
        /// 32、更新时间; ; 字段类型 = varchar(50)
        /// </summary>
        public string Update_Time { get; set; }

        #endregion

    }

}
