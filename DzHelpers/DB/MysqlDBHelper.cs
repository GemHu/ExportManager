using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Dothan.DzHelpers
{
    public abstract class MysqlDBHelper : DBHelper
    {
        public override bool Open()
        {
            if (this.Connection == null)
            {
                try
                {
                    this.Connection = new MySqlConnection(this.ConnStr);
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

    }

    public class SettleDBHelper : MysqlDBHelper
    {
        public const string Tag_ConnName = "SettleDBConn";

        #region Life Cycle

        public SettleDBHelper()
        {
            this.ConnStr = ConfigurationHelper.ConnectionStrings[Tag_ConnName].ConnectionString;
        }

        #endregion

        /// <summary>
        /// 检查数据库链接是否可用。
        /// </summary>
        public static bool CheckConnCanUse()
        {
            bool ret = false;
            try
            {
                string connStr = ConfigurationHelper.ConnectionStrings[Tag_ConnName].ConnectionString;
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    ret = true;
                }
            }
            catch (System.Exception ee)
            {
                Trace.WriteLine("### [" + ee.Source + "]; message = " + ee.Message);
                Trace.WriteLine("### " + ee.StackTrace);
                return false;
            }

            return ret;
        }
    }
}
