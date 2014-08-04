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
    public class MyApartMentController : Controller
    {
        private hotel_infoDBContent db = new hotel_infoDBContent();
        private hotel_infoDBContent dbHotel = new hotel_infoDBContent();
        private PictureDBContent dbImage = new PictureDBContent();
        private hotel_room_infoDBContent dbRoom = new hotel_room_infoDBContent();
        // GET: /MyApartMent/
        int hotel_id = 0;
        //酒店信息
        public ActionResult Hotel(string hotelId)
        {
            int.TryParse(hotelId, out hotel_id);
            ViewData["DTime"] = new hotel_info().getDecorationTime();  //Theme
            ViewData["Themes"] = DBhelp.GetSelectDataByTable("hotel_theme_info");  //Theme
            ViewData["Category"] = DBhelp.GetSelectDataByTable("Hotel_theme_type_info"); ;//Category
            ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
            ViewData["services"] = DBhelp.GetSelectDataByTable("GeneralAmenities_info");//services
            ViewData["provice"] = DBhelp.GetSelectDataByTable("province_info");//provice     
            return View(dbHotel.hotel.Single(h=>h.hotel_id==hotel_id));
        }
        //房型
        public ActionResult Room(string hotelId)
        {
            
            int.TryParse(hotelId, out hotel_id);         
            return View((dbRoom.room.ToList().Where(r=>r.hotel_id==hotel_id)).ToList());
        }
        //图片
        public ActionResult Image(string hotelId)
        {
          
            int.TryParse(hotelId, out hotel_id);
            int[]rf = (from r in dbRoom.room where r.hotel_id == hotel_id select r.room_id).ToArray();           
            return View((from image in dbImage.room where rf.Contains(image.hotel_id) select image).ToList()); 
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
    }
}