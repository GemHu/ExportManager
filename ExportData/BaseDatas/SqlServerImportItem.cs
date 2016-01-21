using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public abstract class SqlServerImportItem : SqlImportItem
    {
        #region Life Cycle

        public SqlServerImportItem(IProject project, string tableName, DBHelper helper)
            : base(project, tableName, helper)
        {
            this.SourceType = ESourceType.SqlServer;
        }

        #endregion

        public abstract override void DoExport(IExportCallback ballback);
    }

    public abstract class LHDBImportItem : SqlServerImportItem
    {
        #region Life Cycle

        public LHDBImportItem(IProject project, string tableName)
            : base(project, tableName, new LHDBHelper())
        {

        }

        #endregion

        #region Export

        public override abstract void DoExport(IExportCallback callback);
        
        #endregion
    }

    public abstract class WindDBImportItem : SqlServerImportItem
    {
        #region Life Cycle

        public WindDBImportItem(IProject project, string tableName)
            : base(project, tableName, new WindDBHelper())
        {

        }

        #endregion

        #region Export

        public override bool SyncImportState2Local()
        {
            if (base.SyncImportState2Local())
                return true;

            int count = this.GetItemsCount();
            if (count > 0)
            {
                this.TotalCount = count;
                this.ImportState = EImportStatus.WaitForImport;
            }
            else
            {
                this.ImportState = EImportStatus.NotDetected;
            }

            return true;
        }

        protected abstract int GetItemsCount();

        public override abstract void DoExport(IExportCallback callback);

        #endregion
    }
}
