using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public class SecurityStoreAccTable : SettleDBHelper
    {
        #region 数据库表字段常量

        /// <summary>
        /// 01、主键; 字段类型 = bigint not null; 
        /// </summary>
        public const string C_idx = "idx";

        /// <summary>
        /// 02、项目代码; 字段类型 = int not null; 
        /// </summary>
        public const string C_xmdm = "xmdm";

        /// <summary>
        /// 03、资产单元; 字段类型 = varchar(20) not null; 
        /// </summary>
        public const string C_zcdy = "zcdy";

        /// <summary>
        /// 04、组合代码; 字段类型 = varchar(20) not null; 
        /// </summary>
        public const string C_zhdm = "zhdm";

        /// <summary>
        /// 05、策略编号; 字段类型 = int not null; 
        /// </summary>
        public const string C_policy_id = "policy_id";

        /// <summary>
        /// 06、汇总日; 字段类型 = char(8); 
        /// </summary>
        public const string C_date_str = "date_str";

        /// <summary>
        /// 07、证券代码; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_security_id = "security_id";

        /// <summary>
        /// 08、方向; 字段类型 = double; 	0=多头，1=空头
        /// </summary>
        public const string C_long_num = "long_num";

        /// <summary>
        /// 09、数量; 字段类型 = double; 
        /// </summary>
        public const string C_short_num = "short_num";

        /// <summary>
        /// 10、; 字段类型 = double; 
        /// </summary>
        public const string C_froze_num = "froze_num";

        /// <summary>
        /// 11、; 字段类型 = double; 
        /// </summary>
        public const string C_hedge_num = "hedge_num";

        /// <summary>
        /// 12、; 字段类型 = double; 
        /// </summary>
        public const string C_long_balance = "long_balance";

        /// <summary>
        /// 13、总金额; 字段类型 = double; 
        /// </summary>
        public const string C_short_balance = "short_balance";

        /// <summary>
        /// 14、; 字段类型 = double; 
        /// </summary>
        public const string C_froze_balance = "froze_balance";

        #endregion

    }

}
