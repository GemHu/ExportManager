using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.ExportData;
using System.ComponentModel;
using System.Diagnostics;
using Dothan.DzHelpers;

namespace Dothan.ExportCmd
{
    public partial class CmdManager
    {

        private void InitExportManager()
        {
            this.TheManager = new ExportManager(this);
            this.TheManager.ItemExportingEvent += this.OnItemStartExporting;
            this.TheManager.ItemExportedEvent += this.OnItemEndExporting;
            this.TheManager.ItemExportException += this.OnItemExportingException;
        }

        protected ExportManager TheManager
        {
            get;
            set;
        }

        protected ILog Log
        {
            get { return this.TheManager.Log; }
        }

        private void ExportItems(IEnumerable<ImportItem> items, bool reExport)
        {
            if (items == null || items.Count() <= 0)
                return;

            try
            {
                // 如果需要重新导入，则首先需要先清除已有数据
                if (reExport)
                    this.TheManager.ClearAllInfo(); ;

                // 初始化中断状态；
                this.StopToExport = false;
                this.TheManager.HasStop = false;
                int index = 0;
                foreach (ImportItem item in items)
                {
                    // 判断导入是否被中断。
                    if (this.StopToExport) break;
                    if (item.Ignore) continue;

                    item.Reset();
                    item.SyncImportState2Local();
                    Console.WriteLine(string.Format("({0}/{1})", ++index, items.Count()));
                    if (item.ImportState == EImportStatus.WaitForImport)
                    {
                        ImportRecordTable.UpdateImportDate(item.DateName, item.TableName, System.DateTime.Now);
                        this.ExportItem(item);
                    }
                    else
                    {
                        item.WriteLine(this.TheManager.GetExportStateInfo(item.ImportState));
                        item.WriteLine();
                    }
                }
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### Exception [" + ee.Message + "]; Source = " + ee.Source);
                Trace.WriteLine("###" + ee.StackTrace);
                this.ShowErrorInfo("Exception : " + ee.Message);
                this.Log.E(this, ee);
            }
        }

        private void ExportItem(ImportItem item)
        {
            try
            {
                this.RegisterExportItem(item);
                this.TheManager.SyncExportItem(item);
                this.UnRegisterExportItem(item);
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### Exception [" + ee.Message + "]; Source = " + ee.Source);
                Trace.WriteLine("###" + ee.StackTrace);
                this.ShowErrorInfo("Exception : " + ee.Message);
                this.Log.E(this, ee);
            }
        }

        private void RegisterExportItem(ImportItem item)
        {
            if (item == null)
                return;

            item.PropertyChanged += OnExportItemPropertyChanged;
        }

        private void UnRegisterExportItem(ImportItem item)
        {
            if (item == null)
                return;

            item.PropertyChanged -= OnExportItemPropertyChanged;
        }

        /// <summary>
        /// 导出状态及导出进度。
        /// </summary>
        private void OnExportItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ImportItem item = sender as ImportItem;
            if (item == null)
                return;

            //
            if (e.PropertyName.Equals("CurrentIndex"))
            {
                if (this.PBBar != null)
                    this.PBBar.CurrentIndex = item.CurrentIndex;
            }
        }

        /// <summary>
        /// 显示进度条。
        /// </summary>
        private void OnItemStartExporting(object sender, ImportItem item)
        {
            if (this.PBBar == null)
            {
                this.PBBar = new MyProgressBar();
            }

            this.PBBar.Show(item.TotalCount);
        }

        /// <summary>
        /// 结束进度条。
        /// </summary>
        private void OnItemEndExporting(object sender, ImportItem item)
        {
            if (this.PBBar != null)
                this.PBBar.Close();
        }

        /// <summary>
        /// 数据导出异常。
        /// </summary>
        private void OnItemExportingException(object sender, Exception ex)
        {
            if (this.PBBar != null)
                this.PBBar.Close();

            this.ShowErrorInfo(string.Format("Exception: {0};", ex.Message));
        }

        #region CheckInterruptCommand

        /// <summary>
        /// 操作中断状态；
        /// </summary>
        public bool StopToExport { get; protected set; }

        ///// <summary>
        ///// 检测数据导入中断命令。
        ///// </summary>
        //private void OnCheckStopCommand()
        //{
        //    if (Console.KeyAvailable)
        //    {
        //        ConsoleKeyInfo keyInfo = Console.ReadKey();
        //        if (keyInfo.Key == ConsoleKey.I && keyInfo.Modifiers == ConsoleModifiers.Control)
        //        {
        //            this.TheManager.StopToImport();
        //            this.StopToExport = true;
        //        }
        //    }
        //}

        private void OnCancelKeyPressed(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;

            if (this.TheManager.IsImporting)
            {
                this.TheManager.StopToImport();
                this.StopToExport = true;
            }
        }

        #endregion

        private MyProgressBar PBBar
        {
            get;
            set;
        }
    }
}
