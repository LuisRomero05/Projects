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
    public class EmpleadoHospitalRepository : IRepository<EmpleadoHospital>
    {
        public int Editar(EmpleadoHospital item)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(EmpleadoHospital tb)
        {
            throw new NotImplementedException();
        }

        public EmpleadoHospital Find(int id)
        {
            throw new NotImplementedException();
        }

        public EmpleadoHospital Find(Expression<Func<EmpleadoHospital, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmpleadoHospital> GetReport()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmpleadoHospital> GetReportInt(int var)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", var, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<EmpleadoHospital>(ScriptsBaseDatos.UDP_Empleados_Cada_Hospital, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<EmpleadoHospital> GetReportString(string var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmpleadoHospital> GetReportUlt()
        {
            throw new NotImplementedException();
        }

        public int Insert(EmpleadoHospital item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmpleadoHospital> List()
        {
            throw new NotImplementedException();
        }
    }
}
