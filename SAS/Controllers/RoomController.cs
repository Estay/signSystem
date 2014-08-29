using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.DBC;
using SAS.help;
using SAS.Models;

namespace SAS.Controllers
{
    public class RoomController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Room/

        public ActionResult Index(object t)
        {
            return View(db.rooms.ToList());
        }

        //
        // GET: /Room/Details/5

        public ActionResult Details(int id = 0)
        {
            ViewBag.sign = 1;
            hotel_room_info hotel_room_info = db.rooms.Find(id);
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
            ViewData["Tag"] = "增加房型";
            ViewBag.sign = 3;
            //hotelId = "48385";
            ViewBag.HoltelId = hotelId;
            ViewBag.Tag = "增加房型";
            getRooms(Convert.ToInt32(hotelId));
            string f = hotelId;
            getfacilities();
            return View(new hotel_room_info());
        }
        //修改房型
        public ActionResult update(string roomId)
        {

            ViewBag.Tag = "修改房型";
            int RId = Convert.ToInt32(roomId);
            //getRooms(Convert.ToInt32(hotelId));
            //string f = hotelId;
            getfacilities();
             hotel_room_info room= (from h in db.rooms where h.room_id == RId select h).Single();
             getRooms(room.hotel_id);
             ViewBag.HoltelId = room.hotel_id;
             return View("Create", room);
        }
        //删除房型
        public ActionResult remove(string roomId)
        {
            int RId = Convert.ToInt32(roomId);
            //getRooms(Convert.ToInt32(hotelId));
            //string f = hotelId;
            getfacilities();
            hotel_room_info room = db.rooms.Find(Convert.ToInt32(roomId));
            if (room != null)
            {

                db.rooms.Remove(room);
                if (db.SaveChanges() > 0)
                    ViewBag.sign = 1;
                else
                    ViewBag.sign = 0;
            }
            ViewBag.HoltelId = room.hotel_id;
            getRooms(room.hotel_id);
            return View("Create", new hotel_room_info());
        }
        //验证房型是否存在
        public int IsOk(string hotelId, string text)
        {
            int hotel_id = Convert.ToInt32(hotelId);
            using (db = new HotelDBContent())
            {
                if ((from h in db.rooms where h.h_r_name_cn == text && h.hotel_id == hotel_id select h).Count() > 0)
                    return 0;
                else
                    return 1;
            }
        }

        public void getfacilities()
        {
            ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
        }
        //
        // POST: /Room/Create

        /// <summary>
        ///新建公寓增加房型
        /// </summary>
        /// <param name="hotel_room_info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(hotel_room_info hotel_room_info)
        {

            int sign = 0;
            if ( hotel_room_info.room_id> 0)
            {

                sign = hotel_room_info.updateRoom(hotel_room_info) == true ? 1 : 0; ;
                
            }
            else
            {
                hotel_room_info.h_r_id = "004";
                hotel_room_info.h_r_utime = DateTime.Now;
                hotel_room_info.h_r_ctime = DateTime.Now;            
                hotel_room_info.h_r_state = true;
                hotel_room_info.h_r_reserve = 3;
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                db.rooms.Add(hotel_room_info);
                sign = db.SaveChanges()>0? 1 : 0; ;
            }
           
            getfacilities();
            getRooms(hotel_room_info.hotel_id);
            ViewBag.HoltelId = hotel_room_info.hotel_id;
            ViewBag.Tag = "增加房型";
            getRooms(hotel_room_info.hotel_id);
            ViewBag.sign = sign;
          
            return View(new hotel_room_info());
        }
        public void getRooms(int hotel_id)
        {
            //List<hotel_room_info> roomsList = (from r in db.room where r.hotel_id == hotel_id select r).ToList();
            ViewData["rooms"] = DBhelp.getRooms(hotel_id);
            ViewData["bedTypes"] = new hotel_room_info().getBedType();
        }
        //
        // GET: /Room/Edit/5

        public ActionResult Edit(int id = 0)
        {
            hotel_room_info hotel_room_info = db.rooms.Find(id);
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
            //db.Entry(hotel_room_info).State=e
            if (ModelState.IsValid)
            {
                db.Entry(hotel_room_info).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel_room_info);
        }

        //
        // GET: /Room/Delete/5

        public ActionResult Delete(int id = 0)
        {
            hotel_room_info hotel_room_info = db.rooms.Find(id);
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
            hotel_room_info hotel_room_info = db.rooms.Find(id);
            db.rooms.Remove(hotel_room_info);
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