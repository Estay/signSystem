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
        public ActionResult MyPrice()
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);

            var f = (from p in db.price where p.room_rp_start_time > d1 && p.room_rp_start_time < d2 && p.hotel_id == 55190 && p.room_id == 352246 select p).ToList();
            return View("MyPrix", f);
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
        public ActionResult Create(List<hotel_room_RP_price_info> hotel_room_RP_price_info)
        {
            // string tableName = TableHotel_Info;
            string tableName = "hotel_room_rp_price_info";
            //存储变化的酒店信息(空表)
            DataTable dtPrice = new DBhelp().getDataTable(DBhelp.getTStructByTName("hotel_room_rp_price_info"));
            int hotelId = 0;
            foreach (var p in hotel_room_RP_price_info)
            {
                if (dtPrice.Rows.Count == 0)
                    hotelId = p.hotel_id;
                DBhelp.InserDataTable(ref dtPrice, typeof(hotel_room_RP_price_info), p);
            }
            if(DBhelp.copyDataToServer(dtPrice, tableName))
                return RedirectToAction("Create", "Image", new { hotelId = hotelId }); 
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