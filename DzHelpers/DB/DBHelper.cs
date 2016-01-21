using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Windows.Threading;

namespace Dothan.DzHelpers
{
    public abstract class DBHelper : IDBHelper
    {
        #region DBHelper Methord

        public static DataTable GetDataTable(DbDataReader reader)
        {
            if (reader == null)
                return null;

            DataTable tab = new DataTable();
            for (int i = 0; i < reader.VisibleFieldCount; i++)
            {
                tab.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
            }
            DataRow row;
            while (reader.Read())
            {
                row = tab.NewRow();
                for (int i = 0; i < tab.Columns.Count; i++)
                {
                    row[i] = reader.GetValue(i);
                }
                tab.Rows.Add(row);
            }
            return tab;
        }

        #endregion

        #region IDBHelper Members

        public virtual string ConnStr { get; protected set; }

        public DbConnection Connection { get; protected set; }

        public abstract bool Open();

        public virtual bool Close()
        {
            try
            {
                if (this.Connection != null)
                {
                    this.Connection.Close();
                    this.Connection.Dispose();
                    this.Connection = null;
                }
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Source + "] Exception: " + ee.Message);
                Trace.WriteLine("### " + ee.StackTrace);
                return false;
            }

            return true;
        }

        public bool IsOpened
        {
            get
            {
                if (this.Connection != null && this.Connection.State == ConnectionState.Open)
                    return true;

                return false;
            }
        }

        public DbDataReader ExecuteReader(string sqlCommand, params DbParameter[] parameters)
        {
            if (!this.Open())
                return null;

            DbCommand cmd = this.Connection.CreateCommand();
            cmd.CommandText = sqlCommand;
            foreach (DbParameter item in parameters)
            {
                cmd.Parameters.Add(item);
            }

            return cmd.ExecuteReader();
        }

        public int ExecuteNonQuery(string sqlCommand, params DbParameter[] parameters)
        {
            int ret = -1;
            bool closeFlag = false;
            if (!this.IsOpened)
            {
                if (!this.Open())
                    return ret;

                closeFlag = true;
            }


            using (DbCommand cmd = this.Connection.CreateCommand())
            {
                cmd.CommandText = sqlCommand;
                foreach (DbParameter item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
                ret = cmd.ExecuteNonQuery();
            }

            if (closeFlag)
                this.Close();

            return ret;
        }

        public object ExecuteScalar(string sqlCommand, params DbParameter[] parameters)
        {
            object ret = null;
            bool closeFlag = false;
            if (!this.IsOpened)
            {
                if (!this.Open())
                    return null;

                closeFlag = true;
            }

            using (DbCommand cmd = this.Connection.CreateCommand())
            {
                cmd.CommandText = sqlCommand;
                foreach (DbParameter item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
                ret = cmd.ExecuteScalar();
            }

            if (closeFlag)
                this.Close();

            return ret;
        }

        #endregion
    }
}
