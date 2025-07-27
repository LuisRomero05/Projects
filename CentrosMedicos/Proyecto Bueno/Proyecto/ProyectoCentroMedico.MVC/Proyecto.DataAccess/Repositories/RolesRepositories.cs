using Dapper;
using Microsoft.Data.SqlClient;
using Proyecto.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DataAccess.Repositories
{
    public class RolesRepositories : IRepository<tbRoles>
    {
        public int Editar(tbRoles item)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(tbRoles tb)
        {
            throw new NotImplementedException();
        }

        public tbRoles Find(int id)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbRoles.Find(id);
            return response;
        }

        public tbRoles Find(Expression<Func<tbRoles, bool>> expression = null)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbRoles.Where(expression).FirstOrDefault();
            return response;
        }

        public IEnumerable<tbRoles> GetReport()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbRoles> GetReportInt(int var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbRoles> GetReportString(string var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbRoles> GetReportUlt()
        {
            throw new NotImplementedException();
        }

        public int Insert(tbRoles item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbRoles> List()
        {
            //using var db = new CentrosMedicosContext();
            //return db.tbRoles.ToList();
            const string query = "UDP_Select_tbRoles";
            var parametres = new DynamicParameters();
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<tbRoles>(query, parametres, commandType: CommandType.Text);

        }
    }
}
