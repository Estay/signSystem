using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.Models;

namespace SAS.Controllers
{
    public class DrrRuleController : Controller
    {
        private DrrRuleDBContent db = new DrrRuleDBContent();

        //
        // GET: /DrrRule/

        public ActionResult Index()
        {
            return View(db.hotel.ToList());
        }

        //
        // GET: /DrrRule/Details/5

        public ActionResult Details(int id = 0)
        {
            DrrRule drrrule = db.hotel.Find(id);
            if (drrrule == null)
            {
                return HttpNotFound();
            }
            return View(drrrule);
        }

        //
        // GET: /DrrRule/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DrrRule/Create

        [HttpPost]
        public ActionResult Create(DrrRule drrrule)
        {
            if (ModelState.IsValid)
            {
                db.hotel.Add(drrrule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drrrule);
        }

        //
        // GET: /DrrRule/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DrrRule drrrule = db.hotel.Find(id);
            if (drrrule == null)
            {
                return HttpNotFound();
            }
            return View(drrrule);
        }

        //
        // POST: /DrrRule/Edit/5

        [HttpPost]
        public ActionResult Edit(DrrRule drrrule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drrrule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drrrule);
        }

        //
        // GET: /DrrRule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DrrRule drrrule = db.hotel.Find(id);
            if (drrrule == null)
            {
                return HttpNotFound();
            }
            return View(drrrule);
        }

        //
        // POST: /DrrRule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DrrRule drrrule = db.hotel.Find(id);
            db.hotel.Remove(drrrule);
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