using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.DBC;
using SAS.help;
using SAS.Models;

namespace SAS.Controllers
{
    public class GiftController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Gift/

        public ActionResult Index()
        {
            //return View(db.hotel.ToList());
            return null;
        }

        public ActionResult MyGift(string id)
        {
            setName();
            GetData(id);
            
            return View(new Gift());
        }

        private void setName()
        {
            ViewBag.title = "添加礼包";
            ViewBag.buttonName = "添加";
        }
        public void GetData(string id)
        {
            int hotel_id;
            int.TryParse(id, out hotel_id);
            string u_id = "test1";
            var hotels= help.HotelInfoHelp.getHotlList(u_id);
            ViewData["hotels"] = hotels;
            if (id == null && hotels.Count > 0)
            {
                hotel_id = hotels[0].hotel_id;
            }
            ViewData["gift"] = new Gift().GiftList(hotel_id);
            ViewData["rooms"] = help.HotelInfoHelp.getRooms(hotel_id);
            ViewBag.Id = hotel_id;
            //所有酒店列表
            
            //所有酒店对应的房型列表
          
        }
        //修改gift
        public ActionResult updateG(string id)
        {
            ViewBag.title = "修改礼包";
            ViewBag.buttonName = "修改";
            int gId;
            int.TryParse(id,out gId);
            var gift = (from g in db.gifts where g.GiftId == gId select g).Single();
            ViewBag.Id = gift.hotel_id;
            GetData(gift.hotel_id.ToString());
            return View("MyGift", gift);
        }
        //删除gift
        public ActionResult deleteG(string id)
        {
            int gId;
            int.TryParse(id, out gId);
            var gift = (from g in db.gifts where g.GiftId == gId select g).Single();
           
            ViewBag.Id = gift.hotel_id;
            db.gifts.Remove(gift);
            if(db.SaveChanges()>0)
                ViewBag.sign=1;
            else
                 ViewBag.sign=0;
            setName();
            GetData(gift.hotel_id.ToString());
            return View("MyGift", new Gift());
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
            gift.WeekSet = "1,2,3,4,5,6,7";
            gift.DateType = "CheckinDate";
            gift.GiftTypes = "1,2,4,7";
            gift.HourType = "Hours24";
            gift.HourNumber = 0;
            gift.WayOfGiving = "EveryRoom";
            ViewBag.title = "添加礼包";
            ViewBag.buttonName = "添加";
            try
            {
                //修改
                if (gift.GiftId > 0)
                {
                    db.Entry(gift).State = EntityState.Modified;
                   // db.SaveChanges();
                }
                //添加
                else
                {
                    if (ModelState.IsValid)
                    {
                        db.gifts.Add(gift);
                      

                    }
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                
                throw e;
            }

            GetData(gift.hotel_id.ToString());
            return View("MyGift", new Gift());
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