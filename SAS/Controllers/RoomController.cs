using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.help;
using SAS.Models;

namespace SAS.Controllers
{
    public class RoomController : Controller
    {
        private hotel_room_infoDBContent db = new hotel_room_infoDBContent();

        //
        // GET: /Room/

        public ActionResult Index(object t)
        {
            return View(db.room.ToList());
        }

        //
        // GET: /Room/Details/5

        public ActionResult Details(int id = 0)
        {
            hotel_room_info hotel_room_info = db.room.Find(id);
            if (hotel_room_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_room_info);
        }

        //
        // GET: /Room/Create

        public ActionResult Create(string hotelId)
        {
            string f = hotelId;
            ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
            return View();
        }

        //
        // POST: /Room/Create

        [HttpPost]
        public ActionResult Create(hotel_room_info hotel_room_info)
        {
            if (ModelState.IsValid)
            {
                db.room.Add(hotel_room_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotel_room_info);
        }

        //
        // GET: /Room/Edit/5

        public ActionResult Edit(int id = 0)
        {
            hotel_room_info hotel_room_info = db.room.Find(id);
            if (hotel_room_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_room_info);
        }

        //
        // POST: /Room/Edit/5

        [HttpPost]
        public ActionResult Edit(hotel_room_info hotel_room_info)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(hotel_room_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel_room_info);
        }

        //
        // GET: /Room/Delete/5

        public ActionResult Delete(int id = 0)
        {
            hotel_room_info hotel_room_info = db.room.Find(id);
            if (hotel_room_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_room_info);
        }

        //
        // POST: /Room/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            hotel_room_info hotel_room_info = db.room.Find(id);
            db.room.Remove(hotel_room_info);
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