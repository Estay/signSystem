﻿using System.Web;
using System.Web.Mvc;
using SAS.help;
namespace SAS
{
    public class FilterConfig
    {
       
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new CheckLogin）;
            filters.Add(new CheckLogin());
            //filters.Add(new HandleErrorAttribute());
        }
    }
}