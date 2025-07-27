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
    public class PlantillaRepository : IRepository<tbPlantilla>
    {
        public int Editar(tbPlantilla item)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(tbPlantilla tb)
        {
            throw new NotImplementedException();
        }

        public tbPlantilla Find(int id)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbPlantilla.Find(id);
            return response;
        }

        public tbPlantilla Find(Expression<Func<tbPlantilla, bool>> expression = null)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbPlantilla.Where(expression).FirstOrDefault();
            return response;
        }

        public IEnumerable<tbPlantilla> GetReport()
        {
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<tbPlantilla>(ScriptsBaseDatos.UDP_Select_tbPlantilla, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<tbPlantilla> GetReportInt(int var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbPlantilla> GetReportString(string var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbPlantilla> GetReportUlt()
        {
            int id = 0;
            String id_ultima = "SELECT distinct TOP 1 (planti_EmpleadoId) FROM tbPlantilla ORDER BY planti_EmpleadoId DESC";
            SqlConnection Con = new SqlConnection("Server= Mauricio; Database= CentrosMedicosDB; User Id= MauJosue; Password= 1234;");
            SqlCommand ejecutar = new SqlCommand(id_ultima, Con);
            Con.Open();
            SqlDataReader leer = ejecutar.ExecuteReader();
            if (leer.Read() == true)
            {
                id = Convert.ToInt32(leer["planti_EmpleadoId"].ToString());
                Con.Close();
            }
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<tbPlantilla>(ScriptsBaseDatos.UDP_tbPlantilla_SelectId, parameters, commandType: CommandType.StoredProcedure);
        }

        public int Insert(tbPlantilla item)
        {
            var parametres = new DynamicParameters();
            parametres.Add("@hospi_Id", item.hospi_Id, DbType.String, ParameterDirection.Input);
            parametres.Add("@sala_Id", item.sala_Id, DbType.String, ParameterDirection.Input);
            parametres.Add("@planti_Apellido", item.planti_Apellido, DbType.String, ParameterDirection.Input);
            parametres.Add("@planti_Funcion", item.planti_Funcion, DbType.String, ParameterDirection.Input);
            parametres.Add("@planti_Turno", item.planti_Turno, DbType.String, ParameterDirection.Input);
            parametres.Add("@planti_Salario", item.planti_Salario, DbType.String, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsBaseDatos.UDP_insertar_Plantilla, parametres, commandType: CommandType.StoredProcedure);


        }

        public IEnumerable<tbPlantilla> List()
        {
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            var result = db.Query<tbPlantilla>(ScriptsBaseDatos.UDP_Select_tbPlantilla, null, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
