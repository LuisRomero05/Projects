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

        public IEnumerable<tbUsuarios> GetReport()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbUsuarios> GetReportInt(int var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbUsuarios> GetReportString(string var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbUsuarios> GetReportUlt()
        {
            throw new NotImplementedException();
        }

        public int Insert(tbUsuarios item)
        {
            //using var db = new CentrosMedicosContext();
            //db.tbUsuarios.Add(item);
            //db.SaveChanges();
            //return item.usu_ID;

            var parametres = new DynamicParameters();
            parametres.Add("@rol_ID", item.rol_ID, DbType.String, ParameterDirection.Input);
            parametres.Add("@usu_Nombre", item.usu_Nombre, DbType.String, ParameterDirection.Input);
            parametres.Add("@usu_Apellido", item.usu_Apellido, DbType.String, ParameterDirection.Input);
            parametres.Add("@usu_Email", item.usu_Email, DbType.String, ParameterDirection.Input);
            parametres.Add("@usu_Password", item.usu_Password, DbType.String, ParameterDirection.Input);
            parametres.Add("@usu_PasswordSalt", item.usu_PasswordSalt, DbType.String, ParameterDirection.Input);
            parametres.Add("@usu_NumeroTelefono", item.usu_NumeroTelefono, DbType.String, ParameterDirection.Input);
            parametres.Add("@usu_NumeroCelular", item.usu_NumeroCelular, DbType.String, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsBaseDatos.UDP_insertar_usuarios, parametres, commandType: CommandType.StoredProcedure);

        }

        public IEnumerable<tbUsuarios> List()
        {
            //using var db = new CentrosMedicosContext();
            //return db.tbUsuarios.ToList();

            const string query = "UDP_Select_tbUsuarios";
            var parametres = new DynamicParameters();
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<tbUsuarios>(query, parametres, commandType: CommandType.Text);

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
