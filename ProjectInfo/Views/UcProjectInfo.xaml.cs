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
using Dothan.DzHelpers;

namespace Dothan.ProjectInfo
{
    /// <summary>
    /// Interaction logic for UcProjectInfo.xaml
    /// </summary>
    public partial class UcProjectInfo : UserControl
    {
        #region Life Cycle

        public UcProjectInfo()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        #endregion

        #region TheLibrary

        public static readonly DependencyProperty TheLibraryProperty =
            DependencyProperty.Register("TheLibrary", typeof(ProjectInfoLibrary), typeof(UcProjectInfo),
                                        new FrameworkPropertyMetadata(null));

        public ProjectInfoLibrary TheLibrary
        {
            get { return (ProjectInfoLibrary)GetValue(TheLibraryProperty); }
            set { SetValue(TheLibraryProperty, value); }
        }

        #endregion

        #region EventHandler

        private void TreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = ViewHelper.HitTestView<TreeViewItem>(sender as Visual, e.GetPosition(sender as IInputElement));
            if (item == null)
                return;

            if (this.DoubleClickCommand != null && this.DoubleClickCommand.CanExecute(item.DataContext))
            {
                this.ExecuteCommand(this.DoubleClickCommand, item.DataContext, null);
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.ClickCommand != null)
            {
                this.ExecuteCommand(this.ClickCommand, e.NewValue, null);
            }
        }

        private void ExecuteCommand(ICommand command, object parameter, IInputElement target)
        {
            RoutedCommand command2 = command as RoutedCommand;
            if (command2 != null)
            {
                if (command2.CanExecute(parameter, target))
                {
                    command2.Execute(parameter, target);
                }
            }
            else if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }

        #endregion

        #region DoubleClickCommand

        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.Register("DoubleClickCommand", typeof(ICommand), typeof(UcProjectInfo),
            new UIPropertyMetadata(null));

        public ICommand DoubleClickCommand
        {
            get { return (ICommand)GetValue(DoubleClickCommandProperty); }
            set { SetValue(DoubleClickCommandProperty, value); }
        }

        #endregion

        #region ClickCommand

        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register("ClickCommand", typeof(ICommand), typeof(UcProjectInfo),
                                        new UIPropertyMetadata(null));

        #endregion

    }
}
