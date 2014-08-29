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
        int hotel_id = 0, result = 0;
        
        //酒店信息
        public ActionResult Hotel(string hotelId)
        {
          
            int.TryParse(hotelId, out hotel_id);
            HelperData();
            ViewBag.HotelId = hotel_id;
            return View(db.hotel.Single(h=>h.hotel_id==hotel_id));
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
            hotel_info.h_id = "01611129";
            hotel_info.h_utime = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                db.Entry(hotel_info).State = EntityState.Modified;
               
                db.SaveChanges();
              
            }
            HelperData();
            ViewBag.HotelId = hotel_info.hotel_id;
            return View(hotel_info);
        }
        //房型
        public ActionResult Room(string hotelId)
        {
           
            int.TryParse(hotelId, out hotel_id);
            getRooms(hotel_id);
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
            return View("Room", room);
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
        public ActionResult Image(string hotelId)
        {
            
            int.TryParse(hotelId, out hotel_id);
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
          
            getRooms(hotel_room_info.hotel_id);
            ViewBag.sign = sign;
            ViewBag.Tag = "增加房型";
            ViewBag.HoltelId = hotel_room_info.hotel_id;
            return View("Room", new hotel_room_info());
        }

        //
        // GET: /MyApartMent/Details/5

        public ActionResult Details(int id = 0)
        {
            hotel_info hotel_info = db.hotel.Find(id);
            if (hotel_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_info);
        }
        public int delApart(string hotelId)
        {
            //try
            //{
            //    int.TryParse(hotelId, out hotel_id); string u_id = HotelInfoHelp.getUId();
            //    using (db = new HotelDBContent())
            //    {
            //        var f = ((from h in db.hotel where h.hotel_id == hotel_id && h.source_id == 5 && h.u_id == u_id select h).SingleOrDefault()).h_state ;
                   
            //       // result = ((from h in db.hotel where h.hotel_id = hotel_id && h.source_id = 5 select h).SingleOrDefault()).h_state=false);
            //    }
            //}
            //catch (Exception)
            //{
                
            //    throw;
            //}
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

        //
        // GET: /MyApartMent/Edit/5

        public ActionResult Edit(int id = 0)
        {
            hotel_info hotel_info = db.hotel.Find(id);
            if (hotel_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_info);
        }

        //
        // POST: /MyApartMent/Edit/5

        [HttpPost]
        public ActionResult Edit(hotel_info hotel_info)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(hotel_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel_info);
        }

        //
        // GET: /MyApartMent/Delete/5

        public ActionResult Delete(int id = 0)
        {
            hotel_info hotel_info = db.hotel.Find(id);
            if (hotel_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_info);
        }

        //
        // POST: /MyApartMent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            hotel_info hotel_info = db.hotel.Find(id);
            db.hotel.Remove(hotel_info);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        //公寓列表
        public ActionResult MyHotel(string UId)
        {
            UId = "test1";
            //int.TryParse(hotelId, out hotel_id);
            //ViewData["DTime"] = new hotel_info().getDecorationTime();  //Theme
            //ViewData["Themes"] = DBhelp.GetSelectDataByTable("hotel_theme_info");  //Theme
            //ViewData["Category"] = DBhelp.GetSelectDataByTable("Hotel_theme_type_info"); ;//Category
            //ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
            //ViewData["services"] = DBhelp.GetSelectDataByTable("GeneralAmenities_info");//services
            //ViewData["provice"] = DBhelp.GetSelectDataByTable("province_info");//provice   
            //ViewBag.HotelId = hotel_id;         
              return View(help.HotelInfoHelp.getHotlList(UId));
  
    
          
        }
    }
}