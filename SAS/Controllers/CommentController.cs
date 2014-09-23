using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.Models;
using SAS.DBC;

namespace SAS.Controllers
{
    public class CommentController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Comment/

        public ActionResult QueryComment(string hotelId,string page,string startTime,string endTime)
        {
             ViewBag.currentHotelId = hotelId;
             ViewBag.allPage = 100;
             ViewBag.curentPage =page;
            return View("MyComment");
        }

        //
        // GET: /Comment/Details/5

        public ActionResult Details(int id = 0)
        {
            Hotel_comment_info hotel_comment_info = db.coments.Find(id);
            if (hotel_comment_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_comment_info);
        }

        //
        // GET: /Comment/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Comment/Create

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
        // GET: /Comment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Hotel_comment_info hotel_comment_info = db.coments.Find(id);
            if (hotel_comment_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_comment_info);
        }

        //
        // POST: /Comment/Edit/5

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
        // GET: /Comment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Hotel_comment_info hotel_comment_info = db.coments.Find(id);
            if (hotel_comment_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_comment_info);
        }

        //
        // POST: /Comment/Delete/5

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