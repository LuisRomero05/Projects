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
    public class PersonsRepository : IRepository<tbPersons, View_tbPersons_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@per_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@per_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Persons_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbPersons Find(Expression<Func<tbPersons, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbPersons.Where(expression).OrderByDescending(x=>x.per_Id).FirstOrDefault();
        }

        public View_tbPersons_List Search(Expression<Func<View_tbPersons_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.View_tbPersons_List.Where(expression).FirstOrDefault();
        }

        public int Insert(tbPersons item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@per_Identidad", item.per_Identidad, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_Firstname", item.per_Firstname, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_Secondname", item.per_Secondname, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_LastNames", item.per_LastNames, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_BirthDate", item.per_BirthDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@per_Sex", item.per_Sex, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_Email", item.per_Email, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_Phone", item.per_Phone, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_Direccion", item.per_Direccion, DbType.String, ParameterDirection.Input);
            parameters.Add("@dep_Id", item.dep_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@mun_Id", item.mun_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@per_Esciv", item.per_Esciv, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_IdUserCreate", item.per_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Persons_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<View_tbPersons_List> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbPersons_List.ToList();
        }
        public int Update(int Id, tbPersons item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@per_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@per_Identidad", item.per_Identidad, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_Firstname", item.per_Firstname, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_Secondname", item.per_Secondname, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_LastNames", item.per_LastNames, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_BirthDate", item.per_BirthDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@per_Sex", item.per_Sex, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_Email", item.per_Email, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_Phone", item.per_Phone, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_Direccion", item.per_Direccion, DbType.String, ParameterDirection.Input);
            parameters.Add("@dep_Id", item.dep_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@mun_Id", item.mun_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@per_Esciv", item.per_Esciv, DbType.String, ParameterDirection.Input);
            parameters.Add("@per_IdUserModified", item.per_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Persons_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
