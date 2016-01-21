using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Oracle.DataAccess.Client;
using System.Diagnostics;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public abstract class OracleImportItem : SqlImportItem
    {
        #region Life Cycle

        public OracleImportItem(IProject project, string tableName, DBHelper helper)
            : base(project, tableName, helper)
        {
            this.SourceType = ESourceType.Oracle;
        }

        #endregion

        #region Export

        public override abstract void DoExport(IExportCallback callback);

        #endregion
    }

    public abstract class O32DBImportItem : OracleImportItem
    {
        #region Life Cycle

        public O32DBImportItem(IProject project, string tableName)
            : base(project, tableName, new O32DBHelper())
        {
        }

        #endregion

        #region Export

        public override abstract void DoExport(IExportCallback callback);

        #endregion
    }
}
