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
    public class CampaignService
    {
        CampaignRepositoryTest _campaignRepository = new CampaignRepositoryTest();
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
    }
}
