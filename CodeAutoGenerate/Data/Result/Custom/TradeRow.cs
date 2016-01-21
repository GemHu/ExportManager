using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.DBFFileMamager
{
    public class TradeRow
    {
        #region 数据库表字段常量

        /// <summary>
        /// 01、主键; 字段类型 = bigint auto_increment; 
        /// </summary>
        public const string C_idx = "idx";

        /// <summary>
        /// 02、成交日期; 字段类型 = char(8); 
        /// </summary>
        public const string C_trade_date = "trade_date";

        /// <summary>
        /// 03、成交ID; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_trade_id = "trade_id";

        /// <summary>
        /// 04、证券代码; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_security_id = "security_id";

        /// <summary>
        /// 05、方向(Direction)标志位(bitset); 字段类型 = int; 	0：买/卖；1：开/平；2-3：投机/套保/套利；4：是否强平；5：是否ETF申赎；6：挂单/抢单；7：是否冻结；8：是否转账；9：是否ETF现金差额；10：是否ETF现金替代；11：是否开放式基金申购赎回；12：是否分级基金拆分合并
        /// </summary>
        public const string C_direction = "direction";

        /// <summary>
        /// 06、数量; 字段类型 = double; 
        /// </summary>
        public const string C_volume = "volume";

        /// <summary>
        /// 07、合约乘数; 字段类型 = double; 
        /// </summary>
        public const string C_multiplier = "multiplier";

        /// <summary>
        /// 08、多头/空头; 字段类型 = int not null; 	0=long(多头); 1=short(空头)
        /// </summary>
        public const string C_side = "side";

        /// <summary>
        /// 09、成交价格; 字段类型 = double; 
        /// </summary>
        public const string C_price = "price";

        /// <summary>
        /// 10、成交金额; 字段类型 = double; 
        /// </summary>
        public const string C_balance = "balance";

        /// <summary>
        /// 11、成交类型; 字段类型 = int; 
        /// </summary>
        public const string C_trade_type = "trade_type";

        /// <summary>
        /// 12、基金公司/内部融券; 字段类型 = int; 	1=基金公司,2=内部融券
        /// </summary>
        public const string C_source_type = "source_type";

        /// <summary>
        /// 13、基金代码/账户信息; 字段类型 = varchar(200); 	source_type为1时填基金代码,2时填账户信息
        /// </summary>
        public const string C_source = "source";

        /// <summary>
        /// 14、成交额; 字段类型 = double; 
        /// </summary>
        public const string C_turn_over = "turn_over";

        /// <summary>
        /// 15、前置编号; 字段类型 = int; 
        /// </summary>
        public const string C_front_id = "front_id";

        /// <summary>
        /// 16、会话编号; 字段类型 = bigint; 
        /// </summary>
        public const string C_session_id = "session_id";

        /// <summary>
        /// 17、本地委托编号; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_local_order_id = "local_order_id";

        /// <summary>
        /// 18、项目代码; 字段类型 = int not null; 
        /// </summary>
        public const string C_xmdm = "xmdm";

        /// <summary>
        /// 19、资产单元; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_zcdy = "zcdy";

        /// <summary>
        /// 20、组合代码; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_zhdm = "zhdm";

        /// <summary>
        /// 21、策略号; 字段类型 = int; 
        /// </summary>
        public const string C_policy_id = "policy_id";

        /// <summary>
        /// 22、委托日期; 字段类型 = char(8); 
        /// </summary>
        public const string C_order_date = "order_date";

        /// <summary>
        /// 23、委托编号; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_order_id = "order_id";

        /// <summary>
        /// 24、交易通道; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_trade_route = "trade_route";

        /// <summary>
        /// 25、经纪商席位代码; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_seat_id = "seat_id";

        /// <summary>
        /// 26、投资者客户代码; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_client_id = "client_id";

        /// <summary>
        /// 27、系统成交日期; 字段类型 = char(8); 
        /// </summary>
        public const string C_sys_trade_date = "sys_trade_date";

        /// <summary>
        /// 28、系统成交结算编号; 字段类型 = int; 
        /// </summary>
        public const string C_sys_trade_settlement_id = "sys_trade_settlement_id";

        /// <summary>
        /// 29、系统成交编号; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_sys_trade_id = "sys_trade_id";

        /// <summary>
        /// 30、系统成交日期; 字段类型 = char(8); 
        /// </summary>
        public const string C_sys_order_date = "sys_order_date";

        /// <summary>
        /// 31、系统委托结算编号; 字段类型 = int; 
        /// </summary>
        public const string C_sys_order_settlement_id = "sys_order_settlement_id";

        /// <summary>
        /// 32、系统委托编号; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_sys_order_id = "sys_order_id";

        /// <summary>
        /// 33、成交时间; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_trade_time = "trade_time";

        #endregion

        #region 数据库表字段对应属性

        /// <summary>
        /// 01、主键; ; 字段类型 = bigint auto_increment
        /// </summary>
        public long Idx { get; set; }

        /// <summary>
        /// 02、成交日期; ; 字段类型 = char(8)
        /// </summary>
        public string Trade_Date { get; set; }

        /// <summary>
        /// 03、成交ID; ; 字段类型 = varchar(50)
        /// </summary>
        public string Trade_Id { get; set; }

        /// <summary>
        /// 04、证券代码; ; 字段类型 = varchar(20)
        /// </summary>
        public string Security_Id { get; set; }

        /// <summary>
        /// 05、方向(Direction)标志位(bitset); 	0：买/卖；1：开/平；2-3：投机/套保/套利；4：是否强平；5：是否ETF申赎；6：挂单/抢单；7：是否冻结；8：是否转账；9：是否ETF现金差额；10：是否ETF现金替代；11：是否开放式基金申购赎回；12：是否分级基金拆分合并; 字段类型 = int
        /// </summary>
        public int Direction { get; set; }

        /// <summary>
        /// 06、数量; ; 字段类型 = double
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// 07、合约乘数; ; 字段类型 = double
        /// </summary>
        public double Multiplier { get; set; }

        /// <summary>
        /// 08、多头/空头; 	0=long(多头); 1=short(空头); 字段类型 = int not null
        /// </summary>
        public int Side { get; set; }

        /// <summary>
        /// 09、成交价格; ; 字段类型 = double
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 10、成交金额; ; 字段类型 = double
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// 11、成交类型; ; 字段类型 = int
        /// </summary>
        public int Trade_Type { get; set; }

        /// <summary>
        /// 12、基金公司/内部融券; 	1=基金公司,2=内部融券; 字段类型 = int
        /// </summary>
        public int Source_Type { get; set; }

        /// <summary>
        /// 13、基金代码/账户信息; 	source_type为1时填基金代码,2时填账户信息; 字段类型 = varchar(200)
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 14、成交额; ; 字段类型 = double
        /// </summary>
        public double Turn_Over { get; set; }

        /// <summary>
        /// 15、前置编号; ; 字段类型 = int
        /// </summary>
        public int Front_Id { get; set; }

        /// <summary>
        /// 16、会话编号; ; 字段类型 = bigint
        /// </summary>
        public long Session_Id { get; set; }

        /// <summary>
        /// 17、本地委托编号; ; 字段类型 = varchar(50)
        /// </summary>
        public string Local_Order_Id { get; set; }

        /// <summary>
        /// 18、项目代码; ; 字段类型 = int not null
        /// </summary>
        public int Xmdm { get; set; }

        /// <summary>
        /// 19、资产单元; ; 字段类型 = varchar(20)
        /// </summary>
        public string Zcdy { get; set; }

        /// <summary>
        /// 20、组合代码; ; 字段类型 = varchar(20)
        /// </summary>
        public string Zhdm { get; set; }

        /// <summary>
        /// 21、策略号; ; 字段类型 = int
        /// </summary>
        public int Policy_Id { get; set; }

        /// <summary>
        /// 22、委托日期; ; 字段类型 = char(8)
        /// </summary>
        public string Order_Date { get; set; }

        /// <summary>
        /// 23、委托编号; ; 字段类型 = varchar(50)
        /// </summary>
        public string Order_Id { get; set; }

        /// <summary>
        /// 24、交易通道; ; 字段类型 = varchar(50)
        /// </summary>
        public string Trade_Route { get; set; }

        /// <summary>
        /// 25、经纪商席位代码; ; 字段类型 = varchar(50)
        /// </summary>
        public string Seat_Id { get; set; }

        /// <summary>
        /// 26、投资者客户代码; ; 字段类型 = varchar(50)
        /// </summary>
        public string Client_Id { get; set; }

        /// <summary>
        /// 27、系统成交日期; ; 字段类型 = char(8)
        /// </summary>
        public string Sys_Trade_Date { get; set; }

        /// <summary>
        /// 28、系统成交结算编号; ; 字段类型 = int
        /// </summary>
        public int Sys_Trade_Settlement_Id { get; set; }

        /// <summary>
        /// 29、系统成交编号; ; 字段类型 = varchar(50)
        /// </summary>
        public string Sys_Trade_Id { get; set; }

        /// <summary>
        /// 30、系统成交日期; ; 字段类型 = char(8)
        /// </summary>
        public string Sys_Order_Date { get; set; }

        /// <summary>
        /// 31、系统委托结算编号; ; 字段类型 = int
        /// </summary>
        public int Sys_Order_Settlement_Id { get; set; }

        /// <summary>
        /// 32、系统委托编号; ; 字段类型 = varchar(50)
        /// </summary>
        public string Sys_Order_Id { get; set; }

        /// <summary>
        /// 33、成交时间; ; 字段类型 = varchar(50)
        /// </summary>
        public string Trade_Time { get; set; }

        #endregion

    }
}
