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
    public class SubCategoriesService : IService<tbSubCategories, View_tbSubCategories_List>
    {
        public SubCategoriesRepositoryTest _subCategoriesRepository = new SubCategoriesRepositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = id;
                var ModUser = mod;
                var usuario = _subCategoriesRepository.Find(x => x.scat_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _subCategoriesRepository.Delete(IdUser, ModUser);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbSubCategories_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbSubCategories_List();
            var errorMessage = "";
            try
            {
                resultado = _subCategoriesRepository.Search(expression);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult Insert(tbSubCategories item)
        {
            var result = new ServiceResult();
            var entidad = new SubCategoriesModel();
            try
            {

                var repeated = _subCategoriesRepository.Find(x => x.scat_Description.ToLower() == item.scat_Description.ToLower() && x.cat_Id == item.cat_Id);
                if (repeated != null && repeated.scat_Status == true)
                    return result.Conflict($"La categoria ya tiene una subcategoria con el nombre '{item.scat_Description}' asignado");
                var query = _subCategoriesRepository.Insert(item);             
                if (query > 0)
                {
                    entidad.scat_Id = query;
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
                var listado = _subCategoriesRepository.List();
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

        public ServiceResult Update(tbSubCategories item, int id)
        {
            var result = new ServiceResult();
            var entidad = new SubCategoriesModel();
            try
            {
                var IdUser = id;
                var usuario = _subCategoriesRepository.Find(x => x.scat_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");               
                var query = _subCategoriesRepository.Update(IdUser, item);
                entidad.scat_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
    }
}
