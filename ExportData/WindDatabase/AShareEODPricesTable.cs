using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dothan.ExportData
{
    /// <summary>
    /// 中国A股日行情数据表。
    /// </summary>
    public class AShareEODPricesTable : WindDBImportItem, IExport2MarketTable
    {
        #region Life Cycle

        private const string Tag_TableName = "AShareEODPrices";

        public AShareEODPricesTable(IProject project)
            : base(project, Tag_TableName)
        {

        }
        
        #endregion

        #region Export

        protected override int GetItemsCount()
        {
            string sql = string.Format("select count(*) from {0} where {1}=@{1}", this.TableName, AShareEODPriceRow.C_TRADE_DT);
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(string.Format("@{0}", AShareEODPriceRow.C_TRADE_DT), this.DateName)
            };

            return Convert.ToInt32(this.ExecuteScalar(sql, parameters));
        }

        public override void DoExport(IExportCallback callback)
        {
            string sql = string.Format("select * from {0} where {1}=@{1}", this.TableName, AShareEODPriceRow.C_TRADE_DT);
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter(string.Format("@{0}", AShareEODPriceRow.C_TRADE_DT), this.DateName)
            };

            DbDataReader reader = this.ExecuteReader(sql, parameters);
            this.Export2MarketTable(reader, callback, this);
            this.Close();
        }

        protected AShareEODPriceRow GetAShareEODPriceRow(DbDataReader reader)
        {
            AShareEODPriceRow row = new AShareEODPriceRow();

            row.OBJECT_ID = this.GetString(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_OBJECT_ID)));
            row.S_INFO_WINDCODE = this.GetString(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_INFO_WINDCODE)));
            row.TRADE_DT = this.GetString(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_TRADE_DT)));
            row.CRNCY_CODE = this.GetString(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_CRNCY_CODE)));
            row.S_DQ_PRECLOSE = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_PRECLOSE)));
            row.S_DQ_OPEN = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_OPEN)));
            row.S_DQ_HIGH = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_HIGH)));
            row.S_DQ_LOW = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_LOW)));
            row.S_DQ_CLOSE = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_CLOSE)));
            row.S_DQ_CHANGE = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_CHANGE)));
            row.S_DQ_PCTCHANGE = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_PCTCHANGE)));
            row.S_DQ_VOLUME = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_VOLUME)));
            row.S_DQ_AMOUNT = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_AMOUNT)));
            row.S_DQ_ADJPRECLOSE = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_ADJPRECLOSE)));
            row.S_DQ_ADJOPEN = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_ADJOPEN)));
            row.S_DQ_ADJHIGH = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_ADJHIGH)));
            row.S_DQ_ADJLOW = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_ADJLOW)));
            row.S_DQ_ADJCLOSE = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_ADJCLOSE)));
            row.S_DQ_ADJFACTOR = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_ADJFACTOR)));
            row.S_DQ_AVGPRICE = this.GetDouble(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_AVGPRICE)));
            row.S_DQ_TRADESTATUS = this.GetString(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_S_DQ_TRADESTATUS)));
            row.OPDATE = this.GetDateTime(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_OPDATE)));
            row.OPMODE = this.GetString(reader.GetValue(reader.GetOrdinal(AShareEODPriceRow.C_OPMODE)));

            return row;
        }

        #endregion

        #region IExport2MarketTable

        private MarketRow GetMarketRow(AShareEODPriceRow row)
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
            market.Pre_Close_Price = row.S_DQ_PRECLOSE;
            //market.Pre_Open_Interest;
            //market.Pre_Settlement_Price;
            market.Open_Price = row.S_DQ_OPEN;
            market.Highest_Price = row.S_DQ_HIGH;
            market.Lowerst_Price = row.S_DQ_LOW;
            market.Close_Price = row.S_DQ_CLOSE;
            //market.Open_Interest;
            //market.Settlement_Price;
            //market.Up_Limit_Price;
            //market.Lower_Limit_Price;
            market.Currency = row.CRNCY_CODE;
            //market.Epsilon;
            //market.Multiplier;
            //market.Rule;
            //market.Ipov;
            //market.Ytm;
            //market.Syl1;
            //market.Syl2;
            //market.Pre_Delta;
            //market.Curr_Delta;
            market.Avg_Price = row.S_DQ_AVGPRICE;
            //market.Sys_Update_Time;
            //market.Update_Time;

            return market;
        }

        public MarketRow GetMarketRow(DbDataReader reader)
        {
            AShareEODPriceRow row = this.GetAShareEODPriceRow(reader);
            return this.GetMarketRow(row);
        }

        #endregion
    }
}
