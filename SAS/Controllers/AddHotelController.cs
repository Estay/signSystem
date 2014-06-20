using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.Models;
using SAS.help;

namespace SAS.Controllers
{
    public class AddHotelController : Controller
    {
        private hotel_infoDBContent db = new hotel_infoDBContent();

        //
        // GET: /AddHotel/

        public ActionResult Index()
        {
            //khotel_theme_infoDBContent db = new khotel_theme_infoDBContent();
               // var themes = db..ToList();
           var list  =hotel_theme_info.AllTheme();
           var Categorylist = Hotel_theme_type_info.allCategory();
           //IEnumerable<hotel_theme_info> accessIDs = list;


           ViewData["Themes"] = DBhelp.GetSelectDataByTable("hotel_theme_info");  //Theme
           ViewData["Category"] = DBhelp.GetSelectDataByTable("Hotel_theme_type_info"); ;//Category
           ViewData["facilities"] = DBhelp.GetSelectDataByTable("Facilities_info");//facilities
           ViewData["services"] = DBhelp.GetSelectDataByTable("GeneralAmenities_info");//services
           ViewData["provice"] = DBhelp.GetSelectDataByTable("province_info");//provice
            //hotel_info.AllTheme();
            return View(new hotel_info());
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

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AddHotel/Create

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
                db.Entry(hotel_info).State = EntityState.Modified;
                db.SaveChanges();
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