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
using Microsoft.Windows.Controls.Ribbon;
using Dothan.ProjectInfo;

namespace Dothan.DataQuery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        #region Life Cycle

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                this.TheProject = new ProjectInfoManager();
                this.TheProject.UpdateProjectInfoLibrary();

                this.ti_project_info.TheLibrary = this.TheProject.TheProjectInfoLibrary;
                this.uc_ProjectInfo_Result.TheResult = this.TheProject.TheProjectInfoResult;

                this.DataContext = this.TheProject;

            }
            catch (Exception ee)
            {
                string msg = string.Format("ErrorMessage: {0}; \nErrorSource:{1};\n{2};", ee.Message, ee.Source, ee.StackTrace);
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                this.Close();
            }
        }

        #endregion

        #region TheProject

        public ProjectInfoManager TheProject
        {
            get { return (ProjectInfoManager)GetValue(TheProjectProperty); }
            set { SetValue(TheProjectProperty, value); }
        }

        public static readonly DependencyProperty TheProjectProperty =
            DependencyProperty.Register("TheProject", typeof(ProjectInfoManager), typeof(MainWindow),
                                        new UIPropertyMetadata(null));

        #endregion
    }
}
