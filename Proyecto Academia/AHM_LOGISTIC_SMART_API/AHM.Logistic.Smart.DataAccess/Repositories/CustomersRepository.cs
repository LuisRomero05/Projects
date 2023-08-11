using AHM.Logistic.Smart.Common.Models;
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
    public class CustomersRepository : IRepository<tbCustomers, View_tbCustomers_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cus_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCustomers_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbCustomers Find(Expression<Func<tbCustomers, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbCustomers.Where(expression).OrderByDescending(x=>x.cus_Id).FirstOrDefault();
        }

        public int Insert(tbCustomers item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cus_AssignedUser", item.cus_AssignedUser, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@tyCh_Id", item.tyCh_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_Name", item.cus_Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@cus_RTN", item.cus_RTN, DbType.String, ParameterDirection.Input);
            parameters.Add("@cus_Address", item.cus_Address, DbType.String, ParameterDirection.Input);
            parameters.Add("@dep_Id", item.dep_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@mun_Id", item.mun_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_Email", item.cus_Email, DbType.String, ParameterDirection.Input);
            parameters.Add("@cus_receive_email",1, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@cus_Phone", item.cus_Phone, DbType.String, ParameterDirection.Input);
            parameters.Add("@cus_AnotherPhone", item.cus_AnotherPhone, DbType.String, ParameterDirection.Input);
            parameters.Add("@cus_IdUserCreate", item.cus_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCustomers_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public View_tbCustomers_List Search(Expression<Func<View_tbCustomers_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCustomers_List.Where(expression).FirstOrDefault();
        }

        public IEnumerable<View_tbCustomers_List> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCustomers_List.ToList();
        }

        public int Update(int Id, tbCustomers item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cus_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_AssignedUser", item.cus_AssignedUser, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@tyCh_Id", item.tyCh_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_Name", item.cus_Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@cus_RTN", item.cus_RTN, DbType.String, ParameterDirection.Input);
            parameters.Add("@cus_Address", item.cus_Address, DbType.String, ParameterDirection.Input);
            parameters.Add("@dep_Id", item.dep_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@mun_Id", item.mun_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_Email", item.cus_Email, DbType.String, ParameterDirection.Input);
            parameters.Add("@cus_receive_email", item.cus_Receive_email, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@cus_Phone", item.cus_Phone, DbType.String, ParameterDirection.Input);
            parameters.Add("@cus_AnotherPhone", item.cus_AnotherPhone, DbType.String, ParameterDirection.Input);
            parameters.Add("@cus_IdUserModified", item.cus_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_Active", item.cus_Active, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString); 
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCustomers_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
