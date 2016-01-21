using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Dothan.ProjectInfo
{
    public class ProjectInfoLibrary : InfoGroup<InfoGroup, ProjectCode>
    {
        public ProjectInfoLibrary()
            : base(null)
        {

        }

        public void BuildProjectInfoLibrary()
        {
            this.Children.Clear();
            SettleEntities context = new SettleEntities();
            List<tb_project_info> infoList = context.tb_project_info.ToList();
            foreach (tb_project_info item in infoList)
            {
                this.Add(item);
            }
        }

        #region Children

        public override void Add(tb_project_info item)
        {
            ProjectCode project = this.Children.Where(o => o.Xmdm == item.xmdm).FirstOrDefault();
            if (project == null)
            {
                project = new ProjectCode(this, item.xmdm, item.project_name);
                this.Add(project);
            }

            project.Add(item);
        }

        #endregion
    }

    public abstract class InfoItem : NotificationObject
    {
        #region Life Cycle

        public InfoItem(InfoGroup parent)
        {
            this.Parent = parent;
        }

        #endregion

        #region Parent

        public InfoGroup Parent
        {
            get { return _Parent; }
            protected set
            {
                _Parent = value;
                this.RaisePropertyChanged("Parent");
            }
        }
        private InfoGroup _Parent;

        #endregion
    }

    public abstract class InfoItem<PT> : InfoItem where PT : InfoGroup
    {
        #region Life Cycle

        public InfoItem(PT parent)
            : base(parent)
        {
        }

        #endregion

        #region Parent

        public new PT Parent
        {
            get { return base.Parent as PT; }
            protected set { base.Parent = value; }
        }

        #endregion
    }

    public abstract class InfoGroup : InfoItem
    {
        #region Life Cycle

        public InfoGroup(InfoGroup parent)
            : base(parent)
        {

        }

        #endregion
    }

    public abstract class InfoGroup<PT, CN> : InfoGroup
        where PT : InfoGroup
        where CN : InfoItem
    {
        #region Life Cycle

        public InfoGroup(PT parent)
            : base(parent)
        {
            this.Children = new ObservableCollection<CN>();
        }

        #endregion

        #region Children

        public ObservableCollection<CN> Children
        {
            get { return _Children; }
            set
            {
                _Children = value;
                this.RaisePropertyChanged("Children");
            }
        }
        private ObservableCollection<CN> _Children;

        public void Add(CN zcdy)
        {
            if (zcdy == null)
                return;

            this.Children.Add(zcdy);
        }

        public abstract void Add(tb_project_info item);

        #endregion
    }

    public class ProjectCode : InfoGroup<ProjectInfoLibrary, AssertsUnit>
    {
        #region Life Cycle

        public ProjectCode(ProjectInfoLibrary parent, int xmdm)
            : this(parent, xmdm, string.Empty)
        {
        }

        public ProjectCode(ProjectInfoLibrary parent, int xmdm, string projectName)
            : base(parent)
        {
            this.Xmdm = xmdm;
            this.ProjectName = projectName;
        }

        #endregion

        #region ProjectName

        private string _ProjectName;

        public string ProjectName
        {
            get { return _ProjectName; }
            set
            {
                _ProjectName = value;
                this.RaisePropertyChanged("ProjectName");
            }
        }

        public string ShownName
        {
            get
            {
                if (string.IsNullOrEmpty(this.ProjectName))
                    return this.Xmdm.ToString();
                else
                    return string.Format("{0}({1})", this.ProjectName, this.Xmdm);
            }
        }

        #endregion

        #region Xmdm

        /// <summary>
        /// 项目代码。
        /// </summary>
        public int Xmdm
        {
            get { return _Xmdm; }
            set
            {
                _Xmdm = value;
                this.RaisePropertyChanged("Xmdm");
            }
        }
        private int _Xmdm;

        #endregion

        #region Children

        public override void Add(tb_project_info row)
        {
            if (row == null)
                return;

            AssertsUnit asserts = this.Children.Where(o => o.Zcdy.Equals(row.zcdy, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (asserts == null)
            {
                asserts = new AssertsUnit(this, row.zcdy);
                this.Add(asserts);
            }

            asserts.Add(row);
        }

        #endregion
    }

    public class AssertsUnit : InfoGroup<ProjectCode, CombinedCode>
    {
        #region Life Cycle

        public AssertsUnit(ProjectCode parent, string zcdy)
            : base(parent)
        {
            this.Zcdy = zcdy;
        }

        #endregion

        #region Property

        public new ProjectCode Parent
        {
            get { return base.Parent as ProjectCode; }
            set { this.Parent = value; }
        }

        /// <summary>
        /// 项目代码。
        /// </summary>
        public int Xmdm
        {
            get { return Parent.Xmdm; }
        }

        /// <summary>
        /// 资产单元。
        /// </summary>
        public string Zcdy
        {
            get { return _Zcdy; }
            set
            {
                _Zcdy = value;
                this.RaisePropertyChanged("Zcdy");
            }
        }
        private string _Zcdy;

        #endregion

        #region Children

        public override void Add(tb_project_info item)
        {
            if (item == null)
                return;

            CombinedCode code = this.Children.Where(o => o.Zhdm.Equals(item.zhdm, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (code == null)
            {
                code = new CombinedCode(this, item.zhdm);
                this.Add(code);
            }
        }

        #endregion
    }

    public class CombinedCode : InfoItem<AssertsUnit>
    {
        #region Life Cycle

        public CombinedCode(AssertsUnit parent, string zhdm)
            : base(parent)
        {
            this.Zhdm = zhdm;
        }

        #endregion

        #region Property

        /// <summary>
        /// 项目代码；通过Parnet获取。
        /// </summary>
        public int Xmdm
        {
            get { return this.Parent.Xmdm; }
        }


        /// <summary>
        /// 资产单元；通过Parent获取。
        /// </summary>
        public string Zcdy
        {
            get { return this.Parent.Zcdy; }
        }

        /// <summary>
        /// 组合代码。
        /// </summary>
        public string Zhdm
        {
            get { return _Zhdm; }
            set
            {
                _Zhdm = value;
                this.RaisePropertyChanged("Zhdm");
            }
        }
        private string _Zhdm;

        #endregion
    }
}
