using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.DBFFileMamager
{
    public class ChinaETFPchRedmList
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
        /// 04、现金差额(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_F_INFO_CASHDIF = "F_INFO_CASHDIF";

        /// <summary>
        /// 05、最小申购赎回单位资产净值(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_F_INFO_MINPRASET = "F_INFO_MINPRASET";

        /// <summary>
        /// 06、预估现金部分(元); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_F_INFO_ESTICASH = "F_INFO_ESTICASH";

        /// <summary>
        /// 07、现金替代比例上限(%); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_F_INFO_CASHSUBUPLIMIT = "F_INFO_CASHSUBUPLIMIT";

        /// <summary>
        /// 08、最小申购赎回单位(份); 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_F_INFO_MINPRUNITS = "F_INFO_MINPRUNITS";

        /// <summary>
        /// 09、申购赎回允许情况; 字段类型 = numeric(1); 
        /// </summary>
        public const string C_F_INFO_PRPERMIT = "F_INFO_PRPERMIT";

        /// <summary>
        /// 10、标的指数成分股数量; 字段类型 = numeric(20,4); 
        /// </summary>
        public const string C_F_INFO_CONNUM = "F_INFO_CONNUM";

        /// <summary>
        /// 11、操作时间; 字段类型 = datetime; 
        /// </summary>
        public const string C_OPDATE = "OPDATE";

        /// <summary>
        /// 12、操作模式; 字段类型 = varchar(1); 	0（新记录）、1（更新/更正）、2（删除），描述对数据库需要进行的操作。对于0和1，记录内容即为最新值；对于2，所有字段内容为空。
        /// </summary>
        public const string C_OPMODE = "OPMODE";

        #endregion

        #region 数据库表字段对应属性

        /// <summary>
        /// 01、对象ID; ; 字段类型 = varchar(100)
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
        /// 04、现金差额(元); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double F_INFO_CASHDIF { get; set; }

        /// <summary>
        /// 05、最小申购赎回单位资产净值(元); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double F_INFO_MINPRASET { get; set; }

        /// <summary>
        /// 06、预估现金部分(元); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double F_INFO_ESTICASH { get; set; }

        /// <summary>
        /// 07、现金替代比例上限(%); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double F_INFO_CASHSUBUPLIMIT { get; set; }

        /// <summary>
        /// 08、最小申购赎回单位(份); ; 字段类型 = numeric(20,4)
        /// </summary>
        public double F_INFO_MINPRUNITS { get; set; }

        /// <summary>
        /// 09、申购赎回允许情况; ; 字段类型 = numeric(1)
        /// </summary>
        public string F_INFO_PRPERMIT { get; set; }

        /// <summary>
        /// 10、标的指数成分股数量; ; 字段类型 = numeric(20,4)
        /// </summary>
        public double F_INFO_CONNUM { get; set; }

        /// <summary>
        /// 11、操作时间; ; 字段类型 = datetime
        /// </summary>
        public DateTime? OPDATE { get; set; }

        /// <summary>
        /// 12、操作模式; 	0（新记录）、1（更新/更正）、2（删除），描述对数据库需要进行的操作。对于0和1，记录内容即为最新值；对于2，所有字段内容为空。; 字段类型 = varchar(1)
        /// </summary>
        public string OPMODE { get; set; }

        #endregion

    }
}
