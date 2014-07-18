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
    public class GuaranteeController : Controller
    {
        private GuaranteeRuleDBContent db = new GuaranteeRuleDBContent();

        //
        // GET: /Guarantee/

        public ActionResult Index()
        {
            return View(db.hotel.ToList());
        }

        //
        // GET: /Guarantee/Details/5

        public ActionResult Details(int id = 0)
        {
            GuaranteeRule guaranteerule = db.hotel.Find(id);
            if (guaranteerule == null)
            {
                return HttpNotFound();
            }
            return View(guaranteerule);
        }

        //
        // GET: /Guarantee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Guarantee/Create

        [HttpPost]
        public ActionResult Create(GuaranteeRule guaranteerule)
        {
            if (ModelState.IsValid)
            {
                db.hotel.Add(guaranteerule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guaranteerule);
        }

        //
        // GET: /Guarantee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            GuaranteeRule guaranteerule = db.hotel.Find(id);
            if (guaranteerule == null)
            {
                return HttpNotFound();
            }
            return View(guaranteerule);
        }

        //
        // POST: /Guarantee/Edit/5

        [HttpPost]
        public ActionResult Edit(GuaranteeRule guaranteerule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guaranteerule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guaranteerule);
        }

        //
        // GET: /Guarantee/Delete/5

        public ActionResult Delete(int id = 0)
        {
            GuaranteeRule guaranteerule = db.hotel.Find(id);
            if (guaranteerule == null)
            {
                return HttpNotFound();
            }
            return View(guaranteerule);
        }

        //
        // POST: /Guarantee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            GuaranteeRule guaranteerule = db.hotel.Find(id);
            db.hotel.Remove(guaranteerule);
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