using AHM.Logistic.Smart.Common.Models;
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
    public class CustomerFileService : IService<tbCustomersFile, View_tbCustomersFile_List>
    {
        CustomerFileRepositoryTest _customerFileRepositoryTest = new CustomerFileRepositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var data = _customerFileRepositoryTest.Find(x => x.cfi_Id == id);
                if (data == null) return result.Error("No existe el registro");
                var query = _customerFileRepositoryTest.Delete(id, mod);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbCustomersFile_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbCustomersFile_List();
            try
            {
                resultado = _customerFileRepositoryTest.Search(expression);
                if (resultado == null) return result.Error("No existe el registro");
                return result.Ok(resultado);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Insert(tbCustomersFile item)
        {
            var result = new ServiceResult();
            var entidad = new CustomerFilesModel();
            try
            {
                var query = _customerFileRepositoryTest.Insert(item);
                entidad.cfi_Id = query;
                if (query > 0)
                    return result.Ok(entidad);

                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult List()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _customerFileRepositoryTest.List();

                return result.Ok(listado);

            }
            catch (Exception ex)
            {
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult Update(tbCustomersFile item, int id)
        {
            throw new NotImplementedException();
        }
    }
}
