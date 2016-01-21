using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public class MoneyStoreTable : SettleDBHelper
    {
        #region 数据库表字段常量

        /// <summary>
        /// 01、主键; 字段类型 = bigint not null; 
        /// </summary>
        public const string C_idx = "idx";

        /// <summary>
        /// 02、项目的唯一标识，; 字段类型 = bigint; 	共用于资金和证券之间。
        /// </summary>
        public const string C_item_id = "item_id";

        /// <summary>
        /// 03、项目代码; 字段类型 = int not null; 
        /// </summary>
        public const string C_xmdm = "xmdm";

        /// <summary>
        /// 04、资产单元; 字段类型 = varchar(20) not null; 
        /// </summary>
        public const string C_zcdy = "zcdy";

        /// <summary>
        /// 05、组合代码; 字段类型 = varchar(20) not null; 
        /// </summary>
        public const string C_zhdm = "zhdm";

        /// <summary>
        /// 06、策略号; 字段类型 = int not null; 
        /// </summary>
        public const string C_policy_id = "policy_id";

        /// <summary>
        /// 07、委托ID; 字段类型 = int; 
        /// </summary>
        public const string C_order_id = "order_id";

        /// <summary>
        /// 08、交易ID; 字段类型 = int; 
        /// </summary>
        public const string C_trade_id = "trade_id";

        /// <summary>
        /// 09、证券代码。; 字段类型 = int; 	主要是为了记录冻结来源，方便解冻
        /// </summary>
        public const string C_security_id = "security_id";

        /// <summary>
        /// 10、资金数量; 字段类型 = double; 
        /// </summary>
        public const string C_money = "money";

        /// <summary>
        /// 11、获得日期; 字段类型 = char(8); 
        /// </summary>
        public const string C_date_str = "date_str";

        /// <summary>
        /// 12、0=not frozed,1=frozed; 字段类型 = int; 
        /// </summary>
        public const string C_froze = "froze";

        /// <summary>
        /// 13、冻结起始日期; 字段类型 = char(8); 
        /// </summary>
        public const string C_froze_date = "froze_date";

        /// <summary>
        /// 14、解冻日期; 字段类型 = char(8); 
        /// </summary>
        public const string C_melt_date = "melt_date";

        /// <summary>
        /// 15、冻结原因,; 字段类型 = int; 	1=期货保证金
        /// </summary>
        public const string C_froze_reason = "froze_reason";

        /// <summary>
        /// 16、冻结源; 字段类型 = varchar(20); 	froze_reason为1时，冻结源为期货ID
        /// </summary>
        public const string C_froze_source = "froze_source";

        #endregion

    }

}
