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
using System.Data;

namespace Dothan.ExportWindow
{
    /// <summary>
    /// Interaction logic for UcDataTableViewer.xaml
    /// </summary>
    public partial class UcDataTableViewer : UserControl
    {
        #region Life Cycle

        public UcDataTableViewer()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region DataTable

        public static readonly DependencyProperty DataTableProperty =
            DependencyProperty.Register("DataTable", typeof(DataTable), typeof(UcDataTableViewer), 
                                        new FrameworkPropertyMetadata(null));

        public DataTable DataTable
        {
            get { return (DataTable)GetValue(DataTableProperty); }
            set 
            {
                if (value == null)
                    this.ClearValue(DataTableProperty);
                else
                    this.SetValue(DataTableProperty, value); 
            }
        }

        #endregion
    }
}
