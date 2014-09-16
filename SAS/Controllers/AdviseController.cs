using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.Models;
using SAS.DBC;
using SAS.help;

namespace SAS.Controllers
{
    public class AdviseController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Advise/

        public ActionResult MyAdivise()
        {
            return View();
        }

        //
        // GET: /Advise/Details/5

        public ActionResult Details(int id = 0)
        {
            TempAdvise tempadvise = db.TempAdvises.Find(id);
            if (tempadvise == null)
            {
                return HttpNotFound();
            }
            return View(tempadvise);
        }

        //
        // GET: /Advise/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Advise/Create

        [HttpPost]
        public ActionResult Create(TempAdvise tempadvise)
        {
            tempadvise.contact = tempadvise.contact == null ? new HotelInfoHelp().getUId() : tempadvise.contact; tempadvise.SubmitTime = DateTime.Now;
           
            if (ModelState.IsValid)
            {
                db.TempAdvises.Add(tempadvise);
              if(db.SaveChanges()>0)
                   return View("Success");
              else
                 return View("Faiture");
            }

            return View(tempadvise);
        }

        //
        // GET: /Advise/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TempAdvise tempadvise = db.TempAdvises.Find(id);
            if (tempadvise == null)
            {
                return HttpNotFound();
            }
            return View(tempadvise);
        }

        //
        // POST: /Advise/Edit/5

        [HttpPost]
        public ActionResult Edit(TempAdvise tempadvise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tempadvise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tempadvise);
        }

        //
        // GET: /Advise/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TempAdvise tempadvise = db.TempAdvises.Find(id);
            if (tempadvise == null)
            {
                return HttpNotFound();
            }
            return View(tempadvise);
        }

        //
        // POST: /Advise/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TempAdvise tempadvise = db.TempAdvises.Find(id);
            db.TempAdvises.Remove(tempadvise);
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