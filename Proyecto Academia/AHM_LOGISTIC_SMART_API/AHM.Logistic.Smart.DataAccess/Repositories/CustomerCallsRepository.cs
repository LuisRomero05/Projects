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
    public class CustomerCallsRepository : IRepository<tbCustomerCalls, View_tbCustomerCalls>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cca_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cca_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCustomerCalls_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbCustomerCalls Find(Expression<Func<tbCustomerCalls, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbCustomerCalls.Where(expression).FirstOrDefault();   
        }

        public int Insert(tbCustomerCalls item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cca_CallType", item.cca_CallType, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cca_Business", item.cca_Business, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cca_Date", item.cca_Date, DbType.Date, ParameterDirection.Input);
            parameters.Add("@cca_StartTime", item.cca_StartTime, DbType.String, ParameterDirection.Input);
            parameters.Add("@cca_EndTime", item.cca_EndTime, DbType.String, ParameterDirection.Input);
            parameters.Add("@cca_Result", item.cca_Result, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_Id", item.cus_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cca_IdUserCreate", item.cca_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCustomerCalls_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbCustomerCalls> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCustomerCalls.ToList();
        }

        public View_tbCustomerCalls Search(Expression<Func<View_tbCustomerCalls, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCustomerCalls.Where(expression).FirstOrDefault();
        }

        public int Update(int id, tbCustomerCalls item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cca_Id", item.cca_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cca_CallType", item.cca_CallType, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cca_Business", item.cca_Business, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cca_Date", item.cca_Date, DbType.Date, ParameterDirection.Input);
            parameters.Add("@cca_StartTime", item.cca_StartTime, DbType.String, ParameterDirection.Input);
            parameters.Add("@cca_EndTime", item.cca_EndTime, DbType.String, ParameterDirection.Input);
            parameters.Add("@cca_Result", item.cca_Result, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_Id", item.cus_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cca_IdUserModified", item.cca_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCustomerCalls_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
