using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Dothan.DzHelpers;
using System.Windows;
using System.Windows.Threading;
using System.Diagnostics;

namespace Dothan.ExportData
{
    public enum EImportType
    {
        None = 0x00,
        Order = 0x01,
        Trade = 0x02,
        Market = 0x04
    }

    public partial class ExportManager
    {
        public void UpdateExportStates()
        {
            if (this.IsImporting)
                return;

            try
            {
                foreach (ImportItem item in this.ExportList)
                {
                    if (item.Ignore)
                        continue;

                    item.SyncImportState2Local();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("导入状态更新失败！\nMessage: " + ee.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Log.E(this, ee);
            }
        }

        public void AsyncUpdateExportStates()
        {
            new Thread(new ThreadStart(this.UpdateExportStates)).Start();
        }

        /// <summary>
        /// 刷新导出列表，并开启工作线程检测DBF文件，如果检测到则进行文件的导入操作。
        /// </summary>
        public void CheckAndExport()
        {
            if (this.IsImporting || !Directory.Exists(this.DbfManager.DbfPath))
                return;

            this.DbfManager.UpdateDbfFileList();
            this.AsyncExportAll();
        }

        public void ClearOrderInfo()
        {
            OrderTable orderService = new OrderTable();
            int count = orderService.RemoveByDate(this.DbfManager.TargetDateName);
            this.Output.WriteLine(string.Format("Delete from {0} where trade_date = {1}", OrderTable.T_TableName, this.DbfManager.TargetDateName));
            this.Output.WriteLine("受影响行数：" + count);

            foreach (ImportItem item in this.ExportList)
            {
                if (item is IExport2OrderTable)
                {
                    item.InitImportState();
                }
            }
        }

        public void ClearTradeInfo()
        {
            TradeTable tradeService = new TradeTable();
            int count = tradeService.RemoveByDate(this.DbfManager.TargetDateName);
            this.Output.WriteLine(string.Format("Delete from {0} where trade_date = {1}", TradeTable.T_TableName, this.DbfManager.TargetDateName));
            this.Output.WriteLine("受影响行数：" + count);

            foreach (ImportItem item in this.ExportList)
            {
                if (item is IExport2TradeTable)
                {
                    item.InitImportState();
                }
            }
        }

        public void ClearMarketInfo()
        {
            MarketTable marketService = new MarketTable();
            int count = marketService.RemoveByDate(this.DbfManager.TargetDateName);
            this.Output.WriteLine(string.Format("Delete from {0} where trade_date = {1}", MarketTable.TableName, this.DbfManager.TargetDateName));
            this.Output.WriteLine("受影响行数：" + count);

            foreach (ImportItem item in this.ExportList)
            {
                if (item is IExport2MarketTable)
                {
                    item.InitImportState();
                }
            }
        }

        public void ClearAllInfo()
        {
            this.ClearOrderInfo();
            this.ClearTradeInfo();
            this.ClearMarketInfo();
        }

        /// <summary>
        /// 启动工作线程，开始处理一些耗时的操作；
        /// </summary>
        private void AsyncExportAll()
        {
            if (this.ExportList == null)
                return;

            new Thread(new ParameterizedThreadStart(this.DoAsyncExportAll)).Start(this.ExportList);
        }

        /// <summary>
        /// 工作线程入口点；
        /// 一些耗时操作如果放到UI线程中的话，则会出现界面的僵死状态。
        /// </summary>
        protected void DoAsyncExportAll(object obj)
        {
            IEnumerable<ImportItem> items = obj as IEnumerable<ImportItem>;
            if (items == null)
                return;

            this.ExportItems(items);
        }

        /// <summary>
        /// DBF文件导入线程入口点；
        /// 导入DBF文件。
        /// </summary>
        protected void OnImportThreadStart(object obj)
        {
            ImportItem item = obj as ImportItem;
            if (item == null)
                return;

            try
            {
                this.SyncExportItem(item);

                // 通知UI线程，当前DBF文件导入完成。
                Application.Current.Dispatcher.BeginInvoke(new Action(() => this.OnItemExportComplete()), DispatcherPriority.Normal);
            }
            catch (Exception ee)
            {
                item.ImportState = EImportStatus.Exception;
                item.SyncImportState2Remote();
                item.Close();
                this.RaiseItemExportedException(item, ee);

                this.Log.E(this, ee);
            }
        }

        /// <summary>
        /// 某一个DBF文件导出完成状态；
        /// </summary>
        protected void OnItemExportComplete()
        {
            if (this.ImportState != EImportStatus.Imported)
                return;

            this.Output.WriteLine(string.Format("目标文件夹:{0} 导出完毕!", this.DbfManager.TargetDateName));

            if (this.AutoImportNext)
            {
                if (this.DbfManager.DateIsLatest(this.TargetDate))
                {
                    this.StopAutoImportData();
                    return;
                }

                if (this.DbfManager.Switch2Next())
                    this.CheckAndExport();
            }
        }

        private void ExportItems(IEnumerable<ImportItem> items)
        {
            if (!SettleDBHelper.CheckConnCanUse())
            {
                this.Output.WriteLine("Mysql数据库打开失败，请确认MySql驱动已经正确安装，以及连接字符串正确配置!");
                this.Output.WriteLine();
                return;
            }

            lock (this)
            {
                try
                {
                    foreach (ImportItem item in items)
                    {
                        if (item.Ignore)
                            continue;

                        item.Reset();
                        item.SyncImportState2Local();
                        // 检查导入状态，如果有无效状态，则清除对应数据，然后再次进行重新导入；



                        if (item.ImportState == EImportStatus.WaitForImport)
                        {
                            ImportRecordTable.UpdateImportDate(item.DateName, item.TableName, System.DateTime.Now);
                            this.AsyncExportItem(item);
                        }
                        else
                        {
                            this.Output.WriteLine(this.GetExportStateInfo(item.ImportState));
                        }
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("导入状态更新失败！\nMessage: " + ee.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Log.E(this, ee);
                }
            }
        }

        public string GetExportStateInfo(EImportStatus state)
        {
            switch (state)
            {
                default:
                case EImportStatus.Init:
                    return "未启动！";
                case EImportStatus.NotDetected:
                    return "未检测到数据！";
                case EImportStatus.WaitForImport:
                    return "待导入";
                case EImportStatus.Importing:
                    return "正在导入。。。";
                case EImportStatus.Interrupt:
                    return "已中断！";
                case EImportStatus.Imported:
                    return "已导入！";
            }
        }

        public IEnumerable<ImportItem> GetExportItems(EImportType type)
        {
            return this.ExportList.Where(o =>
            {
                if ((type & EImportType.Order) != 0 && o is IExport2OrderTable)
                    return true;
                else if ((type & EImportType.Trade) != 0 && o is IExport2TradeTable)
                    return true;
                else if ((type & EImportType.Market) != 0 && o is IExport2MarketTable)
                    return true;

                return false;
            });
        }

        public void SyncExportItem(DateTime targetDate, ImportItem item)
        {
            this.TargetDate = targetDate;
            this.SyncExportItem(item);
        }

        /// <summary>
        /// 开启子线程，进行数据的采集操作。
        /// </summary>
        public void AsyncExportItem(ImportItem item)
        {
            new Thread(new ParameterizedThreadStart(this.OnImportThreadStart)).Start(item);
        }

        /// <summary>
        /// 在当前线程中直接进行数据采集操作。
        /// </summary>
        public void SyncExportItem(ImportItem item)
        {
            item.ImportState = EImportStatus.Importing;
            item.SyncImportState2Remote();

            item.WriteLine("Start to Export");

            this.RaiseItemExportingEvent(item);
            item.DoExport();
            this.RaiseItemExportedEvent(item);

            if (item.ImportState == EImportStatus.Interrupt)
            {
                item.WriteLine("Export Interrupt;");
            }
            else if (item.ImportState == EImportStatus.Importing)
            {
                item.ImportState = EImportStatus.Imported;
                item.WriteLine("Export over;");
            }

            item.WriteLine("TotalCount = " + item.TotalCount);
            item.WriteLine("ValidCount = " + item.ValidIndex);

            item.SyncImportState2Remote();
        }

        /// <summary>
        /// 根据导出项名称获取对应的导出项。
        /// </summary>
        public ImportItem GetExportItem(string itemName)
        {
            if (string.IsNullOrEmpty(itemName))
                return null;

            foreach (ImportItem item in this.ExportList)
            {
                if (itemName.Equals(item.ShownName, StringComparison.OrdinalIgnoreCase) ||
                    itemName.Equals(item.Name, StringComparison.OrdinalIgnoreCase))
                    return item;
            }

            return null;
        }

    }
}
