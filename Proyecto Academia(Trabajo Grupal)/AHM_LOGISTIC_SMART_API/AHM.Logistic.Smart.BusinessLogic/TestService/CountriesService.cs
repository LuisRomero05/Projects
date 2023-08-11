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
    public class CountriesService : IService<tbCountries, View_tbCountries_List>
    {
        public CountriesRespositoryTest _countriesRespository = new CountriesRespositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = id;
                var ModUser = mod;
                var usuario = _countriesRespository.Find(x => x.cou_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _countriesRespository.Delete(IdUser, ModUser);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbCountries_List, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(tbCountries item)
        {
            var result = new ServiceResult();
            var entidad = new CountryModel();
            try
            {
                var query = _countriesRespository.Insert(item);
                if (query > 0)
                {
                    entidad.cou_Id = query;
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
                var listado = _countriesRespository.List();
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

        public ServiceResult Update(tbCountries item, int id)
        {
            var result = new ServiceResult();
            var entidad = new CountryModel();
            try
            {
                var IdUser = id;
                var usuario = _countriesRespository.Find(x => x.cou_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _countriesRespository.Update(IdUser, item);
                entidad.cou_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
    }
}
