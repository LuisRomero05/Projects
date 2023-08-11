using AHM_LOGISTIC_SMART_ADM.Models;
using AHM_LOGISTIC_SMART_ADM.WebApi;
using AHM.Logistic.Smart.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Services
{
    public class CustomersService
    {
        private readonly Api _api;

        public CustomersService(Api api)
        {
            _api = api;
        }


        #region tbCustomers
        public async Task<ServiceResult> CustomersList(List<CustomerViewModel> model)
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Get<List<CustomerViewModel>>(req =>
                {
                    req.Path = $"/api/Customers/List";
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

        public async Task<ServiceResult> CustomersDetails(int id)
        {
            var result = new ServiceResult();
            var model = new CustomerViewModel();
            try
            {
                var response = await _api.Get<CustomerViewModel>(req =>
                {
                    req.Path = $"/api/Customers/Details/{id}";
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


        public async Task<ServiceResult> InsertCustomers(CustomersModel model)
        {
            var result = new ServiceResult();
            try
            {
                if (model.tyCh_Id != 1 && model.tyCh_Id != 2 && model.tyCh_Id != 3 && model.tyCh_Id != 4)
                    return result.Error(ServiceProblemMessage.NotFound);
                var response = await _api.Post<CustomersModel>(req =>
                {
                    req.Path = $"/api/Customers/Insert";
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

        public async Task<ServiceResult> EditCustomers(CustomerViewModel model)
        {
            var result = new ServiceResult();
            var Object = new CustomersModel()
            {
                cus_AssignedUser = model.cus_AssignedUser,
                tyCh_Id = model.tyCh_Id,
                cus_Name = model.cus_Name,
                cus_RTN = model.cus_RTN,
                cus_Address = model.cus_Address,
                dep_Id = model.dep_Id,
                mun_Id = model.mun_Id,
                cus_Email = model.cus_Email,
                cus_Phone = model.cus_Phone,
                cus_AnotherPhone = model.cus_AnotherPhone,
                cus_IdUserCreate = model.cus_IdUserCreate,
                cus_Active = Convert.ToBoolean( model.Estado)
            };
            try
            {
                if (model.tyCh_Id != 1 && model.tyCh_Id != 2 && model.tyCh_Id != 3 && model.tyCh_Id != 4)
                    return result.Error(ServiceProblemMessage.NotFound);

                var response = await _api.Put<CustomersModel>(req =>
                {
                    req.Path = $"/api/Customers/Update/{model.cus_Id}";
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

        public async Task<ServiceResult> DeleteCustomers(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                string msjError = string.Empty;
                //Traer datos de las tablas relacionadas
                var notas = await _api.Get<List<CustomerNotesModel>>(req =>//Notas
                {
                    req.Path = $"/api/CustomerNotes/List";
                });
                //var meets = await _api.Get<List<MeetingsViewModel>>(req =>//Reuniones***
                //{
                //    req.Path = $"/api/Meetings/List";
                //});
                var files = await _api.Get<List<CustomerFilesModel>>(req =>//adjuntos
                {
                    req.Path = $"/api/CustomerFiles/List";
                });
                var calls = await _api.Get<List<CustomerCallsModel>>(req =>//llamadas
                {
                    req.Path = $"/api/CustomerCalls/List";
                });
                var cotiza = await _api.Get<List<CotizationsViewModel>>(req =>//cotizaciones
                {
                    req.Path = $"/api/Cotizations/List";
                });
                var sorder = await _api.Get<List<SalesOrderViewModel>>(req =>//orden de venta
                {
                    req.Path = $"/api/Orders/List";
                });

                //campaña pendiente

                //buscar en esas tablas los registros que se relacionan con el cliente que se eliminara
                var listnotas = notas.Data.Where(w => w.cus_Id == Id);//notas*
                //var listmeets = meets.Data.Where(w => w.cus_Id == Id);//reuniones*
                var listfiles = files.Data.Where(w => w.cus_Id == Id);//adjuntos*
                var listcalls = calls.Data.Where(w => w.cus_Id == Id);//llamadas
                var listcotiza = cotiza.Data.Where(w => w.cus_Id == Id);//cotizacion
                var listsorder = sorder.Data.Where(w => w.cus_Id == Id);//orden de centa

                //Si no se encuentra un registro en alguna de las tablas relacionadas se procedera a eliminar el cliente
                if (
                    listnotas.Count() == 0 &&
                    //listmeets.Count() == 0 && 
                    listfiles.Count() == 0 &&
                     listcalls.Count() == 0  &&
                    listcotiza.Count() == 0  &&
                    listsorder.Count() == 0
                    )
                {
                    var response = await _api.Delete<CustomersModel>(req =>
                    {
                        req.Path = $"/api/Customers/Delete/" + Id + "?Mod=" + Mod;
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

        #region tbContacts
        public async Task<ServiceResult> ApiContactsList(List<ContactsViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<ContactsViewModel>>(req =>
                {
                    req.Path = $"/api/Contacts/List";
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

        public async Task<ServiceResult> ContactsDetails(int id)
        {
            var result = new ServiceResult();
            var model = new ContactsViewModel();
            try
            {
                var response = await _api.Get<ContactsViewModel>(req =>
                {
                    req.Path = $"/api/Contacts/Details/{id}";
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

        public async Task<ServiceResult> InsertContacts(ContactsModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<ContactsModel>(req =>
                {
                    req.Path = $"/api/Contacts/Insert";
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

        public async Task<ServiceResult> DeleteContacts(int Id, int Mod)
        {
            var result = new ServiceResult();
            List<MeetingsViewModel> meetingsDetails = new List<MeetingsViewModel>();
            bool hasDepend = false;
            try
            {
                var orders = await _api.Get<List<MeetingsViewModel>>(req =>
                {
                    req.Path = $"/api/Meetings/List";
                    req.Content = meetingsDetails;
                });

                foreach (var item in orders.Data)
                {
                    var details = await _api.Get<List<MeetingsDetailsViewModel>>(req =>
                    {
                        req.Path = $"/api/Meetings/MeetingsDetails/{item.met_Id}";
                    });
                    ;
                    if (details.Data.Where(x => x.cont_Id == Id).Count() != 0)
                    {
                        hasDepend = true;
                        break;
                    }
                }
                if (!hasDepend)
                {
                    var response = await _api.Delete<ContactsModel>(req =>
                    {
                        req.Path = $"/api/Contacts/Delete/" + Id + "?Mod=" + Mod;
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






























            //var result = new ServiceResult();
            //List<MeetingsDetailsViewModel> emp = new List<MeetingsDetailsViewModel>();
            //try
            //{
            //    var getRepeated = await MeetingsDetails(emp);
            //    emp = (List<CustomerViewModel>)getRepeated.Data;
            //    if (emp.Where(x => x.cont_Id == Id).Count() == 0)
            //    {
            //        var response = await _api.Delete<ContactsModel>(req =>
            //        {
            //            req.Path = $"/api/Contacts/Delete/" + Id + "?Mod=" + Mod;
            //        });

            //        if (!response.Success)
            //            return result.FromApi(response);

            //        return result.Ok(response.StatusCode);
            //    }
            //    else
            //        return result.Error("No se puede eliminar ya que el registro se encuentra en uso.");

            //}
            //catch (Exception ex)
            //{
            //    return result.Error(Helpers.GetMessage(ex));
            //}






















            //var result = new ServiceResult();
            //try
            //{
            //    var response = await _api.Delete<ContactsModel>(req =>
            //    {
            //        req.Path = $"/api/Contacts/Delete/" + Id + "?Mod=" + Mod;
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

        public async Task<ServiceResult> EditContacts(ContactsViewModel model)
        {
            var result = new ServiceResult();
            var Object = new ContactsModel()
            {
                cont_Name = model.cont_Name,
                cont_LastName = model.cont_LastName,
                cont_Email = model.cont_Email,
                cont_Phone = model.cont_Phone,
                occ_Id = model.occ_Id,
                cus_Id = model.cus_Id,
                cont_IdUserCreate = model.cont_IdUserCreate,
                cont_IdUserModified = model.cont_IdUserModified
            };
            try
            {
                var response = await _api.Put<ContactsModel>(req =>
                {
                    req.Path = $"/api/Contacts/Update/{model.cont_Id}";
                    req.Content = Object;
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

















        //public async Task<ServiceResult> EditContacts(ContactsModel model, int id)
        //{
        //    var result = new ServiceResult();
        //    try
        //    {
        //        var response = await _api.Put<ContactsModel>(req =>
        //        {
        //            req.Path = $"/api/Contacts/Update/{id}";
        //            req.Content = model;
        //        });

        //        if (!response.Success)
        //            return result.FromApi(response);

        //        return result.Ok(response.StatusCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        return result.Error(Helpers.GetMessage(ex));
        //    }
        //}
        #endregion

        #region tbCustomerNotes

        public async Task<ServiceResult> InsertCustomerNotes(CustomerNotesModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<CustomerNotesModel>(req =>
                {
                    req.Path = $"/api/CustomerNotes/Insert";
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

        public async Task<ServiceResult> CustomerNotesList()
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Get<List<CustomerNotesModel>>(req =>
                {
                    req.Path = $"/api/CustomerNotes/List";
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

        public async Task<ServiceResult> DeleteCustomerNotes(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Delete<CustomerNotesModel>(req =>
                {
                    req.Path = $"/api/CustomerNotes/Delete/" + Id + "?Mod=" + Mod;
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

        public async Task<ServiceResult> CustomerNotesDetails(int id)
        {
            var result = new ServiceResult();
            var model = new CustomerNotesModel();
            try
            {
                var response = await _api.Get<CustomerNotesModel>(req =>
                {
                    req.Path = $"/api/CustomerNotes/Details/{id}";
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

        public async Task<ServiceResult> EditCustomerNotes(CustomerNotesModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Put<CustomerNotesModel>(req =>
                {
                    req.Path = $"/api/CustomerNotes/Update/{model.cun_Id}";
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

        #region tbPriorities
        public async Task<ServiceResult> PrioritiesList()
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Get<List<PrioritiesModel>>(req =>
                {
                    req.Path = $"/api/Priorities/List";
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

        #region tbCustomerCalls

        public async Task<ServiceResult> InsertCustomerCalls(CustomerCallsModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<CustomerCallsModel>(req =>
                {
                    req.Path = $"/api/CustomerCalls/Insert";
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

        public async Task<ServiceResult> CustomerCallsList()
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Get<List<CustomerCallsModel>>(req =>
                {
                    req.Path = $"/api/CustomerCalls/List";
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

        public async Task<ServiceResult> DeleteCustomerCalls(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Delete<CustomerCallsModel>(req =>
                {
                    req.Path = $"/api/CustomerCalls/Delete/" + Id + "?Mod=" + Mod;
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

        public async Task<ServiceResult> CustomerCallsDetails(int id)
        {
            var result = new ServiceResult();
            var model = new CustomerCallsModel();
            try
            {
                var response = await _api.Get<CustomerCallsModel>(req =>
                {
                    req.Path = $"/api/CustomerCalls/Details/{id}";
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

        public async Task<ServiceResult> EditCustomerCalls(CustomerCallsModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Put<CustomerCallsModel>(req =>
                {
                    req.Path = $"/api/CustomerCalls/Update/{model.cca_Id}";
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

        #region tbCallType
        public async Task<ServiceResult> CallTypeList()
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Get<List<CallTypeModel>>(req =>
                {
                    req.Path = $"/api/CallType/List";
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

        #region tbCallBusiness

        public async Task<ServiceResult> CallBusinessList()
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Get<List<CallBusinessModel>>(req =>
                {
                    req.Path = $"/api/CallBusiness/List";
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

        #region tbCallResult

        public async Task<ServiceResult> CallResultList()
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Get<List<CallResultModel>>(req =>
                {
                    req.Path = $"/api/CallResult/List";
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

        #region tbCustomerFile

        public async Task<ServiceResult> InsertCustomerFile(CustomerFilesModel model)
        {
            var result = new ServiceResult();
            var Object = new CustomersFileViewModel()
            {
                cfi_Id = model.cfi_Id,
                cfi_fileRoute = model.cfi_fileRoute,
                cus_Id = model.cus_Id,
                cfi_IdUserCreate = model.cfi_IdUserCreate,
                cfi_DateCreate = model.cfi_DateCreate,
                cfi_IdUserModified = model.cfi_IdUserModified
            };
            try
            {
                var response = await _api.Post<CustomersFileViewModel>(req =>
                {
                    req.Path = $"/api/CustomerFiles/Insert";
                    req.Content = Object;
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

        public async Task<ServiceResult> CustomerFileList()
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Get<List<CustomerFilesModel>>(req =>
                {
                    req.Path = $"/api/CustomerFiles/List";
                });
                //evaluamos si response trae datos
                if (response.Data != null)
                {
                    //recorremos los datos de response
                    foreach (var item in response.Data)
                    {
                        //capturamos la cadena de la ruta para hacer las particiones
                        string prueba = item.cfi_fileRoute;
                        string[] hrf = item.cfi_fileRoute.Split("wwroot");
                        for (int h = 0; h < hrf.Length; h++) if (h != 0) item.Nombre = hrf[h];
                        item.cfi_fileRoute = null;
                        {
                            //partimos la cadena por guiones
                            string[] fileRoute = prueba.Split("-");

                            //recorremos la cadena partida
                            for (int i = 0; i < fileRoute.Length; i++)
                            {
                                //en la primera vuelta la cadena trae la ruta del proyecto por eso omitimos la primera vuelta
                                //si el for ya dio la primera vuelta y a la cadena aun le quedan mas posiciones entonces quiere decir que el nombre del archivo contiene guiones
                                if (i != 0 && fileRoute.Length > 2)
                                {
                                    //si la cadena aun no esta en la ultima posicion entonces le agregamos un guion
                                    if (i < fileRoute.Length - 1)
                                    {
                                        //agregamos el guion
                                        item.cfi_fileRoute += fileRoute[i] + "-";
                                    }
                                    else
                                    {
                                        //si la cadena esta en su ultima posicion no le agregamos guion
                                        item.cfi_fileRoute += fileRoute[i];
                                    }
                                }
                                else if (i != 0)
                                {
                                    //si entra aca quiere decir que el nombre del archivo no tiene guiones
                                    item.cfi_fileRoute = fileRoute[i];
                                }
                            }
                        }
                    }
                }

                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> DeleteCustomerFile(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Delete<CustomerFilesModel>(req =>
                {
                    req.Path = $"/api/CustomerFiles/Delete/" + Id + "?Mod=" + Mod;
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

        public async Task<ServiceResult> CustomerFileDetails(int id)
        {
            var result = new ServiceResult();
            var model = new CustomerFilesModel();
            try
            {
                var response = await _api.Get<CustomerFilesModel>(req =>
                {
                    req.Path = $"/api/CustomerFiles/Details/{id}";
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

        #region tbMeetings

        public async Task<ServiceResult> InsertMeeting(MeetingsModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<MeetingsModel>(req =>
                {
                    req.Path = $"/api/Meetings/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);
                //errores
                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro creado exitosamente.", response.Data);
                //return result.Ok(response.Data);//resusltado desde la api
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> EditMeeting(MeetingsModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Put<MeetingsModel>(req =>
                {
                    req.Path = $"/api/Meetings/Update/{model.met_Id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);
                //errores
                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro actualizado exitosamente.", response.StatusCode);
                //return result.Ok(response.Data);//resusltado desde la api
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }


        public async Task<ServiceResult> MeetingsList()
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Get<List<MeetingsViewModel>>(req =>
                {
                    req.Path = $"/api/Meetings/List";
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

        public async Task<ServiceResult> MeetingsList(int id)
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Get<List<MeetingsViewModel>>(req =>
                {
                    req.Path = $"/api/Meetings/List";
                });
                var Object = new MeetingsViewModel() { };
                foreach (var item in response.Data)
                {
                    if (item.met_Id == id)
                    {
                        Object = new MeetingsViewModel()
                        {
                            met_Id = item.met_Id,
                            met_Title = item.met_Title,
                            cus_Id = item.cus_Id,
                            cus_Name = item.cus_Name,
                            met_MeetingLink = item.met_MeetingLink,
                            met_Date = item.met_Date,
                            met_StartTime = item.met_StartTime,
                            met_EndTime = item.met_EndTime,
                            met_IdUserCreate = item.met_IdUserCreate,
                        };
                    }
                }
                var response_details = await _api.Get<List<MeetingsDetailUpdateModel>>(req =>
                {
                    req.Path = $"/api/Meetings/MeetingsDetails/{id}";
                });

                var response_customers = await _api.Get<List<CustomerViewModel>>(req =>
                {
                    req.Path = $"/api/Customers/List";
                });

                var response_employees = await _api.Get<List<EmployeesViewModel>>(req =>
                {
                    req.Path = $"/api/Employees/List";
                });

                var response_contacts = await _api.Get<List<ContactsViewModel>>(req =>
                {
                    req.Path = $"/api/Contacts/List";
                });

                var response_ListCusEmpCon = await _api.Get<List<CustomersEmployeesViewModel>>(req =>
               {
                   req.Path = $"/api/Meetings/ListCusEmp";
               });
                if (Object != null)
                {
                    Object.ListCusEmpCon = response_ListCusEmpCon.Data;

                    foreach (var detail in response_details.Data)
                    {
                        foreach (var cus in response_customers.Data)
                        {
                            if (detail.cus_Id == cus.cus_Id)
                            {
                                detail.name = cus.cus_Name + " - Cliente";
                                detail.id = cus.cus_Id.ToString() + ".0";
                            }
                        }
                        foreach (var emp in response_employees.Data)
                        {
                            if (detail.emp_Id == emp.emp_Id)
                            {
                                detail.name = emp.per_Firstname + " " + emp.per_LastNames + " - " + emp.occ_Description;
                                detail.id = emp.emp_Id.ToString() + ".1";
                            }
                        }
                        foreach (var cont in response_contacts.Data)
                        {
                            if (detail.cont_Id == cont.cont_Id)
                            {
                                detail.name = cont.cont_Name + " - Contacto";
                                detail.id = cont.cont_Id.ToString() + ".2";
                            }
                        }
                    }

                    Object.met_details = response_details.Data;

                }
                if (!response.Success)
                    return result.FromApi(response);

                return result.Ok(Object);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> ListCusEmp()
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Get<List<CustomersEmployeesViewModel>>(req =>
                 {
                     req.Path = $"/api/Meetings/ListCusEmp";
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
        public async Task<ServiceResult> MeetingsDetails(int id)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<MeetingsDetailsViewModel>>(req =>
                {
                    req.Path = $"/api/Meetings/MeetingsDetails/{id}";
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

        public async Task<ServiceResult> DeleteMeetings(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Delete<MeetingsModel>(req =>
                {
                    req.Path = $"/api/Meetings/Delete/" + Id + "?Mod=" + Mod;
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

        public async Task<ServiceResult> DeleteMeetingsDetail(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Delete<MeetingsDetailsModel>(req =>
                {
                    req.Path = $"/api/Meetings/DeleteDetail/" + Id + " ? Mod = " + Mod;
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

        public async Task<ServiceResult> InsertMeetingDetails(MeetingsDetailUpdateModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<MeetingsDetailUpdateModel>(req =>
                {
                    req.Path = $"/api/Meetings/InsertDetails";
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

    }
}




