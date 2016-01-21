using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public abstract class MysqlImportItem : SqlImportItem
    {
        #region Life Cycle

        public MysqlImportItem(IProject project, string tableName, DBHelper helper)
            : base(project, tableName, helper)
        {
            this.SourceType = ESourceType.Mysql;
        }

        #endregion

        #region DBHelper

        public new MysqlDBHelper DBHelper
        {
            get { return base.DBHelper as MysqlDBHelper; }
            set { base.DBHelper = value; }
        }

        #endregion

        #region Export

        public override abstract void DoExport(IExportCallback callback);

        #endregion
    }

    public abstract class SettleDBImportItem : MysqlImportItem
    {
        #region Life Cycle
		
        public SettleDBImportItem(IProject project, string tableName)
            : base(project, tableName, new SettleDBHelper())
        {
        }

	    #endregion

        #region Export

        public override abstract void DoExport(IExportCallback callback);

        #endregion
    }
}
