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
    public class ClientsService
    {
        private readonly CustomersRepository _customersRepository;
        private readonly ContactsRepository _contactsRepository;
        private readonly CustomerCallsRepository _customerCallsRepository;
        private readonly CustomersNotesRepository _customerNotesRepository;
        private readonly PrioritiesRepository _prioritiesRepository;
        private readonly CallTypeRepository _callTypeRepository;
        private readonly CallBusinessRepository _callBusinessRepository;
        private readonly CallResultRepository _callResultRepository;
        private readonly MeetingsRepository _meetingsRepository;
        private readonly CustomersFileRepository _customersFileRepository;
        private readonly MunicipalitiesRepository _municipalitiesRepository;
        private readonly DepartmentsRepository _departmentsRepository;

        public ClientsService(CustomersRepository customersRepository, ContactsRepository contactsRepository, CustomerCallsRepository customerCallsRepository, CustomersNotesRepository customersNotesRepository, PrioritiesRepository prioritiesRepository, CallTypeRepository callTypeRepository, CallBusinessRepository callBusinessRepository, CallResultRepository callResultRepository, MeetingsRepository meetingsRepository, CustomersFileRepository customersFileRepository,
            MunicipalitiesRepository municipalitiesRepository, DepartmentsRepository departmentsRepository)
        {
            _customersRepository = customersRepository;
            _contactsRepository = contactsRepository;
            _customerCallsRepository = customerCallsRepository;
            _customerNotesRepository = customersNotesRepository;
            _prioritiesRepository = prioritiesRepository;
            _callTypeRepository = callTypeRepository;
            _callBusinessRepository = callBusinessRepository;
            _callResultRepository = callResultRepository;
            _meetingsRepository = meetingsRepository;
            _customersFileRepository = customersFileRepository;
            _municipalitiesRepository = municipalitiesRepository;
            _departmentsRepository = departmentsRepository;
        }

        #region Customers
        public ServiceResult ListCustomers()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _customersRepository.List();

                return result.Ok(listado);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        public ServiceResult FindCustomers(Expression<Func<View_tbCustomers_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbCustomers_List();
            var errorMessage = "";
            try
            {
                resultado = _customersRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }
        public ServiceResult RegisterCustomers(tbCustomers item)
        {
            var result = new ServiceResult();
            try
            {
                var muni = _municipalitiesRepository.Find(x => x.mun_Id == item.mun_Id);
                if (muni == null)
                    return result.Error($"No existe el municipio seleccionado.");
                var dep = _departmentsRepository.Find(x => x.dep_Id == item.dep_Id);
                if (dep == null)
                    return result.Error($"No existe el departamento seleccionado.");
                var repeated = _customersRepository.Find(x => x.cus_RTN == item.cus_RTN);
                if (repeated != null && repeated.cus_Status == true)
                    return result.Conflict($"Un usuario con el RTN '{item.cus_RTN}' ya existe");
                var query = _customersRepository.Insert(item);
                EventLogger.UserId = item.cus_IdUserCreate;
                EventLogger.Create($"Creado Cliente '{item.cus_Name}'.", item);
                var ejemplo = new CustomersModel();
                ejemplo.cus_Id = query;
                if (query > 0)
                    return result.Ok(ejemplo);

                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult UpdateCustomers(int Id, tbCustomers item)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var muni = _municipalitiesRepository.Find(x => x.mun_Id == item.mun_Id);
                if (muni == null)
                    return result.Error($"No existe el municipio seleccionado.");
                var dep = _departmentsRepository.Find(x => x.dep_Id == item.dep_Id);
                if (dep == null)
                    return result.Error($"No existe el departamento seleccionado.");
                var usuario = _customersRepository.Find(x => x.cus_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var repeated = _customersRepository.Find(x => x.cus_RTN == item.cus_RTN);
                if (repeated != null && repeated.cus_Status == true && repeated.cus_Id!= Id)
                    return result.Conflict($"Un usuario con el RTN '{item.cus_RTN}' ya existe");
               
                var query = _customersRepository.Update(IdUser, item);
                EventLogger.UserId = item.cus_IdUserModified;
                EventLogger.Update($"Actualizado Cliente '{item.cus_Name}'.", usuario, item);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteCustomers(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _customersRepository.Find(x => x.cus_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _customersRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminado Cliente '{usuario.cus_Name}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region Contacts
        public ServiceResult ListContacts()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _contactsRepository.List();

                return result.Ok(listado);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        public ServiceResult FindContacts(Expression<Func<View_tbContacts_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbContacts_List();
            var errorMessage = "";
            try
            {
                resultado = _contactsRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }
        public ServiceResult RegisterContacts(tbContacts item)
        {
            var result = new ServiceResult();
            var entidad = new ContactsModel();
            try
            {
                var query = _contactsRepository.Insert(item);
                EventLogger.UserId = item.cont_IdUserCreate;
                EventLogger.Create($"Creado Contacto '{item.cont_Name}'.", item);
                if (query > 0)
                {
                    entidad.cont_Id = query;
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

        public ServiceResult UpdateContacts(int Id, tbContacts item)
        {
            var result = new ServiceResult();
            var entidad = new ContactsModel();
            try
            {
                var IdUser = Id;
                var usuario = _contactsRepository.Find(x => x.cont_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _contactsRepository.Update(IdUser, item);
                EventLogger.UserId = item.cont_IdUserModified;
                EventLogger.Update($"Actualizado Contacto '{item.cont_Name}'.", usuario, item);
                entidad.cont_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteContacts(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _contactsRepository.Find(x => x.cont_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _contactsRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminado Contacto '{usuario.cont_Name}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region tbCustomerCalls
        public ServiceResult ListCustomersCalls()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _customerCallsRepository.List();

                return result.Ok(listado);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        public ServiceResult FindCustomersCalls(Expression<Func<View_tbCustomerCalls, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbCustomerCalls();
            var errorMessage = "";
            try
            {
                resultado = _customerCallsRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }
        public ServiceResult RegisterCustomersCalls(tbCustomerCalls item)
        {
            var result = new ServiceResult();
            var entidad = new CustomerCallsModel();
            try
            {
                var query = _customerCallsRepository.Insert(item);
                EventLogger.UserId = item.cca_IdUserCreate;
                EventLogger.Create($"Creada Llamada de Cliente con ID: '{item.cca_Id}'.", item);
                if (query > 0)
                {
                    entidad.cca_Id = query;
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

        public ServiceResult UpdateCustomersCalls(int Id, tbCustomerCalls item)
        {
            var result = new ServiceResult();
            var entidad = new CustomerCallsModel();
            try
            {
                var call = _customerCallsRepository.Find(x => x.cca_Id == Id);
                if (call == null)
                    return result.Error($"No existe el registro");
                var query = _customerCallsRepository.Update(Id, item);
                EventLogger.UserId = item.cca_IdUserModified;
                EventLogger.Update($"Actualizada Llamada de Cliente con ID: '{item.cca_Id}'.", call, item);
                entidad.cca_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteCustomersCalls(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _customerCallsRepository.Find(x => x.cca_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _customerCallsRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminada Llamada de Cliente con ID: '{usuario.cca_Id}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region tbCustomerNotes
        public ServiceResult ListCustomersNotes()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _customerNotesRepository.List();

                return result.Ok(listado);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        public ServiceResult FindCustomersNotes(Expression<Func<View_tbCustomerNotes, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbCustomerNotes();
            var errorMessage = "";
            try
            {
                resultado = _customerNotesRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }
        public ServiceResult RegisterCustomersNotes(tbCustomerNotes item)
        {
            var result = new ServiceResult();
            try
            {
                var query = _customerNotesRepository.Insert(item);
                EventLogger.UserId = item.cun_IdUserCreate;
                EventLogger.Create($"Creada Nota de Cliente '{item.cun_Descripcion}'.", item);
                var entidad = new CustomerNotesModel();
                entidad.cun_Id = query;
                if (query > 0)
                    return result.Ok(entidad);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult UpdateCustomersNotes(int Id, tbCustomerNotes item)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var usuario = _customerNotesRepository.Find(x => x.cun_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _customerNotesRepository.Update(IdUser, item);
                EventLogger.UserId = item.cun_IdUserModified;
                EventLogger.Update($"Actualizada Nota de Cliente '{item.cun_Descripcion}'.", usuario, item);
                var entidad = new CustomerNotesModel();
                entidad.cun_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteCustomersNotes(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _customerNotesRepository.Find(x => x.cun_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _customerNotesRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminada Nota de Cliente '{usuario.cun_Descripcion}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region tbPriorities
        public ServiceResult ListPriorities()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _prioritiesRepository.List();

                return result.Ok(listado);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region tbCallType
        public ServiceResult ListCallType()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _callTypeRepository.List();

                return result.Ok(listado);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region tbCallBusiness
        public ServiceResult ListCallBusiness()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _callBusinessRepository.List();

                return result.Ok(listado);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region tbCallResult
        public ServiceResult ListCallResult()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _callResultRepository.List();

                return result.Ok(listado);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region tbMeetings
        public ServiceResult RegisterMeetings(tbMeetings item, List<tbMeetingsDetails> data)
        {
            var result = new ServiceResult();
            try
            {       
                string query = _meetingsRepository.Insert(item, data);
                EventLogger.UserId = item.met_IdUserCreate;
                EventLogger.Create($"Creada Reunion '{item.met_Title}'.", item);
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

        public ServiceResult RegisterMeetingsDetails(tbMeetingsDetails data)
        {
            var result = new ServiceResult();
            try
            {

                string query = _meetingsRepository.InsertDetails(data);
                EventLogger.UserId = data.mde_IdUserCreate;
                EventLogger.Create($"Creada Detalle de Reunion con ID: '{data.mde_Id}'.", data);
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
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }

        }
        public ServiceResult ListMeetingDetails()
        {
            var result = new ServiceResult();
            try
            {
                var query = _meetingsRepository.ListMeetingDetails();
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
                Log.Logger.Error(Utilities.GetMessage(ex));
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
                EventLogger.UserId = item.met_IdUserModified;
                EventLogger.Update($"Actualizada Reunion '{item.met_Title}'.", meeting, item);
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

        public ServiceResult DeleteMeetings(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var cotizations = _meetingsRepository.Find(x => x.met_Id == Id);
                if (cotizations == null)
                    return result.Error($"No existe el registro");
                var query = _meetingsRepository.Delete(Id, Mod);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminada Reunion '{cotizations.met_Title}'.", cotizations);
                return result.Ok(query);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
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
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminada Detalle de Reunion con ID: '{cotizations.mde_Id}'.", cotizations);
                return result.Ok(query);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
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
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }
        #endregion

        #region tbCustomersFile
        public ServiceResult ListCustomersFile()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _customersFileRepository.List();

                return result.Ok(listado);

            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }
        public ServiceResult FindCustomersFile(Expression<Func<View_tbCustomersFile_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbCustomersFile_List();
            var errorMessage = "";
            try
            {
                resultado = _customersFileRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterCustomersFile(tbCustomersFile item)
        {
            var result = new ServiceResult();
            var entidad = new CustomerFilesModel();
            try
            {
                var query = _customersFileRepository.Insert(item);
                EventLogger.UserId = item.cfi_IdUserCreate;
                EventLogger.Create($"Creado Archivo de Cliente con ID: '{item.cfi_Id}'.", item);
                entidad.cfi_Id = query;
                if (query > 0)
                    return result.Ok(entidad);

                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }

        
        public ServiceResult DeleteCustomersFile(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var file = _customersFileRepository.Find(x => x.cfi_Id == Id);
                if (file == null)
                    return result.Error($"No existe el registro");
                var query = _customersFileRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminado Archivo de Cliente con ID: '{file.cfi_Id}'.", file);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(Utilities.GetMessage(ex));
            }
        }
        #endregion
    }
}

    