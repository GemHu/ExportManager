using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Diagnostics;
using System.Configuration;

namespace Dothan.DzHelpers
{
    public abstract class OracleDBHelper : DBHelper
    {
        #region IDBHelper

        public new OracleConnection Connection
        {
            get { return base.Connection as OracleConnection; }
            protected set { base.Connection = value; }
        }

        public override bool Open()
        {
            if (this.Connection == null)
            {
                try
                {
                    this.Connection = new OracleConnection(this.ConnStr);
                    this.Connection.Open();
                }
                catch (System.Exception ee)
                {
                    Trace.WriteLine("### [" + ee.Source + "]; message = " + ee.Message);
                    Trace.WriteLine("### " + ee.StackTrace);
                    this.Connection = null;
                    return false;
                }
            }

            return true;
        }

        #endregion
    }

    public class O32DBHelper : OracleDBHelper
    {
        public const string Tag_ConnName = "O32DBConn";

        #region Life Cycle

        public O32DBHelper()
        {
            this.ConnStr = ConfigurationHelper.ConnectionStrings[Tag_ConnName].ConnectionString;
        }

        #endregion

    }
}
