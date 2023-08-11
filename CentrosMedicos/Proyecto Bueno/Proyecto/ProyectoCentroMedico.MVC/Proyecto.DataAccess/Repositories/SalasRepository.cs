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
    public class SalasRepository : IRepository<tbSala>
    {
        public int Editar(tbSala item)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(tbSala tb)
        {
            throw new NotImplementedException();
        }

        public tbSala Find(int id)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbSala.Find(id);
            return response;
        }

        public tbSala Find(Expression<Func<tbSala, bool>> expression = null)
        {
            using var db = new CentrosMedicosContext();
            var response = db.tbSala.Where(expression).FirstOrDefault();
            return response;
        }

        public IEnumerable<tbSala> GetReport()
        {
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<tbSala>(ScriptsBaseDatos.UDP_Select_tbSala, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<tbSala> GetReportInt(int var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbSala> GetReportString(string var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbSala> GetReportUlt()
        {
            int id = 0;
            String id_ultima = "SELECT distinct TOP 1 (sala_Id) FROM tbSala ORDER BY sala_Id DESC";
            SqlConnection Con = new SqlConnection("data source=MARU\\SQLEXPRESS; initial catalog=CentrosMedicosDB; user id=FernandoRios; password=123");
            SqlCommand ejecutar = new SqlCommand(id_ultima, Con);
            Con.Open();
            SqlDataReader leer = ejecutar.ExecuteReader();
            if (leer.Read() == true)
            {
                id = Convert.ToInt32(leer["sala_Id"].ToString());
                Con.Close();
            }
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<tbSala>(ScriptsBaseDatos.UDP_tbSala_SelectId, parameters, commandType: CommandType.StoredProcedure);
        }

        public int Insert(tbSala item)
        {
            var parametres = new DynamicParameters();
            parametres.Add("@hospi_Id", item.hospi_Id, DbType.String, ParameterDirection.Input);
            parametres.Add("@sala_Nombre", item.sala_Nombre, DbType.String, ParameterDirection.Input);
            parametres.Add("@sala_NumCamas", item.sala_NumCamas, DbType.String, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.ExecuteScalar<int>(ScriptsBaseDatos.UDP_insertar_Sala, parametres, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<tbSala> List()
        {
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            var result = db.Query<tbSala>(ScriptsBaseDatos.UDP_Select_tbSala, null, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
