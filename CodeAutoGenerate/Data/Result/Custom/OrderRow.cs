using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.DBFFileMamager
{
    public class OrderRow
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
        /// 03、委托编号; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_order_id = "order_id";

        /// <summary>
        /// 04、证券代码; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_security_id = "security_id";

        /// <summary>
        /// 05、方向，是一个bitset; 字段类型 = int; 	0：买/卖；1：开/平；2-3：投机/套保/套利；4：是否强平；5：是否ETF申赎；6：挂单/抢单；7：是否冻结；8：是否转账；9：是否ETF现金差额；10：是否ETF现金替代；11：是否开放式基金申购赎回；12：是否分级基金拆分合并；
        /// </summary>
        public const string C_direction = "direction";

        /// <summary>
        /// 06、委托数量; 字段类型 = double; 
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
        /// 09、委托价格; 字段类型 = double; 
        /// </summary>
        public const string C_price = "price";

        /// <summary>
        /// 10、委托类型; 字段类型 = int; 
        /// </summary>
        public const string C_order_type = "order_type";

        /// <summary>
        /// 11、委托状态; 字段类型 = int; 
        /// </summary>
        public const string C_order_status = "order_status";

        /// <summary>
        /// 12、委托状态信息; 字段类型 = varchar(200); 
        /// </summary>
        public const string C_order_status_msg = "order_status_msg";

        /// <summary>
        /// 13、原始数量; 字段类型 = double; 
        /// </summary>
        public const string C_original_volume = "original_volume";

        /// <summary>
        /// 14、已成交额; 字段类型 = double; 
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
        /// 17、本地委托ID; 字段类型 = varchar(50); 
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
        /// 21、策略ID; 字段类型 = int; 
        /// </summary>
        public const string C_policy_id = "policy_id";

        /// <summary>
        /// 22、交易通道; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_trading_route = "trading_route";

        /// <summary>
        /// 23、经纪商交易席位代码; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_seat_id = "seat_id";

        /// <summary>
        /// 24、投资者客户代码; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_client_id = "client_id";

        /// <summary>
        /// 25、系统委托日期; 字段类型 = char(8); 
        /// </summary>
        public const string C_sys_order_date = "sys_order_date";

        /// <summary>
        /// 26、系统委托结算id; 字段类型 = int; 
        /// </summary>
        public const string C_sys_order_settlement_id = "sys_order_settlement_id";

        /// <summary>
        /// 27、系统委托编号; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_sys_order_id = "sys_order_id";

        /// <summary>
        /// 28、创建用户代码; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_create_user_id = "create_user_id";

        /// <summary>
        /// 29、创建时间; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_create_time = "create_time";

        /// <summary>
        /// 30、更新用户代码; 字段类型 = varchar(50); 
        /// </summary>
        public const string C_update_user_id = "update_user_id";

        /// <summary>
        /// 31、更新时间; 字段类型 = varchar(50); 
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
        /// 03、委托编号; ; 字段类型 = varchar(50)
        /// </summary>
        public string Order_Id { get; set; }

        /// <summary>
        /// 04、证券代码; ; 字段类型 = varchar(20)
        /// </summary>
        public string Security_Id { get; set; }

        /// <summary>
        /// 05、方向，是一个bitset; 	0：买/卖；1：开/平；2-3：投机/套保/套利；4：是否强平；5：是否ETF申赎；6：挂单/抢单；7：是否冻结；8：是否转账；9：是否ETF现金差额；10：是否ETF现金替代；11：是否开放式基金申购赎回；12：是否分级基金拆分合并；; 字段类型 = int
        /// </summary>
        public int Direction { get; set; }

        /// <summary>
        /// 06、委托数量; ; 字段类型 = double
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
        /// 09、委托价格; ; 字段类型 = double
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 10、委托类型; ; 字段类型 = int
        /// </summary>
        public int Order_Type { get; set; }

        /// <summary>
        /// 11、委托状态; ; 字段类型 = int
        /// </summary>
        public int Order_Status { get; set; }

        /// <summary>
        /// 12、委托状态信息; ; 字段类型 = varchar(200)
        /// </summary>
        public string Order_Status_Msg { get; set; }

        /// <summary>
        /// 13、原始数量; ; 字段类型 = double
        /// </summary>
        public double Original_Volume { get; set; }

        /// <summary>
        /// 14、已成交额; ; 字段类型 = double
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
        /// 17、本地委托ID; ; 字段类型 = varchar(50)
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
        /// 21、策略ID; ; 字段类型 = int
        /// </summary>
        public int Policy_Id { get; set; }

        /// <summary>
        /// 22、交易通道; ; 字段类型 = varchar(50)
        /// </summary>
        public string Trading_Route { get; set; }

        /// <summary>
        /// 23、经纪商交易席位代码; ; 字段类型 = varchar(50)
        /// </summary>
        public string Seat_Id { get; set; }

        /// <summary>
        /// 24、投资者客户代码; ; 字段类型 = varchar(50)
        /// </summary>
        public string Client_Id { get; set; }

        /// <summary>
        /// 25、系统委托日期; ; 字段类型 = char(8)
        /// </summary>
        public string Sys_Order_Date { get; set; }

        /// <summary>
        /// 26、系统委托结算id; ; 字段类型 = int
        /// </summary>
        public int Sys_Order_Settlement_Id { get; set; }

        /// <summary>
        /// 27、系统委托编号; ; 字段类型 = varchar(50)
        /// </summary>
        public string Sys_Order_Id { get; set; }

        /// <summary>
        /// 28、创建用户代码; ; 字段类型 = varchar(50)
        /// </summary>
        public string Create_User_Id { get; set; }

        /// <summary>
        /// 29、创建时间; ; 字段类型 = varchar(50)
        /// </summary>
        public string Create_Time { get; set; }

        /// <summary>
        /// 30、更新用户代码; ; 字段类型 = varchar(50)
        /// </summary>
        public string Update_User_Id { get; set; }

        /// <summary>
        /// 31、更新时间; ; 字段类型 = varchar(50)
        /// </summary>
        public string Update_Time { get; set; }

        #endregion

    }
}
