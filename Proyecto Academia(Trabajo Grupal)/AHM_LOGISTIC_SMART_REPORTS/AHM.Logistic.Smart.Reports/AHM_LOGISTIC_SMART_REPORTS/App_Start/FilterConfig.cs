using System.Web;
using System.Web.Mvc;

namespace AHM_LOGISTIC_SMART_REPORTS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
