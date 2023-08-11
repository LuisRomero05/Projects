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
    public class EmployeesRepositoryTest : IRepository<tbEmployees, View_tbEmployees_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@emp_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@emp_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Employees_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbEmployees Find(Expression<Func<tbEmployees, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.tbEmployees.Where(expression).FirstOrDefault();
        }

        public int Insert(tbEmployees item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@per_Id", item.per_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@are_Id", item.are_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@occ_Id", item.occ_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@emp_IdUserCreate", item.emp_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Employees_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbEmployees_List> List()
        {
            using var db = new LogisticSmartContext("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.View_tbEmployees_List.ToList();
        }

        public View_tbEmployees_List Search(Expression<Func<View_tbEmployees_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.View_tbEmployees_List.Where(expression).FirstOrDefault();
        }

        public int Update(int Id, tbEmployees item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@emp_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@per_Id", item.per_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@are_Id", item.are_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@occ_Id", item.occ_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@emp_IdUserModified", item.emp_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Employees_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
