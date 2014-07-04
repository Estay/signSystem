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
            hotelId = "48385";
            ViewBag.HoltelId = hotelId;
          
            getRooms(Convert.ToInt32(hotelId));
            string f = hotelId;
            getfacilities();
            return View();
        }
        //修改房型
        public ActionResult update(string roomId)
        {
            int RId = Convert.ToInt32(roomId);
            //getRooms(Convert.ToInt32(hotelId));
            //string f = hotelId;
            getfacilities();
             hotel_room_info room= (from h in db.room where h.room_id == RId select h).Single();
             getRooms(room.hotel_id);
             return View("Create", room);
        }
        //删除房型
        public ActionResult remove(string roomId)
        {
            int RId = Convert.ToInt32(roomId);
            //getRooms(Convert.ToInt32(hotelId));
            //string f = hotelId;
            getfacilities();
            hotel_room_info room = db.room.Find(Convert.ToInt32(roomId));

            db.room.Remove(room);
            db.SaveChanges();
           
            getRooms(room.hotel_id);
            return View("Create", new hotel_room_info());
        }
        public void getfacilities()
        {
            ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
        }
        //
        // POST: /Room/Create

        [HttpPost]
        public ActionResult Create(hotel_room_info hotel_room_info)
        {
            
           

            getfacilities();
           
            hotel_room_info.hotel_id = 48385;
            hotel_room_info.h_r_id = "004";
            hotel_room_info.h_r_utime = DateTime.Now;
            hotel_room_info.h_r_ctime = DateTime.Now;
            hotel_room_info.h_r_bed_type = "大床房";
            hotel_room_info.h_r_bed_number = "1";
            hotel_room_info.h_r_state = true;
            hotel_room_info.h_r_reserve = 3;
            var errors = ModelState.Values.SelectMany(v => v.Errors); 
            if (ModelState.IsValid)
            {
               
                return RedirectToAction("Index");
            }
            db.room.Add(hotel_room_info);
            db.SaveChanges();
            getRooms(hotel_room_info.hotel_id);
            ViewBag.HoltelId = hotel_room_info.hotel_id;
            return View();
        }
        public void getRooms(int hotel_id)
        {
            //List<hotel_room_info> roomsList = (from r in db.room where r.hotel_id == hotel_id select r).ToList();
            ViewData["rooms"] = DBhelp.getRooms(hotel_id); 
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