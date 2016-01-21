using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace CodeAutoGenerate
{
    public interface IFileGenerate
    {
        bool Generate();

        string Name { get; set; }

        string NameSpace { get; set; }

        string SourceFile { get; }

        string ResultFile { get; }
    }

    public abstract class FileGenerate : IFileGenerate
    {
        #region Life Cycle

        public FileGenerate(IProject project, string nspace, string name)
        {
            this.TheProject = project;
            this.Name = name;

            if (string.IsNullOrEmpty(nspace))
                this.NameSpace = "Dothan.DBFFileMamager";
            else
                this.NameSpace = nspace;
        }

        public IProject TheProject { get; protected set; }

        #endregion

        #region Name

        /// <summary>
        /// eg: JSMX_SH
        /// </summary>
        public string Name { get; set; }

        protected string Name_CS
        {
            get { return string.Format("{0}.cs", this.Name); }
        }

        protected string Name_TXT
        {
            get { return string.Format("{0}.txt", this.Name); }
        }

        public virtual string SourcePath
        {
            get { return this.TheProject.SourcePath; }
        }

        public virtual string ResultPath
        {
            get { return this.TheProject.ResultPath; }
        }

        public virtual string SourceFile
        {
            get 
            {  
                string file = Path.Combine(this.SourcePath, this.Name_TXT);
                if (File.Exists(file))
                    return file;

                file = Path.Combine(this.TheProject.SourcePath, this.Name_TXT);
                if (File.Exists(file))
                    return file;

                return string.Empty;
            }
        }

        public string ResultFile
        {
            get { return Path.Combine(this.ResultPath, this.Name_CS); }
        }

        public string NameSpace { get; set; }

        #endregion

        public bool Generate()
        {
            if (string.IsNullOrEmpty(this.SourceFile) || !File.Exists(this.SourceFile))
                return false;
            if (string.IsNullOrEmpty(this.ResultFile))
                return false;

            if (!Directory.Exists(this.ResultPath))
                Directory.CreateDirectory(this.ResultPath);

            try
            {
                using (StreamWriter writer = new StreamWriter(this.ResultFile))
                {
                    // write head
                    writer.WriteLine("using System;");
                    writer.WriteLine("using System.Collections.Generic;");
                    writer.WriteLine("using System.Linq;");
                    writer.WriteLine("using System.Text;");
                    writer.WriteLine();
                    writer.WriteLine(string.Format("namespace {0}", this.NameSpace));
                    writer.WriteLine("{");
                    writer.WriteLine(string.Format("    public class {0}", Name));
                    writer.WriteLine("    {");

                    this.GenerateTableColumn(writer);
                    this.GenerateProperty(writer);

                    // write end
                    writer.WriteLine("    }");
                    writer.WriteLine("}");
                }


                return true;
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Source + "]; Exception = " + ee.Message);
                Trace.WriteLine("### " + ee.StackTrace);

                return false;
            }
        }

        protected bool GenerateTableColumn(StreamWriter writer)
        {
            bool flag = true;
            writer.WriteLine("        #region 数据库表字段常量");
            writer.WriteLine();

            try
            {
                using (StreamReader reader = new StreamReader(this.SourceFile))
                {
                    while (true)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                            break;

                        this.GenerateTableColumnItem(writer, line);
                    }
                }
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Source + "]; Exception = " + ee.Message);
                Trace.WriteLine("### " + ee.StackTrace);

                flag = false;
            }

            writer.WriteLine("        #endregion");
            writer.WriteLine("");
            return flag;
        }

        protected bool GenerateProperty(StreamWriter writer)
        {
            bool flag = true;
            writer.WriteLine("        #region 数据库表字段对应属性");
            writer.WriteLine();

            try
            {
                using (StreamReader reader = new StreamReader(this.SourceFile))
                {
                    while (true)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                            break;

                        this.GeneratePropertyItem(writer, line);
                    }
                }
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Source + "]; Exception = " + ee.Message);
                Trace.WriteLine("### " + ee.StackTrace);

                flag = false;
            }

            writer.WriteLine("        #endregion");
            writer.WriteLine("");
            return flag;
        }

        public abstract void GenerateTableColumnItem(StreamWriter write, string line);

        public abstract void GeneratePropertyItem(StreamWriter write, string line);
    }
}
