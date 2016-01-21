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
using System.ComponentModel;
using System.Collections.ObjectModel;
using Dothan.ExportData;

namespace Dothan.ExportWindow
{
    /// <summary>
    /// Interaction logic for UcImportList.xaml
    /// </summary>
    public partial class UcImportList : UserControl, INotifyPropertyChanged
    {
        public UcImportList()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        #region ImportList

        public static readonly DependencyProperty ImportListProperty =
            DependencyProperty.Register("ImportList", typeof(ObservableCollection<ImportItem>), typeof(UcImportList),
            new FrameworkPropertyMetadata(null));

        public ObservableCollection<ImportItem> ImportList
        {
            get { return (ObservableCollection<ImportItem>)GetValue(ImportListProperty); }
            set { SetValue(ImportListProperty, value); }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class ProcessStatusConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is EImportStatus))
                return "";

            EImportStatus status = (EImportStatus)value;
            string ret = "";
            switch (status)
            {
                case EImportStatus.Init:
                    ret = "未启动";
                    break;
                case EImportStatus.NotDetected:
                    ret = "未检测到数据";
                    break;
                case EImportStatus.WaitForImport:
                    ret = "待导入";
                    break;
                case EImportStatus.Importing:
                    ret = "导入中...";
                    break;
                case EImportStatus.Interrupt:
                    ret = "已中断";
                    break;
                case EImportStatus.Imported:
                    ret = "已完成";
                    break;
                case EImportStatus.Exception:
                    ret = "异常";
                    break;
                default :
                    ret = "其他";
                    break;
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class ProcessInfoVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility ret = Visibility.Hidden;
            if (!(value is EImportStatus))
                return ret;

            switch ((EImportStatus)value)
            {
                case EImportStatus.Importing:
                    ret = Visibility.Visible;
                    break;
                default:
                    break;
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Imported2VisibleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility ret = Visibility.Hidden;
            if (!(value is EImportStatus))
                return ret;

            switch ((EImportStatus)value)
            {
                case EImportStatus.Imported:
                case EImportStatus.Interrupt:
                    ret = Visibility.Visible;
                    break;
                default:
                    break;
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SourceTypeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            string ret = "";
            switch ((ESourceType)value)
            {
                default:
                case ESourceType.UnKnown:
                    ret = "Unknown";
                    break;
                case ESourceType.DBF:
                    ret = "DBF";
                    break;
                case ESourceType.Mysql:
                    ret = "MySql";
                    break;
                case ESourceType.Oracle:
                    ret = "Oracle";
                    break;
                case ESourceType.SqlServer:
                    ret = "SqlServer";
                    break;
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SourceType2ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ESourceType type = (ESourceType)value;
            BitmapImage source = null;
            switch (type)
            {
                case ESourceType.Mysql:
                case ESourceType.SqlServer:
                case ESourceType.Oracle:
                    source = new BitmapImage(new Uri("/Images/Icon_Database.png", UriKind.Relative));
                    break;
                case ESourceType.UnKnown:
                case ESourceType.DBF:
                    source = new BitmapImage(new Uri("/Images/icon_dbf_file.png", UriKind.Relative));
                    break;
            }

            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
