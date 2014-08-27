using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.Models;
using SAS.DBC;

namespace SAS.Controllers
{
    public class LoginController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Login/

        public ActionResult myLogin()
        {
            return View("signLogin");
        }

        //
        // GET: /Login/Details/5

        public ActionResult Details(string id = null)
        {
            Merchant_info merchant_info = db.Merchant_info.Find(id);
            if (merchant_info == null)
            {
                return HttpNotFound();
            }
            return View(merchant_info);
        }

        //
        // GET: /Login/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Login/Create

        [HttpPost]
        public ActionResult Create(Merchant_info merchant_info)
        {
            if (ModelState.IsValid)
            {
                db.Merchant_info.Add(merchant_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(merchant_info);
        }

        //
        // GET: /Login/Edit/5

        public ActionResult Edit(string id = null)
        {
            Merchant_info merchant_info = db.Merchant_info.Find(id);
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

        public ActionResult Delete(string id = null)
        {
            Merchant_info merchant_info = db.Merchant_info.Find(id);
            if (merchant_info == null)
            {
                return HttpNotFound();
            }
            return View(merchant_info);
        }

        //
        // POST: /Login/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Merchant_info merchant_info = db.Merchant_info.Find(id);
            db.Merchant_info.Remove(merchant_info);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}