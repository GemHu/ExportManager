using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Dothan.ExportData
{
    public partial class ExportManager
    {
        #region TargetDateChanged

        public event DateChangedEventHandler TargetDateChanged;

        public void RaiseTargetDateChanged(DateTime oldDate, DateTime newDate)
        {
            if (this.TargetDateChanged != null)
                this.TargetDateChanged.Invoke(this, new DateChangedEventArgs(oldDate, newDate));
        }

        #endregion

        #region ItemExportEvent

        public event ItemExportHandler ItemExportingEvent;

        public event ItemExportHandler ItemExportedEvent;

        public event ItemExportExceptionHandler ItemExportException;

        public event EventHandler CommandStateChanged;

        public void RaiseItemExportingEvent(ImportItem item)
        {
            if (this.ItemExportingEvent != null)
                this.ItemExportingEvent.Invoke(this, item);
        }

        public void RaiseItemExportedEvent(ImportItem item)
        {
            if (this.ItemExportedEvent != null)
                this.ItemExportedEvent.Invoke(this, item);
        }

        public void RaiseItemExportedException(ImportItem item, Exception ee)
        {
            if (this.ItemExportException != null)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.ItemExportException.Invoke(item, ee);
                }));
            }
        }

        public void RaiseCommandStateChanged()
        {
            if (this.CommandStateChanged != null)
            {
                // 调用UI线程去更新相关信息。
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.CommandStateChanged.Invoke(this, EventArgs.Empty);
                }));
            }
        }

        #endregion

    }

    public delegate void DateChangedEventHandler(object sender, DateChangedEventArgs e);

    public class DateChangedEventArgs : EventArgs
    {
        public DateTime OldDate
        {
            get { return this._OldDate; }
        }
        private readonly DateTime _OldDate;

        public DateTime NewDate
        {
            get { return this._NewDate; }
        }
        private readonly DateTime _NewDate;

        public DateChangedEventArgs(DateTime oldDate, DateTime newDate)
        {
            this._OldDate = oldDate;
            this._NewDate = NewDate;
        }
    }

    public delegate void ItemExportHandler(object sender, ImportItem item);

    public delegate void CommandStateChangedHandler(object sender, EventArgs e);

    public delegate void ItemExportExceptionHandler(object sender, Exception ex);

}
