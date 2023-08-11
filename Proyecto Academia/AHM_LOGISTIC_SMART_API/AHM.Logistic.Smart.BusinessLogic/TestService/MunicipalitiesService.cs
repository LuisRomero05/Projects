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
    public class MunicipalitiesService : IService<tbMunicipalities, View_tbMunicipalities_List>
    {
        public MunicipalitiesRepositoryTest _municipalitiesRepositoryTest = new MunicipalitiesRepositoryTest();

        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = id;
                var ModUser = mod;
                var usuario = _municipalitiesRepositoryTest.Find(x => x.dep_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _municipalitiesRepositoryTest.Delete(IdUser, ModUser);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbMunicipalities_List, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(tbMunicipalities item)
        {
            var result = new ServiceResult();
            var entidad = new MunicipalitiesModel();
            try
            {
                var query = _municipalitiesRepositoryTest.Insert(item);
                if (query > 0)
                {
                    entidad.dep_Id = query;
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
                var listado = _municipalitiesRepositoryTest.List();
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

        public ServiceResult Update(tbMunicipalities item, int id)
        {
            var result = new ServiceResult();
            var entidad = new AreasModel();
            try
            {
                var IdUser = id;
                var usuario = _municipalitiesRepositoryTest.Find(x => x.dep_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _municipalitiesRepositoryTest.Update(IdUser, item);
                entidad.are_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
    }
}
