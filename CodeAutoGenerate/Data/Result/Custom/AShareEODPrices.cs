using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.DBFFileMamager
{
    public class AShareEODPrices
    {
        #region 数据库表字段常量

        /// <summary>
        /// 01、对象ID; 字段类型 = varchar(38); 
        /// </summary>
        public const string C_OBJECT_ID = "OBJECT_ID";

        /// <summary>
        /// 02、Wind代码; 字段类型 = varchar(40); 
        /// </summary>
        public const string C_S_INFO_WINDCODE = "S_INFO_WINDCODE";

        /// <summary>
        /// 03、交易日期; 字段类型 = varchar(8); 
        /// </summary>
        public const string C_TRADE_DT = "TRADE_DT";

        /// <summary>
        /// 04、货币代码; 字段类型 = varchar(10); 
        /// </summary>
        public const string C_CRNCY_CODE = "CRNCY_CODE";

        /// <summary>
        /// 05、昨收盘价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_PRECLOSE = "S_DQ_PRECLOSE";

        /// <summary>
        /// 06、开盘价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_OPEN = "S_DQ_OPEN";

        /// <summary>
        /// 07、最高价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_HIGH = "S_DQ_HIGH";

        /// <summary>
        /// 08、最低价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_LOW = "S_DQ_LOW";

        /// <summary>
        /// 09、收盘价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_CLOSE = "S_DQ_CLOSE";

        /// <summary>
        /// 10、涨跌(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_CHANGE = "S_DQ_CHANGE";

        /// <summary>
        /// 11、涨跌幅(%); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_PCTCHANGE = "S_DQ_PCTCHANGE";

        /// <summary>
        /// 12、成交量(手); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_VOLUME = "S_DQ_VOLUME";

        /// <summary>
        /// 13、成交金额(千元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_AMOUNT = "S_DQ_AMOUNT";

        /// <summary>
        /// 14、复权昨收盘价(元); 字段类型 = numeric(20,4); 	昨收盘价*复权因子
        /// </summary>
        public const string C_S_DQ_ADJPRECLOSE = "S_DQ_ADJPRECLOSE";

        /// <summary>
        /// 15、复权开盘价(元); 字段类型 = numeric(20,4); 	开盘价*复权因子
        /// </summary>
        public const string C_S_DQ_ADJOPEN = "S_DQ_ADJOPEN";

        /// <summary>
        /// 16、复权最高价(元); 字段类型 = numeric(20,4); 	最高价*复权因子
        /// </summary>
        public const string C_S_DQ_ADJHIGH = "S_DQ_ADJHIGH";

        /// <summary>
        /// 17、复权最低价(元); 字段类型 = numeric(20,4); 	最低价*复权因子
        /// </summary>
        public const string C_S_DQ_ADJLOW = "S_DQ_ADJLOW";

        /// <summary>
        /// 18、复权收盘价(元); 字段类型 = numeric(20,4); 	收盘价*复权因子
        /// </summary>
        public const string C_S_DQ_ADJCLOSE = "S_DQ_ADJCLOSE";

        /// <summary>
        /// 19、复权因子; 字段类型 = numeric(20,6); 	初始值为1；当日复权因子=前一交易日收盘价/当日昨收盘价*前一交易日复权因子。
        /// </summary>
        public const string C_S_DQ_ADJFACTOR = "S_DQ_ADJFACTOR";

        /// <summary>
        /// 20、均价(VWAP); 字段类型 = numeric(20,4); 	成交金额/成交量
        /// </summary>
        public const string C_S_DQ_AVGPRICE = "S_DQ_AVGPRICE";

        /// <summary>
        /// 21、交易状态; 字段类型 = varchar(10); 
        /// </summary>
        public const string C_S_DQ_TRADESTATUS = "S_DQ_TRADESTATUS";

        /// <summary>
        /// 22、操作时间; 字段类型 = datetime; 
        /// </summary>
        public const string C_OPDATE = "OPDATE";

        /// <summary>
        /// 23、操作模式; 字段类型 = varchar(1); 	0（新记录）、1（更新/更正）、2（删除），描述对数据库需要进行的操作。对于0和1，记录内容即为最新值；对于2，所有字段内容为空。
        /// </summary>
        public const string C_OPMODE = "OPMODE";

        #endregion

        #region 数据库表字段对应属性

        /// <summary>
        /// 01、对象ID; ; 字段类型 = varchar(38)
        /// </summary>
        public string OBJECT_ID { get; set; }

        /// <summary>
        /// 02、Wind代码; ; 字段类型 = varchar(40)
        /// </summary>
        public string S_INFO_WINDCODE { get; set; }

        /// <summary>
        /// 03、交易日期; ; 字段类型 = varchar(8)
        /// </summary>
        public string TRADE_DT { get; set; }

        /// <summary>
        /// 04、货币代码; ; 字段类型 = varchar(10)
        /// </summary>
        public string CRNCY_CODE { get; set; }

        /// <summary>
        /// 05、昨收盘价(元); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_PRECLOSE { get; set; }

        /// <summary>
        /// 06、开盘价(元); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_OPEN { get; set; }

        /// <summary>
        /// 07、最高价(元); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_HIGH { get; set; }

        /// <summary>
        /// 08、最低价(元); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_LOW { get; set; }

        /// <summary>
        /// 09、收盘价(元); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_CLOSE { get; set; }

        /// <summary>
        /// 10、涨跌(元); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_CHANGE { get; set; }

        /// <summary>
        /// 11、涨跌幅(%); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_PCTCHANGE { get; set; }

        /// <summary>
        /// 12、成交量(手); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_VOLUME { get; set; }

        /// <summary>
        /// 13、成交金额(千元); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_AMOUNT { get; set; }

        /// <summary>
        /// 14、复权昨收盘价(元); 	昨收盘价*复权因子; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_ADJPRECLOSE { get; set; }

        /// <summary>
        /// 15、复权开盘价(元); 	开盘价*复权因子; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_ADJOPEN { get; set; }

        /// <summary>
        /// 16、复权最高价(元); 	最高价*复权因子; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_ADJHIGH { get; set; }

        /// <summary>
        /// 17、复权最低价(元); 	最低价*复权因子; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_ADJLOW { get; set; }

        /// <summary>
        /// 18、复权收盘价(元); 	收盘价*复权因子; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_ADJCLOSE { get; set; }

        /// <summary>
        /// 19、复权因子; 	初始值为1；当日复权因子=前一交易日收盘价/当日昨收盘价*前一交易日复权因子。; 字段类型 = numeric(20,6)
        /// </summary>
        public double S_DQ_ADJFACTOR { get; set; }

        /// <summary>
        /// 20、均价(VWAP); 	成交金额/成交量; 字段类型 = numeric(20,4)
        /// </summary>
        public double S_DQ_AVGPRICE { get; set; }

        /// <summary>
        /// 21、交易状态; ; 字段类型 = varchar(10)
        /// </summary>
        public string S_DQ_TRADESTATUS { get; set; }

        /// <summary>
        /// 22、操作时间; ; 字段类型 = datetime
        /// </summary>
        public DateTime? OPDATE { get; set; }

        /// <summary>
        /// 23、操作模式; 	0（新记录）、1（更新/更正）、2（删除），描述对数据库需要进行的操作。对于0和1，记录内容即为最新值；对于2，所有字段内容为空。; 字段类型 = varchar(1)
        /// </summary>
        public string OPMODE { get; set; }

        #endregion

    }
}
