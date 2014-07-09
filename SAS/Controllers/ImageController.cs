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
    public class ImageController : Controller
    {
        private PictureDBContent db = new PictureDBContent();

        //
        // GET: /Image/

        public ActionResult Index()
        {
            return View(db.room.ToList());
        }

        //
        // GET: /Image/Details/5

        public ActionResult Details(int id = 0)
        {
            hotel_picture_info hotel_picture_info = db.room.Find(id);
            if (hotel_picture_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_picture_info);
        }

        //
        // GET: /Image/Create

        public ActionResult Create(string hotelId)
        {
            hotelId = "48385";
            ViewBag.HoltelId = hotelId;
            ViewData["ImageTypes"] = new hotel_picture_info().getImageType();
            getRooms(Convert.ToInt32(hotelId));
            return View();
        }

        //
        // POST: /Image/Create

        [HttpPost]
        public ActionResult Create(hotel_picture_info hotel_picture_info)
        {
            if (ModelState.IsValid)
            {
                db.room.Add(hotel_picture_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotel_picture_info);
        }

        //
        // GET: /Image/Edit/5

        public ActionResult Edit(int id = 0)
        {
            hotel_picture_info hotel_picture_info = db.room.Find(id);
            if (hotel_picture_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_picture_info);
        }

        //
        // POST: /Image/Edit/5

        [HttpPost]
        public ActionResult Edit(hotel_picture_info hotel_picture_info)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(hotel_picture_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel_picture_info);
        }

        //
        // GET: /Image/Delete/5

        public ActionResult Delete(int id = 0)
        {
            hotel_picture_info hotel_picture_info = db.room.Find(id);
            if (hotel_picture_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_picture_info);
        }

        //
        // POST: /Image/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            hotel_picture_info hotel_picture_info = db.room.Find(id);
            db.room.Remove(hotel_picture_info);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public void getRooms(int hotel_id)
        {
            //List<hotel_room_info> roomsList = (from r in db.room where r.hotel_id == hotel_id select r).ToList();
            ViewData["rooms"] = DBhelp.getRooms(hotel_id);
        }
    }
}