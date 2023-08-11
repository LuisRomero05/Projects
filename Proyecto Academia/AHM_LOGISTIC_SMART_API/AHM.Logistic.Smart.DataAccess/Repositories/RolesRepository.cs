using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.Entities.Entities;
using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public class RolesRepository : IRepository<tbRoles, View_tbRoles_List>
    {

        private readonly IMapper _mapper; 
   
        public RolesRepository (IMapper mapper)
        {
            _mapper = mapper;
           
        }
        public int Delete(int Id, int Mod)
        {
            int result = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@rol_Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@rol_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            var parameterDelete = new DynamicParameters();
            parameterDelete.Add("@rol_Id", Id, DbType.Int32, ParameterDirection.Input);
            result = db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbRoleModuleItems_DELETE, parameterDelete, commandType: CommandType.StoredProcedure);
            result = db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbRoles_DELETE, parameters, commandType: CommandType.StoredProcedure);
            return result;

        }

        public View_tbRoles_List Search(Expression<Func<View_tbRoles_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            var response = db.View_tbRoles_List.Where(expression).FirstOrDefault();
            return response;
        }


        public tbRoles Find(Expression<Func<tbRoles, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbRoles.Where(expression).OrderByDescending(x=>x.rol_Id).FirstOrDefault();
        }

        public int Insert(tbRoles item, List<tbRoleModuleItems> data)
        {
            int result = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@Description", item.rol_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@IdUserCreate", item.rol_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            var options = new TransactionOptions();
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
                item.rol_Id = db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbRoles_INSERT, parameters, commandType: CommandType.StoredProcedure);
                var parametersModuleItems = new DynamicParameters();
                parametersModuleItems.Add("@rol_Id", item.rol_Id, DbType.Int32, ParameterDirection.Input);
                foreach (var module in data)
                {
                    parametersModuleItems.Add("@mit_Id", module.mit_Id, DbType.Int32, ParameterDirection.Input);
                    result = db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbRoleModuleItems_INSERT, parametersModuleItems, commandType: CommandType.StoredProcedure);
                        if (result < 0) { }
                }
                scope.Complete();
            }

            return item.rol_Id;
        }

        public IEnumerable<View_tbRoles_List> List()
        {

            using var db = new LogisticSmartContext();
            return db.View_tbRoles_List.ToList();
            
        }

        public IEnumerable<tbComponents> ComponentsList()
        {
            using var db = new LogisticSmartContext();
            return db.tbComponents.ToList();
        }

        public IEnumerable<tbModules> ModulesList()
        {
            using var db = new LogisticSmartContext();
            return db.tbModules.ToList();
        }

        public IEnumerable<tbModuleItems> ModuleItemsList()
        {
            using var db = new LogisticSmartContext();
            return db.tbModuleItems.ToList();
        }

        public List<tbRoleModuleItems> RolModuleItemsDetails(Expression<Func<tbRoleModuleItems, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbRoleModuleItems.Where(expression).ToList();
        }

        public int Update(tbRoles item, List<tbRoleModuleItems> data)
        {
            int result = 0;
            int resultUpdate = 0;
            int resultDelete = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@rol_Id", item.rol_Id, DbType.String, ParameterDirection.Input);
            parameters.Add("@rol_Description", item.rol_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@rol_IdUserModified", item.rol_IdUserModified, DbType.String, ParameterDirection.Input);
            var options = new TransactionOptions();
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
                var parameterDelete = new DynamicParameters();
                parameterDelete.Add("@rol_Id", item.rol_Id, DbType.Int32, ParameterDirection.Input);
                resultDelete = db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbRoleModuleItems_DELETE, parameterDelete, commandType: CommandType.StoredProcedure);
                if(resultDelete == 0)
                {
                    resultUpdate = db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbRoles_UPDATE, parameters, commandType: CommandType.StoredProcedure);
                    if(resultUpdate == 0)
                    {
                        var parametersModuleItems = new DynamicParameters();
                        parametersModuleItems.Add("@rol_Id", item.rol_Id, DbType.Int32, ParameterDirection.Input);
                        foreach (var module in data)
                        {
                            parametersModuleItems.Add("@mit_Id", module.mit_Id, DbType.Int32, ParameterDirection.Input);
                            result = db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbRoleModuleItems_INSERT, parametersModuleItems, commandType: CommandType.StoredProcedure);
                            if (result < 0) { }
                        }
                    }
                }
                scope.Complete();
            }
            return item.rol_Id;
        }

        public int Insert(tbRoles item)
        {
            throw new NotImplementedException();
        }

        public int Update(int Id, tbRoles item)
        {
            throw new NotImplementedException();
        }
    }
}
