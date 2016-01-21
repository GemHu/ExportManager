using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public class Log4netHelper : DzHelpers.ILog
    {
        public void I(object sender, string msg)
        {
            log4net.LogManager.GetLogger(sender.GetType()).Info(msg);
        }

        public void I(object sender, Exception exp)
        {
            log4net.LogManager.GetLogger(sender.GetType()).Info(exp);
        }

        public void W(object sender, string msg)
        {
            log4net.LogManager.GetLogger(sender.GetType()).Warn(msg);
        }

        public void W(object sender, Exception exp)
        {
            log4net.LogManager.GetLogger(sender.GetType()).Warn(exp);
        }

        public void E(object sender, string msg)
        {
            log4net.LogManager.GetLogger(sender.GetType()).Error(msg);
        }

        public void E(object sender, Exception exp)
        {
            log4net.LogManager.GetLogger(sender.GetType()).Error(exp);
        }
    }
}
