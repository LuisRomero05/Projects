using AHM.Logistic.Smart.DataAccess.Repositories;
using AHM.Logistic.Smart.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.BusinessLogic.TestService
{
    public class CallResultServies : IService<tbCallResult, View_tbCallResult_List>
    {
        public CallResultRepository _callResultRepository = new CallResultRepository();
        public ServiceResult Delete(int id, int mod)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Find(Expression<Func<View_tbCallResult_List, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(tbCallResult item)
        {
            throw new NotImplementedException();
        }

        public ServiceResult List()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _callResultRepository.List();
                if (listado.Any())
                    return result.Ok(listado);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Update(tbCallResult item, int id)
        {
            throw new NotImplementedException();
        }
    }
}
