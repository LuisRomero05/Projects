using AHM.Logistic.Smart.BusinessLogic.Logger;
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
    public class OrdersSalesServices
    {
        public OrdersRepositoryTest _ordersRepository = new OrdersRepositoryTest();

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
    }
}
