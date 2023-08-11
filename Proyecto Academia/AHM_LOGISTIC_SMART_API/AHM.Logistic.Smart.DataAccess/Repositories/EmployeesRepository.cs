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
    public class EmployeesRepository : IRepository<tbEmployees, View_tbEmployees_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@emp_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@emp_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Employees_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbEmployees Find(Expression<Func<tbEmployees, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbEmployees.Where(expression).OrderByDescending(x=>x.emp_Id).FirstOrDefault();
        }
        public View_tbEmployees_List Search(Expression<Func<View_tbEmployees_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            var response = db.View_tbEmployees_List.Where(expression).FirstOrDefault();
            return response;
        }
        public int Insert(tbEmployees item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@per_Id", item.per_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@are_Id", item.are_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@occ_Id", item.occ_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@emp_IdUserCreate", item.emp_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Employees_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbEmployees_List> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbEmployees_List.ToList();
        }

        public int Update(int Id, tbEmployees item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@emp_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@per_Id", item.per_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@are_Id", item.are_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@occ_Id", item.occ_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@emp_IdUserModified", item.emp_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Employees_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
