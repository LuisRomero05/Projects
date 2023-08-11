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
    public class CustomersFileRepository : IRepository<tbCustomersFile, View_tbCustomersFile_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cfi_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cfi_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCustomersFile_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbCustomersFile Find(Expression<Func<tbCustomersFile, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbCustomersFile.Where(expression).FirstOrDefault();
        }

        public int Insert(tbCustomersFile item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cfi_fileRoute", item.cfi_fileRoute, DbType.String, ParameterDirection.Input);
            parameters.Add("@cus_Id", item.cus_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cfi_IdUserCreate", item.cfi_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            var r = db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCustomersFile_INSERT, parameters, commandType: CommandType.StoredProcedure);
            return r;
        }

        public IEnumerable<View_tbCustomersFile_List> List()
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCustomersFile_List.ToList();
        }

        public View_tbCustomersFile_List Search(Expression<Func<View_tbCustomersFile_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.View_tbCustomersFile_List.Where(expression).FirstOrDefault();
        }

        public int Update(int Id, tbCustomersFile item)
        {
            throw new NotImplementedException();
        }
    }
}
