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
    public class UserRepositoryTest : IRepository<tbUsers, View_tbUsers_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@usu_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@usu_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbUsers_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public View_tbUsers_List Search(Expression<Func<View_tbUsers_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.View_tbUsers_List.Where(expression).FirstOrDefault();
        }

        public tbUsers Find(Expression<Func<tbUsers, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.tbUsers.Where(expression).OrderByDescending(x => x.usu_Id).FirstOrDefault();
        }

        public int Insert(tbUsers item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@usu_UserName", item.usu_UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@usu_Password", item.usu_Password, DbType.String, ParameterDirection.Input);
            parameters.Add("@usu_PasswordSalt", item.usu_PasswordSalt, DbType.String, ParameterDirection.Input);
            parameters.Add("@usu_Profile_picture", item.usu_Profile_picture, DbType.String, ParameterDirection.Input);
            parameters.Add("@rol_Id", item.rol_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@per_Id", item.Per_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@usu_IdUserCreate", item.usu_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbUsers_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbUsers_List> List()
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.View_tbUsers_List.ToList();
        }

        public int Update(int Id, tbUsers item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@usu_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@usu_UserName", item.usu_UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@usu_Password", item.usu_Password, DbType.String, ParameterDirection.Input);
            parameters.Add("@usu_PasswordSalt", item.usu_PasswordSalt, DbType.String, ParameterDirection.Input);
            parameters.Add("@usu_Profile_picture", item.usu_Profile_picture, DbType.String, ParameterDirection.Input);
            parameters.Add("@rol_Id", item.rol_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@per_Id", item.Per_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@usu_IdUserModified", item.usu_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbUsers_UPDATE, parameters, commandType: CommandType.StoredProcedure);

        }
    }
}
