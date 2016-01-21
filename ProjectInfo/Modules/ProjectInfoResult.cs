using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;
using System.Collections.ObjectModel;

namespace Dothan.ProjectInfo
{
    public class ProjectInfoResult : NotificationObject
    {
        public ProjectInfoResult()
        {
            this.StatisticsInfoList = new List<MoneyStatisticsInfo>() { this.StatisticsInfo };
        }

        #region SecurityStoreAcc

        public List<tb_security_store_acc> SecurityStoreAccList
        {
            get { return _SecurityStoreAccList; }
            set
            {
                _SecurityStoreAccList = value;
                this.RaisePropertyChanged("SecurityStoreAccList");
            }
        }
        private List<tb_security_store_acc> _SecurityStoreAccList;

        #endregion

        #region MoneyStore

        public List<tb_money_store> MoneyStoreList
        {
            get { return _MoneyStoreList; }
            set
            {
                _MoneyStoreList = value;
                this.RaisePropertyChanged("MoneyStoreList");
            }
        }
        private List<tb_money_store> _MoneyStoreList;

        /// <summary>
        /// SecurityStoreAccList信息总计，用于在视图中展示统计信息。
        /// </summary>
        public List<MoneyStatisticsInfo> StatisticsInfoList
        {
            get { return this._StatisticsInfoList; }
            protected set
            {
                this._StatisticsInfoList = value;
                this.RaisePropertyChanged("StatisticsInfoList");
            }
        }
        private List<MoneyStatisticsInfo> _StatisticsInfoList;

        /// <summary>
        /// 当前的资金统计信息。
        /// </summary>
        public MoneyStatisticsInfo StatisticsInfo
        {
            get { return this._StatisticsInfo; }
        }
        private MoneyStatisticsInfo _StatisticsInfo = new MoneyStatisticsInfo();

        #endregion

        /// <summary>
        /// 更新给定分组的所有信息。
        /// </summary>
        /// <param name="xmdm">项目代码</param>
        /// <param name="zcdy">资产单元</param>
        /// <param name="zhdm">组合代码</param>
        public void Update(int xmdm, string zcdy, string zhdm)
        {
            this.UpdateSecurityInfo(xmdm, zcdy, zhdm);
            this.UpdateMoneyInfo(xmdm, zcdy, zhdm);
        }

        /// <summary>
        /// 更新给定分组的证券信息。
        /// </summary>
        /// <param name="xmdm">项目代码</param>
        /// <param name="zcdy">资产单元</param>
        /// <param name="zhdm">组合代码</param>
        public void UpdateSecurityInfo(int xmdm, string zcdy, string zhdm)
        {
            if (xmdm < 0) return;

            SettleEntities dbContext = new SettleEntities();
            if (!string.IsNullOrEmpty(zhdm))
                this.SecurityStoreAccList = dbContext.tb_security_store_acc.Where(o => o.xmdm == xmdm && o.zcdy.Equals(zcdy, StringComparison.OrdinalIgnoreCase) && o.zhdm.Equals(zhdm, StringComparison.OrdinalIgnoreCase)).ToList();
            else if (!string.IsNullOrEmpty(zcdy))
                this.SecurityStoreAccList = dbContext.tb_security_store_acc.Where(o => o.xmdm == xmdm && o.zcdy.Equals(zcdy, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                this.SecurityStoreAccList = dbContext.tb_security_store_acc.Where(o => o.xmdm == xmdm).ToList();
        }

        /// <summary>
        /// 更新给定分组的资金信息。
        /// </summary>
        /// <param name="xmdm">项目代码</param>
        /// <param name="zcdy">资产单元</param>
        /// <param name="zhdm">组合代码</param>
        public void UpdateMoneyInfo(int xmdm, string zcdy, string zhdm)
        {
            if (xmdm < 0) return;

            SettleEntities context = new SettleEntities();
            if (!string.IsNullOrEmpty(zhdm))
                this.MoneyStoreList = context.tb_money_store.Where(o => o.xmdm == xmdm && o.zcdy.Equals(zcdy) && o.zhdm.Equals(zhdm)).ToList();
            else if (!string.IsNullOrEmpty(zcdy))
                this.MoneyStoreList = context.tb_money_store.Where(o => o.xmdm == xmdm && o.zcdy.Equals(zcdy)).ToList();
            else
                this.MoneyStoreList = context.tb_money_store.Where(o => o.xmdm == xmdm).ToList();

            //
            this.StatisticsInfo.FrozeBalance = this.MoneyStoreList.Where(o => o.froze == 1).Sum(o => o.money);
            this.StatisticsInfo.ValidBalance = this.MoneyStoreList.Where(o => o.froze == 0).Sum(o => o.money);
            this.StatisticsInfo.Total = this.MoneyStoreList.Sum(o => o.money);
        }
    }

    /// <summary>
    /// 资金统计信息类。
    /// </summary>
    public class MoneyStatisticsInfo : NotificationObject
    {
        #region Entities

        /// <summary>
        /// 冻结金额。
        /// </summary>
        public double? FrozeBalance
        {
            get { return _FrozeBalance; }
            set
            {
                _FrozeBalance = value;
                this.RaisePropertyChanged("FrozeBalance");
            }
        }
        private double? _FrozeBalance;

        /// <summary>
        /// 可用金额。
        /// </summary>
        public double? ValidBalance
        {
            get { return _ValidBalance; }
            set
            {
                _ValidBalance = value;
                this.RaisePropertyChanged("ValidBalance");
            }
        }
        private double? _ValidBalance;

        private double? _Total;

        public double? Total
        {
            get { return _Total; }
            set
            {
                _Total = value;
                this.RaisePropertyChanged("Total");
            }
        }

        #endregion
    }
}
