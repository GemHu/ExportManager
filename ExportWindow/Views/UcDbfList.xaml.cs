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
using Dothan.ExportData;

namespace Dothan.ExportWindow
{
    /// <summary>
    /// Interaction logic for UcDbfList.xaml
    /// </summary>
    public partial class UcDbfList : UserControl
    {
        #region Life Cycle

        public UcDbfList()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        #endregion

        #region DbfManager

        public DBFManager DbfManager
        {
            get { return (DBFManager)GetValue(DbfManagerProperty); }
            set { SetValue(DbfManagerProperty, value); }
        }

        public static readonly DependencyProperty DbfManagerProperty =
            DependencyProperty.Register("DbfManager", typeof(DBFManager), typeof(UcDbfList),
                                        new UIPropertyMetadata(null));

        #endregion

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.OpenDbfCommand.CanExecute(this.DbfManager.SelectedFile, this))
                MainWindow.OpenDbfCommand.Execute(this.DbfManager.SelectedFile, this);
        }

    }
}
