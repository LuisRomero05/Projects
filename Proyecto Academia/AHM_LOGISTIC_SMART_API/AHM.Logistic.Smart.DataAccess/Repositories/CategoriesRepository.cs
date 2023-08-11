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

    public class CategoriesRepository : IRepository<tbCategories, View_tbCategories_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cat_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cat_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCategories_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbCategories Find(Expression<Func<tbCategories, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            return db.tbCategories.Where(expression).OrderByDescending(x=>x.cat_Id).FirstOrDefault();
        }
        public View_tbCategories_List Search(Expression<Func<View_tbCategories_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            return db.View_tbCategories_List.Where(expression).FirstOrDefault();
        }

        public int Insert(tbCategories item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cat_Description", item.cat_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@cat_IdUserCreate", item.cat_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCategories_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbCategories_List> List()
        {
            using var db = new LogisticSmartContext("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            return db.View_tbCategories_List.ToList();
        }

        public int Update(int Id, tbCategories item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cat_Description", item.cat_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@cat_IdUserModified", item.cat_IdUserModified, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cat_Id",Id, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source = Mauricio; initial catalog = LOGISTIC_SMART_AHM; user id = Mauricio; password = 12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCategories_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
