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
    public class DepartmentsRepositoryTest : IRepository<tbDepartments, View_tbDepartments_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@dep_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@dep_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=DELL-LAPTOP; initial catalog=LOGISTIC_SMART_AHM; user id=Josue; password=expawer.ismael");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbDepartamentos_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }

        public tbDepartments Find(Expression<Func<tbDepartments, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=DELL-LAPTOP; initial catalog=LOGISTIC_SMART_AHM; user id=Josue; password=expawer.ismael");
            return db.tbDepartments.Where(expression).FirstOrDefault(); 
        }

        public int Insert(tbDepartments item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@dep_Code", item.dep_Code, DbType.String, ParameterDirection.Input);
            parameters.Add("@dep_Description", item.dep_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@cou_Id", item.cou_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@dep_UsuarioCrea", item.dep_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=DELL-LAPTOP; initial catalog=LOGISTIC_SMART_AHM; user id=Josue; password=expawer.ismael");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbDepartamentos_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_tbDepartments_List> List()
        {
            using var db = new LogisticSmartContext("data source=DELL-LAPTOP; initial catalog=LOGISTIC_SMART_AHM; user id=Josue; password=expawer.ismael");
            return db.View_tbDepartments_List.ToList();
        }

        public View_tbDepartments_List Search(Expression<Func<View_tbDepartments_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=DELL-LAPTOP; initial catalog=LOGISTIC_SMART_AHM; user id=Josue; password=expawer.ismael");
            return db.View_tbDepartments_List.Where(expression).FirstOrDefault();
        }

        public int Update(int Id, tbDepartments item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@dep_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@dep_Code", item.dep_Code, DbType.String, ParameterDirection.Input);
            parameters.Add("@dep_Description", item.dep_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@cou_Id", item.cou_Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@dep_IdUserModified", item.dep_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=DELL-LAPTOP; initial catalog=LOGISTIC_SMART_AHM; user id=Josue; password=expawer.ismael");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbDepartamentos_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
