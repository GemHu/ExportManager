using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.ExportData
{
	/// <summary>
    /// 上交所返回的结算明细数据接口。
    /// </summary>
 	public class JsmxRow_SH
	{
		#region 数据库表字段常量

		/// <summary>
        /// 01、市场代码; 字段类型 = Character(2); 
        /// </summary>
        public const string C_SCDM = "SCDM";

		/// <summary>
        /// 02、记录类型; 字段类型 = Character(3); 
        /// </summary>
        public const string C_JLLX = "JLLX";

		/// <summary>
        /// 03、交易方式; 字段类型 = Character(3); 
        /// </summary>
        public const string C_JYFS = "JYFS";

		/// <summary>
        /// 04、交收方式; 字段类型 = Character(3); 
        /// </summary>
        public const string C_JSFS = "JSFS";

		/// <summary>
        /// 05、业务类型; 字段类型 = Character(3); 
        /// </summary>
        public const string C_YWLX = "YWLX";

		/// <summary>
        /// 06、清算标志; 字段类型 = Character(3); 
        /// </summary>
        public const string C_QSBZ = "QSBZ";

		/// <summary>
        /// 07、过户类型; 字段类型 = Character(3); 
        /// </summary>
        public const string C_GHLX = "GHLX";

		/// <summary>
        /// 08、交收编号; 字段类型 = Character(16); 
        /// </summary>
        public const string C_JSBH = "JSBH";

		/// <summary>
        /// 09、成交编号; 字段类型 = Character(16); 
        /// </summary>
        public const string C_CJBH = "CJBH";

		/// <summary>
        /// 10、申请编号; 字段类型 = Character(16); 
        /// </summary>
        public const string C_SQBH = "SQBH";

		/// <summary>
        /// 11、委托编号; 字段类型 = Character(16); 
        /// </summary>
        public const string C_WTBH = "WTBH";

		/// <summary>
        /// 12、交易日期; 字段类型 = Character(8); 
        /// </summary>
        public const string C_JYRQ = "JYRQ";

		/// <summary>
        /// 13、清算日期; 字段类型 = Character(8); 
        /// </summary>
        public const string C_QSRQ = "QSRQ";

		/// <summary>
        /// 14、交收日期; 字段类型 = Character(8); 
        /// </summary>
        public const string C_JSRQ = "JSRQ";

		/// <summary>
        /// 15、其它日期; 字段类型 = Character(8); 
        /// </summary>
        public const string C_QTRQ = "QTRQ";

		/// <summary>
        /// 16、委托时间; 字段类型 = Character(6); 
        /// </summary>
        public const string C_WTSJ = "WTSJ";

		/// <summary>
        /// 17、成交时间; 字段类型 = Character(6); 
        /// </summary>
        public const string C_CJSJ = "CJSJ";

		/// <summary>
        /// 18、业务单元1; 字段类型 = Character(5); 
        /// </summary>
        public const string C_XWH1 = "XWH1";

		/// <summary>
        /// 19、业务单元2; 字段类型 = Character(5); 
        /// </summary>
        public const string C_XWH2 = "XWH2";

		/// <summary>
        /// 20、业务单元1所属结算参与人的清算编号; 字段类型 = Character(8); 
        /// </summary>
        public const string C_XWHY = "XWHY";

		/// <summary>
        /// 21、结算参与人的清算编号; 字段类型 = Character(8); 
        /// </summary>
        public const string C_JSHY = "JSHY";

		/// <summary>
        /// 22、托管银行的清算编号; 字段类型 = Character(8); 
        /// </summary>
        public const string C_TGHY = "TGHY";

		/// <summary>
        /// 23、证券账号; 字段类型 = Character(10); 
        /// </summary>
        public const string C_ZQZH = "ZQZH";

		/// <summary>
        /// 24、证券代码1; 字段类型 = Character(6); 
        /// </summary>
        public const string C_ZQDM1 = "ZQDM1";

		/// <summary>
        /// 25、证券代码2; 字段类型 = Character(6); 
        /// </summary>
        public const string C_ZQDM2 = "ZQDM2";

		/// <summary>
        /// 26、证券类别; 字段类型 = Character(2); 
        /// </summary>
        public const string C_ZQLB = "ZQLB";

		/// <summary>
        /// 27、流通类型; 字段类型 = Character(1); 
        /// </summary>
        public const string C_LTLX = "LTLX";

		/// <summary>
        /// 28、权益类别; 字段类型 = Character(2); 
        /// </summary>
        public const string C_QYLB = "QYLB";

		/// <summary>
        /// 29、挂牌年份; 字段类型 = Character(4); 
        /// </summary>
        public const string C_GPNF = "GPNF";

		/// <summary>
        /// 30、买卖标志; 字段类型 = Character(1); 
        /// </summary>
        public const string C_MMBZ = "MMBZ";

		/// <summary>
        /// 31、交收数量; 字段类型 = Character(16); 
        /// </summary>
        public const string C_SL = "SL";

		/// <summary>
        /// 32、成交数量; 字段类型 = Character(16); 
        /// </summary>
        public const string C_CJSL = "CJSL";

		/// <summary>
        /// 33、资金账号; 字段类型 = Character(25); 
        /// </summary>
        public const string C_ZJZH = "ZJZH";

		/// <summary>
        /// 34、币种; 字段类型 = Character(3); 
        /// </summary>
        public const string C_BZ = "BZ";

		/// <summary>
        /// 35、价格1; 字段类型 = Character(17); 
        /// </summary>
        public const string C_JG1 = "JG1";

		/// <summary>
        /// 36、价格2; 字段类型 = Character(17); 
        /// </summary>
        public const string C_JG2 = "JG2";

		/// <summary>
        /// 37、清算金额; 字段类型 = Character(19); 
        /// </summary>
        public const string C_QSJE = "QSJE";

		/// <summary>
        /// 38、印花税; 字段类型 = Character(17); 
        /// </summary>
        public const string C_YHS = "YHS";

		/// <summary>
        /// 39、经手费; 字段类型 = Character(17); 
        /// </summary>
        public const string C_JSF = "JSF";

		/// <summary>
        /// 40、过户费; 字段类型 = Character(17); 
        /// </summary>
        public const string C_GHF = "GHF";

		/// <summary>
        /// 41、证管费; 字段类型 = Character(17); 
        /// </summary>
        public const string C_ZGF = "ZGF";

		/// <summary>
        /// 42、手续费; 字段类型 = Character(17); 
        /// </summary>
        public const string C_SXF = "SXF";

		/// <summary>
        /// 43、其它金额1; 字段类型 = Character(19); 
        /// </summary>
        public const string C_QTJE1 = "QTJE1";

		/// <summary>
        /// 44、其它金额2; 字段类型 = Character(19); 
        /// </summary>
        public const string C_QTJE2 = "QTJE2";

		/// <summary>
        /// 45、其它金额3; 字段类型 = Character(19); 
        /// </summary>
        public const string C_QTJE3 = "QTJE3";

		/// <summary>
        /// 46、实际收付; 字段类型 = Character(19); 
        /// </summary>
        public const string C_SJSF = "SJSF";

		/// <summary>
        /// 47、结果代码; 字段类型 = Character(4); 
        /// </summary>
        public const string C_JGDM = "JGDM";

		/// <summary>
        /// 48、附加说明; 字段类型 = Character(40); 
        /// </summary>
        public const string C_FJSM = "FJSM";

		#endregion

		#region 数据库表字段对应属性
		
		/// <summary>
        /// 01、市场代码; 字段类型 = Character(2); 
        /// </summary>
        public string SCDM { get; set; }
		/// <summary>
        /// 02、记录类型; 字段类型 = Character(3); 
        /// </summary>
        public string JLLX { get; set; }
		/// <summary>
        /// 03、交易方式; 字段类型 = Character(3); 
        /// </summary>
        public string JYFS { get; set; }
		/// <summary>
        /// 04、交收方式; 字段类型 = Character(3); 
        /// </summary>
        public string JSFS { get; set; }
		/// <summary>
        /// 05、业务类型; 字段类型 = Character(3); 
        /// </summary>
        public string YWLX { get; set; }
		/// <summary>
        /// 06、清算标志; 字段类型 = Character(3); 
        /// </summary>
        public string QSBZ { get; set; }
		/// <summary>
        /// 07、过户类型; 字段类型 = Character(3); 
        /// </summary>
        public string GHLX { get; set; }
		/// <summary>
        /// 08、交收编号; 字段类型 = Character(16); 
        /// </summary>
        public string JSBH { get; set; }
		/// <summary>
        /// 09、成交编号; 字段类型 = Character(16); 
        /// </summary>
        public string CJBH { get; set; }
		/// <summary>
        /// 10、申请编号; 字段类型 = Character(16); 
        /// </summary>
        public string SQBH { get; set; }
		/// <summary>
        /// 11、委托编号; 字段类型 = Character(16); 
        /// </summary>
        public string WTBH { get; set; }
		/// <summary>
        /// 12、交易日期; 字段类型 = Character(8); 
        /// </summary>
        public string JYRQ { get; set; }
		/// <summary>
        /// 13、清算日期; 字段类型 = Character(8); 
        /// </summary>
        public string QSRQ { get; set; }
		/// <summary>
        /// 14、交收日期; 字段类型 = Character(8); 
        /// </summary>
        public string JSRQ { get; set; }
		/// <summary>
        /// 15、其它日期; 字段类型 = Character(8); 
        /// </summary>
        public string QTRQ { get; set; }
		/// <summary>
        /// 16、委托时间; 字段类型 = Character(6); 
        /// </summary>
        public string WTSJ { get; set; }
		/// <summary>
        /// 17、成交时间; 字段类型 = Character(6); 
        /// </summary>
        public string CJSJ { get; set; }
		/// <summary>
        /// 18、业务单元1; 字段类型 = Character(5); 
        /// </summary>
        public string XWH1 { get; set; }
		/// <summary>
        /// 19、业务单元2; 字段类型 = Character(5); 
        /// </summary>
        public string XWH2 { get; set; }
		/// <summary>
        /// 20、业务单元1所属结算参与人的清算编号; 字段类型 = Character(8); 
        /// </summary>
        public string XWHY { get; set; }
		/// <summary>
        /// 21、结算参与人的清算编号; 字段类型 = Character(8); 
        /// </summary>
        public string JSHY { get; set; }
		/// <summary>
        /// 22、托管银行的清算编号; 字段类型 = Character(8); 
        /// </summary>
        public string TGHY { get; set; }
		/// <summary>
        /// 23、证券账号; 字段类型 = Character(10); 
        /// </summary>
        public string ZQZH { get; set; }
		/// <summary>
        /// 24、证券代码1; 字段类型 = Character(6); 
        /// </summary>
        public string ZQDM1 { get; set; }
		/// <summary>
        /// 25、证券代码2; 字段类型 = Character(6); 
        /// </summary>
        public string ZQDM2 { get; set; }
		/// <summary>
        /// 26、证券类别; 字段类型 = Character(2); 
        /// </summary>
        public string ZQLB { get; set; }
		/// <summary>
        /// 27、流通类型; 字段类型 = Character(1); 
        /// </summary>
        public char LTLX { get; set; }
		/// <summary>
        /// 28、权益类别; 字段类型 = Character(2); 
        /// </summary>
        public string QYLB { get; set; }
		/// <summary>
        /// 29、挂牌年份; 字段类型 = Character(4); 
        /// </summary>
        public string GPNF { get; set; }
		/// <summary>
        /// 30、买卖标志; 字段类型 = Character(1); 
        /// </summary>
        public char MMBZ { get; set; }
		/// <summary>
        /// 31、交收数量; 字段类型 = Character(16); 
        /// </summary>
        public string SL { get; set; }
		/// <summary>
        /// 32、成交数量; 字段类型 = Character(16); 
        /// </summary>
        public string CJSL { get; set; }
		/// <summary>
        /// 33、资金账号; 字段类型 = Character(25); 
        /// </summary>
        public string ZJZH { get; set; }
		/// <summary>
        /// 34、币种; 字段类型 = Character(3); 
        /// </summary>
        public string BZ { get; set; }
		/// <summary>
        /// 35、价格1; 字段类型 = Character(17); 
        /// </summary>
        public string JG1 { get; set; }
		/// <summary>
        /// 36、价格2; 字段类型 = Character(17); 
        /// </summary>
        public string JG2 { get; set; }
		/// <summary>
        /// 37、清算金额; 字段类型 = Character(19); 
        /// </summary>
        public string QSJE { get; set; }
		/// <summary>
        /// 38、印花税; 字段类型 = Character(17); 
        /// </summary>
        public string YHS { get; set; }
		/// <summary>
        /// 39、经手费; 字段类型 = Character(17); 
        /// </summary>
        public string JSF { get; set; }
		/// <summary>
        /// 40、过户费; 字段类型 = Character(17); 
        /// </summary>
        public string GHF { get; set; }
		/// <summary>
        /// 41、证管费; 字段类型 = Character(17); 
        /// </summary>
        public string ZGF { get; set; }
		/// <summary>
        /// 42、手续费; 字段类型 = Character(17); 
        /// </summary>
        public string SXF { get; set; }
		/// <summary>
        /// 43、其它金额1; 字段类型 = Character(19); 
        /// </summary>
        public string QTJE1 { get; set; }
		/// <summary>
        /// 44、其它金额2; 字段类型 = Character(19); 
        /// </summary>
        public string QTJE2 { get; set; }
		/// <summary>
        /// 45、其它金额3; 字段类型 = Character(19); 
        /// </summary>
        public string QTJE3 { get; set; }
		/// <summary>
        /// 46、实际收付; 字段类型 = Character(19); 
        /// </summary>
        public string SJSF { get; set; }
		/// <summary>
        /// 47、结果代码; 字段类型 = Character(4); 
        /// </summary>
        public string JGDM { get; set; }
		/// <summary>
        /// 48、附加说明; 字段类型 = Character(40); 
        /// </summary>
        public string FJSM { get; set; }

		#endregion
	}
}
