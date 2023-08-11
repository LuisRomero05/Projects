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
    public class EmpleadoSalaRepository : IRepository<EmpleadoSala>
    {
        public int Editar(EmpleadoSala item)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(EmpleadoSala tb)
        {
            throw new NotImplementedException();
        }

        public EmpleadoSala Find(int id)
        {
            throw new NotImplementedException();
        }

        public EmpleadoSala Find(Expression<Func<EmpleadoSala, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmpleadoSala> GetReport()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmpleadoSala> GetReportInt(int var)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", var, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<EmpleadoSala>(ScriptsBaseDatos.UDP_Empleados_Cada_Sala, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<EmpleadoSala> GetReportString(string var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmpleadoSala> GetReportUlt()
        {
            throw new NotImplementedException();
        }

        public int Insert(EmpleadoSala item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmpleadoSala> List()
        {
            throw new NotImplementedException();
        }
    }
}
