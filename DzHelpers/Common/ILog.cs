using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dothan.DzHelpers
{
    public interface ILog
    {
        void I(object sender, string msg);

        void I(object sender, Exception exp);

        void W(object sender, string msg);

        void W(object sender, Exception exp);

        void E(object sender, string msg);

        void E(object sender, Exception exp);
    }

    public abstract class DzLog : ILog
    {
        #region Life Cycle

        public DzLog() : this(null)
        {
        }

        public DzLog(ILog log)
        {
            this.Log = log;
        }

        protected ILog Log { get; set; }

        #endregion

        #region ILog

        public void I(object sender, string msg)
        {
            if (this.Log != null)
                this.Log.I(sender, msg);

            this.Print(sender, msg, LogLevel.Info);
        }

        public void I(object sender, Exception exp)
        {
            if (this.Log != null)
                this.Log.I(sender, exp);

            this.Print(sender, exp, LogLevel.Info);
        }

        public void W(object sender, string msg)
        {
            if (this.Log != null)
                this.Log.W(sender, msg);

            this.Print(sender, msg, LogLevel.Warning);
        }

        public void W(object sender, Exception exp)
        {
            if (this.Log != null)
                this.Log.W(sender, exp);

            this.Print(sender, exp, LogLevel.Warning);
        }

        public void E(object sender, string msg)
        {
            if (this.Log != null)
                this.Log.E(sender, msg);

            this.Print(sender, msg, LogLevel.Error);
        }

        public void E(object sender, Exception exp)
        {
            if (this.Log != null)
                this.Log.E(sender, exp);

            this.Print(sender, exp, LogLevel.Error);
        }

        public abstract void Print(object sender, string msg, LogLevel logLevel);

        public abstract void Print(object sender, Exception exp, LogLevel logLevel);

        #endregion
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }
}
