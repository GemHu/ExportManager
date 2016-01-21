using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.DBFFileMamager
{
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