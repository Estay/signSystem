﻿using System;
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
    public class PriceController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Price/
        DateTime start = DateTime.Now;
        DateTime end;
        public ActionResult Index()
        {
            return View(db.price.ToList());
        }

        public ActionResult MyPrice(string Id,string startDate,string EndDate)
        {
            
            return View("MyPrix", getData(Id, startDate, EndDate));
        }
        public hotel_info getData(string Id, string startDate, string EndDate)
        {
            string uId = "test1";

            int hotel_id = 0;
            int.TryParse(Id, out hotel_id);


            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(startDate))
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
          
            var f = (from p in db.realPrices where p.Effectdate >= start && p.Effectdate <= end && p.Hotel_id == hotel_id select p).ToList();
            hotel.Room.Prices.PriceList = f;
            Dictionary<string, string> dates = new Dictionary<string, string>();
            int t = Convert.ToInt32((end - start).TotalDays)+1;
            for (int i = 0; i < t; i++)
            {
                DateTime d = start.AddDays(i);
                string day = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(d.DayOfWeek).Substring(2);
                dates.Add(d.ToString("MM-dd"), day);
            }
            ViewData["dates"] = dates; ViewBag.Id =Convert.ToInt32(Id);
            return hotel;
        }
        //房价修改接口
            [HttpPost]
        public ActionResult update(string id, string roomId, string startDate, string EndDate, string value)
        {
            int Id;
            decimal price;
            decimal.TryParse(value, out price);
            int.TryParse(roomId, out Id);
            DateTime.TryParse(startDate, out start);
            DateTime.TryParse(startDate, out end);
            string sql = string.Format("update  hotel_room_RP_price set Room_rp_price={2} where room_id={0}  Effectdate  between ({2},{3})", roomId, price, start, end);
            if (DBhelp.ExcuteTableBySQL(sql) > 0)
                ViewBag.sign = 1;
            else
                ViewBag.sign = 0;
            return View("MyPrix", getData(id, startDate, EndDate));
        }

        public ActionResult uPrice(string id, string roomId, string startDate, string EndDate, string value)
        {
            int Id;
            decimal price;
            decimal.TryParse(value, out price);
            int.TryParse(roomId, out Id);
            DateTime.TryParse(startDate, out start);
            DateTime.TryParse(EndDate, out end);
            string sql = string.Format("update  hotel_room_RP_price set Room_rp_price={1} where room_id={0} and Effectdate  between '{2}' and '{3}'", roomId,price, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));
            if (DBhelp.ExcuteTableBySQL(sql) > 0)
                ViewBag.sign = 1;
            else
                ViewBag.sign = 0;
            return View("MyPrix", getData(id, startDate, EndDate));
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