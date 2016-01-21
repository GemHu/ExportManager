using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Dothan.DzHelpers
{
    public class DelegateCommand : ICommand
    {
        #region life cycle

        public DelegateCommand(Action executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.ExecuteMethod = executeMethod;
            this.CanExecuteMethod = canExecuteMethod;
        }

        #endregion

        #region CanExecute

        public bool CanExecute(object parameter)
        {
            if (this.CanExecuteMethod == null)
                return true;

            return this.CanExecuteMethod();
        }
        public Func<bool> CanExecuteMethod { get; set; }

        #endregion

        #region Execute

        public void Execute(object parameter)
        {
            if (this.ExecuteMethod == null)
                return;

            this.ExecuteMethod();
        }
        public Action ExecuteMethod { get; set; }

        #endregion

        #region CanExecuteChanged

        /// <summary>
        /// 当命令的可执行状态发生变化后，用于触发 CanExecuteChanged 事件，来进行实时更新命令的显示状态。
        /// </summary>
        public void RaiseCanExecuteEchanged(object sender, EventArgs e)
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged.Invoke(sender, e);
            }
        }
        public event EventHandler CanExecuteChanged;

        #endregion

    }
}
