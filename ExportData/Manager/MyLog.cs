using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.ExportData;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public class MyLog : DzLog
    {
        public MyLog(IOutput output) : base(new Log4netHelper())
        {
            this.Output = output;
        }

        public IOutput Output
        {
            get;
            protected set;
        }

        public override void Print(object sender, string msg, LogLevel logLevel)
        {
            if (this.Output == null)
                return;

            if (logLevel == LogLevel.Info)
                msg = string.Format("{0}\t{1};", DateTime.Now.ToLocalTime(), msg);
            else
                msg = string.Format("{0}\t{1}\t{2};", DateTime.Now.ToLocalTime(), logLevel, msg);

            this.Output.WriteLine(msg);
        }

        public override void Print(object sender, Exception exp, LogLevel logLevel)
        {
            if (this.Output == null)
                return;

            string msg = string.Empty;
            if (logLevel == LogLevel.Info)
                msg = string.Format("{0}\t{1};", DateTime.Now.ToLocalTime(), exp.Message);
            else
                msg = string.Format("{0}\t{1}\t{2};", DateTime.Now.ToLocalTime(), logLevel, exp.Message);

            this.Output.WriteLine(msg);
        }
    }
}
