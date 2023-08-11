using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    public class UsuariosRepository : IRepository<tbUsuarios>
    {
        public int Delete(tbUsuarios item)
        {
            throw new NotImplementedException();
        }

        public int Editar(tbUsuarios item)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(tbUsuarios tb)
        {
            throw new NotImplementedException();
        }

        public tbUsuarios Find(int id)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbUsuarios.Find(id);
            return response;
        }

        public tbUsuarios Find(Expression<Func<tbUsuarios, bool>> expression = null)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbUsuarios.Where(expression).FirstOrDefault();
            return response;
        }

        public int Insert(tbUsuarios item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbUsuarios> List()
        {
            using var db = new CentrosMedicosContext();
            return db.tbUsuarios.ToList();
        }

        public IEnumerable<string> UserAccess(string userEmail)
        {
            const string query = "dbo.UDP_Permisos_Usuarios";
            var parameters = new DynamicParameters();
            parameters.Add("@usu_Email", userEmail, DbType.String, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<string>(query, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
