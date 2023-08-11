using AHM.Logistic.Smart.DataAccess.Repositories;
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

namespace AHM.Logistic.Smart.DataAccess.TestRepositories
{
    public class AreasRepositoryTest : IRepository<tbAreas, View_Areas_List>
    { 
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@are_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Areas_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbAreas Find(Expression<Func<tbAreas, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.tbAreas.Where(expression).FirstOrDefault();
        }
        public View_Areas_List Search(Expression<Func<View_Areas_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.View_Areas_List.Where(expression).FirstOrDefault();
        }

        public int Insert(tbAreas item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@are_Description", item.are_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@are_IdUserCreate", item.are_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Areas_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_Areas_List> List()
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.View_Areas_List.ToList();
        }

        public int Update(int Id, tbAreas item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@are_Description", item.are_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@are_IdUserModified", item.are_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Areas_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
