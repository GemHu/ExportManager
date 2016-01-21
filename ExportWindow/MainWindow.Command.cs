using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;
using System.Data;
using System.Configuration;
using System.Windows;
using System.IO;
using System.Diagnostics;
using Dothan.DzHelpers;
using System.Windows.Forms;
using Dothan.ExportData;

namespace Dothan.ExportWindow
{
    public partial class MainWindow
    {
        #region Custom Command

        public static RoutedUICommand OpenDbfCommand = new RoutedUICommand("Open Dbf File", "OpenDbfFile", typeof(MainWindow));

        #endregion

        #region TestCommand

        private void TestCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void TestCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                throw new Exception("Test Log4net!");
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Message + "]; Exception = " + ee.Source);
                Trace.WriteLine("### " + ee.StackTrace);

                this.ExportManager.Log.E(this, ee);
            }
        }

        #endregion

        #region Open Command

        protected void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            this.OpenDBFFile(dlg.FileName);
        }

        protected void Open_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void OpenDBFFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !System.IO.File.Exists(fileName))
                return;

            FileInfo file = new FileInfo(fileName);
            DataTable table = DbfDBHelper.GetDataTable(fileName);
            if (table == null)
                return;

            TextBlock txtBlock = new TextBlock();
            txtBlock.Text = file.Name;
            txtBlock.ToolTip = fileName;
            this.ti_main_viewer.Header = txtBlock;
            this.ti_main_viewer.Visibility = Visibility.Visible;
            this.dbfViewer.DataTable = table;
            this.tc_main.SelectedItem = this.ti_main_viewer;
        }

        #endregion

        #region Close Command

        protected void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        protected void Close_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region OpenSelectedItem

        protected void OpenSelectedItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DbfFileInfo dbfFile = e.Parameter as DbfFileInfo;
            if (dbfFile != null)
                this.OpenDBFFile(dbfFile.FileName);
        }

        protected void OpenSelectedItem_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            DbfFileInfo dbfFile = e.Parameter as DbfFileInfo;
            e.CanExecute = dbfFile != null && File.Exists(dbfFile.FileName);
        }

        #endregion

        #region RefreshCommand

        protected void Refresh_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ExportManager.DbfManager.UpdateDbfFileList();
        }

        protected void Refresh_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            if (this.ExportManager == null || this.ExportManager.IsImporting)
                return;

            e.CanExecute = true;
        }

        #endregion

        #region Stop Command

        protected void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ExportManager.StopToImport();
        }

        protected void Stop_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            if (this.ExportManager == null)
                return;

            e.CanExecute = this.ExportManager.IsImporting;
        }

        #endregion

        #region Export Command

        protected void Export_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ExportManager.CheckAndExport();
        }

        protected void Export_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            if (this.ExportManager == null)
                return;

            e.CanExecute = !this.ExportManager.IsImporting;
        }

        #endregion

        #region Update Command

        protected void Update_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.ExportManager == null || this.ExportManager.IsImporting)
                return;

            e.CanExecute = true;
        }

        protected void Update_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ExportManager.AsyncUpdateExportStates();
        }

        #endregion

        #region Options Command

        protected void Options_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OptionsWindow dlg = new OptionsWindow(this.ExportManager);
                dlg.Owner = this;
                dlg.ShowDialog();
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Message + "]; Exception = " + ee.Source);
                Trace.WriteLine("### " + ee.StackTrace);

                this.Log.E(this, ee);
            }
        }

        protected void Options_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region About Command

        protected void About_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.qlzqzg.com/introduction/index.html");
        }

        protected void About_Enabled(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region RunSqlCommand

        private void RunSqlCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;

            //if (this.TheProject == null || string.IsNullOrEmpty(this.TheProject.QueryCommand))
            //    return;

            //e.CanExecute = true;

            if (this.DBFQuery == null || !this.ti_main_DBFQuery.IsSelected)
                return;

            this.DBFQuery.CheckCanExecute();
            e.CanExecute = this.DBFQuery.CanExecute;
        }

        private void RunSqlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //this.TheProject.ExecuteSqlCommand(this.TheProject.QueryCommand);
            //this.SqlResultViewer.DataTable = this.TheProject.QueryResult;
            //this.ti_QueryResult.IsSelected = true;

            this.DBFQuery.ExecuteCommand();
        }

        #endregion

        #region StopSqlCommand

        private void StopSqlCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        private void StopSqlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        #endregion

        #region SwitchNextDate

        private void SwitchNextDate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            if (this.ExportManager == null)
                return;

            e.CanExecute = this.ExportManager.DbfManager.CanSwitch2Next();
        }

        private void SwitchNextDate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ExportManager.DbfManager.Switch2Next();
        }

        #endregion

        #region SwitchPrevDate

        private void SwitchPrevDate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            if (this.ExportManager == null || this.ExportManager.IsImporting)
                return;

            e.CanExecute = true;
        }

        private void SwitchPrevDate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ExportManager.DbfManager.Switch2Prev();
        }

        #endregion

        #region DataQueryCommand

        private void DataQuery_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DataQuery_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("DataQuery.exe");
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Message + "]; Exception = " + ee.Source);
                Trace.WriteLine("### " + ee.StackTrace);

                this.Log.E(this, ee);
            }
        }

        #endregion

        #region ExportCmdCommand

        private void ExportCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExportCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("ExportCmd.exe");
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Message + "]; Exception = " + ee.Source);
                Trace.WriteLine("### " + ee.StackTrace);

                this.Log.E(this, ee);
            }
        }

        #endregion

        #region ClearCurrentData Command

        private void ClearData_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.ExportManager == null)
                return;

            if (this.ExportManager.IsImporting)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        public void ClearData_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
                return;

            // Clear Trade
            if (e.Parameter.ToString() == "1")
            {
                this.ExportManager.ClearOrderInfo();
                this.ExportManager.ClearTradeInfo();
            }
            // Clear Market
            else if (e.Parameter.ToString() == "2")
            {
                this.ExportManager.ClearMarketInfo();
            }
            // Clear All
            else if (e.Parameter.ToString() == "3")
            {
                this.ExportManager.ClearAllInfo();
            }
        }
        
        #endregion
    }
}
