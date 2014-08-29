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

        //
        // GET: /AddHotel/

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

                   hotel = (from h in db.hotel where h.hotel_id == id select h).Single();

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

        //
        // GET: /AddHotel/Details/5

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
        // GET: /AddHotel/Create

        public ActionResult Create(string hotelId)
        {
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

        //
        // POST: /AddHotel/Create

        [HttpPost]
        public ActionResult Create(hotel_info hotel_info)
        {
            hotel_info.u_id = help.HotelInfoHelp.getUId();
            hotel_info.source_id =Convert.ToInt32(help.StringHelper.appSettings("source_id")); ;
            hotel_info.h_id = Guid.NewGuid().ToString();
            hotel_info.h_state = true;
            hotel_info.h_utime = DateTime.Now;
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
            return RedirectToAction("Create", "Room", new { hotelId = ddh.hotel_id });
            
            return View(hotel_info);
        }
        public int IsOk(string text)
        {
            try
            {
             var f =from h in db.hotel where h.h_name_cn == text select h;
                int f3 = (from h in new HotelDBContent("").hotel where h.h_name_cn == text select h).Count();
            }
            catch (Exception e)
            {
                
                throw e;
            }

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

        //
        // GET: /AddHotel/Edit/5

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
        // POST: /AddHotel/Edit/5

        [HttpPost]
        public ActionResult Edit(hotel_info hotel_info)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(hotel_info).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel_info);
        }

        //
        // GET: /AddHotel/Delete/5

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
        // POST: /AddHotel/Delete/5

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