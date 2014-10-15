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
    public class UserController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /User/

        public ActionResult queryUser()
        {
            return View("MyUser");
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(string id = null)
        {
            Merchant_info merchant_info = db.Merchant_infos.Find(id);
            if (merchant_info == null)
            {
                return HttpNotFound();
            }
            return View(merchant_info);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(Merchant_info merchant_info)
        {
            if (ModelState.IsValid)
            {
                db.Merchant_infos.Add(merchant_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(merchant_info);
        }

        //
        // GET: /User/Edit/5

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
        // POST: /User/Edit/5

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
        // GET: /User/Delete/5

        public ActionResult Delete(string id = null)
        {
            Merchant_info merchant_info = db.Merchant_infos.Find(id);
            if (merchant_info == null)
            {
                return HttpNotFound();
            }
            return View(merchant_info);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Merchant_info merchant_info = db.Merchant_infos.Find(id);
            db.Merchant_infos.Remove(merchant_info);
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