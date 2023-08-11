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
    public class ProductsService : IService<tbProducts, View_tbProducts_List>
    {
        public ProductsRepositoryTest _productsRepository = new ProductsRepositoryTest();
        public ServiceResult Delete(int id, int mod)
        {
            var result = new ServiceResult();
            try
            {
                var ModUser = mod;
                var IdUser = id;
                var usuario = _productsRepository.Find(x => x.pro_Id == id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _productsRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = mod;
                EventLogger.Delete($"Eliminado Producto '{usuario.pro_Description}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult List()
        {
            var result = new ServiceResult();
            try
            {

                var listado = _productsRepository.List();
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

        public ServiceResult Find(Expression<Func<View_tbProducts_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbProducts_List();
            var errorMessage = "";
            try
            {
                resultado = _productsRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult Insert(tbProducts item)
        {
            var result = new ServiceResult();
            var entidad = new ProductsModel();
            try
            {
                var repeated = _productsRepository.Find(x => x.pro_Description.ToLower() == item.pro_Description.ToLower());
                if (repeated != null && repeated.pro_Status == true)
                    return result.Conflict($"Un producto con el nombre '{item.pro_Description}' ya existe");
                var query = _productsRepository.Insert(item);
                EventLogger.UserId = item.pro_IdUserCreate;
                EventLogger.Create($"Creado Productos '{item.pro_Description}'.", item);
                if (query > 0)
                {
                    entidad.pro_Id = query;
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

        public ServiceResult Update(tbProducts item, int Id)
        {
            var result = new ServiceResult();
            var entidad = new ProductsModel();
            try
            {
                var IdUser = Id;
                var usuario = _productsRepository.Find(x => x.pro_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var repeated = _productsRepository.Find(x => x.pro_Description.ToLower() == item.pro_Description.ToLower());
                if (repeated != null && repeated.pro_Status == true && repeated.pro_Id != Id)
                    return result.Conflict($"Un producto con el nombre '{item.pro_Description}' ya existe");
                var query = _productsRepository.Update(IdUser, item);
                EventLogger.UserId = item.pro_IdUserModified;
                EventLogger.Update($"Actualizado Producto '{item.pro_Description}'.", usuario, item);
                entidad.pro_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        public ServiceResult UpdateStock(int Id, tbProducts items)
        {
            var result = new ServiceResult();
            var entidad = new ProductStockModel();
            try
            {
                var IdUser = Id;
                var usuario = _productsRepository.Find(x => x.pro_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _productsRepository.UpdateStock(IdUser, items);
                entidad.pro_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
    }
}
