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
    public class CategoriesService : IService<tbCategories, View_tbCategories_List>
    {
        public CategoriesRepositoryTest _categoriesRepository = new CategoriesRepositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var ModUser = mod;
                var IdUser = id;
                var usuario = _categoriesRepository.Find(x => x.cat_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _categoriesRepository.Delete(IdUser, ModUser);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbCategories_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbCategories_List();
            var errorMessage = "";
            try
            {
                resultado = _categoriesRepository.Search(expression);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult Insert(tbCategories item)
        {
            var result = new ServiceResult();
            var entidad = new CategoryModel();
            try
            {
                var repeated = _categoriesRepository.Find(x => x.cat_Description.ToLower() == item.cat_Description.ToLower());             
                var query = _categoriesRepository.Insert(item);
                if (query > 0)
                {
                    entidad.cat_Id = query;
                    return result.Ok(query);
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
                var listado = _categoriesRepository.List();
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

        public ServiceResult Update(tbCategories item, int id)
        {
            var result = new ServiceResult();
            var entidad = new CategoryModel();
            try
            {
                var IdUser = id;
                var usuario = _categoriesRepository.Find(x => x.cat_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");              
                var query = _categoriesRepository.Update(IdUser, item);
                entidad.cat_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
    }
}
