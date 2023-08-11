using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api.Extensions
{
    public class MappingProfileExtensions : Profile
    {
        public MappingProfileExtensions()
        {
            CreateMap<tbProducts, ProductStockModel>().ReverseMap();
            CreateMap<tbRoles, RolModel>().ReverseMap();
            CreateMap<tbEmployees, EmployeesModel>().ReverseMap(); 
            CreateMap<tbContacts, ContactsModel>().ReverseMap();
            CreateMap<tbSubCategories, SubCategoriesModel>().ReverseMap();
            CreateMap<tbCustomers, CustomersModel>().ReverseMap();
            CreateMap<tbUsers, UsersModel>().ReverseMap();
            CreateMap<tbUsers, LoginModel>().ReverseMap();
            CreateMap<tbRoles, RolesModel>().ReverseMap();
            CreateMap<RolesModel, tbRoles>().ReverseMap();
            CreateMap<View_tbRoles_List,RolesModel>().ReverseMap();
            CreateMap<tbCategories, CategoryModel>().ReverseMap();
            CreateMap<tbProducts, ProductsModel>().ReverseMap();
            CreateMap<tbDepartments, DepartmentsModel>().ReverseMap();
            CreateMap<tbMunicipalities, MunicipalitiesModel>().ReverseMap();
            CreateMap<tbAreas, AreasModel>().ReverseMap();
            CreateMap<tbRoles, View_tbRoles_List>().ReverseMap();
            CreateMap<tbOccupations, OccupationsModel>().ReverseMap();
            CreateMap<tbPersons, PersonsModel>().ReverseMap();
            CreateMap<tbCotizations, CotizationsModel>().ReverseMap();
            CreateMap<tbCotizationsDetail, CotizationsDetailsModel>().ReverseMap();
            CreateMap<tbSaleOrders, SaleOrdersModel>().ReverseMap();
            CreateMap<tbOrderDetails, OrderDetailsModel>().ReverseMap();
            CreateMap<tbCountries, CountryModel>().ReverseMap();
            CreateMap<tbCustomerCalls, CustomerCallsModel>().ReverseMap();
            CreateMap<tbCustomerNotes, CustomerNotesModel>().ReverseMap();
            CreateMap<tbCampaign, CampaignModel>().ReverseMap();
            CreateMap<tbCampaignDetails, CampaignDetailsModel>().ReverseMap();
            CreateMap<tbCotizationsDetail,CotizationsDetailsModel>().ReverseMap();
            CreateMap<tbCotizationsDetail, CotizationsDetailUpdateModel>().ReverseMap();
            CreateMap<tbOrderDetails, OrderDetailsUpdateModel>().ReverseMap();
            CreateMap<tbMeetings, MeetingsModel>().ReverseMap();
            CreateMap<tbMeetingsDetails, MeetingsDetailsModel>().ReverseMap();
            CreateMap<tbMeetingsDetails, MeetingsDetailUpdateModel>().ReverseMap();
            CreateMap<tbCustomersFile, CustomerFilesModel>().ReverseMap();
            CreateMap<tbCustomersFile, CustomersFileViewModel>().ReverseMap();

        }
    }
}
