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
    public class OccupationRepositoryTest : IRepository<tbOccupations, View_tbOccupations_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@occ_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@occ_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbOccupations_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbOccupations Find(Expression<Func<tbOccupations, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.tbOccupations.Where(expression).FirstOrDefault();
        }

        public int Insert(tbOccupations item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@occ_Description", item.occ_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@occ_IdUserCreate", item.occ_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbOccupations_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbOccupations_List> List()
        {
            using var db = new LogisticSmartContext("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.View_tbOccupations_List.ToList();
        }

        public View_tbOccupations_List Search(Expression<Func<View_tbOccupations_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.View_tbOccupations_List.Where(expression).FirstOrDefault();
        }

        public int Update(int Id, tbOccupations item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@occ_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@occ_Description", item.occ_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@occ_IdUserModified", item.occ_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=DESKTOP-VKA1GMI; initial catalog=LOGISTIC_SMART_AHM; user id=Dayanne; password=123");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbOccupations_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
