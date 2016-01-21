using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.ExportData
{
	/// <summary>
    /// 量化系统中的OrderTable数据模型。
    /// </summary>
 	public class OrderRow_LH
	{
		#region 数据库表字段常量

		/// <summary>
        /// 01、项目代码*; 字段类型 = int; 
        /// </summary>
        public const string C_xmdm = "xmdm";

		/// <summary>
        /// 02、资金代码; 字段类型 = int; 
        /// </summary>
        public const string C_zjdm = "zjdm";

		/// <summary>
        /// 03、组合代码*; 字段类型 = int; 
        /// </summary>
        public const string C_zmdm = "zmdm";

		/// <summary>
        /// 04、股东代码; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_gddm = "gddm";

		/// <summary>
        /// 05、证券代码*; 字段类型 = varchar(30); 
        /// </summary>
        public const string C_zqdm = "zqdm";

		/// <summary>
        /// 06、市场*; 字段类型 = char(1); 
        /// </summary>
        public const string C_sc = "sc";

		/// <summary>
        /// 07、委托编号*; 字段类型 = int; 
        /// </summary>
        public const string C_orderno = "orderno";

		/// <summary>
        /// 08、委托标记号; 字段类型 = int; 
        /// </summary>
        public const string C_cidx = "cidx";

		/// <summary>
        /// 09、申报编号; 字段类型 = varchar(30); 
        /// </summary>
        public const string C_reportno = "reportno";

		/// <summary>
        /// 10、证券类别; 字段类型 = int; 
        /// </summary>
        public const string C_zqlb = "zqlb";

		/// <summary>
        /// 11、买卖*; 字段类型 = char(1); 
        /// </summary>
        public const string C_BS = "BS";

		/// <summary>
        /// 12、委托类型; 字段类型 = char(1); 
        /// </summary>
        public const string C_OpenOrClose = "OpenOrClose";

		/// <summary>
        /// 13、委托类别; 字段类型 = char(1); 
        /// </summary>
        public const string C_OrderType = "OrderType";

		/// <summary>
        /// 14、委托价格; 字段类型 = float(53); 
        /// </summary>
        public const string C_OrderPr = "OrderPr";

		/// <summary>
        /// 15、委托数量; 字段类型 = float(53); 
        /// </summary>
        public const string C_OrderNum = "OrderNum";

		/// <summary>
        /// 16、成交数量*; 字段类型 = float(53); 
        /// </summary>
        public const string C_CjNum = "CjNum";

		/// <summary>
        /// 17、成交金额*; 字段类型 = float(53); 
        /// </summary>
        public const string C_CjBal = "CjBal";

		/// <summary>
        /// 18、乘数*; 字段类型 = float(53); 
        /// </summary>
        public const string C_mUnitnum = "mUnitnum";

		/// <summary>
        /// 19、冻结资金数; 字段类型 = float(53); 
        /// </summary>
        public const string C_FroseBal = "FroseBal";

		/// <summary>
        /// 20、冻结库存数; 字段类型 = float(53); 
        /// </summary>
        public const string C_FroseStore = "FroseStore";

		/// <summary>
        /// 21、冻结库存数; 字段类型 = float(53); 
        /// </summary>
        public const string C_FroseStoreETF = "FroseStoreETF";

		/// <summary>
        /// 22、取消数量; 字段类型 = float(53); 
        /// </summary>
        public const string C_cancelnum = "cancelnum";

		/// <summary>
        /// 23、; 字段类型 = int; 
        /// </summary>
        public const string C_status = "status";

		/// <summary>
        /// 24、委托时间; 字段类型 = int; 
        /// </summary>
        public const string C_OrderTime = "OrderTime";

		/// <summary>
        /// 25、申报时间; 字段类型 = int; 
        /// </summary>
        public const string C_ReportTime = "ReportTime";

		/// <summary>
        /// 26、最早成交时间; 字段类型 = int; 
        /// </summary>
        public const string C_FirtCjTime = "FirtCjTime";

		/// <summary>
        /// 27、最新成交时间; 字段类型 = int; 
        /// </summary>
        public const string C_LastCjTime = "LastCjTime";

		/// <summary>
        /// 28、交易所最新成交时间*; 字段类型 = int; 
        /// </summary>
        public const string C_CjTimeJys = "CjTimeJys";

		/// <summary>
        /// 29、取消订单编号; 字段类型 = int; 
        /// </summary>
        public const string C_Cancelorderno = "Cancelorderno";

		/// <summary>
        /// 30、调试信息; 字段类型 = varchar(100); 
        /// </summary>
        public const string C_DebugInfo = "DebugInfo";

		/// <summary>
        /// 31、; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_Oper = "Oper";

		/// <summary>
        /// 32、; 字段类型 = datetime; 
        /// </summary>
        public const string C_CrtDate = "CrtDate";

		/// <summary>
        /// 33、; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_CustomInfo1 = "CustomInfo1";

		/// <summary>
        /// 34、; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_CustomInfo2 = "CustomInfo2";

		/// <summary>
        /// 35、; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_CustomInfo3 = "CustomInfo3";

		/// <summary>
        /// 36、; 字段类型 = int; 
        /// </summary>
        public const string C_InstruId = "InstruId";

		/// <summary>
        /// 37、策略号; 字段类型 = int; 
        /// </summary>
        public const string C_PolicyId = "PolicyId";

		#endregion

		#region 数据库表字段对应属性
		
		/// <summary>
        /// 01、项目代码*; 字段类型 = int; 
        /// </summary>
        public int xmdm { get; set; }
		/// <summary>
        /// 02、资金代码; 字段类型 = int; 
        /// </summary>
        public int zjdm { get; set; }
		/// <summary>
        /// 03、组合代码*; 字段类型 = int; 
        /// </summary>
        public int zmdm { get; set; }
		/// <summary>
        /// 04、股东代码; 字段类型 = varchar(20); 
        /// </summary>
        public string gddm { get; set; }
		/// <summary>
        /// 05、证券代码*; 字段类型 = varchar(30); 
        /// </summary>
        public string zqdm { get; set; }
		/// <summary>
        /// 06、市场*; 字段类型 = char(1); 
        /// </summary>
        public char sc { get; set; }
		/// <summary>
        /// 07、委托编号*; 字段类型 = int; 
        /// </summary>
        public int orderno { get; set; }
		/// <summary>
        /// 08、委托标记号; 字段类型 = int; 
        /// </summary>
        public int cidx { get; set; }
		/// <summary>
        /// 09、申报编号; 字段类型 = varchar(30); 
        /// </summary>
        public string reportno { get; set; }
		/// <summary>
        /// 10、证券类别; 字段类型 = int; 
        /// </summary>
        public int zqlb { get; set; }
		/// <summary>
        /// 11、买卖*; 字段类型 = char(1); 
        /// </summary>
        public char BS { get; set; }
		/// <summary>
        /// 12、委托类型; 字段类型 = char(1); 
        /// </summary>
        public char OpenOrClose { get; set; }
		/// <summary>
        /// 13、委托类别; 字段类型 = char(1); 
        /// </summary>
        public char OrderType { get; set; }
		/// <summary>
        /// 14、委托价格; 字段类型 = float(53); 
        /// </summary>
        public double OrderPr { get; set; }
		/// <summary>
        /// 15、委托数量; 字段类型 = float(53); 
        /// </summary>
        public double OrderNum { get; set; }
		/// <summary>
        /// 16、成交数量*; 字段类型 = float(53); 
        /// </summary>
        public double CjNum { get; set; }
		/// <summary>
        /// 17、成交金额*; 字段类型 = float(53); 
        /// </summary>
        public double CjBal { get; set; }
		/// <summary>
        /// 18、乘数*; 字段类型 = float(53); 
        /// </summary>
        public double mUnitnum { get; set; }
		/// <summary>
        /// 19、冻结资金数; 字段类型 = float(53); 
        /// </summary>
        public double FroseBal { get; set; }
		/// <summary>
        /// 20、冻结库存数; 字段类型 = float(53); 
        /// </summary>
        public double FroseStore { get; set; }
		/// <summary>
        /// 21、冻结库存数; 字段类型 = float(53); 
        /// </summary>
        public double FroseStoreETF { get; set; }
		/// <summary>
        /// 22、取消数量; 字段类型 = float(53); 
        /// </summary>
        public double cancelnum { get; set; }
		/// <summary>
        /// 23、; 字段类型 = int; 
        /// </summary>
        public int status { get; set; }
		/// <summary>
        /// 24、委托时间; 字段类型 = int; 
        /// </summary>
        public int OrderTime { get; set; }
		/// <summary>
        /// 25、申报时间; 字段类型 = int; 
        /// </summary>
        public int ReportTime { get; set; }
		/// <summary>
        /// 26、最早成交时间; 字段类型 = int; 
        /// </summary>
        public int FirtCjTime { get; set; }
		/// <summary>
        /// 27、最新成交时间; 字段类型 = int; 
        /// </summary>
        public int LastCjTime { get; set; }
		/// <summary>
        /// 28、交易所最新成交时间*; 字段类型 = int; 
        /// </summary>
        public int CjTimeJys { get; set; }
		/// <summary>
        /// 29、取消订单编号; 字段类型 = int; 
        /// </summary>
        public int Cancelorderno { get; set; }
		/// <summary>
        /// 30、调试信息; 字段类型 = varchar(100); 
        /// </summary>
        public string DebugInfo { get; set; }
		/// <summary>
        /// 31、; 字段类型 = varchar(20); 
        /// </summary>
        public string Oper { get; set; }
		/// <summary>
        /// 32、; 字段类型 = datetime; 
        /// </summary>
        public DateTime? CrtDate { get; set; }
		/// <summary>
        /// 33、; 字段类型 = varchar(20); 
        /// </summary>
        public string CustomInfo1 { get; set; }
		/// <summary>
        /// 34、; 字段类型 = varchar(20); 
        /// </summary>
        public string CustomInfo2 { get; set; }
		/// <summary>
        /// 35、; 字段类型 = varchar(20); 
        /// </summary>
        public string CustomInfo3 { get; set; }
		/// <summary>
        /// 36、; 字段类型 = int; 
        /// </summary>
        public int InstruId { get; set; }
		/// <summary>
        /// 37、策略号; 字段类型 = int; 
        /// </summary>
        public int PolicyId { get; set; }

		#endregion
	}
}
