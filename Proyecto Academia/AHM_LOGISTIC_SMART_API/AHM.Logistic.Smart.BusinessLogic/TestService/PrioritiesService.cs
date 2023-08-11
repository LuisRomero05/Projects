using AHM.Logistic.Smart.DataAccess.TestRepositories;
using AHM.Logistic.Smart.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.BusinessLogic.TestService
{
    public class PrioritiesService : IService<tbPriorities, View_tbPriorities_List>
    {
        PrioritiesRepositoryTest _prioritiesRepository = new PrioritiesRepositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Find(Expression<Func<View_tbPriorities_List, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(tbPriorities item)
        {
            throw new NotImplementedException();
        }

        public ServiceResult List()
        {
            var result = new ServiceResult();
            try
            {
                var data = _prioritiesRepository.List();
                if (data.Any()) return result.Ok(data);
                else return result.Error();;

            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Update(tbPriorities item, int id)
        {
            throw new NotImplementedException();
        }
    }
}
