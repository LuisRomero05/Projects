using AHM.Logistic.Smart.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AHM.Logistic.Smart.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using AHM.Logistic.Smart.Common.Models;
using System.Transactions;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public class CotizationsRepository
    {
        public int DeleteDetail(int proId, int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@pro_Id", proId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cot_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@code_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCotizationsDetail_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cot_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cot_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCotizations_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbCotizationsDetail FindDetails(Expression<Func<tbCotizationsDetail, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbCotizationsDetail.Where(expression).FirstOrDefault();
        }

        public tbCotizations Find(Expression<Func<tbCotizations, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbCotizations.Where(expression).FirstOrDefault();
        }

        public string Insert(tbCotizations item, List<tbCotizationsDetail> data)
        {
            string msj;
            var options = new TransactionOptions();
            using (TransactionScope scope = new(TransactionScopeOption.Required, options))
            {
                DateTime date = DateTime.Now;
                using var db = new LogisticSmartContext();
                item.cot_Status = true;
                item.sta_Id = ((int)Utilities.Status.Entregado);
                item.cot_DateCreate = date;
                item.cot_IdUserModified = null;
                item.cot_DateModified = null;
                db.tbCotizations.Add(item);
                db.SaveChanges();
                foreach (var item2 in data)
                {
                    item2.cot_Id = item.cot_Id;
                    item2.code_Status = true;
                    item2.code_IdUserCreate = item.cot_IdUserCreate;
                    item2.code_DateCreate = date;
                    item2.code_IdUserModified = null;
                    item2.code_DateModified = null;
                    db.tbCotizationsDetail.Add(item2);
                }
                db.SaveChanges();
                scope.Complete();
                msj = "success";
            }
            return msj;
        }

        public string InsertDetails(List<tbCotizationsDetail> data)
        {
            string msj;
            var options = new TransactionOptions();
            using (TransactionScope scope = new(TransactionScopeOption.Required, options))
            {
                DateTime date = DateTime.Now;
                using var db = new LogisticSmartContext();
                foreach (var item2 in data)
                {
                    item2.code_Status = true;
                    item2.code_DateCreate = date;
                    item2.code_IdUserModified = null;
                    item2.code_DateModified = null;
                    db.tbCotizationsDetail.Add(item2);
                }
                db.SaveChanges();
                scope.Complete();
                msj = "success";
            }
            return msj;
        }

        public IEnumerable<View_tbCotizations_List> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCotizations_List.ToList();
        }
        public List<View_tbCotizationsDetails_List> Details(Expression<Func<View_tbCotizationsDetails_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCotizationsDetails_List.Where(expression).ToList();
        }
   
        public string Update(tbCotizations item, List<tbCotizationsDetail> data)
        {
            string msj;
            var options = new TransactionOptions();
            using (TransactionScope scope = new(TransactionScopeOption.Required, options))
            {
                DateTime date = DateTime.Now;
                using var db = new LogisticSmartContext();
                item.cot_DateModified = date;
                db.Entry(item).State = EntityState.Modified;
                foreach (var item2 in data)
                {
                    item2.code_IdUserModified = item.cot_IdUserModified;
                    item2.cot_Id = item.cot_Id;
                    item2.code_DateModified = date;
                    db.Entry(item2).State = EntityState.Modified;
                }
                db.SaveChanges();
                scope.Complete();
                msj = "success";
            }
            return msj;

        }
    }
}
