using AHM.Logistic.Smart.BusinessLogic.Logger;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.DataAccess.TestRepositories;
using AHM.Logistic.Smart.Entities.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.BusinessLogic.TestService
{
    public class CustomerCallsServices : IService<tbCustomerCalls, View_tbCustomerCalls>
    {
        public CustomerCallsRepositoryTest _customerCallsRepositoryTest = new CustomerCallsRepositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = id;
                var ModUser = mod;
                var usuario = _customerCallsRepositoryTest.Find(x => x.cca_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _customerCallsRepositoryTest.Delete(IdUser, ModUser);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbCustomerCalls, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(tbCustomerCalls item)
        {
            
            var result = new ServiceResult();
            var entidad = new CustomerCallsModel();
            try
            {
                var query = _customerCallsRepositoryTest.Insert(item);
                if (query > 0)
                {
                    entidad.cca_Id = query;
                    return result.Ok(entidad);
                }

                else
                    return result.Error();
            }
            catch (Exception ex)
            {

                return result.Error(ex.Message);
            }
        }

        public ServiceResult List()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _customerCallsRepositoryTest.List();
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

        public ServiceResult Update(tbCustomerCalls item, int id)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = id;


                var query = _customerCallsRepositoryTest.Update(IdUser, item);
                EventLogger.UserId = item.cca_IdUserModified;
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
    }
}
