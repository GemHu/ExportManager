using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Dothan.DzHelpers
{
    public interface IDBHelper
    {
        string ConnStr { get; }

        DbConnection Connection { get; }

        bool Open();

        bool Close();

        DbDataReader ExecuteReader(string sqlCommand, params DbParameter[] parameters);

        int ExecuteNonQuery(string sqlCommand, params DbParameter[] parameters);

        object ExecuteScalar(string sqlCommand, params DbParameter[] parameters);
}
}
