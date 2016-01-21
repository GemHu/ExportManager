using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.ExportData
{
	/// <summary>
    /// 量化系统项目信息。
    /// </summary>
 	public class ProjectInfo_LH
	{
		#region 数据库表字段常量

		/// <summary>
        /// 01、项目ID; 字段类型 = int NOT NULL IDENTITY(1,1); 
        /// </summary>
        public const string C_id = "id";

		/// <summary>
        /// 02、项目名称; 字段类型 = varchar(60) NOT NULL; 
        /// </summary>
        public const string C_projName = "projName";

		/// <summary>
        /// 03、项目代码; 字段类型 = int NOT NULL; 
        /// </summary>
        public const string C_xmdm = "xmdm";

		/// <summary>
        /// 04、资金代码; 字段类型 = int NOT NULL; 
        /// </summary>
        public const string C_zjdm = "zjdm";

		/// <summary>
        /// 05、组合代码; 字段类型 = int NOT NULL; 
        /// </summary>
        public const string C_zmdm = "zmdm";

		/// <summary>
        /// 06、股东代码; 字段类型 = varchar(20) NOT NULL; 
        /// </summary>
        public const string C_gddm = "gddm";

		/// <summary>
        /// 07、市场; 字段类型 = char(1) NOT NULL; 
        /// </summary>
        public const string C_sc = "sc";

		#endregion

		#region 数据库表字段对应属性
		
		/// <summary>
        /// 01、项目ID; 字段类型 = int NOT NULL IDENTITY(1,1); 
        /// </summary>
        public int Id { get; set; }
		/// <summary>
        /// 02、项目名称; 字段类型 = varchar(60) NOT NULL; 
        /// </summary>
        public string Projname { get; set; }
		/// <summary>
        /// 03、项目代码; 字段类型 = int NOT NULL; 
        /// </summary>
        public int Xmdm { get; set; }
		/// <summary>
        /// 04、资金代码; 字段类型 = int NOT NULL; 
        /// </summary>
        public int Zjdm { get; set; }
		/// <summary>
        /// 05、组合代码; 字段类型 = int NOT NULL; 
        /// </summary>
        public int Zmdm { get; set; }
		/// <summary>
        /// 06、股东代码; 字段类型 = varchar(20) NOT NULL; 
        /// </summary>
        public string Gddm { get; set; }
		/// <summary>
        /// 07、市场; 字段类型 = char(1) NOT NULL; 
        /// </summary>
        public char Sc { get; set; }

		#endregion
	}
}
