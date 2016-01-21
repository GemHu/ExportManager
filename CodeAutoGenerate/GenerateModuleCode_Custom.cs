using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CodeAutoGenerate
{
    public class GenerateModuleCode_Custom : FileGenerate
    {
        public const string CustomPathName = "Custom";

        #region Life Cycle

        public GenerateModuleCode_Custom(IProject project, string name)
            : this(project, null, name)
        {
        }

        public GenerateModuleCode_Custom(IProject project, string nspace, string name)
            : base(project, nspace, name)
        {
        }

        #endregion

        public override string SourcePath
        {
            get { return Path.Combine(base.SourcePath, CustomPathName); }
        }

        public override string ResultPath
        {
            get { return Path.Combine(base.ResultPath, CustomPathName); }
        }

        public override void GenerateTableColumnItem(System.IO.StreamWriter write, string line)
        {
            string[] items = line.Split(new char[] { '|' });
            if (items.Length >= 4)
            {
                write.WriteLine("        /// <summary>");
                write.WriteLine(string.Format("        /// {0}、{1}; 字段类型 = {2}; {3}", items[0].Trim(), items[3].Trim(), items[2].Trim(), items.Length == 5 ? items[4] : string.Empty));
                write.WriteLine("        /// </summary>");
                write.WriteLine(string.Format("        public const string C_{0} = \"{0}\";", items[1].Trim()));
                write.WriteLine();
            }
        }

        public override void GeneratePropertyItem(System.IO.StreamWriter write, string line)
        {
            string[] items = line.Split(new char[] { '|' });
            if (items.Length >= 4)
            {
                string type;
                if (items[2].Trim().StartsWith("int", StringComparison.OrdinalIgnoreCase))
                    type = "int";
                else if (items[2].Trim().StartsWith("char(1)", StringComparison.OrdinalIgnoreCase) || items[2].Trim().StartsWith("CHAR(1 BYTE)", StringComparison.OrdinalIgnoreCase))
                    type = "char";
                else if (items[2].Trim().StartsWith("bigint", StringComparison.OrdinalIgnoreCase))
                    type = "long";
                else if (items[2].Trim().StartsWith("float", StringComparison.OrdinalIgnoreCase))
                    type = "double";
                else if (items[2].Trim().StartsWith("double", StringComparison.OrdinalIgnoreCase))
                    type = "double";
                else if (items[2].Trim().StartsWith("datetime", StringComparison.OrdinalIgnoreCase))
                    type = "DateTime?";
                else if (items[2].Trim().StartsWith("num", StringComparison.OrdinalIgnoreCase) && (items[2].Split(new char[]{','}).Length == 2))
                    type = "double";
                else
                    type = "string";

                //string propertyName = items[1].Trim();
                string propertyName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(items[1].Trim());

                write.WriteLine("        /// <summary>");
                write.WriteLine(string.Format("        /// {0}、{1}; {2}; 字段类型 = {3}", items[0].Trim(), items[3].Trim(), items.Length == 5 ? items[4] : string.Empty, items[2].Trim()));
                write.WriteLine("        /// </summary>");
                write.WriteLine(string.Format("        public {0} {1} {{ get; set; }}", type, propertyName));
                write.WriteLine();
            }
        }
    }
}
