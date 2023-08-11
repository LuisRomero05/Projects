using AHM.Logistic.Smart.DataAccess.Repositories;
using AHM.Logistic.Smart.Entities.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AHM.Logistic.Smart.DataAccess.TestRepositories
{
    public class OrdersRepositoryTest
    {
        public int DeleteDetail(int proId, int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pro_Id", proId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@sor_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@ode_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbOrderDetails_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@sor_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@sor_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbOrders_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }
        public tbOrderDetails FindDetails(Expression<Func<tbOrderDetails, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbOrderDetails.Where(expression).FirstOrDefault();
        }

        public tbSaleOrders Find(Expression<Func<tbSaleOrders, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbSaleOrders.Where(expression).FirstOrDefault();
        }

        public string InsertDetails(List<tbOrderDetails> data)
        {
            string msj;
            var options = new TransactionOptions();
            using (TransactionScope scope = new(TransactionScopeOption.Required, options))
            {
                DateTime date = DateTime.Now;
                using var db = new LogisticSmartContext();
                foreach (var item2 in data)
                {
                    item2.ode_Status = true;
                    item2.ode_DateCreate = date;
                    item2.ode_IdUserModified = null;
                    item2.ode_DateModified = null;
                    db.tbOrderDetails.Add(item2);
                }
                db.SaveChanges();
                scope.Complete();
                msj = "success";
            }
            return msj;
        }

        public IEnumerable<View_tbSaleOrders_List> List()
        {
            using var db = new LogisticSmartContext();
            var resutl = db.View_tbSaleOrders_List.ToList();
            return resutl;
        }

        public List<View_tbSalesDetails_List> Details(Expression<Func<View_tbSalesDetails_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.View_tbSalesDetails_List.Where(expression).ToList();
        }

        public string Insert(tbSaleOrders item, List<tbOrderDetails> data)
        {
            string msj;
            var options = new TransactionOptions();
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                DateTime date = DateTime.Now;
                using var db = new LogisticSmartContext();
                item.sor_Status = true;
                item.sta_Id = ((int)Utilities.Status.Entregado);
                item.sor_DateCreate = date;
                item.sor_IdUserModified = null;
                item.sor_DateModified = null;
                db.tbSaleOrders.Add(item);
                db.SaveChanges();
                foreach (var item2 in data)
                {
                    item2.sor_Id = item.sor_Id;
                    item2.ode_Status = true;
                    item2.ode_IdUserCreate = item.sor_IdUserCreate;
                    item2.ode_DateCreate = date;
                    item2.ode_IdUserModified = null;
                    item2.ode_DateModified = null;
                    db.tbOrderDetails.Add(item2);
                }
                db.SaveChanges();
                scope.Complete();
                msj = "success";
            }
            return msj;
        }

        public string Update(tbSaleOrders item, List<tbOrderDetails> data)
        {
            string msj;
            var options = new TransactionOptions();
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                DateTime date = DateTime.Now;
                using var db = new LogisticSmartContext();
                item.sor_DateModified = date;
                db.Entry(item).State = EntityState.Modified;
                foreach (var item2 in data)
                {
                    item2.ode_IdUserModified = item.sor_IdUserModified;
                    item2.sor_Id = item.sor_Id;
                    item2.ode_DateModified = date;
                    db.Entry(item2).State = EntityState.Modified;
                }
                db.SaveChanges();
                msj = "success";
                scope.Complete();
            }
            return msj;
        }
    }
}
