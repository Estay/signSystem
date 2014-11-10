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
        int result = 1;

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
            int hotel_id = 0;
            hotel_info hotel = new hotel_info();
            //if (hotelId != null)
            //{
            int.TryParse(hotelId, out hotel_id);

            hotel = (from h in db.hotel where h.hotel_id == hotel_id select h).SingleOrDefault();

               
           // }
            getHelpData();
            if (hotel == null)
                return View("create", new hotel_info());
            else
                return View("create", hotel);
            return View("Create",new hotel_info());
        }

        private void getHelpData()
        {
            ViewData["DTime"] = new hotel_info().getDecorationTime();  //Theme
            ViewData["Themes"] = DBhelp.GetSelectDataByTable("hotel_theme_info");  //Theme
            ViewData["Category"] = DBhelp.GetSelectDataByTable("Hotel_theme_type_info"); ;//Category
            ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
            ViewData["services"] = DBhelp.GetSelectDataByTable("GeneralAmenities_info");//services
            ViewData["provice"] = DBhelp.GetSelectDataByTable("province_info");//provice
        }



        
        /// <summary>
        /// 公寓提交
        /// </summary>
        /// <param name="hotel_info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateHotel(hotel_info hotel_info)
        {
               
            try
            {
               
                //hotel_info.u_id = new HotelInfoHelp().getUId();
                hotel_info.source_id = Convert.ToInt32(help.StringHelper.appSettings("source_id")); ;
                hotel_info.h_id = Guid.NewGuid().ToString();
                hotel_info.h_state = true;
                hotel_info.h_utime = DateTime.Now;
                hotel_info.CheckState = 2;
                hotel_info.h_ctime = DateTime.Now;
                hotel_info.decorateTime = hotel_info.decorateTime == Convert.ToDateTime("0001/1/1 0:00:00") || hotel_info.decorateTime ==null? Convert.ToDateTime("1900-01") : hotel_info.decorateTime;
                hotel_info.h_opening_time = hotel_info.h_opening_time == Convert.ToDateTime("0001/1/1 0:00:00") ? Convert.ToDateTime("1900-01") : hotel_info.h_opening_time;
                //hotel_info.h_room_count = hotel_info.h_room_count == 0 ? 1 : hotel_info.h_room_count;
                hotel_info.u_id = hotel_info.h_mobile_phone;
                using(db=new HotelDBContent())
                {
                   if (hotel_info.hotel_id > 0)
                   {
                       db.Entry(hotel_info).State = EntityState.Modified;
                       
                    
                   }
                   else
                   {

                       var errors = ModelState.Values.SelectMany(v => v.Errors);
                       //if (ModelState.IsValid)
                       //{
                           db.hotel.Add(hotel_info);
                          // result = db.SaveChanges() > 0 ? 1 : 0;
                        
                           var f = (from h in db.hotel where h.h_name_cn == hotel_info.h_name_cn && h.source_id == 4 select h).FirstOrDefault(); 
                           if (f != null)
                               f.h_state = false; //把对应艺龙的酒店干掉
                             
                           // return RedirectToAction("Room/Create/"+ddh.hotel_id);
                       //}
                   }
                   result = db.SaveChanges() > 0 ? 1 : 0;
                   if (result > 0)
                   {
                       var ddh = db.hotel.Select(h => new { h.hotel_id, h.h_id }).Single(h => h.h_id == hotel_info.h_id);
                       return RedirectToAction("room", "AddHotel", new { hotelId = ddh.hotel_id });
                   }

                }    


            }
         
            catch (Exception e)
            {
                throw e;
                help.DBhelp.log("新建公寓基本信息"+e.ToString());
             
            }
            ViewBag.sign = result; getHelpData();
            return View("Create", hotel_info);
        }
        ///// <summary>
        ///// 验证酒店名是否存在
        ///// </summary>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public int IsOk(string text)
        //{
        //    using (db = new HotelDBContent())
        //    {
        //        if ((from h in db.hotel where h.h_name_cn == text select h).Count() > 0 || (from h in new HotelDBContent("").hotel where h.h_name_cn == text select h).Count() > 0)
        //            return 0;
        //        else
        //            return 1;
        //    }
        
        //}
        /// <summary>
        /// 模糊查询公寓
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public ActionResult FindHotel(string text)
        {
           hotel_info hotel=null;
            using (db = new HotelDBContent())
            {
             //   hotel = (from h in db.hotel where h.h_name_cn == text.Trim() && h.source_id == 4 select new { h=h.h_name_cn,name=h.h_city});
                hotel = (from h in db.hotel where h.h_name_cn == text.Trim() && h.source_id == 4 select h).FirstOrDefault();
                hotel.hotel_id = 0;
                 //var tempHotel=(from h1 in db.hotel where h1.h_name_cn==text.Trim() && h1.source_id==5 select h1).Count();
                 // ViewBag.exit = tempHotel > 0 ? 1 : 0;
                hotel.h_province = (from c in db.citys where c.City_id == hotel.h_city select c.Province_id).SingleOrDefault();
                string []serveice = hotel.GeneralAmenities.Split('、');
                string[] temp = new string[serveice.Length];
                for (int i = 0; i < serveice.Length; i++)
                {
                      temp[i]=serveice[i];
                }
                var GeneraIds = from g in db.general where temp.Contains(g.Title) select g.Id;
                string GIds = string.Empty;
                foreach (var item in GeneraIds)
                {
                    if (string.IsNullOrEmpty(GIds))
                       GIds += item ;
                    else
                        GIds += ","+item ;
                }
                hotel.GeneralAmenities = GIds;
                hotel.IntroEditor = hotel.IntroEditor.TrimEnd();
                var tempRoom = ((from o in db.hotel where o.h_name_cn == text.Trim() && o.source_id==5 && o.h_state==true select o).Count());
                ViewBag.exit = tempRoom > 0 ? 0 : 1;
            }
            getHelpData();
            if(hotel!=null)
                return View("Create", hotel);
            else
            return View("Create",new hotel_info());
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
            return View("MyRoom",new hotel_room_info());
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
            return View("MyRoom", room);
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
            return View("MyRoom", new hotel_room_info());
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
            ViewData["facilities"] = DBhelp.GetSelectDataByTable("RoomAmenities_info");//facilities
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

            helpdata(hotel_room_info.hotel_id);
           
            ViewBag.sign = sign;

            return View("MyRoom",new hotel_room_info());
        }

        private void helpdata(int hotel_id)
        {
            getfacilities();
            getRooms(hotel_id);
            ViewBag.HoltelId = hotel_id;
            ViewBag.Tag = "增加房型";
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

        //上一步
        public ActionResult RoomForward(string hotelId)
        {
            //int id = Convert.ToInt32(hotelId);
            //hotel_info hotel = (from h in db.hotel where h.hotel_id ==id select h).Single();
            return RedirectToAction("Create", "AddHotel", new { hotelId = hotelId });
        }

       
        /// <summary>
        /// //匹配房型
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="name"></param>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public ActionResult selectedRoom(string roomId, string hotelId)
        {
            int room_id = 0; int.TryParse(roomId, out room_id);//   int hotel_id = Convert.ToInt32(hotelId);
            int hotel_Id = 0; int.TryParse(hotelId, out hotel_Id);
            hotel_room_info room = new hotel_room_info();
            using (db = new HotelDBContent())
            {

                room = (from r in db.rooms where r.room_id == room_id select r).SingleOrDefault();
                room.room_id = 0; room.hotel_id = 0;

                var tempRoom = ((from o in db.rooms where o.h_r_name_cn == room.h_r_name_cn.Trim() && o.hotel_id == hotel_Id select o).Count());
                ViewBag.exit = tempRoom > 0 ? 0 : 1;

            }
            helpdata(hotel_Id);
            if(room!=null)
             return View("MyRoom", room);
            else
              return View("MyRoom", new hotel_room_info());
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
            ViewBag.hotelId = hotelId;
            getRooms(Convert.ToInt32(hotelId));
            return View("Myprice");
        }
        public ActionResult priceForward(string hotelId)
        {
            getRooms(Convert.ToInt32(hotelId));
            return RedirectToAction("Room", "AddHotel", new { hotelId = hotelId });
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
                return RedirectToAction("Image", "addhotel", new { hotelId = hotelId });
            else
                return RedirectToAction("Image", "addhotel", new { hotelId = hotelId });



        }
        #endregion
        ///////////////////////////////////////////////房价部份结束

       



        ///////////////////////////////////////////////新建公寓图片开始

        #region
        //图片上一步
        public ActionResult ImageForward(string hotelId)
        {
            getRooms(Convert.ToInt32(hotelId));
            return RedirectToAction("price", "AddHotel", new { hotelId = hotelId });
        }

        public ActionResult Image(string hotelId)
        {
            ViewBag.sign = result;
            int hotel_id = 0;
            int.TryParse(hotelId, out hotel_id);
            ViewBag.HotelId = hotelId;
            ViewData["rooms"] = DBhelp.getRooms(hotel_id);
            ViewData["ImageTypes"] = new hotel_picture_info().getImageType();
            int[] rf = (from r in db.rooms where r.hotel_id == hotel_id select r.room_id).ToArray();
            var ff = from image in db.roomImages where rf.Contains(image.room_id) select image;
            return View("MyImage",(from image in db.roomImages where rf.Contains(image.room_id) select image).ToList());
        }
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

            return View("MyImage", new hotel_picture_info());
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