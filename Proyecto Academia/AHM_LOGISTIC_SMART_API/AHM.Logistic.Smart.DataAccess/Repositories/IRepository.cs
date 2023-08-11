using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public interface IRepository<T,V>
    {
        public int Insert(T item);
        public T Find(Expression<Func<T, bool>> expression = null);
        public V Search(Expression<Func<V, bool>> expression = null);
        public int Update(int Id,T item);
        public int Delete(int Id, int Mod);
        public IEnumerable<V> List();
    }
}
