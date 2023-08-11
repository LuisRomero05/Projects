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
    public class MeetingsRepositoryTest
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@met_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@met_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbMeetings_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public int DeleteDetail(int Id, int IdModified)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@mde_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@mde_IdUserModified", IdModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbMeetingsDetails_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbMeetings_List> List()
        {
            using var db = new LogisticSmartContext("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.View_tbMeetings_List.ToList();
        }

        public IEnumerable<View_tbCustomers_tbEmployees_List> ListInvitados()
        {
            using var db = new LogisticSmartContext("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.View_tbCustomers_tbEmployees_List.ToList();
        }

        public List<tbMeetingsDetails> Details(Expression<Func<tbMeetingsDetails, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");

            var list = db.tbMeetingsDetails.Where(expression).ToList();
            return list;
        }

        public string Insert(tbMeetings item, List<tbMeetingsDetails> data)
        {
            string msj;
            var options = new TransactionOptions();
            using (TransactionScope scope = new(TransactionScopeOption.Required, options))
            {
                DateTime date = DateTime.Now;
                using var db = new LogisticSmartContext("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
                item.met_Status = true;
                item.met_DateCreate = date;
                item.met_IdUserModified = null;
                item.met_DateModified = null;
                db.tbMeetings.Add(item);
                db.SaveChanges();

                var id = db.tbMeetings.
                OrderByDescending(x => x.met_Id).
                Select(b => b.met_Id).FirstOrDefault();

                foreach (var item2 in data)
                {
                    item2.met_Id = id;
                    item2.mde_Status = true;
                    item2.mde_IdUserCreate = item.met_IdUserCreate;
                    item2.mde_DateCreate = date;
                    item2.mde_IdUserModified = null;
                    item2.mde_DateModified = null;
                    if (item2.cus_Id == 0)
                    {
                        item2.cus_Id = null;
                    }
                    if (item2.emp_Id == 0)
                    {
                        item2.emp_Id = null;
                    }
                    if (item2.cont_Id == 0)
                    {
                        item2.cont_Id = null;
                    }
                    db.tbMeetingsDetails.Add(item2);
                }
                db.SaveChanges();
                scope.Complete();
                msj = "success";
            }
            return msj;
        }

        public string InsertDetails(tbMeetingsDetails data)
        {
            string msj;
            var options = new TransactionOptions();
            using (TransactionScope scope = new(TransactionScopeOption.Required, options))
            {
                DateTime date = DateTime.Now;
                using var db = new LogisticSmartContext("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
                data.mde_Status = true;
                data.mde_DateCreate = date;
                data.mde_IdUserModified = null;
                data.mde_DateModified = null;
                if (data.cus_Id == 0)
                {
                    data.cus_Id = null;
                }
                if (data.emp_Id == 0)
                {
                    data.emp_Id = null;
                }
                if (data.cont_Id == 0)
                {
                    data.cont_Id = null;
                }
                db.tbMeetingsDetails.Add(data);
                db.SaveChanges();
                scope.Complete();
                msj = "success";
            }
            return msj;
        }

        public string Update(tbMeetings item, List<tbMeetingsDetails> data, int Id)
        {
            string msj;
            var options = new TransactionOptions();
            using (TransactionScope scope = new(TransactionScopeOption.Required, options))
            {
                DateTime date = DateTime.Now;
                using var db = new LogisticSmartContext("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
                item.met_DateModified = date;
                db.Entry(item).State = EntityState.Modified;
                foreach (var item2 in data)
                {
                    item2.mde_IdUserModified = item.met_IdUserModified;
                    item2.met_Id = Id;
                    item2.mde_DateModified = date;
                    db.Entry(item2).State = EntityState.Modified;
                }
                db.SaveChanges();
                scope.Complete();
                msj = "success";
            }
            return msj;

        }
        public tbMeetingsDetails FindDetails(Expression<Func<tbMeetingsDetails, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.tbMeetingsDetails.Where(expression).FirstOrDefault();
        }
        public tbMeetings Find(Expression<Func<tbMeetings, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=LAPSTEVENUP\\LAPSTEVEN; initial catalog=LOGISTIC_SMART_AHM; user id=LAPSTEVEN; password=1234");
            return db.tbMeetings.Where(expression).FirstOrDefault();
        }
    }
}
