using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.DBFFileMamager
{
    public class ProjectInfoRow
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
        /// 03、项目名称; 字段类型 = varchar(20); 
        /// </summary>
        public const string C_project_name = "project_name";

        /// <summary>
        /// 04、资产单元; 字段类型 = varchar(20) not null; 
        /// </summary>
        public const string C_zcdy = "zcdy";

        /// <summary>
        /// 05、组合代码; 字段类型 = varchar(20) not null; 
        /// </summary>
        public const string C_zhdm = "zhdm";

        #endregion

        #region 数据库表字段对应属性

        /// <summary>
        /// 01、主键; ; 字段类型 = bigint not null
        /// </summary>
        public long Idx { get; set; }

        /// <summary>
        /// 02、项目代码; ; 字段类型 = int not null
        /// </summary>
        public int Xmdm { get; set; }

        /// <summary>
        /// 03、项目名称; ; 字段类型 = varchar(20)
        /// </summary>
        public string Project_Name { get; set; }

        /// <summary>
        /// 04、资产单元; ; 字段类型 = varchar(20) not null
        /// </summary>
        public string Zcdy { get; set; }

        /// <summary>
        /// 05、组合代码; ; 字段类型 = varchar(20) not null
        /// </summary>
        public string Zhdm { get; set; }

        #endregion

    }
}
