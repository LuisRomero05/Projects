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
    public class CustomerNotesRepositoryTest : IRepository<tbCustomerNotes, View_tbCustomerNotes>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cun_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cun_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCustomerNotes_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbCustomerNotes Find(Expression<Func<tbCustomerNotes, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.tbCustomerNotes.Where(expression).FirstOrDefault();
        }

        public int Insert(tbCustomerNotes item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cun_Descripcion", item.cun_Descripcion, DbType.String, ParameterDirection.Input);
            parameters.Add("@cun_ExpirationDate", item.cun_ExpirationDate, DbType.Date, ParameterDirection.Input);
            parameters.Add("@pry_Id", item.pry_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_Id", item.cus_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cun_IdUserCreate", item.cun_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCustomerNotes_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbCustomerNotes> List()
        {
            using var db = new LogisticSmartContext("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.View_tbCustomerNotes.ToList();
        }

        public View_tbCustomerNotes Search(Expression<Func<View_tbCustomerNotes, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.View_tbCustomerNotes.Where(expression).FirstOrDefault();
        }

        public int Update(int Id, tbCustomerNotes item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cun_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cun_Descripcion", item.cun_Descripcion, DbType.String, ParameterDirection.Input);
            parameters.Add("@cun_ExpirationDate", item.cun_ExpirationDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@pry_Id", item.pry_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cus_Id", item.cus_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cun_IdUserModified", item.cun_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCustomerNotes_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
