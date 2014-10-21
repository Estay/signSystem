using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SAS.Models;
using SAS.DBC;
using SAS.help;


namespace SAS.Controllers
{
    [CheckLogin(IsCheck = false)]
    public class LoginController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Login/

        public ActionResult myLogin()
        {
            string code = StringHelper.CreateValidateCode(5);
           
            byte[] bytes = StringHelper.CreateValidateGraphic(code);
            Session["code"] = code;
           ViewBag.image= File(bytes, @"image/jpeg");
            return View("signLogin");
        }
        public ActionResult builImage()
        {
            string code = StringHelper.CreateValidateCode(5);

            byte[] bytes = StringHelper.CreateValidateGraphic(code);
            Session["code"] = code;
            ViewBag.image = File(bytes, @"image/jpeg");
            return File(bytes, @"image/jpeg");
        }
       
       

        //
        // GET: /Login/Details/5



        //
        // GET: /Login/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Login/Create

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="merchant_info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginSubmit(Merchant_info merchant_info)
        {
            string code = Session["code"]!=null?Session["code"].ToString():"";
            if (code == merchant_info.guid)
            {
                using (db = new HotelDBContent())
                {
                    Merchant_info mer = (from m in db.Merchant_infos where m.tel == merchant_info.tel &&m.status==true select m).SingleOrDefault();
                    if (mer != null)
                    {
                      //  new help.HotelInfoHelp().Md5(merchant_info.password);
                        //  if (mer.password == merchant_info.password)
                        if (mer.password == new help.HotelInfoHelp().Md5(merchant_info.password))
                        {

                            string limit = string.Empty;
                            Session["menu"] = new help.HotelInfoHelp().GetLimit(mer, out limit); Session["limit"] = limit; Session["limitHotelId"] = mer.limitHotelId;

                            Session["userName"] = mer.name;
                            Session["uid"] = mer.tel;
                            Session.Remove("code");
                            return RedirectToAction("create", "addHotel");
                        }
                        else
                        {
                            ViewBag.LoginInfo = "用户名或者密码错误";
                        }
                    }
                    else
                    {
                        ViewBag.LoginInfo = "用户名或者密码错误";
                    }
                    
                }
            }else
                ViewBag.LoginInfo = "验证码错误,请输入正确的验证码";
            Session.Remove("code");
            ViewBag.userName = merchant_info.tel; ViewBag.pass = merchant_info.password;
            return View("signLogin");;
        }
      
        public ActionResult temp()
        {
            ViewBag.username = Session["username"];
            ViewBag.code = Session["code"];
            return View();
        }
        /// <summary>
        /// 首页加载
        /// </summary>
        /// <returns></returns>
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
            object orderCount = null, noreplyComment = null, totalPrice = null, commission = null, guranteePrice = null;
            new DBhelp().getIndexData(out  orderCount,out  noreplyComment,out  totalPrice,out  commission,out  guranteePrice);


            ViewBag.newOrder = orderCount;   //新单

            ViewBag.NewComment = noreplyComment; //未回复的评论

            ViewBag.totalpPrice = totalPrice;  //上个月总收入
            ViewBag.commission = commission;   //佣金

            ViewBag.guranteePrice = guranteePrice; //担保金额
        }
        public ActionResult Logout()
        {
            if (Session["uid"] != null)
                Session.Remove("uid");
            if(Session["userName"] != null)
                Session.Remove("userName");
            
            return RedirectToAction("MyLogin", "Login");
        }
 
        //
        // GET: /Login/Delete/5



        //
        // POST: /Login/Delete/5

     

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}