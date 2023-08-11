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
    public class ContactsService : IService<tbContacts, View_tbContacts_List>
    {
        public ContactsRepositoryTest _contactsRepository = new ContactsRepositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = id;
                var ModUser = mod;
                var usuario = _contactsRepository.Find(x => x.cont_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _contactsRepository.Delete(IdUser, ModUser);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbContacts_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbContacts_List();
            var errorMessage = "";
            try
            {
                resultado = _contactsRepository.Search(expression);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult Insert(tbContacts item)
        {
            var result = new ServiceResult();
            var entidad = new ContactsModel();
            try
            {
                var query = _contactsRepository.Insert(item);
                if (query > 0)
                {
                    entidad.cont_Id = query;
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
                var listado = _contactsRepository.List();
                return result.Ok(listado);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Update(tbContacts item, int id)
        {
            var result = new ServiceResult();
            var entidad = new ContactsModel();
            try
            {
                var IdUser = id;
                var usuario = _contactsRepository.Find(x => x.cont_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _contactsRepository.Update(IdUser, item);
                entidad.cont_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
    }
}
