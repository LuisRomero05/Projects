using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.DataAccess.Repositories;
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
    public class OccupationService : IService<tbOccupations, View_tbOccupations_List>
    {
        public OccupationRepositoryTest _occupationRepository = new OccupationRepositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = id;
                var ModUser = mod;
                var usuario = _occupationRepository.Find(x => x.occ_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _occupationRepository.Delete(IdUser, ModUser);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbOccupations_List, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(tbOccupations item)
        {
            var result = new ServiceResult();
            var entidad = new OccupationsModel();
            try
            {
                var query = _occupationRepository.Insert(item);
                if (query > 0)
                {
                    entidad.occ_Id = query;
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
                var listado = _occupationRepository.List();
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

        public ServiceResult Update(tbOccupations item, int id)
        {
            var result = new ServiceResult();
            var entidad = new OccupationsModel();
            try
            {
                var IdUser = id;
                var usuario = _occupationRepository.Find(x => x.occ_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _occupationRepository.Update(IdUser, item);
                entidad.occ_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
    }
}
