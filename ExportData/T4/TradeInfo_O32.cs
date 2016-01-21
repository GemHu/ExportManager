using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.ExportData
{
	/// <summary>
    /// O32系统成交信息。
    /// </summary>
 	public class TradeInfo_O32
	{
		#region 数据库表字段常量

		/// <summary>
        /// 01、证券名称; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_STOCK_NAME = "VC_STOCK_NAME";

		/// <summary>
        /// 02、Comment; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_THIRD_REFF = "VC_THIRD_REFF";

		/// <summary>
        /// 03、业务分类; 字段类型 = char(1); 1、交易所业务；4、期货业务；I、上交所固定收益平台；;
        /// </summary>
        public const string C_C_BUSIN_CLASS = "C_BUSIN_CLASS";

		/// <summary>
        /// 04、委托序号; 字段类型 = bigint; 
        /// </summary>
        public const string C_L_ENTRUST_SERIAL_NO = "L_ENTRUST_SERIAL_NO";

		/// <summary>
        /// 05、成交编号; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_DEAL_NO = "VC_DEAL_NO";

		/// <summary>
        /// 06、日期; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_L_DATE = "L_DATE";

		/// <summary>
        /// 07、基金编号; 字段类型 = int; 
        /// </summary>
        public const string C_L_FUND_ID = "L_FUND_ID";

		/// <summary>
        /// 08、组合编号; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_COMBI_NO = "VC_COMBI_NO";

		/// <summary>
        /// 09、组合名称; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_COMBI_NAME = "VC_COMBI_NAME";

		/// <summary>
        /// 10、成交时间; 字段类型 = int; 
        /// </summary>
        public const string C_L_BUSINESS_TIME = "L_BUSINESS_TIME";

		/// <summary>
        /// 11、委托方向; 字段类型 = char(1); 1、买入；2、卖出；3、债券买入；4、债券卖出；5、融资回购；6、融券回购；X、卖出开仓；Y、买入平仓；V、买入开仓；W、卖出平仓；;
        /// </summary>
        public const string C_C_ENTRUST_DIRECTION = "C_ENTRUST_DIRECTION";

		/// <summary>
        /// 12、交易方向(买卖); 字段类型 = VARCHAR2; B、上交所及中金所的买入业务；S、上交所及中金所的卖出业务；0S、深交所的卖出业务；0B、深交所的买入业务；;
        /// </summary>
        public const string C_VC_REPORT_DIRECTION = "VC_REPORT_DIRECTION";

		/// <summary>
        /// 13、委托方向名称; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_ENTRUSTDIR_NAME = "VC_ENTRUSTDIR_NAME";

		/// <summary>
        /// 14、成交数量; 字段类型 = bigint; 
        /// </summary>
        public const string C_L_DEAL_AMOUNT = "L_DEAL_AMOUNT";

		/// <summary>
        /// 15、成交价格; 字段类型 = double; 
        /// </summary>
        public const string C_EN_DEAL_PRICE = "EN_DEAL_PRICE";

		/// <summary>
        /// 16、成交金额; 字段类型 = double; 
        /// </summary>
        public const string C_EN_DEAL_BALANCE = "EN_DEAL_BALANCE";

		/// <summary>
        /// 17、证券代码; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_REPORT_CODE = "VC_REPORT_CODE";

		/// <summary>
        /// 18、股东代码; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_STOCKHOLDER_ID = "VC_STOCKHOLDER_ID";

		/// <summary>
        /// 19、交易席位; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_REPORT_SEAT = "VC_REPORT_SEAT";

		/// <summary>
        /// 20、交易员姓名; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_OPERATOR_NAME = "VC_OPERATOR_NAME";

		/// <summary>
        /// 21、币种; 字段类型 = VARCHAR2; eg:人民币;
        /// </summary>
        public const string C_VC_CURRENCY_NAME = "VC_CURRENCY_NAME";

		/// <summary>
        /// 22、投资类型; 字段类型 = char(1); 1、可交易；a、投机；;
        /// </summary>
        public const string C_C_INVEST_TYPE = "C_INVEST_TYPE";

		/// <summary>
        /// 23、基金名称; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_FUND_NAME = "VC_FUND_NAME";

		/// <summary>
        /// 24、本币币种; 字段类型 = VARCHAR2; eg:ENY;;
        /// </summary>
        public const string C_VC_CURRENCY_NO_BB = "VC_CURRENCY_NO_BB";

		/// <summary>
        /// 25、本币成交金额; 字段类型 = double; 
        /// </summary>
        public const string C_EN_DEAL_BALANCE_BB = "EN_DEAL_BALANCE_BB";

		/// <summary>
        /// 26、成交金额占净值(%); 字段类型 = double; 
        /// </summary>
        public const string C_EN_DEAL_RATIO = "EN_DEAL_RATIO";

		/// <summary>
        /// 27、券商标号; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_BROKER_NO = "VC_BROKER_NO";

		/// <summary>
        /// 28、券商名称; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_BROKER_NAME = "VC_BROKER_NAME";

		/// <summary>
        /// 29、账户组类型; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_GROUPTYPE_NAME = "VC_GROUPTYPE_NAME";

		/// <summary>
        /// 30、账户组; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_GROUP_NAME = "VC_GROUP_NAME";

		/// <summary>
        /// 31、说明; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_INTERNATIONAL_CODE = "VC_INTERNATIONAL_CODE";

		/// <summary>
        /// 32、说明; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_RIC_CODE = "VC_RIC_CODE";

		/// <summary>
        /// 33、说明; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_SEDOL_CODE = "VC_SEDOL_CODE";

		/// <summary>
        /// 34、说明; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_CUSIP_CODE = "VC_CUSIP_CODE";

		/// <summary>
        /// 35、说明; 字段类型 = VARCHAR2; 
        /// </summary>
        public const string C_VC_BLOOMBERG_CODE = "VC_BLOOMBERG_CODE";

		/// <summary>
        /// 36、申报编号; 字段类型 = bigint; 
        /// </summary>
        public const string C_L_REPORT_SERIAL_NO = "L_REPORT_SERIAL_NO";

		/// <summary>
        /// 37、交易市场; 字段类型 = char(1); 1、上交所A；2、深交所A；7、中金所；;
        /// </summary>
        public const string C_C_MARKET_NO = "C_MARKET_NO";

		/// <summary>
        /// 38、发生金额（全价）; 字段类型 = double; 
        /// </summary>
        public const string C_EN_FULL_BALANCE = "EN_FULL_BALANCE";

		/// <summary>
        /// 39、指令下达人; 字段类型 = int; 
        /// </summary>
        public const string C_L_DIRECT_OPERATOR = "L_DIRECT_OPERATOR";

		/// <summary>
        /// 40、代下达人; 字段类型 = int; 
        /// </summary>
        public const string C_L_DIRECT_INSTEAD_OPERATOR = "L_DIRECT_INSTEAD_OPERATOR";

		/// <summary>
        /// 41、是否强平; 字段类型 = char(1); 
        /// </summary>
        public const string C_C_MARGINED_OUT = "C_MARGINED_OUT";

		#endregion

		#region 数据库表字段对应属性
		
		/// <summary>
        /// 01、证券名称; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_STOCK_NAME { get; set; }
		/// <summary>
        /// 02、Comment; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_THIRD_REFF { get; set; }
		/// <summary>
        /// 03、业务分类; 字段类型 = char(1); 1、交易所业务；4、期货业务；I、上交所固定收益平台；;
        /// </summary>
        public char C_BUSIN_CLASS { get; set; }
		/// <summary>
        /// 04、委托序号; 字段类型 = bigint; 
        /// </summary>
        public long L_ENTRUST_SERIAL_NO { get; set; }
		/// <summary>
        /// 05、成交编号; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_DEAL_NO { get; set; }
		/// <summary>
        /// 06、日期; 字段类型 = VARCHAR2; 
        /// </summary>
        public string L_DATE { get; set; }
		/// <summary>
        /// 07、基金编号; 字段类型 = int; 
        /// </summary>
        public int L_FUND_ID { get; set; }
		/// <summary>
        /// 08、组合编号; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_COMBI_NO { get; set; }
		/// <summary>
        /// 09、组合名称; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_COMBI_NAME { get; set; }
		/// <summary>
        /// 10、成交时间; 字段类型 = int; 
        /// </summary>
        public int L_BUSINESS_TIME { get; set; }
		/// <summary>
        /// 11、委托方向; 字段类型 = char(1); 1、买入；2、卖出；3、债券买入；4、债券卖出；5、融资回购；6、融券回购；X、卖出开仓；Y、买入平仓；V、买入开仓；W、卖出平仓；;
        /// </summary>
        public char C_ENTRUST_DIRECTION { get; set; }
		/// <summary>
        /// 12、交易方向(买卖); 字段类型 = VARCHAR2; B、上交所及中金所的买入业务；S、上交所及中金所的卖出业务；0S、深交所的卖出业务；0B、深交所的买入业务；;
        /// </summary>
        public string VC_REPORT_DIRECTION { get; set; }
		/// <summary>
        /// 13、委托方向名称; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_ENTRUSTDIR_NAME { get; set; }
		/// <summary>
        /// 14、成交数量; 字段类型 = bigint; 
        /// </summary>
        public long L_DEAL_AMOUNT { get; set; }
		/// <summary>
        /// 15、成交价格; 字段类型 = double; 
        /// </summary>
        public double EN_DEAL_PRICE { get; set; }
		/// <summary>
        /// 16、成交金额; 字段类型 = double; 
        /// </summary>
        public double EN_DEAL_BALANCE { get; set; }
		/// <summary>
        /// 17、证券代码; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_REPORT_CODE { get; set; }
		/// <summary>
        /// 18、股东代码; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_STOCKHOLDER_ID { get; set; }
		/// <summary>
        /// 19、交易席位; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_REPORT_SEAT { get; set; }
		/// <summary>
        /// 20、交易员姓名; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_OPERATOR_NAME { get; set; }
		/// <summary>
        /// 21、币种; 字段类型 = VARCHAR2; eg:人民币;
        /// </summary>
        public string VC_CURRENCY_NAME { get; set; }
		/// <summary>
        /// 22、投资类型; 字段类型 = char(1); 1、可交易；a、投机；;
        /// </summary>
        public char C_INVEST_TYPE { get; set; }
		/// <summary>
        /// 23、基金名称; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_FUND_NAME { get; set; }
		/// <summary>
        /// 24、本币币种; 字段类型 = VARCHAR2; eg:ENY;;
        /// </summary>
        public string VC_CURRENCY_NO_BB { get; set; }
		/// <summary>
        /// 25、本币成交金额; 字段类型 = double; 
        /// </summary>
        public double EN_DEAL_BALANCE_BB { get; set; }
		/// <summary>
        /// 26、成交金额占净值(%); 字段类型 = double; 
        /// </summary>
        public double EN_DEAL_RATIO { get; set; }
		/// <summary>
        /// 27、券商标号; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_BROKER_NO { get; set; }
		/// <summary>
        /// 28、券商名称; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_BROKER_NAME { get; set; }
		/// <summary>
        /// 29、账户组类型; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_GROUPTYPE_NAME { get; set; }
		/// <summary>
        /// 30、账户组; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_GROUP_NAME { get; set; }
		/// <summary>
        /// 31、说明; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_INTERNATIONAL_CODE { get; set; }
		/// <summary>
        /// 32、说明; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_RIC_CODE { get; set; }
		/// <summary>
        /// 33、说明; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_SEDOL_CODE { get; set; }
		/// <summary>
        /// 34、说明; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_CUSIP_CODE { get; set; }
		/// <summary>
        /// 35、说明; 字段类型 = VARCHAR2; 
        /// </summary>
        public string VC_BLOOMBERG_CODE { get; set; }
		/// <summary>
        /// 36、申报编号; 字段类型 = bigint; 
        /// </summary>
        public long L_REPORT_SERIAL_NO { get; set; }
		/// <summary>
        /// 37、交易市场; 字段类型 = char(1); 1、上交所A；2、深交所A；7、中金所；;
        /// </summary>
        public char C_MARKET_NO { get; set; }
		/// <summary>
        /// 38、发生金额（全价）; 字段类型 = double; 
        /// </summary>
        public double EN_FULL_BALANCE { get; set; }
		/// <summary>
        /// 39、指令下达人; 字段类型 = int; 
        /// </summary>
        public int L_DIRECT_OPERATOR { get; set; }
		/// <summary>
        /// 40、代下达人; 字段类型 = int; 
        /// </summary>
        public int L_DIRECT_INSTEAD_OPERATOR { get; set; }
		/// <summary>
        /// 41、是否强平; 字段类型 = char(1); 
        /// </summary>
        public char C_MARGINED_OUT { get; set; }

		#endregion
	}
}
