using AHM.Logistic.Smart.Entities.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public class MunicipalitiesRepository : IRepository<tbMunicipalities, View_tbMunicipalities_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@mun_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@mun_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbMunicipalities_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public View_tbMunicipalities_List Search(Expression<Func<View_tbMunicipalities_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.View_tbMunicipalities_List.Where(expression).FirstOrDefault();
        }

        public tbMunicipalities Find(Expression<Func<tbMunicipalities, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbMunicipalities.Where(expression).OrderByDescending(x=>x.mun_Id).FirstOrDefault();
        }

        public int Insert(tbMunicipalities item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@mun_Code", item.mun_Code, DbType.String, ParameterDirection.Input);
            parameters.Add("@mun_Description", item.mun_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@dep_Id", item.dep_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@mun_UsuarioCrea", item.mun_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbMunicipalities_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbMunicipalities_List> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbMunicipalities_List.ToList();
        }

        public int Update(int Id, tbMunicipalities item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@mun_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@mun_Code", item.mun_Code, DbType.String, ParameterDirection.Input);
            parameters.Add("@mun_Descripcion", item.mun_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@dep_Id", item.dep_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@mun_UsuarioModifica", item.mun_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbMunicipalities_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
