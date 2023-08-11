using AHM.Logistic.Smart.Common.Models;
using AHM_LOGISTIC_SMART_ADM.Models;
using AHM_LOGISTIC_SMART_ADM.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Services
{
    public class SalesService
    {
        private readonly Api _api;

        public SalesService(Api api)
        {
            _api = api;
        }

        #region tbCategorias

        public async Task<ServiceResult> CategoriesList(List<CategoryViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<CategoryViewModel>>(req =>
                {
                    req.Path = $"/api/Categories/List";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> CategoriesDetails(int id)
        {
            var result = new ServiceResult();
            var model = new CategoryModel();
            try
            {
                var response = await _api.Get<CategoryModel>(req =>
                {
                    req.Path = $"/api/Categories/Details/{id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InsertCategories(CategoryModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<CategoryModel>(req =>
                {
                    req.Path = $"/api/Categories/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> CategoriesEdit(CategoryModel model, int id)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Put<CategoryModel>(req =>
                {
                    req.Path = $"/api/Categories/Update/{id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> DeleteCategories(int Id, int Mod)
        {

            var result = new ServiceResult();

            try
            {
                string msjError = string.Empty;
                //Traer datos de las tablas relacionadas
                var scate = await _api.Get<List<SubCategoriesModel>>(req =>//subcategorias
                {
                    req.Path = $"/api/SubCategories/List";

                });

                //buscar en esas tablas los registros que se relacionan con el cliente que se eliminara
                var listcate = scate.Data.Where(w => w.cat_Id == Id);//notas*

                //Si no se encuentra un registro en alguna de las tablas relacionadas se procedera a eliminar el cliente
                if (listcate.Count() == 0)
                {
                    var response = await _api.Delete<CategoryModel>(req =>
                    {
                        req.Path = $"/api/Categories/Delete/" + Id + "?Mod=" + Mod;
                    });
                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok(response.StatusCode);
                }
                else
                    return result.Error("No se puede eliminar ya que el registro se encuentra en uso.");
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }

            //try
            //{
            //    var response = await _api.Delete<CategoryModel>(req =>
            //    {
            //        //req.Path = $"/api/Employees/Delete" + Id + "?Mod=" + Mod;
            //        req.Path = $"/api/Categories/Delete/" + Id + "?Mod=" + Mod;
            //        //req.Content = model;
            //    });

            //    if (!response.Success)
            //        return result.FromApi(response);

            //    return result.Ok(response.StatusCode);
            //}
            //catch (Exception ex)
            //{
            //    return result.Error(Helpers.GetMessage(ex));
            //}
        }
        #endregion

        #region tbSubCategorias
        //listado
        public async Task<ServiceResult> SubCategoryList(List<SubCategoriesViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<SubCategoriesViewModel>>(req =>
                {
                    req.Path = $"/api/SubCategories/List";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        //insertar
        public async Task<ServiceResult> InsertSubCategories(SubCategoriesModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<SubCategoriesModel>(req =>
                {
                    req.Path = $"/api/SubCategories/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        //details
        public async Task<ServiceResult> DetailsSubCategory(int Id)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<SubCategoriesViewModel>(req =>
                {
                    req.Path = $"/api/SubCategories/Details/{Id}";
                    //req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        //Delete
        public async Task<ServiceResult> DeleteSubCategory(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Delete<SubCategoriesModel>(req =>
                {
                    //req.Path = $"/api/Employees/Delete" + Id + "?Mod=" + Mod;
                    req.Path = $"/api/SubCategories/Delete/" + Id + "?Mod=" + Mod;
                    //req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        //edit
        public async Task<ServiceResult> EditSubCategory(SubCategoriesModel model)
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Put<SubCategoriesModel>(req =>
                {
                    req.Path = $"/api/SubCategories/Update/{model.scat_Id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }


        #endregion

        #region tbProductos
        public async Task<ServiceResult> ProductsList(List<ProductsViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<ProductsViewModel>>(req =>
                {
                    req.Path = $"/api/Products/List";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> ProductDetails(int id)
        {
            var result = new ServiceResult();
            var model = new ProductsViewModel();
            try
            {
                var exist = await _api.Get<List<ProductsViewModel>>(req =>
                {
                    req.Path = $"/api/Products/List";
                });
                if (exist.Data.Where(x => x.pro_Id == id).Count() != 0)
                {
                    var response = await _api.Get<ProductsViewModel>(req =>
                    {
                        req.Path = $"/api/Products/Details/{id}";
                        req.Content = model;
                    });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok(response.Data);
                }
                else
                    return result.Error(ServiceProblemMessage.NotFound);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InsertProducts(ProductsModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<ProductsModel>(req =>
                {
                    req.Path = $"/api/Products/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok("Registro creado exitosamente.", response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> EditProducts(ProductsViewModel model)
        {
            var result = new ServiceResult();
            var Object = new ProductsModel()
            {
                pro_Description = model.pro_Description,
                pro_PurchasePrice = model.pro_PurchasePrice,
                pro_SalesPrice = model.pro_SalesPrice,
                pro_Stock = model.pro_Stock,
                pro_ISV = model.pro_ISV,
                uni_Id = model.uni_Id,
                scat_Id = model.scat_Id,
                pro_IdUserCreate = model.pro_IdUserCreate,
                pro_IdUserModified = model.pro_IdUserModified,
            };
            try
            {
                var response = await _api.Put<ProductsModel>(req =>
                {
                    req.Path = $"/api/Products/Update/{model.pro_Id}";
                    req.Content = Object;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok("Registro actualizado exitosamente.", response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> DeleteProducts(int Id, int Mod)
        {
            var result = new ServiceResult();
            List<SalesOrderViewModel> salesOrders = new List<SalesOrderViewModel>();
            bool hasDepend = false;
            try
            {
                var getDetails = await ProductDetails(Id);
                if (!getDetails.Success && getDetails.Data == null)
                    return result.Error(getDetails.Message);
                var orders = await _api.Get<List<SalesOrderViewModel>>(req =>
                {
                    req.Path = $"/api/Orders/List";
                    req.Content = salesOrders;
                });

                foreach (var item in orders.Data)
                {
                    var details = await _api.Get<List<SalesDetailsViewModel>>(req =>
                    {
                        req.Path = $"/api/Orders/SaleOrdersDetails/{item.sor_Id}";
                    });
                    ;
                    if (details.Data.Where(x => x.pro_Id == Id).Count() != 0)
                    {
                        hasDepend = true;
                        break;
                    }
                }
                if (!hasDepend)
                {
                    var response = await _api.Delete<ProductsModel>(req =>
                    {
                        req.Path = $"/api/Products/Delete/" + Id + "?Mod=" + Mod;
                    });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok(response.StatusCode);
                }
                else
                    return result.Error("No se puede eliminar ya que el registro se encuentra en uso.");
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        #endregion

        #region tbCotizations
        public async Task<ServiceResult> CotizationsList(List<CotizationsViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<CotizationsViewModel>>(req =>
                {
                    req.Path = $"/api/Cotizations/List";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> CotizationsDetails(int id)
        {
            var result = new ServiceResult();
            var model = new List<CotizationsDetailsViewModel>();
            try
            {
                var response = await _api.Get<List<CotizationsDetailsViewModel>>(req =>
                {
                    req.Path = $"/api/Cotizations/CotizationsDetails/{id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InsertCotizations(CotizationsModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<CotizationsModel>(req =>
                {
                    req.Path = $"/api/Cotizations/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InsertCotizationsDetail(List<CotizationsDetailsModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<List<CotizationsDetailsModel>>(req =>
                {
                    req.Path = $"/api/Cotizations/InsertDetails";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> EditCotizations(CotizationsModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<CotizationsModel>(req =>
                {
                    req.Path = "/api/Cotizations/Update";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> EditStock(int Id, ProductStockModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<ProductStockModel>(req =>
                {
                    req.Path = $"/api/Products/StockUpdate/{Id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> DeleteCotizations(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Delete<CotizationsModel>(req =>
                {
                    req.Path = $"/api/Cotizations/Delete/" + Id + "?Mod=" + Mod;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> DeleteCotizationsDetails(int Id, int IdCot, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Delete<CotizationsModel>(req =>
                {
                    req.Path = $"/api/Cotizations/DeleteDetail/" + IdCot + "?proId=" + Id + "&Mod=" + Mod;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        #endregion

        #region SalesOrder
        public async Task<ServiceResult> OrderList(List<SalesOrderViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<SalesOrderViewModel>>(req =>
                {
                    req.Path = $"/api/Orders/List";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InsertOrders(SaleOrdersModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<SaleOrdersModel>(req =>
                {
                    req.Path = $"/api/Orders/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InsertOrdersDetail(List<OrderDetailsModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<List<OrderDetailsModel>>(req =>
                {
                    req.Path = $"/api/Orders/InsertDetails";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception)
            {
                return result.Error();
            }
        }

        public async Task<ServiceResult> DeleteOrders(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Delete<SaleOrdersModel>(req =>
                {
                    req.Path = $"/api/Orders/Delete/" + Id + "?Mod=" + Mod;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> EditOrders(SaleOrdersModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<SaleOrdersModel>(req =>
                {
                    req.Path = "/api/Orders/Update";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> OrdersDetails(int id)
        {
            var result = new ServiceResult();
            var model = new List<SalesDetailsViewModel>();
            try
            {
                var response = await _api.Get<List<SalesDetailsViewModel>>(req =>
                {
                    req.Path = $"/api/Orders/SaleOrdersDetails/{id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        #endregion

        #region Campaign
        public async Task<ServiceResult> CampaignList(List<CampaignViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<CampaignViewModel>>(req =>
                {
                    req.Path = $"/api/Campaign/List";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> CampaignDetails(int id)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<CampaignViewModel>(req =>
                {
                    req.Path = $"/api/Campaign/DetailsCampaign/{id}";
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InsertCampaigns(CampaignModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<CampaignModel>(req =>
                {
                    req.Path = $"/api/Campaign/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> SendCampaign(CampaignDetailsModel model)
        {
            var result = new ServiceResult();
            try
            {
                var campaign = await _api.Get<CampaignViewModel>(req =>
                {
                    req.Path = $"/api/Campaign/DetailsCampaign/{model.cam_Id}";
                });

                if (campaign.Data != null)
                {

                    var response = await _api.Post<CampaignDetailsModel>(req =>
                    {
                        req.Path = $"/api/Campaign/SendCampaign";
                        req.Content = model;
                    });
                    response.Success = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok(response.StatusCode);
                }
                else
                    return result.Error(campaign.Message);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> DeleteCampaign(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var getDetails = await CampaignDetails(Id);
                if (!getDetails.Success && getDetails.Data == null)
                    return result.Error(getDetails.Message);
                var isSended = await _api.Get<List<CampaignDetailsViewModel>>(req =>
                {
                    req.Path = $"/api/Campaign/SendedCampaigns/{Id}";
                });

                if (isSended.Data.Count() == 0)
                {

                    var response = await _api.Delete<CampaignModel>(req =>
                    {
                        req.Path = $"/api/Campaign/DeleteCampaign/{Id}";
                    });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok(response.StatusCode);
                }
                else
                    return result.Error("No se puede eliminar ya que el registro se encuentra en uso.");
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> DeleteOrdersDetails(int Id, int proId, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Delete<SaleOrdersModel>(req =>
                {
                    req.Path = $"/api/Orders/DeleteDetail/" + Id + "?proId=" + proId + "&Mod=" + Mod;
                });

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.StatusCode);
            }
            catch (Exception)
            {
                return result.Error();
            }
        }
        #endregion
    }
}

