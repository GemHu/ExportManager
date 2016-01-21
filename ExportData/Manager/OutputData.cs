using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public class OutputData : NotificationObject, IOutput
    {
        public string OutputInfo
        {
            get { return this._OutputInfo; }
            set
            {
                this._OutputInfo = value;
                this.RaisePropertyChanged("OutputInfo");
            }
        }
        private string _OutputInfo;
        private StringBuilder OutputBuilder;

        public void Write(string msg)
        {
            if (this.OutputBuilder == null)
                this.OutputBuilder = new StringBuilder();

            this.OutputBuilder.Append(msg);
            this.OutputInfo = this.OutputBuilder.ToString();
        }

        public void WriteLine(string msg)
        {
            if (this.OutputBuilder == null)
                this.OutputBuilder = new StringBuilder();

            this.OutputBuilder.AppendLine(msg);
            this.OutputInfo = this.OutputBuilder.ToString();
        }

        public void WriteLine()
        {
            if (this.OutputBuilder == null)
                this.OutputBuilder = new StringBuilder();

            this.OutputBuilder.AppendLine();
        }

        public void Clear()
        {
            this.OutputBuilder = null;
            this.OutputInfo = null;
        }

    }
}
