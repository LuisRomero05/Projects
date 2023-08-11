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
    public class EmployeesService : IService<tbEmployees, View_tbEmployees_List>
    {
        public EmployeesRepositoryTest _employeesRepository = new EmployeesRepositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = id;
                var ModUser = mod;
                var usuario = _employeesRepository.Find(x => x.emp_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _employeesRepository.Delete(IdUser, ModUser);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbEmployees_List, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(tbEmployees item)
        {
            var result = new ServiceResult();
            var entidad = new EmployeesModel();
            try
            {
                var query = _employeesRepository.Insert(item);
                if (query > 0)
                {
                    entidad.emp_Id = query;
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
                var listado = _employeesRepository.List();
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

        public ServiceResult Update(tbEmployees item, int id)
        {
            var result = new ServiceResult();
            var entidad = new EmployeesModel();
            try
            {
                var IdUser = id;
                var usuario = _employeesRepository.Find(x => x.emp_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _employeesRepository.Update(IdUser, item);
                entidad.emp_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
    }
}
