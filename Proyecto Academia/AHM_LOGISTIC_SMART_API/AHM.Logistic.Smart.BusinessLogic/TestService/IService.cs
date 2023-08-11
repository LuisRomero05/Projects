using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.BusinessLogic.TestService
{
    public interface IService<T,V>
    {
        public ServiceResult List();
        public ServiceResult Find(Expression<Func<V, bool>> expression = null);
        public ServiceResult Insert(T item);
        public ServiceResult Update(T item, int id);
        public ServiceResult Delete(int id, int mod);
    }
}
