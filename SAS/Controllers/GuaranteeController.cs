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
    public class GuaranteeController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Guarantee/

        public ActionResult Index()
        {
            return View(db.hotel.ToList());
        }
        private void setName()
        {
            ViewBag.title = "设置担保";
            ViewBag.buttonName = "添加";
        }
        //
        // GET: /Guarantee/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    GuaranteeRule guaranteerule = db.hotel.Find(id);
        //    if (guaranteerule == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(guaranteerule);
        //}

        //
        // GET: /Guarantee/Create

        public ActionResult MyGuarantee(string id)
        {
            GetData(id);
            setName();
            GuaranteeRule guaranteeRule = new GuaranteeRule();
            guaranteeRule.ChangeRule = 1;
            guaranteeRule.GuaranteeType = "FirstNightCost";
            return View(guaranteeRule);
        }
        public void GetData(string id)
        {
            string u_id = "test1";
            int hotel_id;
            int.TryParse(id, out hotel_id);
            var hotels= help.HotelInfoHelp.getHotlList(u_id);
            if(id==null &&hotels.Count>0)
                hotel_id = hotels[0].hotel_id;
         
            ViewData["hotels"] = hotels;
            //所有酒店列表
            ViewData["rooms"] = help.HotelInfoHelp.getRooms(hotel_id);

            //所有酒店对应的房型列表

            ViewData["Gurarantees"] = new GuaranteeRule().GuraranteeList(hotel_id);
            ViewBag.Id = hotel_id;
        }

        //修改担保
        public ActionResult updateG(string id)
        {
            int gId;
            int.TryParse(id, out gId);
            var gu = (from g in db.gu where g.id == gId select g).Single();
            ViewBag.Id = gu.hotel_id;
            GetData(gu.hotel_id.ToString());
            ViewBag.title = "修改担保";
            ViewBag.buttonName = "修改";
            return View("MyGuarantee", gu);
        }
        //删除担保
        public ActionResult deleteG(string id)
        {
            int gId;
            int.TryParse(id, out gId);
            var gu = (from g in db.gu where g.GuaranteeRulesId == gId select g).Single();

            db.gu.Remove(gu);
            if (db.SaveChanges() > 0)
                ViewBag.sign = 1;
            else
                ViewBag.sign = 0;

            ViewBag.Id = gu.hotel_id;
            GetData(gu.hotel_id.ToString());
            setName();
            return View("MyGuarantee", new GuaranteeRule());
        }

        //
        // POST: /Guarantee/Create

        [HttpPost]
        public ActionResult Create(GuaranteeRule guaranteerule)
        {
          

            if (guaranteerule.id > 0)
            {
               
                db.Entry(guaranteerule).State = EntityState.Modified;
            }
            else
            {
                var rp=(from r in db.rps where r.hotel_id==guaranteerule.hotel_id && r.h_room_rp_state==true && r.h_room_rp_name_cn=="标准价"select r.h_room_rp_id).SingleOrDefault();
                if (rp>0)
                {
                    guaranteerule.h_room_rp_id = rp;
                    if (ModelState.IsValid)
                    {
                        db.gu.Add(guaranteerule);
                      
                       
                    }
                }
            }
            db.SaveChanges();
            db.Dispose();
            GetData(guaranteerule.hotel_id.ToString());
            return View("MyGuarantee", new GuaranteeRule());
        }

        //
        // GET: /Guarantee/Edit/5

  

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

        //public ActionResult Delete(int id = 0)
        //{
        //    GuaranteeRule guaranteerule = db.hotel.Find(id);
        //    if (guaranteerule == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(guaranteerule);
        //}

        //
        // POST: /Guarantee/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GuaranteeRule guaranteerule = db.hotel.Find(id);
        //    db.hotel.Remove(guaranteerule);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}