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
    public class PriceController : Controller
    {
        private PriceDBContent db = new PriceDBContent();

        //
        // GET: /Price/

        public ActionResult Index()
        {
            return View(db.price.ToList());
        }

        //
        // GET: /Price/Details/5

        public ActionResult Details(int id = 0)
        {
            hotel_room_RP_price_info hotel_room_rp_price_info = db.price.Find(id);
            if (hotel_room_rp_price_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_room_rp_price_info);
        }

        //
        // GET: /Price/Create

        public ActionResult Create(string hotelId)
        {
            getRooms(Convert.ToInt32(hotelId));
            return View();
        }

        //
        // POST: /Price/Create

        [HttpPost]
        //public ActionResult Create(hotel_room_RP_price_info hotel_room_rp_price_info)
        public ActionResult Create(IEnumerable<hotel_room_RP_price_info>  hotel_room_rp_price_info)
        {
            //if (ModelState.IsValid)
            //{
            //    db.price.Add(hotel_room_rp_price_info);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
           // hotel_room_rp_price_info
            return View();
        }
        public void getRooms(int hotel_id)
        {
            //List<hotel_room_info> roomsList = (from r in db.room where r.hotel_id == hotel_id select r).ToList();
            ViewData["rooms"] = DBhelp.getRooms(hotel_id);
        }
        //
        // GET: /Price/Edit/5

        public ActionResult Edit(int id = 0)
        {
            hotel_room_RP_price_info hotel_room_rp_price_info = db.price.Find(id);
            if (hotel_room_rp_price_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_room_rp_price_info);
        }

        //
        // POST: /Price/Edit/5

        [HttpPost]
        public ActionResult Edit(hotel_room_RP_price_info hotel_room_rp_price_info)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(hotel_room_rp_price_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel_room_rp_price_info);
        }

        //
        // GET: /Price/Delete/5

        public ActionResult Delete(int id = 0)
        {
            hotel_room_RP_price_info hotel_room_rp_price_info = db.price.Find(id);
            if (hotel_room_rp_price_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_room_rp_price_info);
        }

        //
        // POST: /Price/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            hotel_room_RP_price_info hotel_room_rp_price_info = db.price.Find(id);
            db.price.Remove(hotel_room_rp_price_info);
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