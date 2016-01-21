using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.ExportData;
using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Input;

namespace Dothan.ExportWindow
{
    public partial class MainWindow
    {
        #region ExportManager

        private void InitExportManager()
        {
            this.Output = new OutputData();
            this.Output.PropertyChanged += this.OnAsyncProjectManagerPropertyChanged;

            this.ExportManager = new ExportManager(this.Output);
            //this.ExportManager.PropertyChanged += this.OnExportManagerPropertyChanged;
            this.ExportManager.ItemExportException += this.OnItemExportException;
            this.ExportManager.CommandStateChanged += this.OnCommandStateChanged;
        }

        public ExportManager ExportManager
        {
            get { return (ExportManager)GetValue(ExportManagerProperty); }
            set { SetValue(ExportManagerProperty, value); }
        }

        public static readonly DependencyProperty ExportManagerProperty =
            DependencyProperty.Register("ExportManager", typeof(ExportManager), typeof(MainWindow),
                                        new FrameworkPropertyMetadata(null));

        #endregion

        #region 1s中定时器，用于定时启动操作。

        /// <summary>
        /// 指定时间自动开启文件检测定时器。
        /// </summary>
        protected DispatcherTimer AutoStartTimer;

        /// <summary>
        /// 启动AutoStartTimer定时器。
        /// </summary>
        protected void StartAutoStartTimer()
        {
            if (this.AutoStartTimer == null)
            {
                this.AutoStartTimer = new DispatcherTimer();
                this.AutoStartTimer.Interval = new TimeSpan(0, 0, 1);
                this.AutoStartTimer.Tick += AutoStartTimer_Tick;
            }

            if (this.AutoStartTimer.IsEnabled)
                this.AutoStartTimer.Stop();

            this.AutoStartTimer.Start();
        }

        protected void AutoStartTimer_Tick(object sender, EventArgs e)
        {
            if (this.ExportManager.StartDetectTime == null)
                return;

            DateTime now = DateTime.Now;
            if (this.ExportManager.AutoDetectData)
            {
                if (now.ToLongTimeString() == this.ExportManager.StartDetectTime.ToLongTimeString() && !this.ExportManager.HasStartAutoImportDate)
                {
                    this.ExportManager.StartAutoImportData();
                }
            }
            else
            {
                if (this.ExportManager.ImportState != EImportStatus.Importing && this.ExportManager.ImportState != EImportStatus.WaitForImport)
                {
                    this.ExportManager.StopAutoImportData();
                }
            }
        }

        #endregion

        #region OnExportManagerChanged

        //private void OnExportManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    //this.ExportManager.
        //    if (e.PropertyName.Equals("ImportState"))
        //    {
        //        Application.Current.Dispatcher.BeginInvoke((Action)(() =>
        //            {
                        
        //            }));
        //    }
        //}

        #endregion

        private void OnItemExportException(object sender, Exception ex)
        {
            ImportItem item = sender as ImportItem;
            string msg = string.Format("Item {0} Export Exception;\nException Message: {0}\nException Source: {2}", item.ShownName, ex.Message, ex.Source);
            MessageBox.Show(msg, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnCommandStateChanged(object sender, EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
