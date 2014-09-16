using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.DBC;
using SAS.Models;
using SAS.help;

namespace SAS.Controllers
{
    public class DrrRuleController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /DrrRule/

    

        //
        // GET: /DrrRule/Create

        //首次加载
        public ActionResult MyDrr(string id)
        {
            SetName();
            int hotel_id;
            int.TryParse(id,out hotel_id);

            oprationed(id);

            return View(new DrrRules());
        }
        public void oprationed(string id)
        {
            string u_id = "test1";
            var drrs = new HotelInfoHelp().getHotlList(u_id);
            ViewData["hotels"] = drrs;
            ViewData["drrModes"] = help.HotelInfoHelp.getDrrModeList(u_id);
            if (id == null && drrs.Count > 0)
            {
                GetData(drrs[0].hotel_id.ToString());
            }
            else
            {
                GetData(id);
            }
            
        }

        //更新促销
        public ActionResult updateDrr(string id)
        {
            int drrId;
            int.TryParse(id, out drrId);
            GetData();
            
           var drr=(from d in db.drrs where d.id == drrId select d).Single();
           ViewBag.Id = drr.hotel_id;
           ViewBag.title = "修改促销规则";
           ViewBag.buttonName = "修改";
           GetData(drr.hotel_id.ToString());
           return View("MyDrr", drr);
        }
        //删除促销
        public ActionResult deleteDrr(string id)
        {
            try
            {
                int drrId;
                int.TryParse(id, out drrId);
                var drr = (from d in db.drrs where d.id == drrId select d).SingleOrDefault();
                db.drrs.Remove(drr);
                db.SaveChanges();
                var rp = (from r in db.rps where r.h_room_rp_id == drr.h_room_rp_id && r.h_room_rp_state == true select r).SingleOrDefault();
                if (rp!=null)
                {
                    rp.h_room_rp_state = false;
                    rp.h_room_rp_utime = DateTime.Now;
                    if (db.SaveChanges() > 0)
                        ViewBag.sign = 1;
                    else
                        ViewBag.sign = 0;
                }
                GetData();
                ViewBag.Id = drr.hotel_id;
                GetData(drr.hotel_id.ToString());
            }
            catch (Exception e)
            {
                help.DBhelp.log("促销信息删除失败"+e.ToString());
                throw;
            }
            SetName();
           
            return View("MyDrr", new DrrRules());
        }

        private void SetName()
        {
            ViewBag.title = "添加促销规则";
            ViewBag.buttonName = "添加";
        }
        //
        // POST: /DrrRule/Create
        //修改和添加提交
        [HttpPost]
        public ActionResult Create(DrrRules drrrule)
        {
            SetName();
            drrrule.DayNum = drrrule.CheckInNum == null ?0 : drrrule.DayNum;
            drrrule.CheckInNum = drrrule.DayNum == null ? 0 : drrrule.CheckInNum;

            if (drrrule.TypeCode == "DRRBookAhead")
            {
                string last = string.Format("提前{0}天预订，每间晚优惠{1}％", drrrule.DayNum, drrrule.DeductNum * 10);
                drrrule.Description = string.Format("促销规则：入住日期在{0}-{1},{2}", drrrule.StartDate.Value.ToShortDateString(), drrrule.EndDate.Value.ToShortDateString(), last);
            }
            if (drrrule.TypeCode == "DRRStayPerRoomPerNight")
            {
                string last = string.Format("连住{2}天，每间晚优惠{3}％", drrrule.StartDate, drrrule.EndDate, drrrule.CheckInNum, drrrule.DeductNum * 10);
                drrrule.Description = string.Format("促销规则：入住日期在{0}-{1},{2}", drrrule.StartDate.Value.ToShortDateString(), drrrule.EndDate.Value.ToShortDateString(), last);

            }
            if (drrrule.id > 0)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(drrrule).State = EntityState.Modified;
                    db.SaveChanges();
                   
                }
            }
            else
            {
                #region
                string guid = Guid.NewGuid().ToString();

                Hotel_room_RP_info rp = new Hotel_room_RP_info();

                rp.RatePlanId = guid;
                rp.hotel_id = drrrule.hotel_id;
                rp.h_room_rp_is_to_store_pay = true;
                rp.h_room_rp_check_in = "00:00:00";
                rp.h_room_rp_check_out = "23:59:00";
                rp.h_room_rp_least_day = 1;
                rp.h_room_rp_longest_day = 365;
                rp.h_room_rp_ctime = DateTime.Now;
                rp.h_room_rp_name_cn = "";
              
                rp.h_room_rp_name_cn = string.Format("促销({0})", drrrule.DrrName);

                var tempRp = (from r in db.rps where r.h_room_rp_name_cn == rp.h_room_rp_name_cn && r.hotel_id == drrrule.hotel_id select r).SingleOrDefault();
                if (tempRp != null)
                {
                    drrrule.h_room_rp_id = tempRp.h_room_rp_id; ;
                }
                else
                {
                    db.rps.Add(rp);
                    db.SaveChanges();
                    //取rpId
                    var f = (from r in db.rps where r.RatePlanId == guid select r.h_room_rp_id).SingleOrDefault();
                    drrrule.h_room_rp_id = f; ;
                }
                // drrrule.StartDate.Value.tos;
                //drrrule.StartDate=drrrule.EndDate.Value.ToShortDateString();
                //插入RP

                //要操作的酒店

             

                if (ModelState.IsValid)
                {
                    db.drrs.Add(drrrule);
                    db.SaveChanges();
                

                }
                #endregion
            }
            GetData();
            GetData(drrrule.hotel_id.ToString());
            return View("MyDrr", new DrrRules());
          
        }
        public void GetData()
        {
            
            string u_id = "test1";
            ViewData["drrModes"] = help.HotelInfoHelp.getDrrModeList(u_id);
            ViewData["hotels"] = new HotelInfoHelp().getHotlList(u_id);

           // //所有酒店列表
           // ViewData["rooms"] = help.HotelInfoHelp.getRooms(u_id);
           // //所有酒店对应的房型列表
            
           //// ViewData["drrModes"] = help.HotelInfoHelp.getDrrModeList(u_id);
           // ViewData["drrs"] = help.HotelInfoHelp.getDrrList(u_id);

           
        }
        //验证房型是否存在
        public int IsOk(string id, string text)
        {
            int drrId; int.TryParse(id, out drrId);
            using (db = new HotelDBContent())
            {
                if ((from h in db.drrs where h.DrrName == text && h.hotel_id == drrId select h).Count() > 0)
                    return 0;
                else
                    return 1;
            }
        }

        public void GetData(string hotelId)
        {

            int hotel_id;
            int.TryParse(hotelId, out hotel_id);
            string u_id = "test1";
           // GetData();
            //所有酒店列表
            ViewData["rooms"] = new hotel_room_info().getRoomsByHoltelId(hotel_id);
            //所有酒店对应的房型列表
            
            // ViewData["drrModes"] = help.HotelInfoHelp.getDrrModeList(u_id);
            ViewData["drrs"] = new DrrRules().getDrrsByHoltelId(hotel_id);
            ViewBag.Id = hotel_id;

        }
        public ActionResult selectH(string hotelId)
        {
            GetData(hotelId);
            return   View(new DrrRules());
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