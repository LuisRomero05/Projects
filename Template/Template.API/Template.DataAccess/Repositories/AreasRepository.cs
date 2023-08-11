using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Template.DataAccess.Context;
using Template.Entities.Entities;

namespace Template.DataAccess.Repositories
{
    public class AreasRepository : IRepository<tbAreas, View_Areas_List>
    {
        public int Delete(int Id, int Mod)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@are_IdUserModified", Mod, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(TempladeeContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Areas_DELETE, parameters, commandType: CommandType.StoredProcedure);

        }

        public tbAreas Find(Expression<Func<tbAreas, bool>> expression = null)
        {
            using var db = new TempladeeContext();
            return db.tbAreas.Where(expression).FirstOrDefault();
        }

        public int Insert(tbAreas item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@are_Description", item.are_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@are_IdUserCreate", item.are_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(TempladeeContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Areas_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<View_Areas_List> List()
        {
            using var db = new TempladeeContext();
            return db.View_Areas_List.ToList();
        }

        public View_Areas_List Search(Expression<Func<View_Areas_List, bool>> expression = null)
        {
            //using var db = new TempladeeContext();
            //return db.View_Areas_List.Where(expression).FirstOrDefault();
            using var db = new TempladeeContext();
            return db.View_Areas_List.Where(expression).FirstOrDefault();

        }

        public int Update(int Id, tbAreas item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@are_Description", item.are_Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@are_IdUserModified", item.are_IdUserModified, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(TempladeeContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_Areas_UPDATE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
