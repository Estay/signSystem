using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.Models;
using SAS.help;
using SAS.DBC;

namespace SAS.Controllers
{
    public class AddHotelController : Controller
    {                                        
        private HotelDBContent db = new HotelDBContent();


        //////////////////////////////新建公寓开始
        #region
        public ActionResult Index(string hotelId)
        {
            hotel_info hotel = new hotel_info();
            //khotel_theme_infoDBContent db = new khotel_theme_infoDBContent();
               // var themes = db..ToList();
           var list  =hotel_theme_info.AllTheme();
           var Categorylist = Hotel_theme_type_info.allCategory();
           //IEnumerable<hotel_theme_info> accessIDs = list;
           if (hotelId != null)
           {
               int id = Convert.ToInt32(hotelId);
               if (id > 0)
               {

                   hotel = (from h in db.hotel where h.hotel_id == id select h).SingleOrDefault();

               }
           }
          
           ViewData["Themes"] = DBhelp.GetSelectDataByTable("hotel_theme_info");  //Theme
           ViewData["Category"] = DBhelp.GetSelectDataByTable("Hotel_theme_type_info"); ;//Category
           ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
           ViewData["services"] = DBhelp.GetSelectDataByTable("GeneralAmenities_info");//services
           ViewData["provice"] = DBhelp.GetSelectDataByTable("province_info");//provice
            //hotel_info.AllTheme();
           return View(hotel);
        }

      
   

        public ActionResult Create(string hotelId)
        {
           // ViewBag.username = "ff";
            help.DBhelp.log("add hotel Create");
            hotel_info hotel = new hotel_info();
            //khotel_theme_infoDBContent db = new khotel_theme_infoDBContent();
            // var themes = db..ToList();
            
            //IEnumerable<hotel_theme_info> accessIDs = list;
            if (hotelId != null)
            {
                int id = Convert.ToInt32(hotelId);
                if (id > 0)
                {

                    hotel = (from h in db.hotel where h.hotel_id == id select h).Single();

                }
            }
            ViewData["DTime"] = new hotel_info().getDecorationTime();  //Theme
            ViewData["Themes"] = DBhelp.GetSelectDataByTable("hotel_theme_info");  //Theme
            ViewData["Category"] = DBhelp.GetSelectDataByTable("Hotel_theme_type_info"); ;//Category
            ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
            ViewData["services"] = DBhelp.GetSelectDataByTable("GeneralAmenities_info");//services
            ViewData["provice"] = DBhelp.GetSelectDataByTable("province_info");//provice
            return View(hotel);
        }



        
        /// <summary>
        /// 公寓提交
        /// </summary>
        /// <param name="hotel_info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(hotel_info hotel_info)
        {
            hotel_info.u_id = new HotelInfoHelp().getUId();
            hotel_info.source_id =Convert.ToInt32(help.StringHelper.appSettings("source_id")); ;
            hotel_info.h_id = Guid.NewGuid().ToString();
            hotel_info.h_state = true;
            hotel_info.h_utime = DateTime.Now;
            hotel_info.CheckState = 2;
            try
            {
                hotel_info.h_ctime = DateTime.Now;
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if (ModelState.IsValid)
                {
                    db.hotel.Add(hotel_info);
                    db.SaveChanges();
                  
                   // return RedirectToAction("Room/Create/"+ddh.hotel_id);
                 
                }
           


            }
            catch (Exception e)
            {
                throw e;
                help.DBhelp.log("新建公寓基本信息"+e.ToString());
             
            }
            var ddh = db.hotel.Select(h => new { h.hotel_id, h.h_id }).Single(h => h.h_id == hotel_info.h_id);
            db.Dispose();
            return RedirectToAction("room", "AddHotel", new { hotelId = ddh.hotel_id });
            
            return View(hotel_info);
        }
        /// <summary>
        /// 验证酒店名是否存在
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int IsOk(string text)
        {
          
            if ((from h in db.hotel where h.h_name_cn == text select h).Count() > 0 || (from h in new HotelDBContent("").hotel where h.h_name_cn == text select h).Count() > 0)
                return 0;
            else
                return 1;
        
        }
       //上一步
        public ActionResult Forward(string hotelId)
        {
            int id = Convert.ToInt32(hotelId);
            hotel_info hotel = (from h in db.hotel where h.hotel_id ==id select h).Single();
            return View(hotel);
        }
        #endregion
        ////////////////////////////////////////新建公寓结束





        /////////////////////////////////////////房型部份开始
        #region
        /// <summary>
        /// 获得房型
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public ActionResult room(string hotelId)
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
            hotel_room_info room = (from h in db.rooms where h.room_id == RId select h).Single();
            getRooms(room.hotel_id);
            ViewBag.HoltelId = room.hotel_id;
            return View("Create", room);
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
            if (hotel_room_info.room_id > 0)
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
                sign = db.SaveChanges() > 0 ? 1 : 0; ;
            }

            getfacilities();
            getRooms(hotel_room_info.hotel_id);
            ViewBag.HoltelId = hotel_room_info.hotel_id;
            ViewBag.Tag = "增加房型";
            getRooms(hotel_room_info.hotel_id);
            ViewBag.sign = sign;

            return View(new hotel_room_info());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotel_id"></param>
        public void getRooms(int hotel_id)
        {
            //List<hotel_room_info> roomsList = (from r in db.room where r.hotel_id == hotel_id select r).ToList();
            ViewData["rooms"] = DBhelp.getRooms(hotel_id);
            ViewData["bedTypes"] = new hotel_room_info().getBedType();
        }



        #endregion

        ////////////////////////////////////房型结束











        ///////////////////////////////////////////////房价部份开始
        #region
        /// <summary>
        /// 获得所有房型
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public ActionResult price(string hotelId)
        {
            getRooms(Convert.ToInt32(hotelId));
            return View();
        }


        /// <summary>
        /// 新建公寓价格录入
        /// </summary>
        /// <param name="hotel_room_RP_price_info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreatePrice(List<hotel_room_RP_price_info> hotel_room_RP_price_info)
        {
            bool result = false;
            DateTime start = DateTime.Now.Date; DateTime end = DateTime.Now.AddYears(1).Date;
            int hotelId = 0;

            //遍历价格集合
            foreach (var p in hotel_room_RP_price_info)
            {

                int.TryParse(p.hotel_id.ToString(), out hotelId);
                p.room_rp_start_time = start; p.room_rp_end_time = end;
                if (new Hotel_room_RP_price_batch().InsertPriceBatch(p) && new RoomStatus_batch().insertStatuBatch(p))
                    result = true;


            }
            if (result == true)
                return RedirectToAction("Create", "Image", new { hotelId = hotelId });
            else
                return RedirectToAction("Create", "Price", new { hotelId = hotelId });



        }
        #endregion
        ///////////////////////////////////////////////房价部份结束





        ///////////////////////////////////////////////新建公寓图片开始

        #region
        /// <summary>
        /// 图片提交
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateSub(string hotelId)
        {
            try
            {
                int hotel_id;
                if (int.TryParse(hotelId, out hotel_id))
                {
                    if ((from h in db.hotel where h.hotel_id == hotel_id select h).Count() > 0)
                    {
                        string sql = string.Format("update hotel_room_picture_info set state=1 where room_id in(select room_id from hotel_room_info where hotel_id in({0}))", hotel_id);
                        if (DBhelp.ExcuteTableBySQL(sql) > 0)
                            return View("Success");
                        else
                            DBhelp.log("图片提交失败" + hotelId); return View("Faiture");


                    }
                }
                else
                {
                    DBhelp.log("图片提交转换出错");
                    return View("Faiture");
                }
            }
            catch (Exception ex)
            {

                DBhelp.log("图片提交" + ex.ToString());
                return View("Faiture");
            }

            //if (ModelState.IsValid)
            //{
            //    db.room.Add(hotel_picture_info);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View(new hotel_picture_info());
        }

        #endregion
        ///////////////////////////////////////////////新建公寓图片结束


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}