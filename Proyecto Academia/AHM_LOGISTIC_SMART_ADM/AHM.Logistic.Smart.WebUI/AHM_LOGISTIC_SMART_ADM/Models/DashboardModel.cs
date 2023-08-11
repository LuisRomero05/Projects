using AHM.Logistic.Smart.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Models
{
    public class DashboardModel
    {
        public CotizationsViewModel cotizations { get; set; }
        public SalesOrderViewModel sales { get; set; }
    }
}
