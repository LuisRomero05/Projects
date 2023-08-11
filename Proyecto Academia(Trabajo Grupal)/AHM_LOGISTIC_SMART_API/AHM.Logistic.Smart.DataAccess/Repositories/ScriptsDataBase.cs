using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public class ScriptsDataBase
    {
        #region tbLogin
        public static string UDP_Login = "Acce.UDP_Login";
        #endregion

        #region tbUser
        public static string UDP_tbUsers_INSERT = "Acce.UDP_tbUsers_INSERT";
        public static string UDP_tbUsers_UPDATE = "Acce.UDP_tbUsers_UPDATE";
        public static string UDP_tbUsers_DELETE = "Acce.UDP_tbUsers_DELETE";
        #endregion

        #region tbRoleModuleItems
        public static string UDP_tbRoleModuleItems_INSERT = "Acce.UDP_tbRoleModuleItems_INSERT";
        public static string UDP_tbRoleModuleItems_DELETE = "Acce.UDP_tbRoleModuleItems_DELETE";
        #endregion

        #region tbRol
        public static string UDP_tbRoles_INSERT = "Acce.UDP_tbRoles_INSERT";
        public static string UDP_tbRoles_UPDATE = "Acce.UDP_tbRoles_UPDATE";
        public static string UDP_tbRoles_DELETE = "Acce.UDP_tbRoles_DELETE";
        #endregion

        #region tbCategories
        public static string UDP_tbCategories_INSERT = "Vent.UDP_tbCategories_REGISTER";
        public static string UDP_tbCategories_UPDATE = "Vent.UDP_tbCategories_UPDATE";
        public static string UDP_tbCategories_DELETE = "Vent.UDP_tbCategories_DELETE";
        #endregion

        #region tbProducts
        public static string UDP_tbProducts_INSERT = "Vent.UDP_tbProducts_INSERT";
        public static string UDP_tbProducts_UPDATE = "Vent.UDP_tbProducts_UPDATE";
        public static string UDP_tbProducts_DELETE = "Vent.UDP_tbProducts_DELETE";
        public static string UDP_tbProducts_Stock_UPDATE = "Vent.UDP_tbProducts_Stock_UPDATE";
        #endregion

        #region tbCustomers
        public static string UDP_tbCustomers_INSERT = "Clte.UDP_tbCustomers_INSERT";
        public static string UDP_tbCustomers_UPDATE = "Clte.UDP_tbCustomers_UPDATE";
        public static string UDP_tbCustomers_DELETE = "Clte.UDP_tbCustomers_DELETE";
        #endregion

        #region tbSubCategories
        public static string UDP_tbSubCategories_INSERT = "Vent.UDP_tbSubCategories_INSERT";
        public static string UDP_tbSubCategories_UPDATE = "Vent.UDP_tbSubCategories_UPDATE";
        public static string UDP_tbSubCategories_DELETE = "Vent.UDP_tbSubCategories_DELETE";
        #endregion

        #region tbDepartments
        public static string UDP_tbDepartamentos_INSERT = "Gral.UDP_tbDepartamentos_INSERT";
        public static string UDP_tbDepartamentos_UPDATE = "Gral.UDP_tbDepartamentos_UPDATE";
        public static string UDP_tbDepartamentos_DELETE = "Gral.UDP_tbDepartamentos_DELETE";
        #endregion

        #region tbMunicipalities
        public static string UDP_tbMunicipalities_INSERT = "Gral.UDP_tbMunicipalities_INSERT";
        public static string UDP_tbMunicipalities_UPDATE = "Gral.UDP_tbMunicipalities_UPDATE";
        public static string UDP_tbMunicipalities_DELETE = "Gral.UDP_tbMunicipalities_DELETE";
        #endregion

        #region tbContacts
        public static string UDP_tbContacts_INSERT = "Clte.UDP_tbContacts_INSERT";
        public static string UDP_tbContacts_UPDATE = "Clte.UDP_tbContacts_UPDATE";
        public static string UDP_tbContacts_DELETE = "Clte.UDP_tbContacts_DELETE";
        #endregion

        #region tbAreas
        public static string UDP_Areas_INSERT = "Clte.UDP_Areas_INSERT";
        public static string UDP_Areas_UPDATE = "Clte.UDP_Areas_UPDATE";
        public static string UDP_Areas_DELETE = "Clte.UDP_Areas_DELETE";
        #endregion

        #region tbEmployees
        public static string UDP_Employees_INSERT = "Gral.UDP_tbEmployees_INSERT";
        public static string UDP_Employees_UPDATE = "Gral.UDP_tbEmployees_UPDATE";
        public static string UDP_Employees_DELETE = "Gral.UDP_tbEmployees_DELETE";
        #endregion

        #region tbOcuppations
        public static string View_tbOccupations_List = "Gral.View_tbOccupations_List";
        public static string UDP_tbOccupations_INSERT = "Gral.UDP_tbOccupations_INSERT";
        public static string UDP_tbOccupations_UPDATE = "Gral.UDP_tbOccupations_UPDATE";
        public static string UDP_tbOccupations_DELETE = "Gral.UDP_tbOccupations_DELETE";
        #endregion

        #region tbSaleOrders
        public static string UDP_tbOrders_DELETE = "Vent.UDP_tbOrders_DELETE";
        public static string UDP_tbSaleOrders_Details = "Vent.UDP_tbSaleOrders_Details";
        public static string UDP_tbOrderDetails_DELETE = "Vent.UDP_tbOrdersDetail_DELETE";
        #endregion

        #region tbCotizations
        public static string UDP_tbCotizations_DELETE = "Vent.UDP_tbCotizations_DELETE";
        public static string View_tbCotizations_Details = "Vent.View_tbCotizations_Details";
        #endregion

        #region tbPersons
        public static string UDP_Persons_INSERT = "Vent.UDP_tbPersons_INSERT";
        public static string UDP_Persons_UPDATE = "Vent.UDP_tbPersons_UPDATE";
        public static string UDP_Persons_DELETE = "Vent.UDP_tbPersons_DELETE";
        #endregion

        #region Security
        public static string UDP_UserPermits_SELECT = "Acce.UDP_UserPermits_SELECT";
        #endregion

        #region tbCountries
        public static string UDP_tbCountries_INSERT = "Gral.UDP_tbCountries_INSERT";
        public static string UDP_tbCountries_UPDATE = "Gral.UDP_tbCountries_UPDATE";
        public static string UDP_tbCountries_DELETE = "Gral.UDP_tbCountries_DELETE";
        #endregion

        #region tbCustomerCalls
        public static string UDP_tbCustomerCalls_INSERT = "Clte.UDP_tbCustomerCalls_INSERT";
        public static string UDP_tbCustomerCalls_UPDATE = "Clte.UDP_tbCustomerCalls_UPDATE";
        public static string UDP_tbCustomerCalls_DELETE = "Clte.UDP_tbCustomerCalls_DELETE";
        #endregion

        #region tbCustomerNotes
        public static string UDP_tbCustomerNotes_INSERT = "Clte.UDP_tbCustomerNotes_INSERT";
        public static string UDP_tbCustomerNotes_UPDATE = "Clte.UDP_tbCustomerNotes_UPDATE";
        public static string UDP_tbCustomerNotes_DELETE = "Clte.UDP_tbCustomerNotes_DELETE";
        #endregion

        #region tbCotizationsDetails
        public static string UDP_tbCotizationsDetail_DELETE = "Vent.UDP_tbCotizationsDetail_DELETE";
        #endregion

        #region tbCampaign
        public static string UDP_tbCampaign_INSERT = "Vent.UDP_tbCampaign_INSERT";
        public static string UDP_tbCampaign_DELETE = "Vent.UDP_tbCampaign_DELETE";
        #endregion

        #region tbMeetings
        public static string UDP_tbMeetings_DELETE = "Clte.UDP_tbMeetings_DELETE";
        public static string UDP_tbMeetingsDetails_DELETE = "Clte.UDP_tbMeetingsDetails_DELETE";
        #endregion

        #region tbCustomersFile
        public static string UDP_tbCustomersFile_INSERT = "Clte.UDP_tbCustomersFile_INSERT";
        public static string UDP_tbCustomersFile_DELETE = "Clte.UDP_tbCustomersFile_DELETE";
        #endregion

        #region DashBoard
        public static string UDP_Dashboard_Cotizations_Filter = "Vent.UDP_Dashboard_Cotizations_Filter";
        public static string UDP_Dashboard_Sales_Filter = "Vent.UDP_Dashboard_Sales_Filter";
        public static string UDP_Top10Products_Metrics = "Vent.UDP_Top10Products_Metrics";
        public static string UDP_TopCustomerOrders_Metrics = "Vent.UDP_TopCustomerOrders_Metrics";
        #endregion

    }
}


