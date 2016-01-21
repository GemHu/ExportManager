using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.ExportData
{
	/// <summary>
    /// ETF退补款数据表数据。
    /// </summary>
 	public class CilRow
	{
		#region 数据库表字段常量

		/// <summary>
        /// 01、基金代码; 字段类型 = C(6); 备注：一级市场基金代码;
        /// </summary>
        public const string C_JJDM = "JJDM";

		/// <summary>
        /// 02、资金类型; 字段类型 = C(2); 备注：02：现金替代退补款；04：赎回替代款;
        /// </summary>
        public const string C_ZJLX = "ZJLX";

		/// <summary>
        /// 03、证券账户; 字段类型 = C(10); 备注：投资人证券账号;
        /// </summary>
        public const string C_ZQZH = "ZQZH";

		/// <summary>
        /// 04、交易席位; 字段类型 = C(5); 备注：投资人交易席位;
        /// </summary>
        public const string C_JYXW = "JYXW";

		/// <summary>
        /// 05、席位会员; 字段类型 = C(8); 备注：席位所属会员;
        /// </summary>
        public const string C_XWHY = "XWHY";

		/// <summary>
        /// 06、退款金额; 字段类型 = C(17); 备注：退款金额;
        /// </summary>
        public const string C_TKJE = "TKJE";

		/// <summary>
        /// 07、补款金额; 字段类型 = C(17); 备注：补款金额;
        /// </summary>
        public const string C_BKJE = "BKJE";

		/// <summary>
        /// 08、交收日期; 字段类型 = C(8); 备注：对应文件名中清算日期的下一上交所交易日;
        /// </summary>
        public const string C_JSRQ = "JSRQ";

		/// <summary>
        /// 09、交易日期; 字段类型 = C(8); 备注：申购/赎回委托交易日期;
        /// </summary>
        public const string C_JYRQ = "JYRQ";

		/// <summary>
        /// 10、证券代码; 字段类型 = C(6); 备注：一级市场基金代码;
        /// </summary>
        public const string C_ZQDM = "ZQDM";

		#endregion

		#region 数据库表字段对应属性
		
		/// <summary>
        /// 01、基金代码; 字段类型 = C(6); 备注：一级市场基金代码;
        /// </summary>
        public string JJDM { get; set; }
		/// <summary>
        /// 02、资金类型; 字段类型 = C(2); 备注：02：现金替代退补款；04：赎回替代款;
        /// </summary>
        public string ZJLX { get; set; }
		/// <summary>
        /// 03、证券账户; 字段类型 = C(10); 备注：投资人证券账号;
        /// </summary>
        public string ZQZH { get; set; }
		/// <summary>
        /// 04、交易席位; 字段类型 = C(5); 备注：投资人交易席位;
        /// </summary>
        public string JYXW { get; set; }
		/// <summary>
        /// 05、席位会员; 字段类型 = C(8); 备注：席位所属会员;
        /// </summary>
        public string XWHY { get; set; }
		/// <summary>
        /// 06、退款金额; 字段类型 = C(17); 备注：退款金额;
        /// </summary>
        public string TKJE { get; set; }
		/// <summary>
        /// 07、补款金额; 字段类型 = C(17); 备注：补款金额;
        /// </summary>
        public string BKJE { get; set; }
		/// <summary>
        /// 08、交收日期; 字段类型 = C(8); 备注：对应文件名中清算日期的下一上交所交易日;
        /// </summary>
        public string JSRQ { get; set; }
		/// <summary>
        /// 09、交易日期; 字段类型 = C(8); 备注：申购/赎回委托交易日期;
        /// </summary>
        public string JYRQ { get; set; }
		/// <summary>
        /// 10、证券代码; 字段类型 = C(6); 备注：一级市场基金代码;
        /// </summary>
        public string ZQDM { get; set; }

		#endregion
	}
}
