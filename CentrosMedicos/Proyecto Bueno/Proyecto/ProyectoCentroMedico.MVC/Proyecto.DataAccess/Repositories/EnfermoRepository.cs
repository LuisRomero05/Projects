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
    public class EnfermoRepository : IRepository<tbEnfermo>
    {
        public int Editar(tbEnfermo item)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(tbEnfermo tb)
        {
            throw new NotImplementedException();
        }

        public tbEnfermo Find(int id)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbEnfermo.Find(id);
            return response;
        }

        public tbEnfermo Find(Expression<Func<tbEnfermo, bool>> expression = null)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbEnfermo.Where(expression).FirstOrDefault();
            return response;
        }

        public IEnumerable<tbEnfermo> GetReport()
        {
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<tbEnfermo>(ScriptsBaseDatos.UDP_Select_tbEnfermo, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<tbEnfermo> GetReportInt(int var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbEnfermo> GetReportString(string var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbEnfermo> GetReportUlt()
        {
            int id = 0;
            String id_ultima = "SELECT distinct TOP 1 (enfer_Inscripcioon) FROM tbEnfermo ORDER BY enfer_Inscripcioon DESC";
            SqlConnection Con = new SqlConnection("data source=MARU\\SQLEXPRESS; initial catalog=CentrosMedicosDB; user id=FernandoRios; password=123");
            SqlCommand ejecutar = new SqlCommand(id_ultima, Con);
            Con.Open();
            SqlDataReader leer = ejecutar.ExecuteReader();
            if (leer.Read() == true)
            {
                id = Convert.ToInt32(leer["enfer_Inscripcioon"].ToString());
                Con.Close();
            }
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<tbEnfermo>(ScriptsBaseDatos.UDP_tbEnfermo_SelectId, parameters, commandType: CommandType.StoredProcedure);
        }

        public int Insert(tbEnfermo item)
        {
            var parametres = new DynamicParameters();
            parametres.Add("@enfer_Apellido", item.enfer_Apellido, DbType.String, ParameterDirection.Input);
            parametres.Add("@enfer_Direccion", item.enfer_Direccion, DbType.String, ParameterDirection.Input);
            parametres.Add("@enfer_FechaNac", item.enfer_FechaNac, DbType.String, ParameterDirection.Input);
            parametres.Add("@enfer_NSS", item.enfer_NSS, DbType.String, ParameterDirection.Input);
            parametres.Add("@planti_EmpleadoId", item.planti_EmpleadoId, DbType.String, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsBaseDatos.UDP_insertar_enfermo, parametres, commandType: CommandType.StoredProcedure);

        }

        public IEnumerable<tbEnfermo> List()
        {
            const string query = "UDP_Select_tbEnfermo";
            var parametres = new DynamicParameters();
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<tbEnfermo>(query, parametres, commandType: CommandType.Text);
        }
    }
}
