using AHM.Logistic.Smart.BusinessLogic.Logger;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.DataAccess.Repositories;
using AHM.Logistic.Smart.Entities.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AHM.Logistic.Smart.BusinessLogic.Services
{
    public class SalesService
    {
        private readonly CategoriesRepository _categoriesRepository;
        private readonly ProductsRepository _productsRepository;
        private readonly SubCategoriesRepository _subCategoriesRepository;
        private readonly UnitsRepository _unitsRepository;
        private readonly CotizationsRepository _cotizationsRepository;
        private readonly OrdersRepository _ordersRepository;
        private readonly CampaignRepository _campaignRepository;
        public SalesService(    CategoriesRepository categoriesRepository, 
                                ProductsRepository productsRepository,
                                SubCategoriesRepository subCategoriesRepository, 
                                UnitsRepository unitsRepository,
                                CotizationsRepository cotizationsRepository,
                                OrdersRepository ordersRepository,
                                CampaignRepository campaignRepository)
        {
            _unitsRepository = unitsRepository;
            _categoriesRepository = categoriesRepository;
            _productsRepository = productsRepository;
            _subCategoriesRepository = subCategoriesRepository;
            _cotizationsRepository = cotizationsRepository;
            _ordersRepository = ordersRepository;
            _campaignRepository = campaignRepository;
        }

        #region tbProducts
        public ServiceResult ListProducts()
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

        public ServiceResult FindProducts(Expression<Func<View_tbProducts_List, bool>> expression = null)
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

        public ServiceResult RegisterProducts(tbProducts item)
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

        public ServiceResult UpdateProducts(int Id, tbProducts item)
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
        public ServiceResult UpdateStockProducts(int Id, tbProducts items)
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

        public ServiceResult DeleteProducts(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var ModUser = Mod;
                var IdUser = Id;
                var usuario = _productsRepository.Find(x => x.pro_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _productsRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminado Producto '{usuario.pro_Description}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        #endregion

        #region tbCategories

        public ServiceResult RegisterCategory(tbCategories item)
        {
            var result = new ServiceResult();
            var entidad = new CategoryModel();
            try
            {
                var repeated = _categoriesRepository.Find(x => x.cat_Description.ToLower() == item.cat_Description.ToLower());
                if (repeated != null && repeated.cat_Status == true)
                    return result.Conflict($"Una categoria con el nombre '{item.cat_Description}' ya existe");
                var query = _categoriesRepository.Insert(item);
                EventLogger.UserId = item.cat_IdUserCreate;
                EventLogger.Create($"Creada Categoria '{item.cat_Description}'.", item);
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
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        public ServiceResult FindCategories(Expression<Func<View_tbCategories_List, bool>> expression = null)
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
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult ListCategory()
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
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult UpdateCategory(int Id, tbCategories item)
        {
            var result = new ServiceResult();
            var entidad = new CategoryModel();
            try
            {
                var IdUser = Id;
                var usuario = _categoriesRepository.Find(x => x.cat_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var repeated = _categoriesRepository.Find(x => x.cat_Description.ToLower() == item.cat_Description.ToLower());
                if (repeated != null && repeated.cat_Status == true)
                    return result.Conflict($"Una categoria con el nombre '{item.cat_Description}' ya existe");
                var query = _categoriesRepository.Update(IdUser, item);
                EventLogger.UserId = item.cat_IdUserModified;
                EventLogger.Update($"Actualizada Categoria '{item.cat_Description}'.", usuario, item);
                entidad.cat_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteCategory(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var ModUser = Mod;
                var IdUser = Id;
                var usuario = _categoriesRepository.Find(x => x.cat_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _categoriesRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminada Categoria '{usuario.cat_Description}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        #endregion

        #region SubCategories
        public ServiceResult ListSubCategories()
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
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult FindSubCategories(Expression<Func<View_tbSubCategories_List, bool>> expression = null)
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
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterSubCategories(tbSubCategories item)
        {
            var result = new ServiceResult();
            var entidad = new SubCategoriesModel();
            try
            {

                var repeated = _subCategoriesRepository.Find(x => x.scat_Description.ToLower() == item.scat_Description.ToLower() && x.cat_Id == item.cat_Id);
                if (repeated != null && repeated.scat_Status == true)
                    return result.Conflict($"La categoria ya tiene una subcategoria con el nombre '{item.scat_Description}' asignado");
                var query = _subCategoriesRepository.Insert(item);
                EventLogger.UserId = item.scat_IdUserCreate;
                EventLogger.Create($"Creada Sub-Categoria '{item.scat_Description}'.", item);
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
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult UpdateSubCategories(int Id, tbSubCategories item)
        {
            var result = new ServiceResult();
            var entidad = new SubCategoriesModel();
            try
            {
                var IdUser = Id;
                var usuario = _subCategoriesRepository.Find(x => x.scat_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var repeated = _subCategoriesRepository.Find(x => x.scat_Description.ToLower() == item.scat_Description.ToLower() && x.cat_Id == item.cat_Id);
                if (repeated != null && repeated.scat_Status == true)
                    return result.Conflict($"La categoria ya tiene una subcategoria con el nombre '{item.scat_Description}' asignado");
                var query = _subCategoriesRepository.Update(IdUser, item);
                EventLogger.UserId = item.scat_IdUserModified;
                EventLogger.Update($"Actualizada Sub-Categoria '{item.scat_Description}'.", usuario, item);
                entidad.scat_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteSubCategories(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _subCategoriesRepository.Find(x => x.scat_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _subCategoriesRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminada Sub-Categoria '{usuario.scat_Description}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        #endregion

        #region Units
        public ServiceResult ListUnits()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _unitsRepository.List();
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
        #endregion

        #region Cotizations
        public ServiceResult RegisterCotization(tbCotizations item, List<tbCotizationsDetail> data)
        {
            var result = new ServiceResult();
            try
            {
                if (item.cus_Id > 0)
                {
                    string query = _cotizationsRepository.Insert(item, data);
                    EventLogger.UserId = item.cot_IdUserCreate;
                    EventLogger.Create($"Creado Cotizacion con ID: '{item.cot_Id}'.", item);
                    if (query == "success")
                        return result.Ok(query);
                    else
                        return result.Error();
                }
                else
                {
                    return result.Error();
                }
                
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult RegisterCotizationDetails(List<tbCotizationsDetail> data)
        {
            var result = new ServiceResult();
            try
            {
                string query = _cotizationsRepository.InsertDetails(data);
                if (query == "success")
                        return result.Ok(query);
                    else
                        return result.Error();
                
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult ListCotization()
        {
            var result = new ServiceResult();
            try
            {
                var query = _cotizationsRepository.List();
                if (query.Any())
                    return result.Ok(query);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult UpdateCotizations(tbCotizations item, List<tbCotizationsDetail> data)
        {
            var result = new ServiceResult();
            try
            {
                var cotizations = _cotizationsRepository.Find(x => x.cot_Id == item.cot_Id);
                if (cotizations == null)
                    return result.Error($"No existe el registro");
                item.cot_Status = cotizations.cot_Status;
                item.cot_IdUserCreate = cotizations.cot_IdUserCreate;
                item.cot_DateCreate = cotizations.cot_DateCreate;
                foreach (var item2 in data)
                {
                    item2.code_Status = cotizations.cot_Status;
                    item2.code_IdUserCreate = cotizations.cot_IdUserCreate;
                    item2.code_DateCreate = cotizations.cot_DateCreate;
                }
                string query = _cotizationsRepository.Update(item, data);
                EventLogger.UserId = item.cot_IdUserModified;
                EventLogger.Update($"Actualizada Cotizacion con ID: '{item.cot_Id}'.", cotizations, item);
                if (query == "success")
                    return result.Ok(query);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult DeleteCotizations(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var cotizations = _cotizationsRepository.Find(x => x.cot_Id == Id);
                if (cotizations == null)
                    return result.Error($"No existe el registro");
                   var query = _cotizationsRepository.Delete(Id, Mod);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminada Cotizacion con ID: '{cotizations.cot_Id}'.", cotizations);
                return result.Ok(query);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult DeleteDetail(int proId, int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var cotizations = _cotizationsRepository.FindDetails(x => x.cot_Id == Id && x.pro_Id == proId);
                if (cotizations == null)
                    return result.Error($"No existe el registro");
                var query = _cotizationsRepository.DeleteDetail(proId, Id, Mod);

                    return result.Ok(query);
  
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult DetailsCotizations(Expression<Func<View_tbCotizationsDetails_List, bool>> expression = null)
        {
            var result = new ServiceResult();

            List<View_tbCotizationsDetails_List> resultado = new List<View_tbCotizationsDetails_List>();
            var errorMessage = "";
            try
            {
                resultado = _cotizationsRepository.Details(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }
        #endregion

        #region SaleOrders
        public ServiceResult ListSaleOrder()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _ordersRepository.List();
                if (listado.Any())
                    return result.Ok(listado);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }
        
        public ServiceResult DetailsSalesOrder(Expression<Func<View_tbSalesDetails_List, bool>> expression = null)
        {
            var result = new ServiceResult();

            List<View_tbSalesDetails_List> resultado = new List<View_tbSalesDetails_List>();
            var errorMessage = "";
            try
            {
                resultado = _ordersRepository.Details(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterSaleOrder(tbSaleOrders item, List<tbOrderDetails> data)
        {
            var result = new ServiceResult();
            try
            {
                if (item.cus_Id > 0)
                {
                    string query = _ordersRepository.Insert(item, data);
                    EventLogger.UserId = item.sor_IdUserCreate;
                    EventLogger.Create($"Creada Orden de Venta Con ID: '{item.sor_Id}'.", item);
                    if (query == "success")
                        return result.Ok(query);
                    else
                        return result.Error();
                }
                else
                {
                    return result.Error();
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult UpdateSaleOrder(tbSaleOrders item, List<tbOrderDetails> data)
        {
            var result = new ServiceResult();
            try
            {
                var orders = _ordersRepository.Find(x => x.sor_Id == item.sor_Id);
                if (orders == null)
                    return result.Error($"No existe el registro");
                item.sor_Status = orders.sor_Status;
                item.sor_IdUserCreate = orders.sor_IdUserCreate;
                item.sor_DateCreate = orders.sor_DateCreate;
                foreach (var item2 in data)
                {
                    item2.ode_Status = orders.sor_Status;
                    item2.ode_IdUserCreate = orders.sor_IdUserCreate;
                    item2.ode_DateCreate = orders.sor_DateCreate;
                }
                string query = _ordersRepository.Update(item, data);
                EventLogger.UserId = item.sor_IdUserModified;
                EventLogger.Update($"Actualizada Orden de Venta Con ID: '{item.sor_Id}'.", orders, item);
                if (query == "success")
                    return result.Ok(query);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult DeleteSaleOrder(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var usuario = _ordersRepository.Find(x => x.sor_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _ordersRepository.Delete(Id, Mod);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminada Orden de Venta Con ID: '{usuario.sor_Id}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult RegisterOrdersDetails(List<tbOrderDetails> data)
        {
            var result = new ServiceResult();
            try
            {

                string query = _ordersRepository.InsertDetails(data);

                if (query == "success")
                    return result.Ok(query);
                else
                    return result.Error();



            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult DeleteOrdersDetail(int Id, int proId, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var cotizations = _ordersRepository.FindDetails(x => x.sor_Id == Id && x.pro_Id == proId);
                if (cotizations == null)
                    return result.Error($"No existe el registro");
                var query = _ordersRepository.DeleteDetail(proId, Id, Mod);

                return result.Ok(query);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }
        #endregion

        #region Campaigns
        public ServiceResult SendCampaigns(tbCampaignDetails details)
        {
            var result = new ServiceResult();
            try
            {
                var query = _campaignRepository.SendCampaign(details);

                    return result.Ok(query);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        public ServiceResult FindCampaigns(Expression<Func<View_tbCampaign_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbCampaign_List();
            var errorMessage = "";
            try
            {
                resultado = _campaignRepository.CampaignDetails(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult ListCampaigns()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _campaignRepository.List();
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

 

        public ServiceResult ListCampaignsDetails(Expression<Func<View_tbCampaignDetails_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            List<View_tbCampaignDetails_List> resultado = new List<View_tbCampaignDetails_List>();
            var errorMessage = "";
            try
            {
              
                resultado = _campaignRepository.ListDetails(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterCampaigns(tbCampaign item)
        {
            var result = new ServiceResult();
            var entidad = new CampaignModel();
            try
            {
                var query = _campaignRepository.Insert(item);
                EventLogger.UserId = item.cam_IdUserCreate;
                EventLogger.Create($"Creado Registro '{item.cam_Descripcion}'.", item);
                if (query > 0)
                {
                    entidad.cam_Id = query;
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

        public ServiceResult DeleteCampaign(int Id)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var usuario = _campaignRepository.Find(x => x.cam_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _campaignRepository.Delete(IdUser);
                EventLogger.UserId = 0;
                EventLogger.Delete($"Eliminada Area '{usuario.cam_Descripcion}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        #endregion

    }
}
