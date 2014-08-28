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
            Session[code] = code;
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
            string code = Session["code"].ToString();
            if (code == merchant_info.guid)
            {
                using (db = new HotelDBContent())
                {
                    string pass=(from m in db.Merchant_infos where m.tel == merchant_info.tel || m.name == merchant_info.name select m.password).SingleOrDefault();
                    if (pass == merchant_info.password)
                    {
                        Session["userName"] = merchant_info.tel;
                        Session.Remove("code");
                        return RedirectToAction("create", "addHotel");
                    }
                    else
                    {
                        ViewBag.LoginInfo = "用户名或者密码错误";
                    }

                }
            }else
                ViewBag.LoginInfo = "验证码错误,请输入正确的验证码";
            return View("signLogin");;
        }

        //
        // GET: /Login/Edit/5

        public ActionResult Edit(string id = null)
        {
            Merchant_info merchant_info = db.Merchant_infos.Find(id);
            if (merchant_info == null)
            {
                return HttpNotFound();
            }
            return View(merchant_info);
        }

        //
        // POST: /Login/Edit/5

        [HttpPost]
        public ActionResult Edit(Merchant_info merchant_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchant_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(merchant_info);
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