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
    public class CustomerNotesService : IService<tbCustomerNotes, View_tbCustomerNotes>
    {
        public CustomerNotesRepositoryTest _customerNotesRepository = new CustomerNotesRepositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var note = _customerNotesRepository.Find(x => x.cun_Id == id);
                if (note == null) return result.Error();
                var query = _customerNotesRepository.Delete(id, mod);
                return result.Ok(query);
            }
            catch (Exception ex)
            {

                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbCustomerNotes, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(tbCustomerNotes item)
        {
            var result = new ServiceResult();
            var model = new CustomerNotesModel();
            try
            {
                var query = _customerNotesRepository.Insert(item);
                if (query > 0)
                {
                    model.cun_Id = query;
                    return result.Ok(model);
                }
                else return result.Error();
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
                var data = _customerNotesRepository.List();
                if (data.Any()) return result.Ok(data);
                else return result.Error();
            }
            catch (Exception ex)
            {

                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult Update(tbCustomerNotes item, int id)
        {
            var result = new ServiceResult();
            var model = new CustomerNotesModel();
            try
            {
                var data = _customerNotesRepository.Find(x => x.cun_Id == id);
                if (data == null) return result.Error("No existe el registro");
                var query = _customerNotesRepository.Update(id, item);
                model.cun_Id = query;
                return result.Ok(model);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
    }
}
