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
    public class CustomerService : IService<tbCustomers, View_tbCustomers_List>
    {
        public CustomerRepositoryTest _CustomerRepository = new CustomerRepositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = id;
                var ModUser = mod;
                var usuario = _CustomerRepository.Find(x => x.cus_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _CustomerRepository.Delete(IdUser, ModUser);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbCustomers_List, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(tbCustomers item)
        {
            var result = new ServiceResult();
            var entidad = new CustomersModel();
            try
            {
                var query = _CustomerRepository.Insert(item);
                if (query > 0)
                {
                    entidad.cus_Id = query;
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
                var listado = _CustomerRepository.List();
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

        public ServiceResult Update(tbCustomers item, int Id)
        {
            //var result = new ServiceResult();
            //var entidad = new CustomersModel();
            //try
            //{
            //    var IdUser = id;
            //    var usuario = _CustomerRepository.Find(x => x.cus_Id == id);
            //    if (usuario == null)
            //        return result.Error($"No existe el registro");
            //    var query = _CustomerRepository.Update(IdUser, item);
            //    entidad.cus_Id = query;
            //    return result.Ok(entidad);
            //}
            //catch (Exception ex)
            //{
            //    return result.Error(ex.Message);
            //}
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;


                var query = _CustomerRepository.Update(IdUser, item);
                EventLogger.UserId = item.cus_IdUserModified;
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
