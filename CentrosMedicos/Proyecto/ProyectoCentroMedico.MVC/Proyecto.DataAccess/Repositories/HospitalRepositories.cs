using Proyecto.Entities.Entities;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public tbHospiltales Find(Expression<Func<tbHospiltales, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public int Insert(tbHospiltales item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbHospiltales> List()
        {
            throw new NotImplementedException();
        }
    }
}
