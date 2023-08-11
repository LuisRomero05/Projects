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
    public class PersonsService : IService<tbPersons, View_tbPersons_List>
    {
        PersonsRepositoryTest _personsRepository = new PersonsRepositoryTest();
        MunicipalitiesRepositoryTest _municipalitiesRepository = new MunicipalitiesRepositoryTest();
        DepartmentsRepositoryTest _departmentsRepository = new DepartmentsRepositoryTest();
        public ServiceResult List()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _personsRepository.List();
                if (listado.Any())
                    return result.Ok(listado);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }


        public ServiceResult Find(Expression<Func<View_tbPersons_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbPersons_List();
            var errorMessage = "";
            try
            {
                resultado = _personsRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult Insert(tbPersons item)
        {
            var result = new ServiceResult();
            var entidad = new PersonsModel();
            try
            {
                var muni = _municipalitiesRepository.Find(x => x.mun_Id == item.mun_Id);
                if (muni == null)
                    return result.Error($"No existe el municipio seleccionado");
                var muniDepId = _municipalitiesRepository.Find(s => s.mun_Id == item.mun_Id);
                var usuario = _departmentsRepository.Find(x => x.dep_Id == item.dep_Id && x.dep_Id == muniDepId.dep_Id);
                if (usuario == null)
                    return result.Error($"No se podra realizar el registro, el municipio no esta ligado a ese departamento");
                var repeated = _personsRepository.Find(x => x.per_Identidad == item.per_Identidad);
                if (repeated != null && repeated.per_Status == true)
                    return result.Conflict($"Una persona con la identidad '{item.per_Identidad}' ya existe");
                var query = _personsRepository.Insert(item);
                EventLogger.UserId = item.per_IdUserCreate;
                EventLogger.Create($"Creada Persona '{item.per_Firstname}'.", item);
                if (query > 0)
                {
                    entidad.per_Id = query;
                    return result.Ok(entidad);
                }
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }


        public ServiceResult Update(tbPersons item, int Id)
        {
            var result = new ServiceResult();
            var entidad = new PersonsModel();
            try
            {
                var IdUser = Id;
                var muni = _municipalitiesRepository.Find(x => x.mun_Id == item.mun_Id);
                if (muni == null)
                    return result.Error($"No existe el municipio seleccionado.");
                var dep = _departmentsRepository.Find(x => x.dep_Id == item.dep_Id);
                if (dep == null)
                    return result.Error($"No existe el departamento seleccionado.");
                var usuario = _personsRepository.Find(x => x.per_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro.");
                var repeated = _personsRepository.Find(x => x.per_Identidad == item.per_Identidad);
                if (repeated != null && repeated.per_Status == true && repeated.per_Id != Id)
                    return result.Conflict($"Una persona con la identidad '{repeated.per_Identidad}' ya existe.");
                var muniDepId = _municipalitiesRepository.Find(s => s.mun_Id == item.mun_Id);
                var validacion = _departmentsRepository.Find(x => x.dep_Id == item.dep_Id && x.dep_Id == muniDepId.dep_Id);
                if (validacion == null)
                    return result.Error($"No se podra realizar el registro, el municipio no esta ligado a ese departamento.");
                var query = _personsRepository.Update(IdUser, item);
                EventLogger.UserId = item.per_IdUserModified;
                EventLogger.Update($"Actualizada Persona '{item.per_Firstname}'.", usuario, item);
                entidad.per_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Delete(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _personsRepository.Find(x => x.per_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _personsRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminada Persona '{usuario.per_Firstname}'.", usuario);
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
