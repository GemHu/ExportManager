using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;

namespace Dothan.DzHelpers
{
    public abstract class SqlServerHelper : DBHelper
    {
        #region IDBHelper

        public new SqlConnection Connection
        {
            get { return base.Connection as SqlConnection; }
            protected set { base.Connection = value; }
        }

        public override bool Open()
        {
            if (this.Connection == null)
            {
                try
                {
                    this.Connection = new SqlConnection(this.ConnStr);
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
    public class LHDBHelper : SqlServerHelper
    {
        public const string Tag_ConnName = "LHDBConn";

        #region Life Cycle

        public LHDBHelper()
        {
            this.ConnStr = ConfigurationHelper.ConnectionStrings[Tag_ConnName].ConnectionString;
        }

        #endregion

    }

    public class WindDBHelper : SqlServerHelper
    {
        public const string Tag_ConnName = "WindDBConn";

        #region Life Cycle

        public WindDBHelper()
        {
            this.ConnStr = ConfigurationHelper.ConnectionStrings[Tag_ConnName].ConnectionString;
        }

        #endregion

    }
}
