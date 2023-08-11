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
    public class ContactsRepositoryTest : IRepository<tbContacts, View_tbContacts_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cont_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cont_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbContacts_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbContacts Find(Expression<Func<tbContacts, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            return db.tbContacts.Where(expression).FirstOrDefault();
        }

        public int Insert(tbContacts item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cont_Name", item.cont_Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@cont_LastName", item.cont_LastName, DbType.String, ParameterDirection.Input);
            parameters.Add("@cont_Email", item.cont_Email, DbType.String, ParameterDirection.Input);
            parameters.Add("@cont_Phone", item.cont_Phone, DbType.String, ParameterDirection.Input);
            parameters.Add("@occ_Id", item.occ_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_Id", item.cus_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cont_IdUserCreate", item.cont_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbContacts_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbContacts_List> List()
        {
            using var db = new LogisticSmartContext("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            return db.View_tbContacts_List.ToList();
        }

        public View_tbContacts_List Search(Expression<Func<View_tbContacts_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            var response = db.View_tbContacts_List.Where(expression).FirstOrDefault();
            return response;
        }

        public int Update(int Id, tbContacts item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cont_Id", Id, DbType.String, ParameterDirection.Input);
            parameters.Add("@cont_Name", item.cont_Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@cont_LastName", item.cont_LastName, DbType.String, ParameterDirection.Input);
            parameters.Add("@cont_Email", item.cont_Email, DbType.String, ParameterDirection.Input);
            parameters.Add("@cont_Phone", item.cont_Phone, DbType.String, ParameterDirection.Input);
            parameters.Add("@occ_Id", item.occ_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_Id", item.cus_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cont_IdUserModified", item.cont_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbContacts_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
