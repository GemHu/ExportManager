using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Dothan.DzHelpers;

namespace Dothan.ExportData
{
    public class ImportRecordTable : SettleDBHelper
    {
        #region Columns

        public const string T_TableName = "tb_import_record";
        public const string C_Id = "_Id";
        public const string C_TradeDate = "_TradeDate";
        public const string C_TableName = "_TableName";
        public const string C_SourceType = "_SourceType";
        public const string C_ImportState = "_ImportState";
        public const string C_ImportCount = "_ImportCount";
        public const string C_TotalCount = "_TotalCount";
        public const string C_ImportDate = "_ImportDate";
        public const string C_Comment = "_Comment";

        #endregion

        #region Life Cycle

        public ImportRecordTable()
        {
            this.CreateTableIfNotExist();
        }

        protected void CreateTableIfNotExist()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("CREATE TABLE IF NOT EXISTS {0}(", T_TableName));
            builder.Append(string.Format("{0} BIGINT NOT NULL PRIMARY KEY auto_increment,", C_Id));
            builder.Append(string.Format("{0} VARCHAR(8) NOT NULL,", C_TradeDate));
            builder.Append(string.Format("{0} VARCHAR(64) NOT NULL,", C_TableName));
            builder.Append(string.Format("{0} INT,", C_SourceType));
            builder.Append(string.Format("{0} INT,", C_ImportState));
            builder.Append(string.Format("{0} INT,", C_ImportCount));
            builder.Append(string.Format("{0} INT,", C_TotalCount));
            builder.Append(string.Format("{0} DATETIME,", C_ImportDate));
            builder.Append(string.Format("{0} VARCHAR(64));", C_Comment));
            string sql = builder.ToString();

            this.ExecuteNonQuery(sql);
        }

        #endregion

        #region CURD

        public static void UpdateImportDate(string tradeDate, string tableName, DateTime date)
        {
            ImportRecordTable service = new ImportRecordTable();
            if (!service.Open())
                return;

            ImportRecordRow info = service.GetImportRecordRow(tradeDate, tableName);
            if (info != null)
            {
                service.UpdateImportDate(info.Id, date);
            }
            service.Close();
        }

        public void UpdateImportDate(long id, DateTime importTime)
        {
            string sql = string.Format("update {0} set {1} = @{1} where {2} = @{2}", T_TableName,
                C_ImportDate, C_Id);

            DbParameter[] parameters = new DbParameter[]{
                new MySqlParameter(string.Format("@{0}", C_ImportDate), importTime),
                new MySqlParameter(string.Format("@{0}", C_Id), id)
            };

            this.ExecuteNonQuery(sql, parameters);
        }

        public void Add(ImportRecordRow info)
        {
            this.Add(info.TradeDate, info.TableName, (int)info.SourceType, (int)info.ImportState, info.ImportCount, info.TotalCount, info.ImportDate, info.Comment);
        }

        public void Add(string tradeDate, string tableName, int sourceType, int importState, int importCount, int totalCount, DateTime importDate, string comment)
        {
            string sql = string.Format("insert into {0}({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}) VALUES(@{1}, @{2}, @{3}, @{4}, @{5}, @{6}, @{7}, @{8})", T_TableName,
                C_TradeDate, C_TableName, C_SourceType, C_ImportState, C_ImportCount, C_TotalCount, C_ImportDate, C_Comment);

            MySqlParameter[] parameters = new MySqlParameter[]{
                new MySqlParameter(string.Format("@{0}", C_TradeDate), tradeDate), 
                new MySqlParameter(string.Format("@{0}", C_TableName), tableName), 
                new MySqlParameter(string.Format("@{0}", C_SourceType), sourceType), 
                new MySqlParameter(string.Format("@{0}", C_ImportState), importState), 
                new MySqlParameter(string.Format("@{0}", C_ImportCount), importCount),
                new MySqlParameter(string.Format("@{0}", C_TotalCount), totalCount),
                new MySqlParameter(string.Format("@{0}", C_ImportDate), importDate), 
                new MySqlParameter(string.Format("@{0}", C_Comment), comment)
            };

            this.ExecuteNonQuery(sql, parameters);
        }

        public ImportRecordRow GetImportRecordRow(string tradeDate, string tableName)
        {
            string sql = string.Format("select * FROM {0} where {1} = @{1} AND {2} = @{2}", T_TableName,
                C_TradeDate, C_TableName);

            DbParameter[] parameters = new DbParameter[]{
                new MySqlParameter(string.Format("@{0}", C_TradeDate), tradeDate),
                new MySqlParameter(string.Format("@{0}", C_TableName), tableName)
            };

            using (DbDataReader reader = this.ExecuteReader(sql, parameters))
            {
                if (reader.Read())
                    return this.GetImportRecordRow(reader);
            }

            return null;
        }

        public ImportRecordRow GetImportRecordRow(DbDataReader reader)
        {
            ImportRecordRow row = new ImportRecordRow();

            row.Id = Convert.ToInt64(reader.GetValue(reader.GetOrdinal(C_Id)));
            row.TradeDate = Convert.ToString(reader.GetValue(reader.GetOrdinal(C_TradeDate)));
            row.TableName = Convert.ToString(reader.GetValue(reader.GetOrdinal(C_TableName)));
            row.SourceType = (ESourceType)Convert.ToInt32(reader.GetValue(reader.GetOrdinal(C_SourceType)));
            row.ImportState = (EImportStatus)Convert.ToInt32(reader.GetValue(reader.GetOrdinal(C_ImportState)));
            row.ImportCount = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(C_ImportCount)));
            row.TotalCount = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(C_TotalCount)));
            row.ImportDate = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal(C_ImportDate)));
            row.Comment = Convert.ToString(reader.GetValue(reader.GetOrdinal(C_Comment)));

            return row;
        }

        /// <summary>
        /// 更新文件处理进度。
        /// </summary>
        public void UpdateImportStatus(long id, EImportStatus importState)
        {
            string sql = string.Format("update {0} set {1} = @{1} where {2} = @{2}", T_TableName,
                C_ImportState, C_Id);

            DbParameter[] parameters = new DbParameter[]{
                new MySqlParameter(string.Format("@{0}", C_ImportState), importState),
                new MySqlParameter(string.Format("@{0}", C_Id), id)
            };

            this.ExecuteNonQuery(sql, parameters);
        }

        public void Update(long id, EImportStatus importState, int importCount, int totalCount)
        {
            string sql = string.Format("update {0} set {1} = @{1}, {2} = @{2}, {3} = @{3} where {4} = @{4}", T_TableName,
                C_ImportState, C_ImportCount, C_TotalCount, C_Id);

            DbParameter[] parameters = new DbParameter[]{
                new MySqlParameter(string.Format("@{0}", C_ImportState), importState),
                new MySqlParameter(string.Format("@{0}", C_ImportCount), importCount),
                new MySqlParameter(string.Format("@{0}", C_TotalCount), totalCount),
                new MySqlParameter(string.Format("@{0}", C_Id), id)
            };

            this.ExecuteNonQuery(sql, parameters);
        }

        #endregion
    }

    public class ImportRecordRow
    {
        public ImportRecordRow()
        {

        }

        public ImportRecordRow(string tradeDate, string tableName)
        {
            this.TradeDate = tradeDate;
            this.TableName = tableName;
        }

        /// <summary>
        /// 导入项ID,自增。
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 导入日期字符串(DBF文件夹名称)。
        /// </summary>
        public string TradeDate { get; set; }

        /// <summary>
        /// 数据表名称或DBF文件名称。
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 数据源类型，如DBF文件、MySql数据库等。
        /// </summary>
        public ESourceType SourceType { get; set; }

        /// <summary>
        /// 数据的导入状态，如果已经导入完成，则之后就不会在去重新导入该文件，或数据表中对应日期的数据了。
        /// </summary>
        public EImportStatus ImportState { get; set; }

        /// <summary>
        /// 目标日期数据表中已经导入数量。
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 目标日期数据表中数据总数。
        /// </summary>
        public int ImportCount { get; set; }

        /// <summary>
        /// 数据导入的时间。
        /// </summary>
        public DateTime ImportDate { get; set; }

        /// <summary>
        /// 备注信息。
        /// </summary>
        public string Comment { get; set; }
    }
}
