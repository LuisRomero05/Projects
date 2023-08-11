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
    public class HospitalRepositories : IRepository<tbHospiltales>
    {
        public int Editar(tbHospiltales item)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(tbHospiltales tb)
        {
            throw new NotImplementedException();
        }

        public tbHospiltales Find(int id)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbHospiltales.Find(id);
            return response;
        }

        public tbHospiltales Find(Expression<Func<tbHospiltales, bool>> expression = null)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbHospiltales.Where(expression).FirstOrDefault();
            return response;
        }

        public IEnumerable<tbHospiltales> GetReport()
        {
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<tbHospiltales>(ScriptsBaseDatos.UDP_Select_tbHospiltales, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<tbHospiltales> GetReportInt(int var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbHospiltales> GetReportString(string var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbHospiltales> GetReportUlt()
        {
            int id = 0;
            String id_ultima = "SELECT distinct TOP 1 (hospi_Id) FROM tbHospiltales ORDER BY hospi_Id DESC";
            SqlConnection Con = new SqlConnection("data source=MARU\\SQLEXPRESS; initial catalog=CentrosMedicosDB; user id=FernandoRios; password=123");
            SqlCommand ejecutar = new SqlCommand(id_ultima, Con);
            Con.Open();
            SqlDataReader leer = ejecutar.ExecuteReader();
            if (leer.Read() == true)
            {
                id = Convert.ToInt32(leer["hospi_Id"].ToString());
                Con.Close();
            }
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<tbHospiltales>(ScriptsBaseDatos.UDP_tbHospital_SelectId, parameters, commandType: CommandType.StoredProcedure);
        }

        public int Insert(tbHospiltales item)
        {
            var parametres = new DynamicParameters();
            parametres.Add("@hospi_Nombre", item.hospi_Nombre, DbType.String, ParameterDirection.Input);
            parametres.Add("@hospi_Telefono", item.hospi_Telefono, DbType.String, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsBaseDatos.UDP_insertar_hospitales, parametres, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<tbHospiltales> List()
        {
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            var result = db.Query<tbHospiltales>(ScriptsBaseDatos.UDP_Select_tbHospiltales, null, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
