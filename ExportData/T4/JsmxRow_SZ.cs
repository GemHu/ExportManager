using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.ExportData
{
	/// <summary>
    /// 深交所返回的结算明细数据接口。
    /// </summary>
 	public class JsmxRow_SZ
	{
		#region 数据库表字段常量

		/// <summary>
        /// 01、结算账号; 字段类型 = C(6); 
        /// </summary>
        public const string C_MXJSZH = "MXJSZH";

		/// <summary>
        /// 02、备付金账户; 字段类型 = C(25); 
        /// </summary>
        public const string C_MXBFZH = "MXBFZH";

		/// <summary>
        /// 03、数据类型; 字段类型 = C(2); 
        /// </summary>
        public const string C_MXSJLX = "MXSJLX";

		/// <summary>
        /// 04、业务类别; 字段类型 = C(4); 
        /// </summary>
        public const string C_MXYWLB = "MXYWLB";

		/// <summary>
        /// 05、证券代码; 字段类型 = C(8); 
        /// </summary>
        public const string C_MXZQDM = "MXZQDM";

		/// <summary>
        /// 06、交易单元; 字段类型 = C(6); 
        /// </summary>
        public const string C_MXJYDY = "MXJYDY";

		/// <summary>
        /// 07、托管单元; 字段类型 = C(6); 
        /// </summary>
        public const string C_MXTGDY = "MXTGDY";

		/// <summary>
        /// 08、证券账户代码; 字段类型 = C(20); 
        /// </summary>
        public const string C_MXZQZH = "MXZQZH";

		/// <summary>
        /// 09、客户订单编号/申请单号; 字段类型 = C(24); 
        /// </summary>
        public const string C_MXDDBH = "MXDDBH";

		/// <summary>
        /// 10、营业部代码; 字段类型 = C(4); 
        /// </summary>
        public const string C_MXYYB = "MXYYB";

		/// <summary>
        /// 11、执行编号; 字段类型 = C(16); 
        /// </summary>
        public const string C_MXZXBH = "MXZXBH";

		/// <summary>
        /// 12、业务流水号; 字段类型 = C(16); 
        /// </summary>
        public const string C_MXYWLSH = "MXYWLSH";

		/// <summary>
        /// 13、成交数量; 字段类型 = N(15,2); 
        /// </summary>
        public const string C_MXCJSL = "MXCJSL";

		/// <summary>
        /// 14、清算数量; 字段类型 = N(15,2); 
        /// </summary>
        public const string C_MXQSSL = "MXQSSL";

		/// <summary>
        /// 15、成交价格; 字段类型 = N(13,4); 
        /// </summary>
        public const string C_MXCJJG = "MXCJJG";

		/// <summary>
        /// 16、清算价格; 字段类型 = N(18,9); 
        /// </summary>
        public const string C_MXQSJG = "MXQSJG";

		/// <summary>
        /// 17、信用交易标识; 字段类型 = C(1); 
        /// </summary>
        public const string C_MXXYJY = "MXXYJY";

		/// <summary>
        /// 18、平仓标识; 字段类型 = C(1); 
        /// </summary>
        public const string C_MXPCBS = "MXPCBS";

		/// <summary>
        /// 19、证券类别; 字段类型 = C(2); 
        /// </summary>
        public const string C_MXZQLB = "MXZQLB";

		/// <summary>
        /// 20、证券子类别; 字段类型 = C(2); 
        /// </summary>
        public const string C_MXZQZL = "MXZQZL";

		/// <summary>
        /// 21、股份性质; 字段类型 = C(2); 
        /// </summary>
        public const string C_MXGFXZ = "MXGFXZ";

		/// <summary>
        /// 22、交收方式; 字段类型 = C(1); 
        /// </summary>
        public const string C_MXJSFS = "MXJSFS";

		/// <summary>
        /// 23、货币代号; 字段类型 = C(3); 
        /// </summary>
        public const string C_MXHBDH = "MXHBDH";

		/// <summary>
        /// 24、清算本金; 字段类型 = N(17,2); 
        /// </summary>
        public const string C_MXQSBJ = "MXQSBJ";

		/// <summary>
        /// 25、印花税; 字段类型 = (N); 备注：12,2;
        /// </summary>
        public const string C_MXYHS = "MXYHS";

		/// <summary>
        /// 26、交易经手费; 字段类型 = N(12,2); 
        /// </summary>
        public const string C_MXJYJSF = "MXJYJSF";

		/// <summary>
        /// 27、监管规费; 字段类型 = N(12,2); 
        /// </summary>
        public const string C_MXJGGF = "MXJGGF";

		/// <summary>
        /// 28、过户费; 字段类型 = N(12,2); 
        /// </summary>
        public const string C_MXGHF = "MXGHF";

		/// <summary>
        /// 29、结算费; 字段类型 = N(12,2); 
        /// </summary>
        public const string C_MXJSF = "MXJSF";

		/// <summary>
        /// 30、手续费; 字段类型 = N(12,2); 
        /// </summary>
        public const string C_MXSXF = "MXSXF";

		/// <summary>
        /// 31、结算参与人佣金; 字段类型 = N(12,2); 备注：结算参与人系统自用;
        /// </summary>
        public const string C_MXQSYJ = "MXQSYJ";

		/// <summary>
        /// 32、其他费用; 字段类型 = N(12,2); 
        /// </summary>
        public const string C_MXQTFY = "MXQTFY";

		/// <summary>
        /// 33、资金金额; 字段类型 = N(17,2); 
        /// </summary>
        public const string C_MXZJJE = "MXZJJE";

		/// <summary>
        /// 34、收付净额; 字段类型 = N(18,2); 
        /// </summary>
        public const string C_MXSFJE = "MXSFJE";

		/// <summary>
        /// 35、成交日期; 字段类型 = C(8); 备注：CCYYMMDD;
        /// </summary>
        public const string C_MXCJRQ = "MXCJRQ";

		/// <summary>
        /// 36、清算日期; 字段类型 = C(8); 备注：CCYYMMDD;
        /// </summary>
        public const string C_MXQSRQ = "MXQSRQ";

		/// <summary>
        /// 37、交收日期; 字段类型 = C(8); 备注：CCYYMMDD;
        /// </summary>
        public const string C_MXJSRQ = "MXJSRQ";

		/// <summary>
        /// 38、发送日期; 字段类型 = C(8); 备注：CCYYMMDD;
        /// </summary>
        public const string C_MXFSRQ = "MXFSRQ";

		/// <summary>
        /// 39、其它日期; 字段类型 = C(8); 备注：CCYYMMDD;
        /// </summary>
        public const string C_MXQTRQ = "MXQTRQ";

		/// <summary>
        /// 40、市场代码; 字段类型 = C(2); 备注：保留字段;
        /// </summary>
        public const string C_MXSCDM = "MXSCDM";

		/// <summary>
        /// 41、交易方式; 字段类型 = C(2); 备注：;
        /// </summary>
        public const string C_MXJYFS = "MXJYFS";

		/// <summary>
        /// 42、证券代码2; 字段类型 = C(8); 
        /// </summary>
        public const string C_MXZQDM2 = "MXZQDM2";

		/// <summary>
        /// 43、托管单元2; 字段类型 = C(6); 
        /// </summary>
        public const string C_MXTGDY2 = "MXTGDY2";

		/// <summary>
        /// 44、订单编号2; 字段类型 = C(16); 备注：保留字段;
        /// </summary>
        public const string C_MXDDBH2 = "MXDDBH2";

		/// <summary>
        /// 45、错误代号; 字段类型 = C(4); 
        /// </summary>
        public const string C_MXCWDH = "MXCWDH";

		/// <summary>
        /// 46、匹配号码; 字段类型 = C(10); 备注：备用;
        /// </summary>
        public const string C_MXPPHM = "MXPPHM";

		/// <summary>
        /// 47、附加说明; 字段类型 = C(30); 
        /// </summary>
        public const string C_MXFJSM = "MXFJSM";

		/// <summary>
        /// 48、备用标志; 字段类型 = C(1); 备注：参与人系统自用;
        /// </summary>
        public const string C_MXBYBZ = "MXBYBZ";

		#endregion

		#region 数据库表字段对应属性
		
		/// <summary>
        /// 01、结算账号; 字段类型 = C(6); 
        /// </summary>
        public string MXJSZH { get; set; }
		/// <summary>
        /// 02、备付金账户; 字段类型 = C(25); 
        /// </summary>
        public string MXBFZH { get; set; }
		/// <summary>
        /// 03、数据类型; 字段类型 = C(2); 
        /// </summary>
        public string MXSJLX { get; set; }
		/// <summary>
        /// 04、业务类别; 字段类型 = C(4); 
        /// </summary>
        public string MXYWLB { get; set; }
		/// <summary>
        /// 05、证券代码; 字段类型 = C(8); 
        /// </summary>
        public string MXZQDM { get; set; }
		/// <summary>
        /// 06、交易单元; 字段类型 = C(6); 
        /// </summary>
        public string MXJYDY { get; set; }
		/// <summary>
        /// 07、托管单元; 字段类型 = C(6); 
        /// </summary>
        public string MXTGDY { get; set; }
		/// <summary>
        /// 08、证券账户代码; 字段类型 = C(20); 
        /// </summary>
        public string MXZQZH { get; set; }
		/// <summary>
        /// 09、客户订单编号/申请单号; 字段类型 = C(24); 
        /// </summary>
        public string MXDDBH { get; set; }
		/// <summary>
        /// 10、营业部代码; 字段类型 = C(4); 
        /// </summary>
        public string MXYYB { get; set; }
		/// <summary>
        /// 11、执行编号; 字段类型 = C(16); 
        /// </summary>
        public string MXZXBH { get; set; }
		/// <summary>
        /// 12、业务流水号; 字段类型 = C(16); 
        /// </summary>
        public string MXYWLSH { get; set; }
		/// <summary>
        /// 13、成交数量; 字段类型 = N(15,2); 
        /// </summary>
        public double MXCJSL { get; set; }
		/// <summary>
        /// 14、清算数量; 字段类型 = N(15,2); 
        /// </summary>
        public double MXQSSL { get; set; }
		/// <summary>
        /// 15、成交价格; 字段类型 = N(13,4); 
        /// </summary>
        public double MXCJJG { get; set; }
		/// <summary>
        /// 16、清算价格; 字段类型 = N(18,9); 
        /// </summary>
        public double MXQSJG { get; set; }
		/// <summary>
        /// 17、信用交易标识; 字段类型 = C(1); 
        /// </summary>
        public char MXXYJY { get; set; }
		/// <summary>
        /// 18、平仓标识; 字段类型 = C(1); 
        /// </summary>
        public char MXPCBS { get; set; }
		/// <summary>
        /// 19、证券类别; 字段类型 = C(2); 
        /// </summary>
        public string MXZQLB { get; set; }
		/// <summary>
        /// 20、证券子类别; 字段类型 = C(2); 
        /// </summary>
        public string MXZQZL { get; set; }
		/// <summary>
        /// 21、股份性质; 字段类型 = C(2); 
        /// </summary>
        public string MXGFXZ { get; set; }
		/// <summary>
        /// 22、交收方式; 字段类型 = C(1); 
        /// </summary>
        public char MXJSFS { get; set; }
		/// <summary>
        /// 23、货币代号; 字段类型 = C(3); 
        /// </summary>
        public string MXHBDH { get; set; }
		/// <summary>
        /// 24、清算本金; 字段类型 = N(17,2); 
        /// </summary>
        public double MXQSBJ { get; set; }
		/// <summary>
        /// 25、印花税; 字段类型 = (N); 备注：12,2;
        /// </summary>
        public string MXYHS { get; set; }
		/// <summary>
        /// 26、交易经手费; 字段类型 = N(12,2); 
        /// </summary>
        public double MXJYJSF { get; set; }
		/// <summary>
        /// 27、监管规费; 字段类型 = N(12,2); 
        /// </summary>
        public double MXJGGF { get; set; }
		/// <summary>
        /// 28、过户费; 字段类型 = N(12,2); 
        /// </summary>
        public double MXGHF { get; set; }
		/// <summary>
        /// 29、结算费; 字段类型 = N(12,2); 
        /// </summary>
        public double MXJSF { get; set; }
		/// <summary>
        /// 30、手续费; 字段类型 = N(12,2); 
        /// </summary>
        public double MXSXF { get; set; }
		/// <summary>
        /// 31、结算参与人佣金; 字段类型 = N(12,2); 备注：结算参与人系统自用;
        /// </summary>
        public double MXQSYJ { get; set; }
		/// <summary>
        /// 32、其他费用; 字段类型 = N(12,2); 
        /// </summary>
        public double MXQTFY { get; set; }
		/// <summary>
        /// 33、资金金额; 字段类型 = N(17,2); 
        /// </summary>
        public double MXZJJE { get; set; }
		/// <summary>
        /// 34、收付净额; 字段类型 = N(18,2); 
        /// </summary>
        public double MXSFJE { get; set; }
		/// <summary>
        /// 35、成交日期; 字段类型 = C(8); 备注：CCYYMMDD;
        /// </summary>
        public string MXCJRQ { get; set; }
		/// <summary>
        /// 36、清算日期; 字段类型 = C(8); 备注：CCYYMMDD;
        /// </summary>
        public string MXQSRQ { get; set; }
		/// <summary>
        /// 37、交收日期; 字段类型 = C(8); 备注：CCYYMMDD;
        /// </summary>
        public string MXJSRQ { get; set; }
		/// <summary>
        /// 38、发送日期; 字段类型 = C(8); 备注：CCYYMMDD;
        /// </summary>
        public string MXFSRQ { get; set; }
		/// <summary>
        /// 39、其它日期; 字段类型 = C(8); 备注：CCYYMMDD;
        /// </summary>
        public string MXQTRQ { get; set; }
		/// <summary>
        /// 40、市场代码; 字段类型 = C(2); 备注：保留字段;
        /// </summary>
        public string MXSCDM { get; set; }
		/// <summary>
        /// 41、交易方式; 字段类型 = C(2); 备注：;
        /// </summary>
        public string MXJYFS { get; set; }
		/// <summary>
        /// 42、证券代码2; 字段类型 = C(8); 
        /// </summary>
        public string MXZQDM2 { get; set; }
		/// <summary>
        /// 43、托管单元2; 字段类型 = C(6); 
        /// </summary>
        public string MXTGDY2 { get; set; }
		/// <summary>
        /// 44、订单编号2; 字段类型 = C(16); 备注：保留字段;
        /// </summary>
        public string MXDDBH2 { get; set; }
		/// <summary>
        /// 45、错误代号; 字段类型 = C(4); 
        /// </summary>
        public string MXCWDH { get; set; }
		/// <summary>
        /// 46、匹配号码; 字段类型 = C(10); 备注：备用;
        /// </summary>
        public string MXPPHM { get; set; }
		/// <summary>
        /// 47、附加说明; 字段类型 = C(30); 
        /// </summary>
        public string MXFJSM { get; set; }
		/// <summary>
        /// 48、备用标志; 字段类型 = C(1); 备注：参与人系统自用;
        /// </summary>
        public char MXBYBZ { get; set; }

		#endregion
	}
}
