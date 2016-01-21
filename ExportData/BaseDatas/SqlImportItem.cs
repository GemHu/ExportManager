using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public abstract class SqlImportItem : ImportItem
    {
        #region Life Cycle

        public SqlImportItem(IProject project, string tableName, DBHelper dbHelper)
            : base(project, dbHelper)
        {
            this.TableName = tableName;
        }

        #endregion

        #region Name

        public override string Name
        {
            get { return this.TableName; }
        }

        public override string ShownName
        {
            get { return this.TableName; }
        }

        public override string DataSource
        {
            get { return ConnStr; }
        }

        #endregion

        #region override

        public abstract override void DoExport(IExportCallback ballback);

        #endregion
    }

}
