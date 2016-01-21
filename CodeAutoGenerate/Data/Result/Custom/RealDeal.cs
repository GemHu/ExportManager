using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.DBFFileMamager
{
    public class RealDeal
    {
        #region 数据库表字段常量

        /// <summary>
        /// 01、成交日期; 字段类型 = NUMBER(8); 
        /// </summary>
        public const string C_L_DATE = "L_DATE";

        /// <summary>
        /// 02、成交序号; 字段类型 = NUMBER(10); 	从SEQREALDEALNO取值。SEQREALDEALNO每天从1开始
        /// </summary>
        public const string C_L_REALDEAL_SERIAL_NO = "L_REALDEAL_SERIAL_NO";

        /// <summary>
        /// 03、委托序号; 字段类型 = NUMBER(10); 
        /// </summary>
        public const string C_L_ENTRUST_SERIAL_NO = "L_ENTRUST_SERIAL_NO";

        /// <summary>
        /// 04、基金序号; 字段类型 = NUMBER(8); 
        /// </summary>
        public const string C_L_FUND_ID = "L_FUND_ID";

        /// <summary>
        /// 05、投资组合序号; 字段类型 = NUMBER(10); 
        /// </summary>
        public const string C_L_BASECOMBI_ID = "L_BASECOMBI_ID";

        /// <summary>
        /// 06、交易市场编号; 字段类型 = CHAR(1 BYTE); 
        /// </summary>
        public const string C_C_MARKET_NO = "C_MARKET_NO";

        /// <summary>
        /// 07、股东代码; 字段类型 = VARCHAR2(30 BYTE); 
        /// </summary>
        public const string C_VC_STOCKHOLDER_ID = "VC_STOCKHOLDER_ID";

        /// <summary>
        /// 08、申报席位; 字段类型 = VARCHAR2(6 BYTE); 
        /// </summary>
        public const string C_VC_REPORT_SEAT = "VC_REPORT_SEAT";

        /// <summary>
        /// 09、申报代码; 字段类型 = VARCHAR2(20 BYTE); 
        /// </summary>
        public const string C_VC_REPORT_CODE = "VC_REPORT_CODE";

        /// <summary>
        /// 10、证券类别; 字段类型 = CHAR(1 BYTE); 
        /// </summary>
        public const string C_C_STOCK_TYPE = "C_STOCK_TYPE";

        /// <summary>
        /// 11、委托方向; 字段类型 = CHAR(1 BYTE); 
        /// </summary>
        public const string C_C_ENTRUST_DIRECTION = "C_ENTRUST_DIRECTION";

        /// <summary>
        /// 12、成交时间; 字段类型 = NUMBER(6); 
        /// </summary>
        public const string C_L_BUSINESS_TIME = "L_BUSINESS_TIME";

        /// <summary>
        /// 13、成交数量; 字段类型 = NUMBER(12); 
        /// </summary>
        public const string C_L_DEAL_AMOUNT = "L_DEAL_AMOUNT";

        /// <summary>
        /// 14、成交价格; 字段类型 = NUMBER(14,8); 
        /// </summary>
        public const string C_EN_DEAL_PRICE = "EN_DEAL_PRICE";

        /// <summary>
        /// 15、指令序号; 字段类型 = NUMBER(10); 	用于和指令表关联。如果对应不上，则填0
        /// </summary>
        public const string C_L_DAILY_INSTRUCTION_NO = "L_DAILY_INSTRUCTION_NO";

        /// <summary>
        /// 16、指令修改次序; 字段类型 = NUMBER(10); 	用于和指令表关联。如果对应不上，则填0
        /// </summary>
        public const string C_L_INDEX_DAILY_MODIFY = "L_INDEX_DAILY_MODIFY";

        /// <summary>
        /// 17、申报编号; 字段类型 = NUMBER(10); 
        /// </summary>
        public const string C_L_REPORT_SERIAL_NO = "L_REPORT_SERIAL_NO";

        /// <summary>
        /// 18、证券内码; 字段类型 = VARCHAR2(8 BYTE); 
        /// </summary>
        public const string C_VC_INTER_CODE = "VC_INTER_CODE";

        /// <summary>
        /// 19、交割日期; 字段类型 = NUMBER(8); 	用于QDII业务，成交回报中有这 交割日期字段
        /// </summary>
        public const string C_L_SETTLE_DATE = "L_SETTLE_DATE";

        /// <summary>
        /// 20、平仓类型; 字段类型 = CHAR(1 BYTE); 	用于期货的平仓业务：0:开仓；1：普通平仓；2：强制平仓；3：到期交割；4：平昨仓；5：平今仓
        /// </summary>
        public const string C_C_CLOSE_TYPE = "C_CLOSE_TYPE";

        /// <summary>
        /// 21、交易费; 字段类型 = NUMBER(12,2); 
        /// </summary>
        public const string C_EN_FEE_JY = "EN_FEE_JY";

        /// <summary>
        /// 22、印花税; 字段类型 = NUMBER(12,2); 
        /// </summary>
        public const string C_EN_FEE_YH = "EN_FEE_YH";

        /// <summary>
        /// 23、过户费; 字段类型 = NUMBER(12,2); 
        /// </summary>
        public const string C_EN_FEE_GH = "EN_FEE_GH";

        /// <summary>
        /// 24、佣金; 字段类型 = NUMBER(12,2); 
        /// </summary>
        public const string C_EN_FEE_YJ = "EN_FEE_YJ";

        /// <summary>
        /// 25、经手费; 字段类型 = NUMBER(12,2); 
        /// </summary>
        public const string C_EN_FEE_JS = "EN_FEE_JS";

        /// <summary>
        /// 26、证管费; 字段类型 = NUMBER(12,2); 
        /// </summary>
        public const string C_EN_FEE_ZG = "EN_FEE_ZG";

        /// <summary>
        /// 27、其他费用; 字段类型 = NUMBER(12,2); 
        /// </summary>
        public const string C_EN_FEE_QT = "EN_FEE_QT";

        /// <summary>
        /// 28、结算费; 字段类型 = NUMBER(12,2); 
        /// </summary>
        public const string C_EN_FEE_JS2 = "EN_FEE_JS2";

        /// <summary>
        /// 29、交割费; 字段类型 = NUMBER(12,2); 
        /// </summary>
        public const string C_EN_FEE_JG = "EN_FEE_JG";

        /// <summary>
        /// 30、时间戳; 字段类型 = DATE; 	缺省值为 数据库机器时间 sysdate
        /// </summary>
        public const string C_D_DATETIME = "D_DATETIME";

        /// <summary>
        /// 31、**成交编号; 字段类型 = VARCHAR2(64 BYTE); 	以前是整数l_deal_no。 从QDII版本开始改的，填写交易所返回的成交编号，对于分仓，采用委托查询方式生成成交记录的，填写'FC'+SEQREALDEALNO.NEXTVAL
        /// </summary>
        public const string C_VC_DEAL_NO = "VC_DEAL_NO";

        /// <summary>
        /// 32、市场成交日期; 字段类型 = NUMBER(8); 	对于当地市场来说的成交日期。和业务上的成交日期可能不同。比如，对于QDII基金，某个市场T日的成交数据，可能算作基金T+1业务的。
        /// </summary>
        public const string C_L_MARKET_DATE = "L_MARKET_DATE";

        /// <summary>
        /// 33、撤销时间; 字段类型 = NUMBER(6); 
        /// </summary>
        public const string C_C_CANCEL_TIME = "C_CANCEL_TIME";

        /// <summary>
        /// 34、数据来源; 字段类型 = CHAR(1 BYTE); 	1：接口生成；2：手工录入
        /// </summary>
        public const string C_C_SOURCE = "C_SOURCE";

        /// <summary>
        /// 35、操作员; 字段类型 = NUMBER(8); 	手工录入成交回报的操作员
        /// </summary>
        public const string C_L_OPERATOR_NO = "L_OPERATOR_NO";

        /// <summary>
        /// 36、配对标志; 字段类型 = CHAR(1 BYTE); 	0：未配对；1：配对成功；2：配对失败；对应境外的成交回报，要事后要人工和券商的数据配对
        /// </summary>
        public const string C_C_MATCH_FLAG = "C_MATCH_FLAG";

        /// <summary>
        /// 37、有效状态; 字段类型 = CHAR(1 BYTE); 	1：有效；2：已撤销；QDII业务，有券商发起撤销成交的。对应境外的成交回报，事后要人工和券商的数据配对，配对不上也要撤销
        /// </summary>
        public const string C_C_VALID = "C_VALID";

        /// <summary>
        /// 38、券商编号; 字段类型 = VARCHAR2(16 BYTE); 
        /// </summary>
        public const string C_VC_BROKER_NO = "VC_BROKER_NO";

        /// <summary>
        /// 39、业务分类; 字段类型 = CHAR(1 BYTE); 
        /// </summary>
        public const string C_C_BUSIN_CLASS = "C_BUSIN_CLASS";

        /// <summary>
        /// 40、成交撤销序号; 字段类型 = NUMBER(10); 	撤销成交时将被撤成交的序号填入
        /// </summary>
        public const string C_L_REALDEAL_CANCEL_NO = "L_REALDEAL_CANCEL_NO";

        /// <summary>
        /// 41、外部处理标志; 字段类型 = CHAR(1 BYTE); 	用于外部成交接口，需要反馈成交信息给外部系统，接口程序读取本表中未处理过的记录给外部系统，然后打上处理标志‘1’
        /// </summary>
        public const string C_C_EXTERNAL_DEAL_FLAG = "C_EXTERNAL_DEAL_FLAG";

        /// <summary>
        /// 42、固定发送标示; 字段类型 = CHAR(1 BYTE); 
        /// </summary>
        public const string C_C_FIXSEND_FLAG = "C_FIXSEND_FLAG";

        /// <summary>
        /// 43、交易选项; 字段类型 = CHAR(1 BYTE); 
        /// </summary>
        public const string C_C_TRADE_OPTION = "C_TRADE_OPTION";

        /// <summary>
        /// 44、真实交易费; 字段类型 = NUMBER(19,2); 
        /// </summary>
        public const string C_EN_FEE_JY_TRUE = "EN_FEE_JY_TRUE";

        /// <summary>
        /// 45、真实佣金; 字段类型 = NUMBER(19,2); 
        /// </summary>
        public const string C_EN_FEE_YJ_TRUE = "EN_FEE_YJ_TRUE";

        /// <summary>
        /// 46、总过户费; 字段类型 = NUMBER(19,2); 
        /// </summary>
        public const string C_EN_FEE_GH_TOTAL = "EN_FEE_GH_TOTAL";

        /// <summary>
        /// 47、金额; 字段类型 = NUMBER(18,2); 
        /// </summary>
        public const string C_EN_BALANCE = "EN_BALANCE";

        /// <summary>
        /// 48、保证金; 字段类型 = CHAR(1 BYTE); 
        /// </summary>
        public const string C_C_MARGINED_OUT = "C_MARGINED_OUT";

        #endregion

        #region 数据库表字段对应属性

        /// <summary>
        /// 01、成交日期; ; 字段类型 = NUMBER(8)
        /// </summary>
        public string L_DATE { get; set; }

        /// <summary>
        /// 02、成交序号; 	从SEQREALDEALNO取值。SEQREALDEALNO每天从1开始; 字段类型 = NUMBER(10)
        /// </summary>
        public string L_REALDEAL_SERIAL_NO { get; set; }

        /// <summary>
        /// 03、委托序号; ; 字段类型 = NUMBER(10)
        /// </summary>
        public string L_ENTRUST_SERIAL_NO { get; set; }

        /// <summary>
        /// 04、基金序号; ; 字段类型 = NUMBER(8)
        /// </summary>
        public string L_FUND_ID { get; set; }

        /// <summary>
        /// 05、投资组合序号; ; 字段类型 = NUMBER(10)
        /// </summary>
        public string L_BASECOMBI_ID { get; set; }

        /// <summary>
        /// 06、交易市场编号; ; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_MARKET_NO { get; set; }

        /// <summary>
        /// 07、股东代码; ; 字段类型 = VARCHAR2(30 BYTE)
        /// </summary>
        public string VC_STOCKHOLDER_ID { get; set; }

        /// <summary>
        /// 08、申报席位; ; 字段类型 = VARCHAR2(6 BYTE)
        /// </summary>
        public string VC_REPORT_SEAT { get; set; }

        /// <summary>
        /// 09、申报代码; ; 字段类型 = VARCHAR2(20 BYTE)
        /// </summary>
        public string VC_REPORT_CODE { get; set; }

        /// <summary>
        /// 10、证券类别; ; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_STOCK_TYPE { get; set; }

        /// <summary>
        /// 11、委托方向; ; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_ENTRUST_DIRECTION { get; set; }

        /// <summary>
        /// 12、成交时间; ; 字段类型 = NUMBER(6)
        /// </summary>
        public string L_BUSINESS_TIME { get; set; }

        /// <summary>
        /// 13、成交数量; ; 字段类型 = NUMBER(12)
        /// </summary>
        public string L_DEAL_AMOUNT { get; set; }

        /// <summary>
        /// 14、成交价格; ; 字段类型 = NUMBER(14,8)
        /// </summary>
        public double EN_DEAL_PRICE { get; set; }

        /// <summary>
        /// 15、指令序号; 	用于和指令表关联。如果对应不上，则填0; 字段类型 = NUMBER(10)
        /// </summary>
        public string L_DAILY_INSTRUCTION_NO { get; set; }

        /// <summary>
        /// 16、指令修改次序; 	用于和指令表关联。如果对应不上，则填0; 字段类型 = NUMBER(10)
        /// </summary>
        public string L_INDEX_DAILY_MODIFY { get; set; }

        /// <summary>
        /// 17、申报编号; ; 字段类型 = NUMBER(10)
        /// </summary>
        public string L_REPORT_SERIAL_NO { get; set; }

        /// <summary>
        /// 18、证券内码; ; 字段类型 = VARCHAR2(8 BYTE)
        /// </summary>
        public string VC_INTER_CODE { get; set; }

        /// <summary>
        /// 19、交割日期; 	用于QDII业务，成交回报中有这 交割日期字段; 字段类型 = NUMBER(8)
        /// </summary>
        public string L_SETTLE_DATE { get; set; }

        /// <summary>
        /// 20、平仓类型; 	用于期货的平仓业务：0:开仓；1：普通平仓；2：强制平仓；3：到期交割；4：平昨仓；5：平今仓; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_CLOSE_TYPE { get; set; }

        /// <summary>
        /// 21、交易费; ; 字段类型 = NUMBER(12,2)
        /// </summary>
        public double EN_FEE_JY { get; set; }

        /// <summary>
        /// 22、印花税; ; 字段类型 = NUMBER(12,2)
        /// </summary>
        public double EN_FEE_YH { get; set; }

        /// <summary>
        /// 23、过户费; ; 字段类型 = NUMBER(12,2)
        /// </summary>
        public double EN_FEE_GH { get; set; }

        /// <summary>
        /// 24、佣金; ; 字段类型 = NUMBER(12,2)
        /// </summary>
        public double EN_FEE_YJ { get; set; }

        /// <summary>
        /// 25、经手费; ; 字段类型 = NUMBER(12,2)
        /// </summary>
        public double EN_FEE_JS { get; set; }

        /// <summary>
        /// 26、证管费; ; 字段类型 = NUMBER(12,2)
        /// </summary>
        public double EN_FEE_ZG { get; set; }

        /// <summary>
        /// 27、其他费用; ; 字段类型 = NUMBER(12,2)
        /// </summary>
        public double EN_FEE_QT { get; set; }

        /// <summary>
        /// 28、结算费; ; 字段类型 = NUMBER(12,2)
        /// </summary>
        public double EN_FEE_JS2 { get; set; }

        /// <summary>
        /// 29、交割费; ; 字段类型 = NUMBER(12,2)
        /// </summary>
        public double EN_FEE_JG { get; set; }

        /// <summary>
        /// 30、时间戳; 	缺省值为 数据库机器时间 sysdate; 字段类型 = DATE
        /// </summary>
        public string D_DATETIME { get; set; }

        /// <summary>
        /// 31、**成交编号; 	以前是整数l_deal_no。 从QDII版本开始改的，填写交易所返回的成交编号，对于分仓，采用委托查询方式生成成交记录的，填写'FC'+SEQREALDEALNO.NEXTVAL; 字段类型 = VARCHAR2(64 BYTE)
        /// </summary>
        public string VC_DEAL_NO { get; set; }

        /// <summary>
        /// 32、市场成交日期; 	对于当地市场来说的成交日期。和业务上的成交日期可能不同。比如，对于QDII基金，某个市场T日的成交数据，可能算作基金T+1业务的。; 字段类型 = NUMBER(8)
        /// </summary>
        public string L_MARKET_DATE { get; set; }

        /// <summary>
        /// 33、撤销时间; ; 字段类型 = NUMBER(6)
        /// </summary>
        public string C_CANCEL_TIME { get; set; }

        /// <summary>
        /// 34、数据来源; 	1：接口生成；2：手工录入; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_SOURCE { get; set; }

        /// <summary>
        /// 35、操作员; 	手工录入成交回报的操作员; 字段类型 = NUMBER(8)
        /// </summary>
        public string L_OPERATOR_NO { get; set; }

        /// <summary>
        /// 36、配对标志; 	0：未配对；1：配对成功；2：配对失败；对应境外的成交回报，要事后要人工和券商的数据配对; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_MATCH_FLAG { get; set; }

        /// <summary>
        /// 37、有效状态; 	1：有效；2：已撤销；QDII业务，有券商发起撤销成交的。对应境外的成交回报，事后要人工和券商的数据配对，配对不上也要撤销; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_VALID { get; set; }

        /// <summary>
        /// 38、券商编号; ; 字段类型 = VARCHAR2(16 BYTE)
        /// </summary>
        public string VC_BROKER_NO { get; set; }

        /// <summary>
        /// 39、业务分类; ; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_BUSIN_CLASS { get; set; }

        /// <summary>
        /// 40、成交撤销序号; 	撤销成交时将被撤成交的序号填入; 字段类型 = NUMBER(10)
        /// </summary>
        public string L_REALDEAL_CANCEL_NO { get; set; }

        /// <summary>
        /// 41、外部处理标志; 	用于外部成交接口，需要反馈成交信息给外部系统，接口程序读取本表中未处理过的记录给外部系统，然后打上处理标志‘1’; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_EXTERNAL_DEAL_FLAG { get; set; }

        /// <summary>
        /// 42、固定发送标示; ; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_FIXSEND_FLAG { get; set; }

        /// <summary>
        /// 43、交易选项; ; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_TRADE_OPTION { get; set; }

        /// <summary>
        /// 44、真实交易费; ; 字段类型 = NUMBER(19,2)
        /// </summary>
        public double EN_FEE_JY_TRUE { get; set; }

        /// <summary>
        /// 45、真实佣金; ; 字段类型 = NUMBER(19,2)
        /// </summary>
        public double EN_FEE_YJ_TRUE { get; set; }

        /// <summary>
        /// 46、总过户费; ; 字段类型 = NUMBER(19,2)
        /// </summary>
        public double EN_FEE_GH_TOTAL { get; set; }

        /// <summary>
        /// 47、金额; ; 字段类型 = NUMBER(18,2)
        /// </summary>
        public double EN_BALANCE { get; set; }

        /// <summary>
        /// 48、保证金; ; 字段类型 = CHAR(1 BYTE)
        /// </summary>
        public char C_MARGINED_OUT { get; set; }

        #endregion

    }
}
