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
    public class BillController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Bill/
        int InthotelId = 0; 
        public ActionResult QureyBill(string hotelId,string startTime,string endTime,string page)
        {

            DateTime tempS, tempE; DateTime.TryParse(startTime, out tempS); DateTime.TryParse(endTime, out tempE); int.TryParse(hotelId, out InthotelId);
            DateTime now = DateTime.Now.AddMonths(-1); DateTime s = startTime == null ? now.AddDays(1 - now.Day).Date :tempS; DateTime e = endTime == null? now.AddDays(1-now.Day).AddMonths(1).AddDays(-1).Date:tempE;
            //int.TryParse(hotelId,out InthotelId);int.TryParse();
            object totalPrice, totalGureetePrice, totalOtherPrice, totalPage; int cureentPage; int.TryParse(page, out cureentPage);
            Order_info order = new Order_info() { OrderList = new Order_info().getOrderInfos(new Order_info() { hotel_id = InthotelId, o_check_in_date = s, o_check_out_date = e }, cureentPage, out totalPrice, out totalGureetePrice, out totalOtherPrice, out totalPage) };
            ViewBag.allPage = totalPage;
            ViewBag.curentPage = page == null ? "1" : cureentPage.ToString();
            
            //int t=order.OrderList.Count>0?order.OrderList[0].hotel_id:0;
            ViewBag.curentHotelId = hotelId == null ? order.OrderList.Count > 0 ? order.OrderList[0].hotel_id : 0 : InthotelId;
            ViewBag.totalGureetePrice = totalGureetePrice;
            ViewBag.totalOtherPrice = totalOtherPrice;
            ViewBag.totalPrice = totalPrice;
            return View("MyBill", order);
        }

        // 用户管理零时页面
        public ActionResult MyUser(string hotelId,string startTime,string endTime,string page)
        {
            ViewBag.allPage =10;
            ViewBag.curentPage = page==null?"1":page;
            ViewBag.curentHotelId = hotelId;
            ViewBag.curentstartTime = startTime;
            ViewBag.curentendTime = endTime;
            return View("MyUser");
        }

        //
        // GET: /Bill/Details/5

        public ActionResult Details(int id = 0)
        {
            Order_info order_info = db.orders.Find(id);
            if (order_info == null)
            {
                return HttpNotFound();
            }
            return View(order_info);
        }

        //
        // GET: /Bill/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Bill/Create

        [HttpPost]
        public ActionResult Create(Order_info order_info)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(order_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order_info);
        }

        //
        // GET: /Bill/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Order_info order_info = db.orders.Find(id);
            if (order_info == null)
            {
                return HttpNotFound();
            }
            return View(order_info);
        }

        //
        // POST: /Bill/Edit/5

        [HttpPost]
        public ActionResult Edit(Order_info order_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order_info);
        }

        //
        // GET: /Bill/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Order_info order_info = db.orders.Find(id);
            if (order_info == null)
            {
                return HttpNotFound();
            }
            return View(order_info);
        }

        //
        // POST: /Bill/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_info order_info = db.orders.Find(id);
            db.orders.Remove(order_info);
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