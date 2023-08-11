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
    public class MeetingsService
    {
        MeetingsRepositoryTest _meetingsRepository = new MeetingsRepositoryTest();
        public ServiceResult RegisterMeetings(tbMeetings item, List<tbMeetingsDetails> data)
        {
            var result = new ServiceResult();
            try
            {
                string query = _meetingsRepository.Insert(item, data);
                if (query == "success")
                    return result.Ok(query);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult RegisterMeetingsDetails(tbMeetingsDetails data)
        {
            var result = new ServiceResult();
            try
            {

                string query = _meetingsRepository.InsertDetails(data);
                if (query == "success")
                    return result.Ok(query);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult ListMeeting()
        {
            var result = new ServiceResult();
            try
            {
                var query = _meetingsRepository.List();
                if (query.Any())
                    return result.Ok(query);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                return result.Error(Utilities.GetMessage(ex));
            }

        }

        public ServiceResult ListInvEmp()
        {
            var result = new ServiceResult();
            try
            {
                var query = _meetingsRepository.ListInvitados();
                if (query.Any())
                    return result.Ok(query);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                return result.Error(Utilities.GetMessage(ex));
            }

        }

        public ServiceResult UpdateMeetings(tbMeetings item, List<tbMeetingsDetails> data, int Id)
        {
            var result = new ServiceResult();
            try
            {
                var meeting = _meetingsRepository.Find(x => x.met_Id == Id);
                if (meeting == null)
                    return result.Error($"No existe el registro");
                item.met_Id = Id;
                item.met_Status = meeting.met_Status;
                item.met_IdUserCreate = meeting.met_IdUserCreate;
                item.met_DateCreate = meeting.met_DateCreate;
                foreach (var item2 in data)
                {
                    item2.mde_Status = meeting.met_Status;
                    item2.mde_IdUserCreate = meeting.met_IdUserCreate;
                    item2.mde_DateCreate = meeting.met_DateCreate;
                }
                string query = _meetingsRepository.Update(item, data, Id);
                if (query == "success")
                    return result.Ok(query);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult DeleteMeetings(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var cotizations = _meetingsRepository.Find(x => x.met_Id == Id);
                if (cotizations == null)
                    return result.Error($"No existe el registro");
                var query = _meetingsRepository.Delete(Id, Mod);
                return result.Ok(query);

            }
            catch (Exception ex)
            {
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult DeleteDetail(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var cotizations = _meetingsRepository.FindDetails(x => x.mde_Id == Id);
                if (cotizations == null)
                    return result.Error($"No existe el registro");
                var query = _meetingsRepository.DeleteDetail(Id, Mod);
                return result.Ok(query);

            }
            catch (Exception ex)
            {
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        public ServiceResult DetailsMeetings(Expression<Func<tbMeetingsDetails, bool>> expression = null)
        {
            var result = new ServiceResult();

            List<tbMeetingsDetails> resultado = new List<tbMeetingsDetails>();
            var errorMessage = "";
            try
            {
                resultado = _meetingsRepository.Details(expression);
                foreach (var item in resultado)
                {
                    if (item.emp_Id == null) item.emp_Id = 0;

                    if (item.cus_Id == null) item.cus_Id = 0;

                    if (item.cont_Id == null) item.cont_Id = 0;

                    if (item.mde_IdUserModified == null) item.mde_IdUserModified = 0;
                }
                return result.Ok(resultado);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
            
        }
    }
}
