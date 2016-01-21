using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dothan.DzHelpers;
using System.Data.Common;

namespace Dothan.ExportData
{
    public class ProjectInfoService_LH
    {
        public IDBHelper DB
        {
            get 
            {
                if (this._Db == null)
                    this._Db = new LHDBHelper();

                return this._Db;
            }
            set { this._Db = value; }
        }
        private IDBHelper _Db;

        public DbDataReader GetProjInfos(bool validZmdm)
        {
            string sql = string.Empty;
            if (validZmdm)
                sql = "SELECT * FROM dbo.tb_Projinfo WHERE zmdm > 0";
            else
                sql = "SELECT * FROM dbo.tb_Projinfo";

            return this.DB.ExecuteReader(sql);
        }

        public List<ProjectInfo_LH> GetProjectInfos(bool validZmdm)
        {
            List<ProjectInfo_LH> datas = new List<ProjectInfo_LH>();

            using (DbDataReader reader = this.GetProjInfos(validZmdm))
            {
                while (reader.Read())
                {
                    datas.Add(this.GetProjectInfo(reader));
                }
            }

            return datas;
        }

        protected ProjectInfo_LH GetProjectInfo(DbDataReader reader)
        {
            ProjectInfo_LH row = new ProjectInfo_LH();

            row.Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(ProjectInfo_LH.C_id)));
            row.Projname = Convert.ToString(reader.GetValue(reader.GetOrdinal(ProjectInfo_LH.C_projName)));
            row.Xmdm = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(ProjectInfo_LH.C_xmdm)));
            row.Zjdm = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(ProjectInfo_LH.C_zjdm)));
            row.Zmdm = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(ProjectInfo_LH.C_zmdm)));
            row.Sc = Convert.ToChar(reader.GetValue(reader.GetOrdinal(ProjectInfo_LH.C_sc)));

            return row;
        }
    }
}
