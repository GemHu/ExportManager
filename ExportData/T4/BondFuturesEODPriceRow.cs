using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.ExportData
{
	/// <summary>
    /// 中国国债期货仿真交易日行情。
    /// </summary>
 	public class BondFuturesEODPriceRow
	{
		#region 数据库表字段常量

		/// <summary>
        /// 01、对象ID; 字段类型 = varchar(100); 
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
        /// 04、前结算价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_PRESETTLE = "S_DQ_PRESETTLE";

		/// <summary>
        /// 05、开盘价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_OPEN = "S_DQ_OPEN";

		/// <summary>
        /// 06、最高价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_HIGH = "S_DQ_HIGH";

		/// <summary>
        /// 07、最低价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_LOW = "S_DQ_LOW";

		/// <summary>
        /// 08、收盘价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_CLOSE = "S_DQ_CLOSE";

		/// <summary>
        /// 09、结算价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_SETTLE = "S_DQ_SETTLE";

		/// <summary>
        /// 10、成交量(手); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_VOLUME = "S_DQ_VOLUME";

		/// <summary>
        /// 11、成交金额(万元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_AMOUNT = "S_DQ_AMOUNT";

		/// <summary>
        /// 12、持仓量(手); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_S_DQ_OI = "S_DQ_OI";

		/// <summary>
        /// 13、涨跌(元); 字段类型 = numeric(20,4); 收盘价-前结算价;
        /// </summary>
        public const string C_S_DQ_CHANGE = "S_DQ_CHANGE";

		/// <summary>
        /// 14、操作时间; 字段类型 = datetime; 
        /// </summary>
        public const string C_OPDATE = "OPDATE";

		/// <summary>
        /// 15、操作模式; 字段类型 = varchar(1); 0（新记录）、1（更新/更正）、2（删除），描述对数据库需要进行的操作。对于0和1，记录内容即为最新值；对于2，所有字段内容为空。;
        /// </summary>
        public const string C_OPMODE = "OPMODE";

		#endregion

		#region 数据库表字段对应属性
		
		/// <summary>
        /// 01、对象ID; 字段类型 = varchar(100); 
        /// </summary>
        public string OBJECT_ID { get; set; }
		/// <summary>
        /// 02、Wind代码; 字段类型 = varchar(40); 
        /// </summary>
        public string S_INFO_WINDCODE { get; set; }
		/// <summary>
        /// 03、交易日期; 字段类型 = varchar(8); 
        /// </summary>
        public string TRADE_DT { get; set; }
		/// <summary>
        /// 04、前结算价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public double S_DQ_PRESETTLE { get; set; }
		/// <summary>
        /// 05、开盘价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public double S_DQ_OPEN { get; set; }
		/// <summary>
        /// 06、最高价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public double S_DQ_HIGH { get; set; }
		/// <summary>
        /// 07、最低价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public double S_DQ_LOW { get; set; }
		/// <summary>
        /// 08、收盘价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public double S_DQ_CLOSE { get; set; }
		/// <summary>
        /// 09、结算价(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public double S_DQ_SETTLE { get; set; }
		/// <summary>
        /// 10、成交量(手); 字段类型 = numeric(20,4); 
        /// </summary>
        public double S_DQ_VOLUME { get; set; }
		/// <summary>
        /// 11、成交金额(万元); 字段类型 = numeric(20,4); 
        /// </summary>
        public double S_DQ_AMOUNT { get; set; }
		/// <summary>
        /// 12、持仓量(手); 字段类型 = numeric(20,4); 
        /// </summary>
        public double S_DQ_OI { get; set; }
		/// <summary>
        /// 13、涨跌(元); 字段类型 = numeric(20,4); 收盘价-前结算价;
        /// </summary>
        public double S_DQ_CHANGE { get; set; }
		/// <summary>
        /// 14、操作时间; 字段类型 = datetime; 
        /// </summary>
        public DateTime? OPDATE { get; set; }
		/// <summary>
        /// 15、操作模式; 字段类型 = varchar(1); 0（新记录）、1（更新/更正）、2（删除），描述对数据库需要进行的操作。对于0和1，记录内容即为最新值；对于2，所有字段内容为空。;
        /// </summary>
        public string OPMODE { get; set; }

		#endregion
	}
}
