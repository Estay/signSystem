using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.DBC;
using SAS.Models;

namespace SAS.Controllers
{
    public class DrrRuleController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /DrrRule/

        public ActionResult Index()
        {
           return View();
        }

        //
        // GET: /DrrRule/Details/5

        public ActionResult Details(int id = 0)
        {
            //DrrRule drrrule = db.hotel.Find(id);
            //if (drrrule == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        //
        // GET: /DrrRule/Create

        public ActionResult MyDrr()
        {
            GetData();
            return View(new DrrRules());
        }

        //
        // POST: /DrrRule/Create

        [HttpPost]
        public ActionResult Create(DrrRules drrrule)
        {
            //if (ModelState.IsValid)
            //{
            //    db.hotel.Add(drrrule);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View(drrrule);
        }
        public void GetData()
        {
            
            string u_id = "test1";
            ViewData["drrModes"] = help.HotelInfoHelp.getDrrModeList(u_id);
            //所有酒店列表
            ViewData["rooms"] = help.HotelInfoHelp.getRooms(u_id);
            //所有酒店对应的房型列表
            ViewData["hotels"] = help.HotelInfoHelp.getHotlList(u_id);
           // ViewData["drrModes"] = help.HotelInfoHelp.getDrrModeList(u_id);
            ViewData["drrs"] = help.HotelInfoHelp.getDrrList(u_id);

           
        }
        //
        // GET: /DrrRule/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //DrrRule drrrule = db.hotel.Find(id);
            //if (drrrule == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        //
        // POST: /DrrRule/Edit/5

        [HttpPost]
        public ActionResult Edit(DrrRules drrrule)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(drrrule).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View(drrrule);
        }

        //
        // GET: /DrrRule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            //DrrRule drrrule = db.hotel.Find(id);
            //if (drrrule == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        //
        // POST: /DrrRule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //DrrRule drrrule = db.hotel.Find(id);
            //db.hotel.Remove(drrrule);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}