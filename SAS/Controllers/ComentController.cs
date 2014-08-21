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
    public class ComentController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Coment/

        public ActionResult Index()
        {
            return View(db.hotel.ToList());
        }

        //
        // GET: /Coment/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    Hotel_comment_info hotel_comment_info = db.hotel.Find(id);
        //    if (hotel_comment_info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hotel_comment_info);
        //}

        //
        // GET: /Coment/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Coment/Create

        [HttpPost]
        public ActionResult Create(Hotel_comment_info hotel_comment_info)
        {
            if (ModelState.IsValid)
            {
                db.coments.Add(hotel_comment_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotel_comment_info);
        }

        //
        // GET: /Coment/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Hotel_comment_info hotel_comment_info = db.coments.Find(id);
        //    if (hotel_comment_info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hotel_comment_info);
        //}

        //
        // POST: /Coment/Edit/5

        [HttpPost]
        public ActionResult Edit(Hotel_comment_info hotel_comment_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel_comment_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel_comment_info);
        }

        //
        // GET: /Coment/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    Hotel_comment_info hotel_comment_info = db.hotel.Find(id);
        //    if (hotel_comment_info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hotel_comment_info);
        //}

        //
        // POST: /Coment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel_comment_info hotel_comment_info = db.coments.Find(id);
            db.coments.Remove(hotel_comment_info);
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