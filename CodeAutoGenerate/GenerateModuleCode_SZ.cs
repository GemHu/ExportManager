using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CodeAutoGenerate
{
    public class GenerateModuleCode_SZ : FileGenerate
    {
        public const string SZFolderName = "SZ";

        #region Life Cycle

        public GenerateModuleCode_SZ(IProject project, string name)
            : this(project, null, name)
        {

        }

        public GenerateModuleCode_SZ(IProject project, string nspace, string name)
            : base(project, nspace, name)
        {

        }

        #endregion

        public override string SourcePath
        {
            get { return Path.Combine(base.SourcePath, SZFolderName); }
        }

        public override string ResultPath
        {
            get { return Path.Combine(base.ResultPath, SZFolderName); }
        }

        public override void GenerateTableColumnItem(StreamWriter write, string line)
        {
            if (write == null)
                return;

            string[] items = line.Split(new char[] { ' ', '\t' });
            if (items.Length > 4)
            {
                write.WriteLine("        /// <summary>");
                write.WriteLine(string.Format("        /// {0}、{1}; 字段类型 = {2}({3}); {4}", items[0].Trim(), items[2].Trim(), items[3].Trim(), items[4].Trim(), items.Length == 6 ? "备注：" + items[5].Trim() : string.Empty));
                write.WriteLine("        /// </summary>");
                write.WriteLine(string.Format("        public const string C_{0} = \"{0}\";", items[1].Trim()));
                write.WriteLine();
            }
        }

        public override void GeneratePropertyItem(System.IO.StreamWriter write, string line)
        {
            if (write == null)
                return;

            string[] items = line.Split(new char[] { ' ', '\t' });
            if (items.Length > 4)
            {
                string propType = GenerateModuleCode_SH.GetPropertyType(items[3], items[4]);

                write.WriteLine("        /// <summary>");
                write.WriteLine(string.Format("        /// {0}、{1}; type = {2}({3}); {4}", items[0].Trim(), items[2].Trim(), items[3].Trim(), items[4].Trim(), items.Length == 6 ? "备注：" + items[5].Trim() : string.Empty));
                write.WriteLine("        /// </summary>");
                write.WriteLine(string.Format("        public {0} {1} {{ get; set; }}", propType, items[1].Trim()));
                write.WriteLine();
            }
        }
    }
}
