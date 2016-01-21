using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.ExportData;
using System.Globalization;
using System.ComponentModel;
using System.Threading;
using System.Runtime.InteropServices;

namespace Dothan.ExportCmd
{
    /// <summary>
    /// 定义处理程序委托，用于截获系统的关闭事件。
    /// </summary>
    public delegate bool ConsoleCtrlDelegate(int ctrlType);

    public partial class CmdManager : IOutput
    {
        public CmdManager()
        {
            this.InitExportManager();

            // 截获CTRL+C方法1：
            Console.CancelKeyPress += this.OnCancelKeyPressed;
            // 截获CTRL+C方法2：
            //ConsoleCtrlDelegate consoleDelegate = new ConsoleCtrlDelegate(this.HandlerRoutine);
            //bool ret = SetConsoleCtrlHandler(consoleDelegate, true);
            //if (!ret)
            //{
            //    Console.WriteLine("CancelKeyPress安装事件处理失败!");
            //}
        }

        #region SetConsolCtrHandler

        ///// <summary>
        ///// 导入SetCtrlHandlerHandler API  
        ///// </summary>
        //[DllImport("kernel32.dll")]
        //private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);
        ///// <summary>
        ///// 当用户关闭Console时，系统会发送次消息
        ///// </summary>
        //private const int CTRL_CLOSE_EVENT = 2;
        ///// <summary>
        ///// Ctrl+C，系统会发送次消息  
        ///// </summary>
        //private const int CTRL_C_EVENT = 0;
        ///// <summary>
        ///// Ctrl+break，系统会发送次消息  
        ///// </summary>
        //private const int CTRL_BREAK_EVENT = 1;
        ///// <summary>
        ///// 用户退出（注销），系统会发送次消息  
        ///// </summary>
        //private const int CTRL_LOGOFF_EVENT = 5;
        ///// <summary>
        ///// 系统关闭，系统会发送次消息  
        ///// </summary>
        //private const int CTRL_SHUTDOWN_EVENT = 6;

        ///// <summary>
        ///// 处理程序例程，在这里编写对指定事件的处理程序代码  
        ///// 注意：在VS中调试执行时，在这里设置断点，但不会中断；会提示：无可用源；
        ///// </summary>
        //private bool HandlerRoutine(int ctrlType)
        //{
        //    switch (ctrlType)
        //    {
        //        case CTRL_C_EVENT:
        //            Console.WriteLine("C");
        //            return true; //这里返回true，表示阻止响应系统对该程序的操作  
        //        //break;  
        //        case CTRL_BREAK_EVENT:
        //            Console.WriteLine("BREAK");
        //            break;
        //        case CTRL_CLOSE_EVENT:
        //            Console.WriteLine("CLOSE");
        //            break;
        //        case CTRL_LOGOFF_EVENT:
        //            Console.WriteLine("LOGOFF");
        //            break;
        //        case CTRL_SHUTDOWN_EVENT:
        //            Console.WriteLine("SHUTDOWN");
        //            break;
        //    }
        //    //return true;//表示阻止响应系统对该程序的操作  
        //    return false;//忽略处理，让系统进行默认操作  
        //}  

        #endregion

        public void ExecuteCommand(string[] args)
        {
            try
            {
                if (args[0].Equals("help", StringComparison.OrdinalIgnoreCase))
                    this.ExecuteHelpCommand();
                else if (args[0].Equals("set", StringComparison.OrdinalIgnoreCase))
                    this.ExecuteSetCommand(args.Skip(1).ToArray());
                else if (args[0].Equals("import", StringComparison.OrdinalIgnoreCase))
                    this.ExecuteExport(args.Skip(1).ToArray());
                else if (args[0].Equals("export", StringComparison.OrdinalIgnoreCase))
                    this.ExecuteExport(args.Skip(1).ToArray());
                else if (args[0].Equals("clear", StringComparison.OrdinalIgnoreCase))
                    this.ExecuteClearCommand();
                else if (args[0].Equals("test", StringComparison.OrdinalIgnoreCase))
                    this.ExecuteTestCommand(args.Skip(1).ToArray());
                else if (args[0].Equals("ls", StringComparison.OrdinalIgnoreCase))
                    this.ExecuteLSCommand(args.Skip(1).ToArray());
                else if (args[0].Equals("date", StringComparison.OrdinalIgnoreCase))
                    this.ExecuteDateCommand(args.Skip(1).ToArray());
                else
                    this.ShowErrorInfo("无效的命令！");
            }
            catch (Exception exp)
            {
                this.ShowErrorInfo("命令格式错误！");
                this.Log.E(this, exp);
            }
        }

        /// <summary>
        /// /d
        /// </summary>
        public const string DateParam_d = "/d";
        /// <summary>
        /// date
        /// </summary>
        public const string DateParam_date = "date";
        /// <summary>
        /// on
        /// </summary>
        public const string DateParam_on = "on";

        /// <summary>
        /// /f
        /// </summary>
        public const string FromParam_f = "/f";
        /// <summary>
        /// from
        /// </summary>
        public const string FromParam_from = "from";

        /// <summary>
        /// /t
        /// </summary>
        public const string ToParam_t = "/t";
        /// <summary>
        /// to
        /// </summary>
        public const string ToParam_to = "to";

        /// <summary>
        /// /r : 表示需要重新导入所有数据。
        /// </summary>
        public const string ReExportAll = "/r";

        private void ExecuteHelpCommand(string type = null)
        {
            Console.WriteLine("List of all commands:");
            Console.WriteLine("---------------------------------------------");
            this.ShowCommand("help                     ");
            this.ShowCommandComment("：查看帮助信息");
            this.ShowCommand("ls [/d 日期]             ");
            this.ShowCommandComment("：查看制定日期下的所有的导出数据源项以及目标项");
            this.ShowCommand("date                     ");
            this.ShowCommandComment("：查看操作的默认日期");
            this.ShowCommand("set [date日期][/d 日期]  ");
            this.ShowCommandComment("：设置默认数据采集日期！日期格式：yyyyMMdd (eg: set date 20151224)");
            this.ShowCommand("export [from 导出项][/f 导出项][to 目标项][/t 目标项][on 日期][/d 日期][/r]");
            this.ShowCommandComment("");
            this.ShowCommand("import [to 目标项][/t 目标项][from 导出项][/f 导出项][on 日期][/d 日期][/r]");
            this.ShowCommandComment("");
            this.ShowCommand("                         ");
            this.ShowCommandComment("：[from 导出项][/f 导出项]表示数据是从哪个导出项中获取的，可以通过'ls'命令查询");
            this.ShowCommand("");
            this.ShowCommandComment("：[from all][/f all]表示采集所有的数据到对应的数据远中");
            this.ShowCommand("                         ");
            this.ShowCommandComment("：[to 目标项][/t 目标项]表示采集指定目标的数据，如果参数中没有制定from参数的话，默认表示从所有符合要求的数据远中采集；数据的导入目标可以通过命令'ls'查询");
            this.ShowCommand("                         ");
            this.ShowCommandComment("：[on 日期][/d 日期]表示采集制定日期的数据，默认为上次操作日期，可以通过命令\"date\"命令查询默认日期");
            this.ShowCommand("                         ");
            this.ShowCommandComment("：[/r]表示需要重新导入目标数据");
            this.ShowCommand("CTRL+C 快捷键            ");
            this.ShowCommandComment("：如果正在进行数据导入，则停止导入操作，否则退出应用");
            this.ShowCommand("exit                     ");
            this.ShowCommandComment("：退出");
            Console.WriteLine();
        }

        private void ShowCommand(string cmd)
        {
            ConsoleColor colorBack = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(cmd);
            Console.ForegroundColor = colorBack;
        }

        private void ShowCommandComment(string comment)
        {
            Console.WriteLine(comment);
        }

        private void ExecuteClearCommand()
        {
            Console.Clear();
        }

        private void ExecuteLSCommand(string[] args)
        {
            if (args.Length > 1)
            {
                if (args[0].Equals(DateParam_d, StringComparison.OrdinalIgnoreCase) || 
                    args[0].Equals(DateParam_date, StringComparison.OrdinalIgnoreCase))
                {
                    if (!this.SetDate(args[1], false))
                        return;
                }
                else
                {
                    this.ShowErrorInfo("参数错误！");
                    return;
                }
            }

            Console.Write("TargetDate: ");
            this.ShowDate();
            Console.WriteLine("Export Items:");
            this.ShowExportItemsInfo();
            Console.WriteLine("Import Items:");
            this.ShowTargetItemsName();
        }

        private void ExecuteDateCommand(string[] args)
        {
            this.ShowDate();
        }

        #region ExecuteSetCommand

        private void ExecuteSetCommand(string[] args)
        {
            if (args.Length != 2)
            {
                this.ShowErrorInfo("命令错误，请重新输入!");
                return;
            }

            if (args[0].Equals(DateParam_d, StringComparison.OrdinalIgnoreCase) ||
                args[0].Equals(DateParam_date, StringComparison.OrdinalIgnoreCase))
            {
                this.SetDate(args[1], true);
            }
            else
            {
                this.ShowErrorInfo("无法识别的命令!");
            }
        }

        private bool SetDate(string date, bool showEcho)
        {
            DateTime? dt = this.ParseDateTime(date);
            if (dt != null)
            {
                if (dt.Value.Date != this.TheManager.TargetDate.Date)
                {
                    this.TheManager.TargetDate = dt.Value;
                    if (showEcho)
                        Console.WriteLine("Update Date Successfull!");
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private DateTime? ParseDateTime(string date)
        {
            try { return DateTime.ParseExact(date, "yyyyMMdd", DateTimeFormatInfo.CurrentInfo); }
            catch (Exception) { }

            try 
            { 
                return Convert.ToDateTime(date); 
            }
            catch (Exception exp) 
            {
                this.ShowErrorInfo("日期格式输入错误！");
                this.Log.E(this, exp);

                return null; 
            }
        }

        #endregion

        #region ExecuteShowCommand

        private void ShowDate()
        {
            Console.WriteLine(this.TheManager.TargetDate.ToShortDateString());
            Console.WriteLine();
        }

        private void ShowExportItemsInfo()
        {
            Console.WriteLine(string.Format("{0, -30}{1, -15}{2, -15}", "Name", "DataBaseType", "ExportState"));
            Console.WriteLine("----------------------------------------------------------------------");
            foreach (ImportItem item in this.TheManager.ExportList)
            {
                item.SyncImportState2Local();
                Console.WriteLine(string.Format("{0, -30}{1, -15}{2, -15}", item.TableName, item.SourceType, this.TheManager.GetExportStateInfo(item.ImportState)));
            }
            Console.WriteLine();
        }

        private void ShowTargetItemsName()
        {
            Console.WriteLine("Name");
            Console.WriteLine("----------");
            Console.WriteLine(EImportType.Trade.ToString());
            Console.WriteLine(EImportType.Order.ToString());
            Console.WriteLine(EImportType.Market.ToString());
            Console.WriteLine();
        }

        #endregion

        public void ExecuteExport(string[] args)
        {
            string source = string.Empty;
            string target = string.Empty;
            string date = string.Empty;
            bool reexport = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals(DateParam_on, StringComparison.OrdinalIgnoreCase) ||
                    args[i].Equals(DateParam_d, StringComparison.OrdinalIgnoreCase))
                {
                    date = args[++i];
                }
                else if (args[i].Equals(FromParam_from, StringComparison.OrdinalIgnoreCase) ||
                    args[i].Equals(FromParam_f, StringComparison.OrdinalIgnoreCase))
                {
                    source = args[++i];
                }
                else if (args[i].Equals(ToParam_to, StringComparison.OrdinalIgnoreCase) ||
                    args[i].Equals(ToParam_t, StringComparison.OrdinalIgnoreCase))
                {
                    target = args[++i];
                }
                else if (args[i].Equals(ReExportAll, StringComparison.OrdinalIgnoreCase))
                {
                    reexport = true;
                }
            }

            // 解析Source
            IEnumerable<ImportItem> froms = null;
            if (string.IsNullOrEmpty(source) || source.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                froms = this.TheManager.ExportList;
            }
            else
            {
                ImportItem item = this.TheManager.GetExportItem(source);
                if (item != null)
                {
                    froms = new List<ImportItem> { item };
                }
            }

            if (froms == null || froms.Count() <= 0)
            {
                this.ShowErrorInfo("数据源名称输入错误，请重新输入！");
                return;
            }

            // 解析Target
            IEnumerable<ImportItem> tos = null;
            if (string.IsNullOrEmpty(target) || target.Equals("all", StringComparison.OrdinalIgnoreCase))
                tos = this.TheManager.ExportList;
            else if (target.Equals(EImportType.Trade.ToString(), StringComparison.OrdinalIgnoreCase))
                tos = this.TheManager.GetExportItems(EImportType.Trade);
            else if (target.Equals(EImportType.Order.ToString(), StringComparison.OrdinalIgnoreCase))
                tos = this.TheManager.GetExportItems(EImportType.Order);
            else if (target.Equals(EImportType.Market.ToString(), StringComparison.OrdinalIgnoreCase))
                tos = this.TheManager.GetExportItems(EImportType.Market);

            if (tos == null || tos.Count() <= 0)
            {
                this.ShowErrorInfo("目标数据表不存在，请重新输入！");
                return;
            }

            // 解析Date
            if (!string.IsNullOrEmpty(date))
            {
                if (!this.SetDate(date, false))
                    return;
            }

            // 导入导出操作
            this.ExportItems(froms.Where(o => tos.Contains(o)), reexport);
        }

        private void ExecuteTestCommand(string[] args)
        {
            try
            {
                throw new Exception("log4net Test!");
            }
            catch (System.Exception ex)
            {
                this.TheManager.Log.E(this, ex);
            }
        }

        private void ShowErrorInfo(string msg)
        {
            ConsoleColor colorBack = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = colorBack;
        }

        #region IOutput

        public void Write(string msg)
        {
            Console.Write(msg);
        }

        public void WriteLine(string msg)
        {
            Console.WriteLine(msg);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void Clear()
        {
            Console.Clear();
        }

        #endregion
    }
}
