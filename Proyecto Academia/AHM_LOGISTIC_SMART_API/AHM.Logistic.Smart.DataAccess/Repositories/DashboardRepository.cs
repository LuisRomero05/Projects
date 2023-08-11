using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.Entities.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public class DashboardRepository
    {
        public dynamic DashBoardMetrics(int Id)
        {
            using var dbfind = new LogisticSmartContext();
            
            var rol = dbfind.tbUsers
           .Where(x => x.usu_Id == Id)
           .Select(x => x.rol_Id)
           .FirstOrDefault();

            using (var db = new SqlConnection(LogisticSmartContext.ConnectionString))
            {
                dynamic data = new System.Dynamic.ExpandoObject();
                if (rol == 1)
                {
                    using (var query = db.QueryMultiple("Gral.UDP_Dashboard_Metrics",
                   null,
                   commandType: System.Data.CommandType.StoredProcedure))
                    {
                        var Customers = query.ReadSingle<int>();
                        var Cotizations = query.ReadSingle<int>();
                        var Sales = query.ReadSingle<int>();
                        var Campaigns = query.ReadSingle<int>();
                        var prueba = new
                        {
                            Customers,
                            Cotizations,
                            Sales,
                            Campaigns
                        };
                        return prueba;

                    }
                }
                else
                {
                    using (var query = db.QueryMultiple("Gral.UDP_Dashboard_Metrics_Filter",
                     new { Id },
                     commandType: System.Data.CommandType.StoredProcedure))
                    {
                        //data.Activity = new System.Dynamic.ExpandoObject();
                        var Customers = query.ReadSingle<int>();
                        var Cotizations = query.ReadSingle<int>();
                        var Sales = query.ReadSingle<int>();
                        var Campaigns = query.ReadSingle<int>();
                        var prueba = new
                        {
                            Customers,
                            Cotizations,
                            Sales,
                            Campaigns
                        };
                        return prueba;
                    }
                }
                


            }
        }
        public IEnumerable<View_tbCotizations_List> LastCotizations(int Id)
        {
            using var db = new LogisticSmartContext();

            var rol = db.tbUsers
            .Where(x => x.usu_Id == Id)
            .Select(x => x.rol_Id)
            .FirstOrDefault();
            if (rol == 3)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
                using var dbe = new SqlConnection(LogisticSmartContext.ConnectionString);
                var Cotizations = dbe.Query<View_tbCotizations_List>(ScriptsDataBase.UDP_Dashboard_Cotizations_Filter, parameters, commandType: CommandType.StoredProcedure);
                return Cotizations.ToList();
            }
            else
            {
             var  Cotizations = db.View_tbCotizations_List
               .Where(x=> x.cot_DateCreate >= DateTime.Now.AddDays(-30))
               .OrderByDescending(x => x.cot_DateCreate)
               .Take(5);
                return Cotizations.ToList();
            }
           
        }
        public IEnumerable<View_tbSaleOrders_List> LastSales(int Id)
        {
            using var db = new LogisticSmartContext();

            var rol = db.tbUsers
            .Where(x => x.usu_Id == Id)
            .Select(x => x.rol_Id)
            .FirstOrDefault();
            if (rol == 3)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
                using var dbe = new SqlConnection(LogisticSmartContext.ConnectionString);
                var Sales = dbe.Query<View_tbSaleOrders_List>(ScriptsDataBase.UDP_Dashboard_Sales_Filter, parameters, commandType: CommandType.StoredProcedure);
                return Sales.ToList();
            }
            else
            {
                var Sales = db.View_tbSaleOrders_List
                  .Where(x=> x.sor_DateCreate >= DateTime.Now.AddDays(-30))
                  .OrderByDescending(x => x.sor_DateCreate)
                  .Take(5);
                return Sales.ToList();
            }

        }
        public IEnumerable<TopProductsModel> TopProducts()
        {
        
            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            var Products = db.Query<TopProductsModel>(ScriptsDataBase.UDP_Top10Products_Metrics, null, commandType: CommandType.StoredProcedure);
    
            return Products.ToList();
        }

        public IEnumerable<TopCustomersModel> TopCustomers()
        {

            using var db = new SqlConnection(LogisticSmartContext.ConnectionString);
            var Customers = db.Query<TopCustomersModel>(ScriptsDataBase.UDP_TopCustomerOrders_Metrics, null, commandType: CommandType.StoredProcedure);

            return Customers.ToList();
        }

    }

}