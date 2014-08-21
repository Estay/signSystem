using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.DBC;
using SAS.Models;

namespace SAS.Controllers
{
    public class RStatusController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /RStatus/

        public ActionResult Index()
        {
            return View(db.hotel.ToList());
        }

        //
        // GET: /RStatus/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    Room_status_info room_status_info = db.hotel.Find(id);
        //    if (room_status_info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(room_status_info);
        //}

        //
        // GET: /RStatus/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RStatus/Create

        [HttpPost]
        public ActionResult Create(Room_status_info room_status_info)
        {
            if (ModelState.IsValid)
            {
                db.rstatus.Add(room_status_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room_status_info);
        }

        //
        // GET: /RStatus/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Room_status_info room_status_info = db.hotel.Find(id);
        //    if (room_status_info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(room_status_info);
        //}

        //
        // POST: /RStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(Room_status_info room_status_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room_status_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room_status_info);
        }

        //
        // GET: /RStatus/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    Room_status_info room_status_info = db.hotel.Find(id);
        //    if (room_status_info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(room_status_info);
        //}

        //
        // POST: /RStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Room_status_info room_status_info = db.rstatus.Find(id);
            db.rstatus.Remove(room_status_info);
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