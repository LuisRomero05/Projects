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
    public class ReporteGeneralRepository : IRepository<ReporteGeneral>
    {
        public int Editar(ReporteGeneral item)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(ReporteGeneral tb)
        {
            throw new NotImplementedException();
        }

        public ReporteGeneral Find(int id)
        {
            throw new NotImplementedException();
        }

        public ReporteGeneral Find(Expression<Func<ReporteGeneral, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReporteGeneral> GetReport()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReporteGeneral> GetReportInt(int var)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", var, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection(CentrosMedicosContext.ConnectionString);
            return db.Query<ReporteGeneral>(ScriptsBaseDatos.UDP_Registro_Paciente, parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<ReporteGeneral> GetReportString(string var)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReporteGeneral> GetReportUlt()
        {
            throw new NotImplementedException();
        }

        public int Insert(ReporteGeneral item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReporteGeneral> List()
        {
            throw new NotImplementedException();
        }
    }
}
