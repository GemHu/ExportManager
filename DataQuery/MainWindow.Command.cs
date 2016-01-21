using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using Dothan.ProjectInfo;

namespace Dothan.DataQuery
{
    public partial class MainWindow
    {
        #region CloseCommand

        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        
        #endregion

        #region RefreshCommand

        private void Refresh_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Refresh_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.TheProject.UpdateProjectInfoLibrary();
        }

        #endregion

        #region ProjectInfoClickCommand

        private void ProjectInfoClick_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            InfoItem item = e.Parameter as InfoItem;
            if (item == null)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void ProjectInfoClick_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            InfoItem item = e.Parameter as InfoItem;
            if (item == null)
                return;

            this.TheProject.UpdateResultInfo(item);
        }

        private void ProjectInfoDoubleClick_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CombinedCode zhdm = e.Parameter as CombinedCode;
            if (zhdm == null)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void ProjectInfoDoubleClick_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CombinedCode zhdm = e.Parameter as CombinedCode;
            if (zhdm == null)
                return;

            // TODO:
            return;
        }

        #endregion

    }
}
