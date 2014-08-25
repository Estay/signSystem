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
        //获取房态数据
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
          
            var f = (from p in db.realPrices where p.Effectdate >= start && p.Effectdate <= end && p.Hotel_id == hotel_id select p).ToList();
            hotel.Room.Prices.PriceList = f;
            Dictionary<string, string> dates = new Dictionary<string, string>();
       
            ViewBag.startDate = start.ToString("yyyy-MM-dd");
            ViewData["dates"] = HotelInfoHelp.getDate(start, end); ViewBag.Id = Convert.ToInt32(Id);
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
        //修改房价
        public int uPrice(string id, string roomId, string startDate, string EndDate, string value)
        {
            int Id;
            decimal price;
            decimal.TryParse(value, out price);
            int.TryParse(roomId, out Id);
            DateTime.TryParse(startDate, out start);
            DateTime.TryParse(EndDate, out end);
            string sql = string.Format("update  hotel_room_RP_price set Room_rp_price={1} where room_id={0} and Effectdate  between '{2}' and '{3}'", roomId,price, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));
            if (DBhelp.ExcuteTableBySQL(sql) > 0)
                return 1;
            else
                return 0;
           // return View("MyPrix", getData(id, startDate, EndDate));
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

        /// <summary>
        /// 新建公寓价格录入
        /// </summary>
        /// <param name="hotel_room_RP_price_info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(List<hotel_room_RP_price_info> hotel_room_RP_price_info)
        {
         
           
            int hotelId = 0;
          
            //遍历价格集合
            foreach (var p in hotel_room_RP_price_info)
            {
               
                 int.TryParse(p.hotel_id.ToString(),out hotelId);
                 Hotel_room_RP_info rp = new Hotel_room_RP_info();
                 rp.h_room_rp_name_cn = "标准价";
                 rp.hotel_id = p.hotel_id;
                 Hotel_room_RP_price_batch pBacth = new Hotel_room_RP_price_batch();
                 pBacth.Addbed = -1;
                 pBacth.HpStatus = 0;
                 pBacth.Room_rp_id=help.HotelInfoHelp.getRatePlanId(rp);
                 DateTime start=DateTime.Now.Date;DateTime end=DateTime.Now.Date;
                 pBacth.Room_rp_start_time =start ;
                 pBacth.Room_rp_end_time = end;
                 pBacth.Room_id = p.room_id;
                 pBacth.Hotel_id = p.hotel_id;
                 pBacth.Price = p.room_rp_price;
                 pBacth.Idate = DateTime.Now;
                 pBacth.Hpdate = DateTime.Now;
                 pBacth.AuditDate = DateTime.Now;

                 Room_status_info roomStatus = new Room_status_info();
                 roomStatus.r_s_time = start;
                 roomStatus.EndDate = end;
                 roomStatus.room_id = p.room_id;
                 roomStatus.hotel_id = p.hotel_id;
                 roomStatus.OverBooking = true;
                 string number = (from r in db.rooms where r.room_id == p.room_id select r.h_r_house_number).SingleOrDefault(); int roomNubmer; int.TryParse(number, out roomNubmer);

                 roomStatus.r_s_number = roomNubmer;
                 if (ModelState.IsValid)
                 {
                     db.publicPrices.Add(pBacth);
                     db.SaveChanges();
                 }

                
            }
           
           return RedirectToAction("Create", "Image", new { hotelId = hotelId }); 

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