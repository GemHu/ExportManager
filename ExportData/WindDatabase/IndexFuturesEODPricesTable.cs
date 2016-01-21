using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dothan.ExportData
{
    /// <summary>
    /// 中国股指期货日行情。
    /// </summary>
    public class IndexFuturesEODPricesTable : WindDBImportItem, IExport2MarketTable
    {
        #region Life Cycle

        private const string Tag_TableName = "CIndexFuturesEODPrices";

        public IndexFuturesEODPricesTable(IProject project)
            : base(project, Tag_TableName)
        {

        }
        
        #endregion

        #region Export

        protected override int GetItemsCount()
        {
            string sql = string.Format("select count(*) from {0} where {1}=@{1}", this.TableName, IndexFuturesEODPriceRow.C_TRADE_DT);
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(string.Format("@{0}", IndexFuturesEODPriceRow.C_TRADE_DT), this.DateName)
            };

            return Convert.ToInt32(this.ExecuteScalar(sql, parameters));
        }

        public override void DoExport(IExportCallback callback)
        {
            string sql = string.Format("select * from {0} where {1}=@{1}", this.TableName, IndexFuturesEODPriceRow.C_TRADE_DT);
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(string.Format("@{0}", IndexFuturesEODPriceRow.C_TRADE_DT), this.DateName)
            };

            DbDataReader reader = this.ExecuteReader(sql, parameters);
            this.Export2MarketTable(reader, callback, this);
            this.Close();
        }

        protected IndexFuturesEODPriceRow GetIndexFuturesEODPriceRow(DbDataReader reader)
        {
            IndexFuturesEODPriceRow row = new IndexFuturesEODPriceRow();

            row.OBJECT_ID = this.GetString(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_OBJECT_ID)));
            row.S_INFO_WINDCODE = this.GetString(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_S_INFO_WINDCODE)));
            row.TRADE_DT = this.GetString(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_TRADE_DT)));
            row.S_DQ_PRESETTLE = this.GetDouble(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_S_DQ_PRESETTLE)));
            row.S_DQ_OPEN = this.GetDouble(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_S_DQ_OPEN)));
            row.S_DQ_HIGH = this.GetDouble(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_S_DQ_HIGH)));
            row.S_DQ_LOW = this.GetDouble(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_S_DQ_LOW)));
            row.S_DQ_CLOSE = this.GetDouble(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_S_DQ_CLOSE)));
            row.S_DQ_SETTLE = this.GetDouble(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_S_DQ_SETTLE)));
            row.S_DQ_VOLUME = this.GetDouble(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_S_DQ_VOLUME)));
            row.S_DQ_AMOUNT = this.GetDouble(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_S_DQ_AMOUNT)));
            row.S_DQ_OI = this.GetDouble(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_S_DQ_OI)));
            row.S_DQ_CHANGE = this.GetDouble(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_S_DQ_CHANGE)));
            row.FS_INFO_TYPE = this.GetString(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_FS_INFO_TYPE)));
            row.OPDATE = this.GetDateTime(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_OPDATE)));
            row.OPMODE = this.GetString(reader.GetValue(reader.GetOrdinal(IndexFuturesEODPriceRow.C_OPMODE)));

            return row;
        }

        #endregion

        #region IExport2MarketTable

        public MarketRow GetMarketRow(DbDataReader reader)
        {
            IndexFuturesEODPriceRow row = this.GetIndexFuturesEODPriceRow(reader);
            return this.GetMarketRow(row);
        }

        private MarketRow GetMarketRow(IndexFuturesEODPriceRow row)
        {
            // TODO:
            MarketRow market = new MarketRow();

            //market.Idx;
            market.Trade_Date = row.TRADE_DT;
            market.Security_Id = this.ConvertSecurityId(row.S_INFO_WINDCODE);
            //market.Last_Price = ;
            market.Change_Rate = row.S_DQ_CHANGE;
            market.Volume = row.S_DQ_VOLUME;
            //market.Trade_Count;
            market.Turn_Over = row.S_DQ_AMOUNT;
            //market.Pre_Close_Price;
            //market.Pre_Open_Interest;
            market.Pre_Settlement_Price = row.S_DQ_PRESETTLE;
            market.Open_Price = row.S_DQ_OPEN;
            market.Highest_Price = row.S_DQ_HIGH;
            market.Lowerst_Price = row.S_DQ_LOW;
            market.Close_Price = row.S_DQ_CLOSE;
            market.Open_Interest = row.S_DQ_OI;
            market.Settlement_Price = row.S_DQ_SETTLE;
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
