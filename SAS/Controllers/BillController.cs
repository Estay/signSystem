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
    public class BillController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Bill/

        public ActionResult Index()
        {
            return View(db.orders.ToList());
        }

        //
        // GET: /Bill/Details/5

        public ActionResult Details(int id = 0)
        {
            Order_info order_info = db.orders.Find(id);
            if (order_info == null)
            {
                return HttpNotFound();
            }
            return View(order_info);
        }

        //
        // GET: /Bill/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Bill/Create

        [HttpPost]
        public ActionResult Create(Order_info order_info)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(order_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order_info);
        }

        //
        // GET: /Bill/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Order_info order_info = db.orders.Find(id);
            if (order_info == null)
            {
                return HttpNotFound();
            }
            return View(order_info);
        }

        //
        // POST: /Bill/Edit/5

        [HttpPost]
        public ActionResult Edit(Order_info order_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order_info);
        }

        //
        // GET: /Bill/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Order_info order_info = db.orders.Find(id);
            if (order_info == null)
            {
                return HttpNotFound();
            }
            return View(order_info);
        }

        //
        // POST: /Bill/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_info order_info = db.orders.Find(id);
            db.orders.Remove(order_info);
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