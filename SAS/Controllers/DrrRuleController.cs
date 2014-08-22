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

        public ActionResult updateDrr(string id)
        {
            int drrId;
            int.TryParse(id, out drrId);
            GetData();
            return View("MyDrr", (from d in db.drrs where d.id == drrId select d).Single());
        }
        //
        // POST: /DrrRule/Create

        [HttpPost]
        public ActionResult Create(DrrRules drrrule)
        {
            string guid = Guid.NewGuid().ToString();

            Hotel_room_RP_info rp = new Hotel_room_RP_info();
           
            rp.RatePlanId = guid;
            rp.hotel_id = drrrule.hotel_id;
            rp.h_room_rp_is_to_store_pay = true;
            rp.h_room_rp_check_in = "00:00:00";
            rp.h_room_rp_check_out = "23:59:00";
            rp.h_room_rp_least_day=1;
            rp.h_room_rp_longest_day = 365;
            rp.h_room_rp_ctime = DateTime.Now;
                rp.h_room_rp_name_cn = "";
            if (drrrule.TypeCode == "DRRBookAhead")
            {
                string last = string.Format("提前{0}天预订，每间晚优惠{1}％",  drrrule.DayNum, drrrule.DeductNum * 10);
                if (string.IsNullOrEmpty(drrrule.DrrName))
                    rp.h_room_rp_name_cn = string.Format("促销({0})", last);
                else
                rp.h_room_rp_name_cn = string.Format("促销({0})",drrrule.DrrName);                
                drrrule.Description = string.Format("促销规则：入住日期在{0}-{1},{2}", drrrule.StartDate.Value.ToShortDateString(), drrrule.EndDate.Value.ToShortDateString(), last);
            }
            if (drrrule.TypeCode == "DRRStayPerRoomPerNight")
            {
                string last = string.Format("连住{2}天，每间晚优惠{3}％", drrrule.StartDate, drrrule.EndDate, drrrule.CheckInNum, drrrule.DeductNum * 10);
                if (string.IsNullOrEmpty(drrrule.DrrName))
                    rp.h_room_rp_name_cn = string.Format("促销({0})", last);                  
                else
                    rp.h_room_rp_name_cn = string.Format("促销({0})", drrrule.DrrName);
                drrrule.Description = string.Format("促销规则：入住日期在{0}-{1},{2}", drrrule.StartDate.Value.ToShortDateString(), drrrule.EndDate.Value.ToShortDateString(), last);
          
            }
           // drrrule.StartDate.Value.tos;
          //drrrule.StartDate=drrrule.EndDate.Value.ToShortDateString();
            //插入RP
            db.rps.Add(rp);
            db.SaveChanges();
            //取rpId
            var f=(from r in db.rps where r.RatePlanId == guid select r.h_room_rp_id).SingleOrDefault().ToString();
            drrrule.RatePlanId = f.ToString();;

            if (ModelState.IsValid)
            {
                db.drrs.Add(drrrule);
                db.SaveChanges();
                return RedirectToAction("MyDrr", drrrule);
            }
            GetData();
            return View("MyDrr", drrrule);
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
            if (ModelState.IsValid)
            {
                db.Entry(drrrule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("",drrrule);
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