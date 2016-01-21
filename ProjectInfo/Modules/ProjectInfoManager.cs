using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;
using System.Windows;

namespace Dothan.ProjectInfo
{
    public class ProjectInfoManager : NotificationObject
    {
        #region Life Cycle

        public ProjectInfoManager()
        {
            this.TheProjectInfoLibrary = new ProjectInfoLibrary();
            this.TheProjectInfoResult = new ProjectInfoResult();
        }

        #endregion

        #region ProjectLibrary

        public ProjectInfoLibrary TheProjectInfoLibrary
        {
            get { return _TheProjectInfoLibrary; }
            set
            {
                _TheProjectInfoLibrary = value;
                this.RaisePropertyChanged("TheProjectInfoLibrary");
            }
        }
        private ProjectInfoLibrary _TheProjectInfoLibrary;

        /// <summary>
        /// 生成ProjectInfo列表数据。
        /// </summary>
        public void UpdateProjectInfoLibrary()
        {
            this.TheProjectInfoLibrary.BuildProjectInfoLibrary();
            if (this.TheProjectInfoLibrary.Children.Count <= 0)
                this.StatusInfo = "No data detected";
            else
                this.StatusInfo = string.Format("Projects Count = {0}", this.TheProjectInfoLibrary.Children.Count);
        }

        #endregion

        #region ProjectInfoResult

        public ProjectInfoResult TheProjectInfoResult
        {
            get { return _TheProjectInfoResult; }
            set
            {
                _TheProjectInfoResult = value;
                this.RaisePropertyChanged("TheProjectInfoResult");
            }
        }
        private ProjectInfoResult _TheProjectInfoResult;

        public void UpdateResultInfo(InfoItem item)
        {
            CombinedCode item1 = item as CombinedCode;
            AssertsUnit item2 = item as AssertsUnit;
            ProjectCode item3 = item as ProjectCode;

            if (item1 != null)
                this.UpdateResultInfo(item1.Xmdm, item1.Zcdy, item1.Zhdm);
            else if (item2 != null)
                this.UpdateResultInfo(item2.Xmdm, item2.Zcdy, null);
            else if (item3 != null)
                this.UpdateResultInfo(item3.Xmdm, null, null);
        }

        /// <summary>
        /// 更新查询结果。
        /// </summary>
        /// <param name="xmdm">项目代码</param>
        /// <param name="zjdy">资金单元</param>
        /// <param name="zhdm">组合代码</param>
        public void UpdateResultInfo(int xmdm, string zjdy, string zhdm)
        {
            this.TheProjectInfoResult.Update(xmdm, zjdy, zhdm);
        }

        #endregion

        #region StatusInfo

        public string StatusInfo
        {
            get { return _StatusInfo; }
            set 
            {
                if (this._StatusInfo != value)
                {
                    _StatusInfo = value;
                    this.RaisePropertyChanged("StatusInfo");
                }
            }
        }
        private string _StatusInfo;

        #endregion
    }
}
