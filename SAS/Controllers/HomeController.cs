using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.help;

namespace SAS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            getIndexData();
            return View();
        }
        /// <summary>
        /// 首页数据统计
        /// </summary>
        public void getIndexData()
        {
            decimal orderCount = 0, noreplyComment = 0, totalPrice = 0, commission = 0, guranteePrice = 0;
            new DBhelp().getIndexData(out  orderCount, out  noreplyComment, out  totalPrice, out  commission, out  guranteePrice);


            ViewBag.newOrder = orderCount;   //新单

            ViewBag.NewComment = noreplyComment; //未回复的评论

            ViewBag.totalpPrice = totalPrice;  //上个月总收入
            ViewBag.commission = 0;   //佣金

            ViewBag.guranteePrice = guranteePrice; //担保金额
        }

        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }
    }
}
