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
            int Id, RoomId,Sell, MyStutus; DateTime _startDate, _EndDate;

            int.TryParse(CanSell, out Sell); int.TryParse(id, out Id); int.TryParse(roomId, out RoomId); DateTime.TryParse(startDate, out _startDate); DateTime.TryParse(EndDate, out _EndDate); int.TryParse(status, out MyStutus);

            string sql = string.Format("update RoomStatus set aviebeds={0},r_status={1} where hotel_id={2} and room_id={0} and Effectdate  between '{2}' and '{3}'", Sell, MyStutus, Id, RoomId, _startDate.ToString("yyyy-MM-dd"), _EndDate.ToString("yyyy-MM-dd"));
            if (DBhelp.ExcuteTableBySQL(sql) > 0)
                return 1;
            else
                return 0;
            // return View("MyPrix", getData(id, startDate, EndDate));
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