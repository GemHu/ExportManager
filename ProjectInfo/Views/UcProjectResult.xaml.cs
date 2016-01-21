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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dothan.ProjectInfo
{
    /// <summary>
    /// Interaction logic for UcProjectResult.xaml
    /// </summary>
    public partial class UcProjectResult : UserControl
    {
        #region Life Cycle

        public UcProjectResult()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        #endregion

        #region TheResult

        public ProjectInfoResult TheResult
        {
            get { return (ProjectInfoResult)GetValue(TheResultProperty); }
            set { SetValue(TheResultProperty, value);}
        }

        public static readonly DependencyProperty TheResultProperty =
            DependencyProperty.Register("TheResult", typeof(ProjectInfoResult), typeof(UcProjectResult), 
                                        new FrameworkPropertyMetadata(null));

        #endregion

    }
}
