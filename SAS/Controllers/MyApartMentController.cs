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
    public class MyApartMentController : BaseController
    {
        private HotelDBContent db = new HotelDBContent();
     
        // GET: /MyApartMent/
        int hotel_id = 0, result = 1;
        
        //酒店信息
        public ActionResult Hotel(string hotelId)
        {
          
            int.TryParse(hotelId, out hotel_id);
            HelperData();
            ViewBag.HotelId = hotel_id;
            var hotel=db.hotel.SingleOrDefault(h => h.hotel_id == hotel_id);
            if (hotel == null)
                return View(new hotel_info());
            else
                return View(hotel);
        }

        private void HelperData()
        {
            ViewData["DTime"] = new hotel_info().getDecorationTime();  //Theme
            ViewData["Themes"] = DBhelp.GetSelectDataByTable("hotel_theme_info");  //Theme
            ViewData["Category"] = DBhelp.GetSelectDataByTable("Hotel_theme_type_info"); ;//Category
            ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
            ViewData["services"] = DBhelp.GetSelectDataByTable("GeneralAmenities_info");//services
            ViewData["provice"] = DBhelp.GetSelectDataByTable("province_info");//provice   
        }
        [HttpPost]
        public ActionResult Hotel(hotel_info hotel_info)
        {
            try
            {
                hotel_info.source_id = Convert.ToInt32(help.StringHelper.appSettings("source_id")); hotel_info.h_id = ""; hotel_info.h_utime = DateTime.Now;
              
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if (ModelState.IsValid)
                {
                    db.Entry(hotel_info).State = EntityState.Modified;

                    result = db.SaveChanges() > 0 ? 0 : 1;

                }
            }
            catch (Exception e)
            {
                DBhelp.log("修改公寓失败"+e.ToString());
                result = 1;
           
            }
         
            HelperData();
            ViewBag.HotelId = hotel_info.hotel_id; ViewBag.sign = result;   
            return View(hotel_info);
        }
        //房型
        public ActionResult Room(string hotelId)
        {
            ViewBag.Tag = "增加房型";
            int.TryParse(hotelId, out hotel_id);
            getRooms(hotel_id);
            getfacilities();
            ViewBag.HoltelId = hotel_id;
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
            hotel_room_info room = (from h in db.rooms where h.room_id == RId select h).Single();
            getRooms(room.hotel_id);
            ViewBag.HoltelId = room.hotel_id;
            return View("Room", room);
        }
        //删除房型
        public ActionResult remove(string roomId)
        {
            ViewBag.Tag = "增加房型";
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
            return View("room", new hotel_room_info());
        }
        public void getfacilities()
        {
            ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
        }
        public void getRooms(int hotel_id)
        {
            //List<hotel_room_info> roomsList = (from r in db.room where r.hotel_id == hotel_id select r).ToList();
            ViewData["rooms"] = DBhelp.getRooms(hotel_id);
            ViewData["bedTypes"] = new hotel_room_info().getBedType();
        }
        //图片
        public ActionResult Image(string hotelId,string result)
        {
            ViewBag.sign = result;
            int.TryParse(hotelId, out hotel_id);
            ViewBag.HotelId = hotelId;
            ViewData["rooms"] = DBhelp.getRooms(hotel_id);
            ViewData["ImageTypes"] = new hotel_picture_info().getImageType();
            int[]rf = (from r in db.rooms where r.hotel_id == hotel_id select r.room_id).ToArray();           
            return View((from image in db.roomImages where rf.Contains(image.room_id) select image).ToList()); 
        }

        /// <summary>
        ///我的公寓增加房型
        /// </summary>
        /// <param name="hotel_room_info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RoomSubmit(hotel_room_info hotel_room_info)
        {

            int sign = 0;
            string guid=Guid.NewGuid().ToString();
            if (hotel_room_info.room_id > 0)
            {

                sign = hotel_room_info.updateRoom(hotel_room_info) == true ? 1 : 0; ;

            }
            else
            {

                hotel_room_info.h_r_id = guid;
                hotel_room_info.h_r_utime = DateTime.Now;
                hotel_room_info.h_r_ctime = DateTime.Now;
                hotel_room_info.h_r_state = true;
                hotel_room_info.h_r_reserve = 3;
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                db.rooms.Add(hotel_room_info);
                sign = db.SaveChanges() > 0 ? 1 : 0;
                sign=sign > 0 ? FilishedRoom((from r in db.rooms where r.h_r_id==guid select r).SingleOrDefault()) : 0;
                
            }

            getfacilities();
            getRooms(hotel_room_info.hotel_id);
            ViewBag.sign = sign;
            ViewBag.Tag = "增加房型";
            ViewBag.HoltelId = hotel_room_info.hotel_id;
            return View("Room", new hotel_room_info());
        }

  
        //删除公寓
        public int delApart(string hotelId)
        {
            try
            {
                
                int.TryParse(hotelId, out hotel_id); string u_id = new HotelInfoHelp().getUId();
                using (db = new HotelDBContent())
                {
                    ((from h in db.hotel where h.hotel_id == hotel_id && h.source_id == 5 && h.u_id == u_id select h).SingleOrDefault()).h_state=false;
                    result = db.SaveChanges() > 0 ? 1 : 0;
               }
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        //
        // GET: /MyApartMent/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MyApartMent/Create

        [HttpPost]
        public ActionResult Create(hotel_info hotel_info)
        {
            if (ModelState.IsValid)
            {
                db.hotel.Add(hotel_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotel_info);
        }
        [HttpPost]
        public ActionResult ImageDone(string hotelId)
        {
            try
            {
                int hotel_id;
                if (int.TryParse(hotelId, out hotel_id))
                {
                    if ((from h in db.hotel where h.hotel_id == hotel_id select h).Count() > 0)
                    {
                        string sql = string.Format("update hotel_room_picture_info set state=1 where room_id in(select room_id from hotel_room_info where hotel_id in({0}))", hotel_id);
                        result= DBhelp.ExcuteTableBySQL(sql) > 0?1:0;                        

                    }
                }
                else
                {
                    DBhelp.log("图片提交转换出错"); result = 0;

                }
            }
            catch (Exception ex)
            {

                DBhelp.log("图片提交" + ex.ToString());
                result = 1;
            }
            result = 0;
            ViewBag.sign = result;
            if(result==1)
              return RedirectToAction("MyHotel", "MyApartMent");
            else
                return RedirectToAction("Image", "MyApartMent", new { hotelId = hotelId, result = result });
           
        }


     
   

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        //公寓列表
        public ActionResult MyHotel(string UId)
        {
           
          // UId = Session["uid"].ToString();
        //    UId = new help.HotelInfoHelp().getUId();
            //int.TryParse(hotelId, out hotel_id);
            //ViewData["DTime"] = new hotel_info().getDecorationTime();  //Theme
            //ViewData["Themes"] = DBhelp.GetSelectDataByTable("hotel_theme_info");  //Theme
            //ViewData["Category"] = DBhelp.GetSelectDataByTable("Hotel_theme_type_info"); ;//Category
            //ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
            //ViewData["services"] = DBhelp.GetSelectDataByTable("GeneralAmenities_info");//services
            //ViewData["provice"] = DBhelp.GetSelectDataByTable("province_info");//provice   
            //ViewBag.HotelId = hotel_id;         
            return View(new HotelInfoHelp().getHotlList(UId));
  
    
          
        }


        public int FilishedRoom(hotel_room_info r)
        {
            DateTime start = DateTime.Now.Date; DateTime end = DateTime.Now.AddYears(1).Date; bool re = false;
            //var rooms = (from h in db.rooms where h.hotel_id == hotelId && h.DefaultPrice > 0 select h).ToList();
            //foreach (var r in rooms)
            //{
                hotel_room_RP_price_info p = new hotel_room_RP_price_info() { room_rp_start_time = start, room_rp_end_time = end,room_rp_price=r.DefaultPrice,hotel_id=r.hotel_id,room_id=r.room_id};
                if (new Hotel_room_RP_price_batch().InsertPriceBatch(p) && new RoomStatus_batch().insertStatuBatch(p))
                {
                    re = true; ;// DBhelp.CallProc(p.room_id, "proc_hotel_room_ebeds_batch_roomid"); DBhelp.CallProc(p.room_id, "proc_hotel_room_RP_price_batch_roomid");
                }
                result = re == true ? 1 : 0;
                return result;
        }

      
    }
}