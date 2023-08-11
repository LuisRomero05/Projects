using AHM.Logistic.Smart.BusinessLogic.Logger;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.DataAccess.Repositories;
using AHM.Logistic.Smart.Entities.Entities;
using AutoMapper;
using Serilog;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AHM.Logistic.Smart.BusinessLogic.Services
{
    public class CatalogService
    {
        private readonly IMapper _mapper;
        private readonly CountryRepository _countryRepository;
        private readonly DepartmentsRepository _departmentsRepository;
        private readonly MunicipalitiesRepository _municipalitiesRepository;
        private readonly PersonsRepository _personsRepository;
        private readonly AreasRepository _areasRepository;
        private readonly EmployeesRepository _employeesRepository;
        private readonly OccupationsRepository _occupationsRepository;
        private readonly DashboardRepository _dashboardRepository;
        public CatalogService(
                                DepartmentsRepository departmentsRepository,
                                MunicipalitiesRepository municipalitiesRepository,
                                PersonsRepository personsRepository,
                                AreasRepository areasRepository,
                                EmployeesRepository employeesRepository,
                                OccupationsRepository occupationsRepository,
                                CountryRepository countryRepository,
                                DashboardRepository dashboardRepository,
                                IMapper mapper)
        {
            _departmentsRepository = departmentsRepository;
            _municipalitiesRepository = municipalitiesRepository;
            _personsRepository = personsRepository;
            _areasRepository = areasRepository;
            _employeesRepository = employeesRepository;
            _occupationsRepository = occupationsRepository;
            _countryRepository = countryRepository;
            _dashboardRepository = dashboardRepository;
            _mapper = mapper;
        }

        #region tbPersons
        public ServiceResult ListPersons()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _personsRepository.List();
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


        public ServiceResult FindPersons(Expression<Func<View_tbPersons_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbPersons_List();
            var errorMessage = "";
            try
            {
                resultado = _personsRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterPersons(tbPersons item)
        {
            var result = new ServiceResult();
            var entidad = new PersonsModel();
            try
            {
                var muni = _municipalitiesRepository.Find(x => x.mun_Id == item.mun_Id);
                if (muni == null)
                    return result.Error($"No existe el municipio seleccionado");
                var muniDepId = _municipalitiesRepository.Find(s => s.mun_Id == item.mun_Id);
                var usuario = _departmentsRepository.Find(x => x.dep_Id ==item.dep_Id && x.dep_Id == muniDepId.dep_Id);
                if (usuario == null)
                    return result.Error($"No se podra realizar el registro, el municipio no esta ligado a ese departamento");
                var repeated = _personsRepository.Find(x => x.per_Identidad == item.per_Identidad);
                if (repeated != null && repeated.per_Status == true)
                    return result.Conflict($"Una persona con la identidad '{item.per_Identidad}' ya existe");
                var query = _personsRepository.Insert(item);
                EventLogger.UserId = item.per_IdUserCreate;
                EventLogger.Create($"Creada Persona '{item.per_Firstname}'.", item);
                if (query > 0)
                {
                    entidad.per_Id = query;
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


        public ServiceResult UpdatePersons(int Id, tbPersons item)
        {
            var result = new ServiceResult();
            var entidad = new PersonsModel();
            try
            {
                var IdUser = Id;
                var muni = _municipalitiesRepository.Find(x => x.mun_Id == item.mun_Id);
                if (muni == null)
                    return result.Error($"No existe el municipio seleccionado.");
                var dep = _departmentsRepository.Find(x => x.dep_Id == item.dep_Id);
                if (dep == null)
                    return result.Error($"No existe el departamento seleccionado.");
                var usuario = _personsRepository.Find(x => x.per_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro.");
                var repeated = _personsRepository.Find(x => x.per_Identidad == item.per_Identidad);
                if (repeated != null && repeated.per_Status == true && repeated.per_Id != Id) 
                    return result.Conflict($"Una persona con la identidad '{repeated.per_Identidad}' ya existe.");
                var muniDepId = _municipalitiesRepository.Find(s => s.mun_Id == item.mun_Id);
                var validacion = _departmentsRepository.Find(x => x.dep_Id == item.dep_Id && x.dep_Id == muniDepId.dep_Id);
                if (validacion == null)
                    return result.Error($"No se podra realizar el registro, el municipio no esta ligado a ese departamento.");               
                var query = _personsRepository.Update(IdUser, item);
                EventLogger.UserId = item.per_IdUserModified;
                EventLogger.Update($"Actualizada Persona '{item.per_Firstname}'.", usuario, item);
                entidad.per_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeletePersons(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _personsRepository.Find(x => x.per_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _personsRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminada Persona '{usuario.per_Firstname}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        #endregion

        #region tbCountry
        public ServiceResult ListCountries()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _countryRepository.List();
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

        public ServiceResult FindCountries(Expression<Func<View_tbCountries_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbCountries_List();
            var errorMessage = "";
            try
            {
                resultado = _countryRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterCountries(tbCountries item)
        {
            var result = new ServiceResult();
            var entidad = new CountryModel();
            try
            {
                var repeated = _countryRepository.Find(x => x.cou_Description.ToLower() == item.cou_Description.ToLower());
                if (repeated != null && repeated.cou_Status == true)
                    return result.Conflict($"Un pais con el nombre '{item.cou_Description}' ya existe");
                var query = _countryRepository.Insert(item);
                EventLogger.UserId = item.cou_IdUserCreate;
                EventLogger.Create($"Creado Pais '{item.cou_Description}'.", item);
                if (query > 0)
                {
                    entidad.cou_Id = query;
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


        public ServiceResult UpdateCountries(int Id, tbCountries item)
        {
            var result = new ServiceResult();
            var entidad = new CountryModel();
            try
            {
                var IdUser = Id;
                var usuario = _countryRepository.Find(x => x.cou_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var repeated = _countryRepository.Find(x => x.cou_Description.ToLower() == item.cou_Description.ToLower());
                if (repeated != null && repeated.cou_Status == true)
                    return result.Conflict($"Un pais con el nombre '{item.cou_Description}' ya existe");
                var query = _countryRepository.Update(IdUser, item);
                EventLogger.UserId = item.cou_IdUserModified;
                EventLogger.Update($"Actualizado Pais '{item.cou_Description}'.", usuario, item);
                entidad.cou_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteCountries(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _countryRepository.Find(x => x.cou_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _countryRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminado Pais '{usuario.cou_Description}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region Departments
        public ServiceResult ListDepartments()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _departmentsRepository.List();
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

        public ServiceResult FindDepartments(Expression<Func<View_tbDepartments_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbDepartments_List();
            var errorMessage = "";
            try
            {
                resultado = _departmentsRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterDepartment(tbDepartments item)
        {
            var result = new ServiceResult();
            var entidad = new DepartmentsModel();
            try
            {
                var country = _countryRepository.Find(x => x.cou_Id == item.cou_Id && x.cou_Status==true);
                if(country==null)
                {
                    return result.Error($"El pais no existe");
                }
                var repeatedcod = _departmentsRepository.Find(x => x.dep_Code == item.dep_Code && x.cou_Id == item.cou_Id);
                if (repeatedcod != null && repeatedcod.dep_Status==true)
                    return result.Error($"El pais ya tiene un departamento con el codigo '{item.dep_Code}' asignado ");
                var repeatedname = _departmentsRepository.Find(x => x.dep_Description.ToLower() == item.dep_Description.ToLower() && x.cou_Id == item.cou_Id);
                if (repeatedname != null && repeatedname.dep_Status == true)
                    return result.Error($"El pais ya tiene un departamento con el nombre '{item.dep_Description}' asignado ");
                var query = _departmentsRepository.Insert(item);
                EventLogger.UserId = item.dep_IdUserCreate;
                EventLogger.Create($"Creado Departamento '{item.dep_Description}'.", item);
                if (query > 0)
                {
                    entidad.dep_Id = query;
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


        public ServiceResult UpdateDepartments(int Id, tbDepartments item)
        {
            var result = new ServiceResult();
            var entidad = new DepartmentsModel();
            try
            {
                var IdUser = Id;
                var usuario = _departmentsRepository.Find(x => x.dep_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var country = _countryRepository.Find(x => x.cou_Id == item.cou_Id && x.cou_Status == true);
                if (country == null)
                {
                    return result.Error($"El pais no existe ");
                }
                var repeatedcod = _departmentsRepository.Find(x => x.dep_Code == item.dep_Code && x.cou_Id == item.cou_Id);
                if (repeatedcod != null && repeatedcod.dep_Status == true && repeatedcod.dep_Id != Id)
                    return result.Error($"El pais ya tiene un departamento con el codigo '{item.dep_Code}' asignado ");
                var repeatedname = _departmentsRepository.Find(x => x.dep_Description.ToLower() == item.dep_Description.ToLower() && x.cou_Id == item.cou_Id);
                if (repeatedname != null && repeatedname.dep_Status == true && repeatedname.dep_Id != Id)
                    return result.Error($"El pais ya tiene un departamento con el nombre '{item.dep_Description}' asignado ");
                var query = _departmentsRepository.Update(IdUser, item);
                EventLogger.UserId = item.dep_IdUserModified;
                EventLogger.Update($"Actualizado Departamento '{item.dep_Description}'.", usuario, item);
                entidad.dep_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteDepartments(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _departmentsRepository.Find(x => x.dep_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _departmentsRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminado Departamento '{usuario.dep_Description}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region tbMunicipalities
        public ServiceResult ListMunicipalities()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _municipalitiesRepository.List();
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

        public ServiceResult FindMunicipalities(Expression<Func<View_tbMunicipalities_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbMunicipalities_List();
            var errorMessage = "";
            try
            {
                resultado = _municipalitiesRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterMunicipalities(tbMunicipalities item)
        {
            var result = new ServiceResult();
            var entidad = new MunicipalitiesModel();
            try
            {
                var departamento = _departmentsRepository.Find(x => x.dep_Id == item.dep_Id && x.dep_Status == true);
                if(departamento==null)
                    return result.Error($"El departamento no existe");
                var repeatedcod = _municipalitiesRepository.Find(x => x.mun_Code == item.mun_Code && x.dep_Id == item.dep_Id);
                if (repeatedcod != null && repeatedcod.mun_Status == true)
                    return result.Error($"El departamento ya tiene un municipio con el codigo '{item.mun_Code}' asignado ");
                var repeatedname = _municipalitiesRepository.Find(x => x.mun_Description.ToLower() == item.mun_Description.ToLower() && x.dep_Id == item.dep_Id);
                if (repeatedname != null && repeatedname.mun_Status == true)
                    return result.Error($"El departamento ya tiene un municipio con el nombre '{item.mun_Description}' asignado ");
                var query = _municipalitiesRepository.Insert(item);
                EventLogger.UserId = item.mun_IdUserCreate;
                EventLogger.Create($"Creado Municipio '{item.mun_Description}'.", item);
                if (query > 0)
                {
                    entidad.mun_Id = query;
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


        public ServiceResult UpdateMunicipalities(int Id, tbMunicipalities item)
        {
            var result = new ServiceResult();
            var entidad = new MunicipalitiesModel();
            try
            {
                var IdUser = Id;
                var usuario = _municipalitiesRepository.Find(x => x.mun_Id == Id);
                var departamento = _departmentsRepository.Find(x => x.dep_Id == item.dep_Id && x.dep_Status == true);
                if (departamento == null)
                    return result.Error($"El departamento no existe");
                var repeatedcod = _municipalitiesRepository.Find(x => x.mun_Code == item.mun_Code && x.dep_Id == item.dep_Id);
                if (repeatedcod != null && repeatedcod.mun_Status == true && repeatedcod.mun_Id != Id)
                    return result.Error($"El departramento ya tiene un municipio con el codigo '{item.mun_Code}' asignado ");
                var repeatedname = _municipalitiesRepository.Find(x => x.mun_Description.ToLower() == item.mun_Description.ToLower() && x.dep_Id == item.dep_Id);
                if (repeatedname != null && repeatedname.mun_Status == true && repeatedname.mun_Id != Id)
                    if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _municipalitiesRepository.Update(IdUser, item);
                EventLogger.UserId = item.mun_IdUserModified;
                EventLogger.Update($"Actualizado Municipio '{item.mun_Description}'.", usuario, item);
                entidad.mun_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteMunicipalities(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _municipalitiesRepository.Find(x => x.mun_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _municipalitiesRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminado Municipio '{usuario.mun_Description}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region tbAreas
        public ServiceResult ListAreas()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _areasRepository.List();
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

        public ServiceResult FindAreas(Expression<Func<View_Areas_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_Areas_List();
            var errorMessage = "";
            try
            {
                resultado = _areasRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterAreas(tbAreas item)
        {
            var result = new ServiceResult();
            var entidad = new AreasModel();
            try
            {
                var repeated = _areasRepository.Find(x => x.are_Description.ToLower() == item.are_Description.ToLower());
                if (repeated != null && repeated.are_Status == true)
                    return result.Conflict($"El area con el nombre '{item.are_Description}' ya existe");
                var query = _areasRepository.Insert(item);
                EventLogger.UserId = item.are_IdUserCreate;
                EventLogger.Create($"Creada Area '{item.are_Description}'.", item);
                if (query > 0)
                {
                    entidad.are_Id = query;
                    return result.Ok(entidad);
                }
                else
                {
                    return result.Error();
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }


        public ServiceResult UpdateAreas(int Id, tbAreas item)
        {
            var result = new ServiceResult();
            var entidad = new AreasModel();
            try
            {
                var IdUser = Id;
                var usuario = _areasRepository.Find(x => x.are_Id == Id);
                if (usuario == null)
                {
                    return result.Error($"No existe el registro");
                }
                var repeated = _areasRepository.Find(x => x.are_Description.ToLower() == item.are_Description.ToLower());
                if (repeated != null && repeated.are_Status == true)
                    return result.Conflict($"El area con el nombre '{item.are_Description}' ya existe");
                var query = _areasRepository.Update(IdUser, item);
                EventLogger.UserId = item.are_IdUserModified;
                EventLogger.Update($"Actualizada Area '{item.are_Description}'.", usuario, item);
                entidad.are_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteAreas(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _areasRepository.Find(x => x.are_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _areasRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminada Area '{usuario.are_Description}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region tbEmployees
        public ServiceResult ListEmployees()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _employeesRepository.List();
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
        public ServiceResult FindEmployees(Expression<Func<View_tbEmployees_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbEmployees_List();
            var errorMessage = "";
            try
            {
                resultado = _employeesRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }
        public ServiceResult RegisterEmployees(tbEmployees item)
        {
            var result = new ServiceResult();
            var entidad = new EmployeesModel();
            try
            {
                var persona = _personsRepository.Find(x => x.per_Id == item.per_Id);
                if (persona == null)
                    return result.Conflict($"La persona seleccionada no existe");
                var area = _areasRepository.Find(x => x.are_Id == item.are_Id);
                if (area == null)
                    return result.Conflict($"La area seleccionada no existe");
                var ocupacion = _occupationsRepository.Find(x => x.occ_Id == item.occ_Id);
                if (ocupacion == null)
                    return result.Conflict($"La ocupacion seleccionada no existe");
                var query = _employeesRepository.Insert(item);
                EventLogger.UserId = item.emp_IdUserCreate;
                EventLogger.Create($"Creado Empleado con ID: '{item.emp_Id}'.", item);
                if (query > 0)
                {
                    entidad.emp_Id = query;
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


        public ServiceResult UpdateEmployees(int Id, tbEmployees item)
        {
            var result = new ServiceResult();
            var entidad = new EmployeesModel();
            try
            {
                var IdUser = Id;
                var usuario = _employeesRepository.Find(x => x.emp_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var persona = _personsRepository.Find(x => x.per_Id == item.per_Id);
                if (persona == null)
                    return result.Conflict($"La persona seleccionada no existe");
                var area = _areasRepository.Find(x => x.are_Id == item.are_Id);
                if (area == null)
                    return result.Conflict($"La area seleccionada no existe");
                var ocupacion = _occupationsRepository.Find(x => x.occ_Id == item.occ_Id);
                if (ocupacion == null)
                    return result.Conflict($"La ocupacion seleccionada no existe");
                var query = _employeesRepository.Update(IdUser, item);
                EventLogger.UserId = item.emp_IdUserModified;
                EventLogger.Update($"Actualizado Empleado con ID: '{item.emp_Id}'.", usuario, item);
                entidad.emp_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteEmployees(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _employeesRepository.Find(x => x.emp_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _employeesRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminado Empleado con ID: '{usuario.emp_Id}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region tbOccupations
        public ServiceResult ListOccupations()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _occupationsRepository.List();
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

        public ServiceResult FindOccupations(Expression<Func<View_tbOccupations_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_tbOccupations_List();
            var errorMessage = "";
            try
            {
                resultado = _occupationsRepository.Search(expression);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterOccupations(tbOccupations item)
        {
            var result = new ServiceResult();
            var entidad = new OccupationsModel();
            try
            {
                var repeated = _occupationsRepository.Find(x => x.occ_Description.ToLower() == item.occ_Description.ToLower());
                if (repeated != null && repeated.occ_Status == true)
                    return result.Conflict($"Un puesto con el nombre '{item.occ_Description}' ya existe");
              
                var query = _occupationsRepository.Insert(item);
                EventLogger.UserId = item.occ_IdUserCreate;
                EventLogger.Create($"Creada Ocupacion '{item.occ_Description}'.", item);
                if (query > 0)
                {                   
                    entidad.occ_Id = query;
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


        public ServiceResult UpdateOccupations(int Id, tbOccupations item)
        {
            var result = new ServiceResult();
            var entidad = new OccupationsModel();
            try
            {
                var IdUser = Id;
                var usuario = _occupationsRepository.Find(x => x.occ_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var repeated = _occupationsRepository.Find(x => x.occ_Description.ToLower() == item.occ_Description.ToLower());
                if (repeated != null && repeated.occ_Status == true)
                    return result.Conflict($"Un puesto con el nombre '{item.occ_Description}' ya existe");
                var query = _occupationsRepository.Update(IdUser, item);
                EventLogger.UserId = item.occ_IdUserModified;
                EventLogger.Update($"Actualizado Ocupacion '{item.occ_Description}'.", usuario, item);
                entidad.occ_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteOccupations(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _occupationsRepository.Find(x => x.occ_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _occupationsRepository.Delete(IdUser, ModUser);
                EventLogger.UserId = Mod;
                EventLogger.Delete($"Eliminado Ocupacion '{usuario.occ_Description}'.", usuario);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region DashBoard
        public ServiceResult DashboardMetrics(int IdUser)
        {
            var result = new ServiceResult();
            try
            {
                var query = _dashboardRepository.DashBoardMetrics(IdUser);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult TopProducts()
        {
            var result = new ServiceResult();
            try
            {
                var query = _dashboardRepository.TopProducts();
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult TopCustomers()
        {
            var result = new ServiceResult();
            try
            {
                var query = _dashboardRepository.TopCustomers();
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
                return result.Error(ex.Message);
            }
        }

        public ServiceResult LastCotizations(int IdUser)
        {
            var result = new ServiceResult();
            try
            {
                var listado = _dashboardRepository.LastCotizations(IdUser);
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
        public ServiceResult LastSales(int IdUser)
        {
            var result = new ServiceResult();
            try
            {
                var listado = _dashboardRepository.LastSales(IdUser);
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

    }
}