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
    public class CotizationsServices
    {
        public CotizationsRepositoryTest _cotizationsRepository = new CotizationsRepositoryTest();

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

    }
}
