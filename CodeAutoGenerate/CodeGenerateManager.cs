using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CodeAutoGenerate
{
    public class CodeGenerateManager : IProject
    {
        public const string SourceFolderName = "Source";
        public const string ResultFolderName = "Result";

        #region ProjectPath

        public string ProjectPath { get; set; }

        public string SourcePath
        {
            get { return Path.Combine(this.ProjectPath, SourceFolderName); }
        }

        public string ResultPath
        {
            get { return Path.Combine(this.ProjectPath, ResultFolderName); }
        }

        #endregion

        #region Life Cycle

        public CodeGenerateManager(string projectPath)
        {
            this.ProjectPath = projectPath;

            this.GenerateList = new List<IFileGenerate>();
            // TODO:

            // 上交所DBF文件：
            this.GenerateList.Add(new GenerateModuleCode_SH(this, "JSMX_SH"));

            // 深交所DBF文件： 
            this.GenerateList.Add(new GenerateModuleCode_SZ(this, "JSMX_SZ"));
            this.GenerateList.Add(new GenerateModuleCode_SZ(this, "CIL"));

            // 其他数据，如：Oracle数据库等；
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "OrderRow"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "TradeRow"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "RealDeal"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "LH_OrderRow"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "MarketRow"));

            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "AShareEODPrices"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "BondFuturesEODPrices"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "ChinaETFPchRedmList"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "CommodityFuturesEODPrices"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "IndexFuturesEODPrices"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "ProjectInfoRow"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "MoneyStoreRow"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "SecurityStoreAccRow"));
            this.GenerateList.Add(new GenerateModuleCode_Custom(this, "TradeRow_O32"));
        }

        /// <summary>
        /// 开始自动生成代码。
        /// </summary>
        public void Start()
        {
            if (this.GenerateList == null || string.IsNullOrEmpty(this.ProjectPath) || !Directory.Exists(this.ProjectPath))
                return;

            foreach (IFileGenerate item in this.GenerateList)
            {
                Console.WriteLine(string.Format("Progress : {0}/{1}", this.GenerateList.IndexOf(item) + 1, this.GenerateList.Count));
                Console.WriteLine(item.ResultFile);
                if (item.Generate())
                {
                    Console.WriteLine("Success!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            Console.WriteLine("按任意键退出！");
            Console.ReadKey();
        }

        #endregion

        protected List<IFileGenerate> GenerateList
        {
            get;
            set;
        }

    }

    public interface IProject
    {
        string ProjectPath { get; set; }

        string SourcePath { get; }

        string ResultPath { get; }
    }
}
