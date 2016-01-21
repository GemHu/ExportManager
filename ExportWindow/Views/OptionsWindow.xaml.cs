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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Dothan.ExportData;

namespace Dothan.ExportWindow
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow(ExportManager project)
        {
            InitializeComponent();

            if (project != null)
            {
                this.Project = project;
                this.ImportList = project.ExportList;
            }

            this.DataContext = this;
        }

        #region Project

        public ExportManager Project { get; set; }

        #endregion

        #region Command

        #region OK Command

        private void OK_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OK_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #endregion

        #region ImportList

        public static readonly DependencyProperty ImportListProperty =
            DependencyProperty.Register("ImportList", typeof(ObservableCollection<ImportItem>), typeof(OptionsWindow),
                                        new UIPropertyMetadata(null));

        public ObservableCollection<ImportItem> ImportList
        {
            get { return (ObservableCollection<ImportItem>)GetValue(ImportListProperty); }
            set { SetValue(ImportListProperty, value); }
        }

        #endregion

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            if (this.ImportList != null)
            {
                foreach (ImportItem item in this.ImportList)
                {
                    item.Ignore = false;
                }
            }
        }

        private void DisSelectAll_Click(object sender, RoutedEventArgs e)
        {
            if (this.ImportList != null)
            {
                foreach (ImportItem item in this.ImportList)
                {
                    item.Ignore = true;
                }
            }
        }
    }
}
