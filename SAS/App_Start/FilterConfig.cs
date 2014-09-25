using System.Web;
using System.Web.Mvc;
using SAS.help;
namespace SAS
{
    public class FilterConfig
    {
       
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            
           //filters.Add(new CheckLogin() { IsCheck = true });
           filters.Add(new HandleErrorAttribute());
        }
    }
}