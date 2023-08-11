using AHM_LOGISTIC_SMART_ADM.Models;
using AHM_LOGISTIC_SMART_ADM.WebApi;
using AHM.Logistic.Smart.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;
using System.Globalization;

namespace AHM_LOGISTIC_SMART_ADM.Services
{
    public class CatalogService
    {
        private readonly Api _api;
        public CatalogService(Api api)
        {
            _api = api;
        }

        #region tbCountries
        public async Task<ServiceResult> CountriesList(List<CountryViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<CountryViewModel>>(req =>
                {
                    req.Path = $"/api/Country/List";
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

        public async Task<ServiceResult> DetailsCountries(int Id)
        {
            var result = new ServiceResult();
            var model = new CountryViewModel();
            try
            {
                var exist = await _api.Get<List<CountryViewModel>>(req =>
                {
                    req.Path = $"/api/Country/List";
                });

                if (exist.Data.Where(x => x.cou_Id == Id).Count() != 0)
                {
                    var response = await _api.Get<CountryViewModel>(req =>
                    {
                        req.Path = $"/api/Country/Details/{Id}";
                        req.Content = model;
                    });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok("Registro creado exitosamente.", response.Data);
                }
                else
                    return result.Error(ServiceProblemMessage.NotFound);

            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InsertCountries(CountryModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<CountryModel>(req =>
                {
                    req.Path = $"/api/Country/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);


                return result.Ok("Registro creado exitosamente.", response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> EditCountries(CountryModel model, int Id)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Put<CountryModel>(req =>
                {
                    req.Path = $"/api/Country/Update/{Id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro actualizado exitosamente.", response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> DeleteCountries(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var getDetails = await DetailsCountries(Id);
                if (!getDetails.Success && getDetails.Data == null)
                    if (!getDetails.Success && getDetails.Data == null)
                        return result.Error(getDetails.Message);
                var departments = await _api.Get<List<DepartmentsViewModel>>(req =>
            {
                req.Path = $"/api/Departments/List";
            });

                var listdepartments = departments.Data.Where(w => w.cou_Id == Id);//contactos*

                if (listdepartments.Count() == 0)
                {
                    var response = await _api.Delete<CountryModel>(req =>
                    {
                        req.Path = $"/api/Country/Delete/" + Id + "?Mod=" + Mod;
                        //req.Content = model;
                    });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok(response.StatusCode);
                }

                return result.Error("No se puede eliminar ya que el registro se encuentra en uso.");
            }

            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        #endregion

        #region tbDepartments
        //List
        public async Task<ServiceResult> ApiDepartmentsList(List<DepartmentsViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<DepartmentsViewModel>>(req =>
                {
                    req.Path = $"/api/Departments/List";
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
        //Insert
        public async Task<ServiceResult> InsertDepartments(DepartmentsModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<DepartmentsModel>(req =>
                {
                    req.Path = $"/api/Departments/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro creado exitosamente.", response.Data);

            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        //Delete
        public async Task<ServiceResult> DeleteDepartments(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                string msjError = string.Empty;
                //Traer datos de las tablas relacionadas
                var muni = await _api.Get<List<MunicipalitiesViewModel>>(req =>//Municipios
                {
                    req.Path = $"/api/Municipalities/List";
                });

                var perso = await _api.Get<List<PersonsViewModel>>(req =>//Personas
                {
                    req.Path = $"/api/Persons/List";
                });

                var listMuni = muni.Data.Where(w => w.dep_Id == Id);//Municipios*
                var listPerso = perso.Data.Where(w => w.dep_Id == Id);//Personas*

                if (listMuni.Count() == 0 && listPerso.Count() == 0)
                {
                    var response = await _api.Delete<DepartmentsModel>(req =>
                {
                    //req.Path = $"/api/Employees/Delete" + Id + "?Mod=" + Mod;
                    req.Path = $"/api/Departments/Delete/" + Id + "?Mod=" + Mod;
                    //req.Content = model;
                });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok(response.StatusCode);
                }
                return result.Error("No se puede eliminar ya que el registro se encuentra en uso.");
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        //edit
        public async Task<ServiceResult> EditDepartments(DepartmentsModel model, int id)
        {
            var result = new ServiceResult();

            try
            {
                var response = await _api.Put<DepartmentsModel>(req =>
                {
                    req.Path = $"/api/Departments/Update/{id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro actualizado exitosamente.", response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        //details
        public async Task<ServiceResult> DetailsDepartments(int Id)
        {
            var result = new ServiceResult();
            try
            {

                var response = await _api.Get<DepartmentsViewModel>(req =>
                {
                    req.Path = $"/api/Departments/Details/{Id}";
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

        #endregion

        #region tbAreas
        public async Task<ServiceResult> AreasList(List<AreasViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<AreasViewModel>>(req =>
                {
                    req.Path = $"/api/Areas/List";
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

        public async Task<ServiceResult> AreasDetails(int id)
        {
            var result = new ServiceResult();
            var model = new AreasViewModel();
            try
            {
                var exist = await _api.Get<List<AreasViewModel>>(req =>
                {
                    req.Path = $"/api/Areas/List";
                });

                if (exist.Data.Where(x => x.are_Id == id).Count() != 0)
                {
                    var response = await _api.Get<AreasViewModel>(req =>
                    {
                        req.Path = $"/api/Areas/Details/{id}";
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

        public async Task<ServiceResult> InsertAreas(AreasModel model)
        {
            var result = new ServiceResult();
            try
            {

                var response = await _api.Post<AreasModel>(req =>
                {
                    req.Path = $"/api/Areas/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro creado exitosamente.", response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> EditAreas(AreasModel model, int id)
        {
            var result = new ServiceResult();
            try
            {

                var response = await _api.Put<AreasModel>(req =>
                {
                    req.Path = $"/api/Areas/Update/{id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro actualizado exitosamente.", response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> DeleteAreas(int Id, int Mod)
        {
            var result = new ServiceResult();
            List<EmployeesViewModel> emp = new List<EmployeesViewModel>();
            try
            {
                var getDetails = await AreasDetails(Id);
                if (!getDetails.Success && getDetails.Data == null)
                    return result.Error(getDetails.Message);
                var getRepeated = await EmployeesList(emp);
                emp = (List<EmployeesViewModel>)getRepeated.Data;
                if (emp.Where(x => x.are_Id == Id).Count() == 0)
                {
                    var response = await _api.Delete<AreasModel>(req =>
                    {
                        //req.Path = $"/api/Employees/Delete" + Id + "?Mod=" + Mod;
                        req.Path = $"/api/Areas/Delete/" + Id + "?Mod=" + Mod;
                        //req.Content = model;
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

        #region tbMuncipalities
        public async Task<ServiceResult> MunicipalitiesList(List<MunicipalitiesViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<MunicipalitiesViewModel>>(req =>
                {
                    req.Path = $"/api/Municipalities/List";
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

        public async Task<ServiceResult> MunicipalitiesDetails(int Id)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<MunicipalitiesViewModel>(req =>
                {
                    req.Path = $"/api/Municipalities/Details/{Id}";
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

        public async Task<ServiceResult> MunicipalitiesEdit(MunicipalitiesModel model, int Id)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Put<MunicipalitiesModel>(req =>
                {
                    req.Path = $"/api/Municipalities/Update/{Id}";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro actualizado exitosamente.", response.StatusCode);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> MunicipalitiesInsert(MunicipalitiesModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<MunicipalitiesModel>(req =>
                {
                    req.Path = $"/api/Municipalities/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro creado exitosamente.", response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> MunicipalitiesDelete(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                string msjError = string.Empty;
                //Traer datos de las tablas relacionadas

                var perso = await _api.Get<List<PersonsViewModel>>(req =>//Personas
                {
                    req.Path = $"/api/Persons/List";
                });
                var custo = await _api.Get<List<CustomerViewModel>>(req =>//Cliente
                {
                    req.Path = $"/api/Customers/List";
                });

                var listCusto = custo.Data.Where(w => w.mun_Id == Id);//Municipios*
                var listPerso = perso.Data.Where(w => w.mun_Id == Id);//Cliente*

                if (listCusto.Count() == 0 && listPerso.Count() == 0)
                {
                    var response = await _api.Delete<MunicipalitiesModel>(req =>
                {
                    //req.Path = $"/api/Employees/Delete" + Id + "?Mod=" + Mod;
                    req.Path = $"/api/Municipalities/Delete/" + Id + "?Mod=" + Mod;
                    //req.Content = model;
                });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok(response.StatusCode);
                }
                return result.Error("No se puede eliminar ya que el registro se encuentra en uso.");
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }


        #endregion

        #region tbPersonas
        public async Task<ServiceResult> PersonsList(List<PersonsViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<PersonsViewModel>>(req =>
                {
                    req.Path = $"/api/Persons/List";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Success && response.Data.Count() > 0)
                {
                    foreach (var item in response.Data)
                    {
                        HelpersGeneral helpersGeneral = new HelpersGeneral();
                        item.per_Firstname = $"{item.per_Firstname} {item.per_Secondname} {item.per_LastNames}";
                        item.per_Esciv = helpersGeneral.GetEstadoCivil(item.per_Esciv);
                        item.per_Sex = helpersGeneral.GetEstadoCivil(item.per_Sex);
                    }
                }
                return result.Ok(response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> PersonsDetails(int id)
        {
            var result = new ServiceResult();
            var model = new PersonsViewModel();
            try
            {
                var exist = await _api.Get<List<PersonsViewModel>>(req =>
                {
                    req.Path = $"/api/Persons/List";
                });

                if (exist.Data.Where(x => x.per_Id == id).Count() != 0)
                {
                    var response = await _api.Get<PersonsViewModel>(req =>
                    {
                        req.Path = $"/api/Persons/Details/{id}";
                        req.Content = model;
                    });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok("Registro creado exitosamente.", response.Data);
                }
                else
                    return result.Error(ServiceProblemMessage.NotFound);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InsertPersons(PersonsModel model)
        {
            var result = new ServiceResult();
            try
            {
                if (model.per_Sex != "F" && model.per_Sex != "M")
                    return result.Error(ServiceProblemMessage.NotFound);

                if (model.per_Esciv != "C" && model.per_Esciv != "S" && model.per_Esciv != "D" && model.per_Esciv != "V" && model.per_Esciv != "U")
                    return result.Error(ServiceProblemMessage.NotFound);

                var response = await _api.Post<PersonsModel>(req =>
                {
                    req.Path = $"/api/Persons/Insert";
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

        public async Task<ServiceResult> EditPersons(PersonsViewModel model)
        {
            var result = new ServiceResult();
            var Object = new PersonsModel()
            {
                per_Identidad = model.per_Identidad,
                per_Firstname = model.per_Firstname,
                per_Secondname = model.per_Secondname,
                per_LastNames = model.per_LastNames,
                per_BirthDate = model.per_BirthDate,
                per_Sex = model.per_Sex,
                per_Email = model.per_Email,
                per_Phone = model.per_Phone,
                per_Direccion = model.per_Direccion,
                dep_Id = model.dep_Id,
                mun_Id = model.mun_Id,
                per_Esciv = model.per_Esciv,
                per_IdUserCreate = model.per_IdUserCreate,
                per_IdUserModified = model.per_IdUserModified
            };
            try
            {
                if (model.per_Sex != "F" && model.per_Sex != "M")
                    return result.Error(ServiceProblemMessage.NotFound);

                if (model.per_Esciv != "C" && model.per_Esciv != "S" && model.per_Esciv != "D" && model.per_Esciv != "V" && model.per_Esciv != "U")
                    return result.Error(ServiceProblemMessage.NotFound);

                var response = await _api.Put<PersonsModel>(req =>
                {
                    req.Path = $"/api/Persons/Update/{model.per_Id}";
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

        public async Task<ServiceResult> DeletePersons(int Id, int Mod)
        {
            var result = new ServiceResult();
            List<EmployeesViewModel> emp = new List<EmployeesViewModel>();
            try
            {
                var getDetails = await PersonsDetails(Id);
                if (!getDetails.Success && getDetails.Data == null)
                    return result.Error(getDetails.Message);
                var getRepeated = await EmployeesList(emp);
                emp = (List<EmployeesViewModel>)getRepeated.Data;
                if (emp.Where(x => x.are_Id == Id).Count() == 0)
                {
                    var response = await _api.Delete<PersonsModel>(req =>
                {
                    req.Path = $"/api/Persons/Delete/" + Id + "?Mod=" + Mod;
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

        #region tbOccupations
        public async Task<ServiceResult> OccupationsList(List<OccupationsViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<OccupationsViewModel>>(req =>
                {
                    req.Path = $"/api/Occupations/List";
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

        public async Task<ServiceResult> OccupationsDetails(int id)
        {
            var result = new ServiceResult();
            var model = new OccupationsViewModel();
            try
            {
                var exist = await _api.Get<List<OccupationsViewModel>>(req =>
                {
                    req.Path = $"/api/Occupations/List";
                });

                if (exist.Data.Where(x => x.occ_Id == id).Count() != 0)
                {
                    var response = await _api.Get<OccupationsViewModel>(req =>
                    {
                        req.Path = $"/api/Occupations/Details/{id}";
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

        public async Task<ServiceResult> InsertOccupations(OccupationsModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<OccupationsModel>(req =>
                {
                    req.Path = $"/api/Occupations/Insert";
                    req.Content = model;
                });

                if (!response.Success)
                    return result.FromApi(response);

                if (response.Type == ApiResultType.Error)
                    return result.Error(response.Message);

                return result.Ok("Registro creado exitosamente.", response.Data);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> EditOccupations(OccupationsModel model, int id)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Put<OccupationsModel>(req =>
                {
                    req.Path = $"/api/Occupations/Update/{id}";
                    req.Content = model;
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


        public async Task<ServiceResult> DeleteOccupation(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var getDetails = await OccupationsDetails(Id);
                if (!getDetails.Success && getDetails.Data == null)
                    return result.Error(getDetails.Message);
                var employees = await _api.Get<List<EmployeesViewModel>>(req =>
                {
                    req.Path = $"/api/Employees/List";
                });

                var contacts = await _api.Get<List<ContactsViewModel>>(req =>
                {
                    req.Path = $"/api/Contacts/List";
                });

                var listemployees = employees.Data.Where(w => w.emp_Id == Id);//Empleados*
                var listcontacts = contacts.Data.Where(w => w.cont_Id == Id);//contactos*

                if (listemployees.Count() == 0 && listcontacts.Count() == 0)
                {
                    var response = await _api.Delete<OccupationsModel>(req =>
                    {
                        req.Path = $"/api/Occupations/Delete/" + Id + "?Mod=" + Mod;
                        //req.Content = model;
                    });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok(response.StatusCode);
                }

                return result.Error("No se puede eliminar ya que el registro se encuentra en uso.");
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        #endregion

        #region tbEmployees
        public async Task<ServiceResult> EmployeesList(List<EmployeesViewModel> model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<EmployeesViewModel>>(req =>
                {
                    req.Path = $"/api/Employees/List";
                    req.Content = model;
                });
                List<PersonsViewModel> personsViews = new List<PersonsViewModel>();
                var personResult = await PersonsList(personsViews);
                personsViews = (List<PersonsViewModel>)personResult.Data;
                if(personResult.Success && personsViews.Count() > 0)
                {
                    foreach (var emp in response.Data)
                    {
                        PersonsViewModel per = personsViews.Find(x => x.per_Id == emp.per_Id);
                        emp.per_Firstname = per.per_Firstname;
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

        public async Task<ServiceResult> DetailsEmployees(int Id)
        {
            var result = new ServiceResult();
            var model = new EmployeesViewModel();
            try
            {
                var exist = await _api.Get<List<EmployeesViewModel>>(req =>
                {
                    req.Path = $"/api/Employees/List";
                });

                if (exist.Data.Where(x => x.emp_Id == Id).Count() != 0)
                {
                    var response = await _api.Get<EmployeesViewModel>(req =>
                    {
                        req.Path = $"/api/Employees/Details/{Id}";
                        req.Content = model;
                    });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok("Registro creado exitosamente.", response.Data);
                }
                else
                    return result.Error(ServiceProblemMessage.NotFound);
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }

        public async Task<ServiceResult> InsertEmployees(EmployeesModel model)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Post<EmployeesModel>(req =>
                {
                    req.Path = $"/api/Employees/Insert";
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

        public async Task<ServiceResult> EditEmployees(EmployeesModel model, int Id)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Put<EmployeesModel>(req =>
                {
                    req.Path = $"/api/Employees/Update/{Id}";
                    req.Content = model;
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

        public async Task<ServiceResult> DeleteEmployees(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var getDetails = await DetailsEmployees(Id);
                
                if (!getDetails.Success && getDetails.Data == null)
                    return result.Error(getDetails.Message);

                EmployeesViewModel DetailsEmp = (EmployeesViewModel)getDetails.Data;
               
                var usuarios = await _api.Get<List<UsersViewModel>>(req =>
                {
                    req.Path = $"/api/Users/List";
                    //req.Content = model;
                });

                var Listusuario = usuarios.Data.Where(w => w.Per_Id == DetailsEmp.per_Id);//Personas*
                if (Listusuario.Count() == 0)
                {
                    var response = await _api.Delete<EmployeesModel>(req =>
                    {
                        req.Path = $"/api/Employees/Delete/" + Id + "?Mod=" + Mod;
                        //req.Content = model;
                    });

                    if (!response.Success)
                        return result.FromApi(response);

                    return result.Ok(response.StatusCode);
                }

                return result.Error("No se puede eliminar ya que el registro se encuentra en uso.");
            }
            catch (Exception ex)
            {
                return result.Error(Helpers.GetMessage(ex));
            }
        }
        #endregion

        #region DashBoard
        public async Task<ServiceResult> Dashboard(int IdUserxd)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<Dictionary<string, string>>(req =>
                {
                    req.Path = $"/api/Dashboard/DashBoardMetrics/{IdUserxd}";
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

        public async Task<ServiceResult> LastCotizations(List<CotizationsViewModel> model, int IdUser2)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<CotizationsViewModel>>(req =>
                {
                    req.Path = $"/api/Dashboard/LastCotizations/{IdUser2}";
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

        public async Task<ServiceResult> LastSales(List<SalesOrderViewModel> model, int IdUser2)
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<SalesOrderViewModel>>(req =>
                {
                    req.Path = $"/api/Dashboard/LastSales/{IdUser2}";
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

        public async Task<ServiceResult> TopProducts()
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<TopProductsModel>>(req =>
                {
                    req.Path = $"/api/Dashboard/TopProducts";
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

        public async Task<ServiceResult> TopCustomers()
        {
            var result = new ServiceResult();
            try
            {
                var response = await _api.Get<List<TopCustomersModel>>(req =>
                {
                    req.Path = $"/api/Dashboard/TopCustomers";
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
        #endregion
    }
}
