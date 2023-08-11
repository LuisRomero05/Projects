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
    public class ProductsRepositoryTest : IRepository<tbProducts, View_tbProducts_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pro_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pro_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbProducts_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbProducts Find(Expression<Func<tbProducts, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.tbProducts.Where(expression).OrderByDescending(x => x.pro_Id).FirstOrDefault();
        }

        public View_tbProducts_List Search(Expression<Func<View_tbProducts_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.View_tbProducts_List.Where(expression).FirstOrDefault();
        }

        public int Insert(tbProducts item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pro_Description", item.pro_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@pro_PurchasePrice", item.pro_PurchasePrice, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@pro_SalesPrice ", item.pro_SalesPrice, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@pro_Stock", item.pro_Stock, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pro_ISV", item.pro_ISV, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@uni_Id", item.uni_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@scat_Id", item.scat_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pro_IdUserCreate", item.pro_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbProducts_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbProducts_List> List()
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.View_tbProducts_List.ToList();
        }

        public int Update(int Id, tbProducts item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pro_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pro_Description", item.pro_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@pro_PurchasePrice", item.pro_PurchasePrice, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@pro_SalesPrice ", item.pro_SalesPrice, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@pro_Stock", item.pro_Stock, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pro_ISV", item.pro_ISV, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@uni_Id", item.uni_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@scat_Id", item.scat_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pro_IdUserModified", item.pro_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbProducts_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }

        public int UpdateStock(int Id, tbProducts item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pro_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pro_Stock", item.pro_Stock, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@pro_IdUserModified", item.pro_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbProducts_Stock_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbProducts Search(Expression<Func<tbProducts, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.tbProducts.Where(expression).FirstOrDefault();
        }
    }
}
