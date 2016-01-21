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

namespace Dothan.ExportWindow
{
    /// <summary>
    /// Interaction logic for UcDBFQueryClient.xaml
    /// </summary>
    public partial class UcDBFQuery : UserControl
    {
        public UcDBFQuery()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        #region TheModule

        public DBFQueryModule TheModule
        {
            get { return (DBFQueryModule)GetValue(TheModuleProperty); }
            set { SetValue(TheModuleProperty, value); }
        }

        public static readonly DependencyProperty TheModuleProperty =
            DependencyProperty.Register("TheModule", typeof(DBFQueryModule), typeof(UcDBFQuery),
                                        new UIPropertyMetadata(null));

        #endregion
    }
}
