using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Data;

namespace Dothan.DzHelpers
{
    public class DbfDBHelper : DBHelper
    {
        public const string Tag_ConnName = "DbfDBConn";

        #region Life Cycle

        public DbfDBHelper(string tablePath)
        {
            this.TablePath = tablePath;
        }

        public string TablePath { get; set; }

        #endregion

        #region IDBHelper

        public override string ConnStr
        {
            get
            {
                if (string.IsNullOrEmpty(this.TablePath))
                    return string.Empty;

                return string.Format("Provider=VFPOLEDB.1;Data Source={0};collating Sequence=MACHINE", this.TablePath);
            }
        }

        public new OleDbConnection Connection
        {
            get { return base.Connection as OleDbConnection; }
            protected set { base.Connection = value; }
        }

        public override bool Open()
        {
            if (string.IsNullOrEmpty(this.ConnStr))
                return false;

            if (this.Connection == null)
            {
                try
                {
                    this.Connection = new OleDbConnection(this.ConnStr);
                    this.Connection.Open();
                }
                catch (Exception ee)
                {
                    Trace.WriteLine("### [ " + ee.Source + " ]; Exception: " + ee.Message);
                    Trace.WriteLine("### " + ee.StackTrace);
                    this.Connection = null;
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 静态方法

        public static DataTable GetDataTable(string tablePath, string tableName)
        {
            string sql = string.Format("select * from {0};", tableName);
            string msg = string.Empty;

            return ExecuteCommand(tablePath, sql, ref msg);
        }

        public static DataTable ExecuteCommand(string tablePath, string sqlCommand, ref string msg)
        {
            try
            {
                string strConn = string.Format("Provider=VFPOLEDB.1;Data Source={0};collating Sequence=MACHINE", tablePath);
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    using (OleDbCommand cmd = new OleDbCommand(sqlCommand, conn))
                    {
                        return DBHelper.GetDataTable(cmd.ExecuteReader());
                    }
                }
            }
            catch (Exception ee)
            {
                Trace.WriteLine("### [" + ee.Source + "] Exception: " + ee.Message);
                Trace.WriteLine("### " + ee.StackTrace);
                msg = ee.Message;
                return null;
            }
        }

        public static DataTable GetDataTable(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
                return null;

            FileInfo file = new FileInfo(fileName);

            return GetDataTable(file.Directory.FullName, file.Name);
        }

        #endregion

    }
}
