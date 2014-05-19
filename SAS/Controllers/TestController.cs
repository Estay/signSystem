using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAS.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index(string name, int numTimes)
        {
            ViewBag.Name = name;
            ViewBag.NumTimes = numTimes;
            List<string> list = new List<string>();
            list.Add("a");
            list.Add("b");
            list.Add("c");
            list.Add("d");
            ViewBag.List = list;
            return View();
           // return "MVC Test";
        }
        public ActionResult gogo(string name, int numTimes)
        {
            ViewBag.Name = name;
            ViewBag.NumTimes = numTimes;
            List<string> list = new List<string>();
            list.Add("a");
            list.Add("b");
            list.Add("c");
            list.Add("d");
            ViewBag.List=list;
            //return "this is gogo name=" + name + "   numTimes="+numTimes;
            return View();
        }
    }
}
