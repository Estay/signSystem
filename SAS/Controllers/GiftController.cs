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
    public class GiftController : Controller
    {
        private GiftDBContent db = new GiftDBContent();

        //
        // GET: /Gift/

        public ActionResult Index()
        {
            //return View(db.hotel.ToList());
            return null;
        }

        public ActionResult MyGift()
        {
            GetData();
            return View();
        }
        public void GetData()
        {
            string u_id = "test1";
            //所有酒店列表
            ViewData["rooms"] = help.HotelInfoHelp.getRooms(u_id);
            //所有酒店对应的房型列表
            ViewData["hotels"] = help.HotelInfoHelp.getHotlList(u_id);

            ViewData["gift"] = new Gift().GiftList();
        }
        //
        // GET: /Gift/Details/5

        public ActionResult Details(int id = 0)
        {
            //Gift gift = db.hotel.Find(id);
            //if (gift == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        //
        // GET: /Gift/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Gift/Create

        [HttpPost]
        public ActionResult Create(Gift gift)
        {
            //if (ModelState.IsValid)
            //{
            //    db.hotel.Add(gift);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View(gift);
        }

        //
        // GET: /Gift/Edit/5

     

        //
        // POST: /Gift/Edit/5

        [HttpPost]
        public ActionResult Edit(Gift gift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gift);
        }

        //
        // GET: /Gift/Delete/5

        public ActionResult Delete(int id = 0)
        {

            return null;
        }
        //得到当前酒店所有房型
        public void getRooms(int hotel_id)
        {
            //List<hotel_room_info> roomsList = (from r in db.room where r.hotel_id == hotel_id select r).ToList();
            ViewData["rooms"] = DBhelp.getRooms(hotel_id);
            ViewData["bedTypes"] = new hotel_room_info().getBedType();
        }
        //
        // POST: /Gift/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Gift gift = db.hotel.Find(id);
            //db.hotel.Remove(gift);
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