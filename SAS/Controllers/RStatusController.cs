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
    public class RStatusController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /RStatus/
        DateTime start = DateTime.Now;
        DateTime end;
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
        public ActionResult Status(string Id, string startDate, string EndDate)
        {
            return View("MyStatus", getData(Id, startDate, EndDate));
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
        public hotel_info getData(string Id, string startDate, string EndDate)
        {
            string uId = "test1";

            int hotel_id = 0;
            int.TryParse(Id, out hotel_id);


            if (string.IsNullOrEmpty(startDate))
            {
                start = DateTime.Now.Date; end = start.AddDays(14);
            }
            else
            {
                DateTime.TryParse(startDate, out start); end = start.AddDays(14);
            }

            hotel_info hotel = new hotel_info();
            var hotels = HotelInfoHelp.getHotlList("");
            hotel.HotelList = hotels;
            if (string.IsNullOrEmpty(Id) && hotels.Count > 0)
                hotel_id = hotels[0].hotel_id;

            hotel.Room.RoomList = HotelInfoHelp.getRooms(hotel_id);

            var f = (from p in db.roomStatuss where p.r_s_time >= start && p.r_s_time <= end && p.hotel_id == hotel_id select p).ToList();
            hotel.Room.RoomStatus.RoomStatusList = f;
            
       
            ViewBag.startDate = start.ToString("yyyy-MM-dd");
            ViewData["dates"] =HotelInfoHelp.getDate(start,end) ; ViewBag.Id = Convert.ToInt32(Id);
            return hotel;
        }
        //修改房态
        public int uStatus(string id, string roomId, string startDate, string EndDate, string CanSell, string status)
        {
            int result = 0;
            int Id, room_id, count = 0; int Rstatus; int RCanSell; int.TryParse(status, out Rstatus); int.TryParse(CanSell, out RCanSell); int.TryParse(id, out Id); int.TryParse(roomId, out room_id); DateTime.TryParse(startDate, out start); DateTime.TryParse(EndDate, out end);
            using (db = new HotelDBContent())
            {
                count = (from r in db.rooms where r.hotel_id == Id && r.room_id == room_id select r).Count();
            }
            if (count > 0)
                  result = new RoomStatus_batch().insertStatuBatch(new RoomStatus_batch() { hotel_id = Id, room_id = room_id, r_s_time = start, EndDate = end, eBeds = RCanSell }) == true && DBhelp.CallProc(room_id, "proc_hotel_room_ebeds_batch_roomid") == true ? 1 : 0;
          
               // result = new RoomStatus_batch().insertStatuBatch(new RoomStatus_batch() { hotel_id = Id, room_id = room_id, r_s_time = start, EndDate = end, eBeds = RCanSell }) == true?1 : 0;
           
          return result;
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