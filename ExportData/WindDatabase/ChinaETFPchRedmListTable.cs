using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dothan.ExportData
{
    /// <summary>
    /// 中国ETF申购赎回清单。
    /// </summary>
    public class ChinaETFPchRedmListTable : WindDBImportItem, IExport2MarketTable
    {
        #region Life Cycle

        private const string Tag_TableName = "CHINAEFTPCHREDMLIST";

        public ChinaETFPchRedmListTable(IProject project)
            : base(project, Tag_TableName)
        {

        }
        
        #endregion

        #region Export

        protected override int GetItemsCount()
        {
            string sql = string.Format("select count(*) from {0} where {1}=@{1}", this.TableName, ChinaETFPchRedmRow.C_TRADE_DT);
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(string.Format("@{0}", ChinaETFPchRedmRow.C_TRADE_DT), this.DateName)
            };

            return Convert.ToInt32(this.ExecuteScalar(sql, parameters));
        }

        public override void DoExport(IExportCallback callback)
        {
            string sql = string.Format("select * from {0} where {1}=@{1}", this.TableName, ChinaETFPchRedmRow.C_TRADE_DT);
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(string.Format("@{0}", ChinaETFPchRedmRow.C_TRADE_DT), this.DateName)
            };

            DbDataReader reader = this.ExecuteReader(sql, parameters);
            this.Export2MarketTable(reader, callback, this);
            this.Close();
        }

        protected ChinaETFPchRedmRow GetChinaETFPchRedmListRow(DbDataReader reader)
        {
            ChinaETFPchRedmRow row = new ChinaETFPchRedmRow();

            row.OBJECT_ID = this.GetString(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_OBJECT_ID)));
            row.S_INFO_WINDCODE = this.GetString(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_S_INFO_WINDCODE)));
            row.TRADE_DT = this.GetString(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_TRADE_DT)));
            row.F_INFO_CASHDIF = this.GetDouble(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_F_INFO_CASHDIF)));
            row.F_INFO_MINPRASET = this.GetDouble(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_F_INFO_MINPRASET)));
            row.F_INFO_ESTICASH = this.GetDouble(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_F_INFO_ESTICASH)));
            row.F_INFO_CASHSUBUPLIMIT = this.GetDouble(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_F_INFO_CASHSUBUPLIMIT)));
            row.F_INFO_MINPRUNITS = this.GetDouble(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_F_INFO_MINPRUNITS)));
            row.F_INFO_PRPERMIT = this.GetString(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_F_INFO_PRPERMIT)));
            row.F_INFO_CONNUM = this.GetDouble(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_F_INFO_CONNUM)));
            row.OPDATE = this.GetDateTime(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_OPDATE)));
            row.OPMODE = this.GetString(reader.GetValue(reader.GetOrdinal(ChinaETFPchRedmRow.C_OPMODE)));

            return row;
        }

        #endregion

        #region IExport2MarketTable

        public MarketRow GetMarketRow(DbDataReader reader)
        {
            ChinaETFPchRedmRow row = this.GetChinaETFPchRedmListRow(reader);
            return this.GetMarketRow(row);
        }

        private MarketRow GetMarketRow(ChinaETFPchRedmRow row)
        {
            // TODO:
            MarketRow market = new MarketRow();

            //market.Idx;
            market.Trade_Date = row.TRADE_DT;
            market.Security_Id = this.ConvertSecurityId(row.S_INFO_WINDCODE);
            //market.Last_Price = ;
            //market.Change_Rate;
            //market.Volume;
            //market.Trade_Count;
            //market.Turn_Over;
            //market.Pre_Close_Price;
            //market.Pre_Open_Interest;
            //market.Pre_Settlement_Price;
            //market.Open_Price;
            //market.Highest_Price;
            //market.Lowerst_Price;
            //market.Close_Price;
            //market.Open_Interest;
            //market.Settlement_Price;
            //market.Up_Limit_Price;
            //market.Lower_Limit_Price;
            //market.Currency;
            //market.Epsilon;
            //market.Multiplier;
            //market.Rule;
            //market.Ipov;
            //market.Ytm;
            //market.Syl1;
            //market.Syl2;
            //market.Pre_Delta;
            //market.Curr_Delta;
            //market.Avg_Price;
            //market.Sys_Update_Time;
            //market.Update_Time;

            return market;
        }

        #endregion
    }
}
