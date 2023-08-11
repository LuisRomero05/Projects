using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DataAccess.Repositories
{
    public interface IRepository<T> //T es la entidad
    {    
        public IEnumerable<T> List();
        public IEnumerable<T> GetReport();
        public IEnumerable<T> GetReportUlt();
        public IEnumerable<T> GetReportString(string var);
        public IEnumerable<T> GetReportInt(int var);
        public int Insert(T item);//Retorna el id que se genera
        public T Find(int id);
        public int Editar(T item);
        public int Eliminar(T tb);
        public T Find(Expression<Func<T, bool>> expression = null);

    }
}
