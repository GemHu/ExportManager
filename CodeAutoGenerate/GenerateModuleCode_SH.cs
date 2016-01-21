using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CodeAutoGenerate
{
    public class GenerateModuleCode_SH : FileGenerate
    {
        public const string SHFolderName = "SH";

        #region Life Cycle

        public GenerateModuleCode_SH(IProject project, string name)
            : this(project, null, name)
        {

        }

        public GenerateModuleCode_SH(IProject project, string nspace, string name)
            : base(project, nspace, name)
        {

        }

        #endregion

        public override string SourcePath
        {
            get { return Path.Combine(base.SourcePath, SHFolderName); }
        }

        public override string ResultPath
        {
            get { return Path.Combine(base.ResultPath, SHFolderName); }
        }

        public override void GenerateTableColumnItem(System.IO.StreamWriter write, string line)
        {
            string[] items = line.Split(new char[] { ' ', '\t' });
            if (items.Length > 4)
            {
                write.WriteLine("        /// <summary>");
                write.WriteLine(string.Format("        /// {0}、{1}; 字段类型 = {2}({3});", items[0].Trim(), items[items.Length - 1].Trim(), items[2].Trim(), items[3].Trim()));
                write.WriteLine("        /// </summary>");
                write.WriteLine(string.Format("        public const string C_{0} = \"{0}\";", items[1].Trim()));
                write.WriteLine();
            }
        }

        public override void GeneratePropertyItem(System.IO.StreamWriter write, string line)
        {
            string[] items = line.Split(new char[] { ' ', '\t' });
            if (items.Length > 4)
            {
                string propType = GenerateModuleCode_SH.GetPropertyType(items[2], items[3]);

                write.WriteLine("        /// <summary>");
                write.WriteLine(string.Format("        /// {0}、{1}; type = {2}({3});", items[0].Trim(), items[items.Length - 1].Trim(), items[2].Trim(), items[3].Trim()));
                write.WriteLine("        /// </summary>");
                write.WriteLine(string.Format("        public {0} {1} {{ get; set; }}", propType, items[1].Trim()));
                write.WriteLine();
            }
        }

        public static string GetPropertyType(string columnType, string columnLength)
        {
            string propType = "string";
            if (columnType == null || columnLength == null)
                return propType;

            columnType = columnType.Trim();
            columnLength = columnLength.Trim();

            if (columnType.Equals("Character", StringComparison.OrdinalIgnoreCase) || columnType.Equals("C", StringComparison.OrdinalIgnoreCase))
            {
                if (columnLength == "1")
                    propType = "char";
            }
            else if (columnType.Equals("Numeric", StringComparison.OrdinalIgnoreCase) || columnType.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                if (columnLength.Split(',').Length == 1)
                    propType = "int";
                else
                    propType = "double";
            }

            return propType;
        }
    }
}
