using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public partial class ImportItem : IDBHelper
    {
        #region IDBHelper

        protected DBHelper DBHelper
        {
            get;
            set;
        }

        public string ConnStr
        {
            get { return DBHelper != null ? this.DBHelper.ConnStr : string.Empty; }
        }

        public System.Data.Common.DbConnection Connection
        {
            get { return DBHelper != null ? this.DBHelper.Connection : null; }
        }

        public bool Open()
        {
            if (this.DBHelper == null)
                return false;

            return this.DBHelper.Open();
        }

        public bool Close()
        {
            if (this.DBHelper == null)
                return false;

            return this.DBHelper.Close();
        }

        public DbDataReader ExecuteReader(string sqlCommand, params DbParameter[] parameters)
        {
            if (this.DBHelper == null)
                return null;

            return this.DBHelper.ExecuteReader(sqlCommand, parameters);
        }

        public int ExecuteNonQuery(string sqlCommand, params DbParameter[] parameters)
        {
            if (this.DBHelper == null)
                return -1;

            return this.DBHelper.ExecuteNonQuery(sqlCommand, parameters);
        }

        public object ExecuteScalar(string sqlCommand, params DbParameter[] parameters)
        {
            if (this.DBHelper == null)
                return null;

            return this.DBHelper.ExecuteScalar(sqlCommand, parameters);
        }
        
        #endregion
    }
}
