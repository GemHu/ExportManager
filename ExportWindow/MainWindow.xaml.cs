using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Configuration;
using Microsoft.Windows.Controls.Ribbon;
using Dothan.ExportData;
using Dothan.DzHelpers;

namespace Dothan.ExportWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                this.InitExportManager();
                this.ucDbfList.DbfManager = this.ExportManager.DbfManager;
                this.uc_ImportList.ImportList = this.ExportManager.ExportList;
                this.DBFQuery = new DBFQueryModule(this.ExportManager.DbfManager);
                this.ucDBFQuery.TheModule = this.DBFQuery;

                this.DataContext = this;
                this.StartAutoStartTimer();
            }
            catch (Exception ee)
            {
                string msg = string.Format("Error message = {0};\n{1}", ee.Message, ee.StackTrace);
                System.Windows.MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Log.E(this, ee);
                this.Close();
            }
        }

        public DBFQueryModule DBFQuery
        {
            get;
            set;
        }

        public ILog Log
        {
            get { return this.ExportManager.Log; }
        }

        #region OutputInfo

        public OutputData Output
        {
            get { return (OutputData)GetValue(OutputProperty); }
            set { SetValue(OutputProperty, value); }
        }

        public static readonly DependencyProperty OutputProperty =
            DependencyProperty.Register("Output", typeof(IOutput), typeof(MainWindow), 
                                        new UIPropertyMetadata(null));

        private void OnAsyncProjectManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.OnProjectManagerPropertyChanged(sender, e);
            }));
        }

        private void OnProjectManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "OutputInfo")
                this.scOutput.ScrollToEnd();
        }

        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.ExportManager.IsImporting)
            {
                if (System.Windows.MessageBox.Show("正在进行数据的导入操作，确定要退出吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    this.ExportManager.StopToImport();
                    // 等待工作线程提交导入状态
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }

    public class Bool2VisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is bool))
                return null;

            bool v = (bool)value;
            bool reverse = false;
            if (parameter is string && (parameter as string).Equals("reverse", StringComparison.OrdinalIgnoreCase))
                reverse = true;

            if (reverse)
            {
                if (v)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
            else
            {
                if (v)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class BoolReverseConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
