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
    public class CountryRepository : IRepository<tbCountries, View_tbCountries_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cou_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cou_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCountries_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbCountries Find(Expression<Func<tbCountries, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbCountries.Where(expression).OrderByDescending(x=>x.cou_Id).FirstOrDefault();
        }

        public int Insert(tbCountries item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cou_Description", item.cou_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@cou_IdUserCreate", item.cou_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCountries_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbCountries_List> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCountries_List.ToList();
        }

        public int Update(int Id, tbCountries item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cou_Description", item.cou_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@cou_IdUserModified", item.cou_IdUserModified, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cou_Id", Id, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCountries_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }

        public View_tbCountries_List Search(Expression<Func<View_tbCountries_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCountries_List.Where(expression).FirstOrDefault();
        }
    }
}
