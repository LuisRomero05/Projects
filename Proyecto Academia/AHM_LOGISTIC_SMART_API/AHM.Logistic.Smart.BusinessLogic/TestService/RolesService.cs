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
    public class RolesService : IService<tbRoles, View_tbRoles_List>
    {
        public RolesRepositoryTest _rolesRepository = new RolesRepositoryTest();

        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            RolModel model = new RolModel();
            try
            {
                var ModUser = mod;
                var IdUser = id;
                var usuario = _rolesRepository.Find(x => x.rol_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _rolesRepository.Delete(IdUser, ModUser);
                if (query > 0)
                {
                    model.rol_Id = query;
                    return result.Ok(model);
                }
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Find(Expression<Func<View_tbRoles_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbRoles_List();
            var errorMessage = "";
            try
            {
                resultado = _rolesRepository.Search(expression);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult List()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _rolesRepository.List();
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

        public ServiceResult RegisterRol(tbRoles item, List<tbRoleModuleItems> rolModule)
        {
            var result = new ServiceResult();
            RolModel model = new RolModel();
            try
            {
                var repeated = _rolesRepository.Find(x => x.rol_Description.ToLower() == item.rol_Description.ToLower());
                if (repeated != null && repeated.rol_Status == true)
                    return result.Conflict($"Un rol con el nombre '{repeated.rol_Description}' ya existe");
                var query = _rolesRepository.InsertRol(item, rolModule);
                if (query > 0)
                {
                    model.rol_Id = query;
                    return result.Ok(model);
                }
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult UpdateRol(tbRoles item, List<tbRoleModuleItems> rolModule)
        {
            var result = new ServiceResult();
            RolModel model = new RolModel();
            try
            {
                var usuario = _rolesRepository.Find(x => x.rol_Id == item.rol_Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _rolesRepository.UpdateRol(item, rolModule);
                if (query > 0)
                {
                    model.rol_Id = query;
                    return result.Ok(model);
                }
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
        public ServiceResult Insert(tbRoles item)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Update(tbRoles item, int id)
        {
            throw new NotImplementedException();
        }
    }
}
