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
    public class OrderController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Order/
        Order_info order =null;
        public ActionResult MyOrder()
        {
            order = new Order_info();
            order.OrderList = order.getOrderInfos(Order_info.orderStatus.newOrder);
            return View(order);
        }


        public ActionResult CheckOrder()
        {
            order = new Order_info();
            order.OrderList = order.getOrderInfos(Order_info.orderStatus.confirmed);
            return View("CheckOrderinfo", order);
        }
        public ActionResult QueryOrder()
        {
            return View("QueryOrderInfo");
        }
       
        //
        // POST: /Order/Create

        [HttpPost]
        public ActionResult Create(Order_info order_info)
        {
            //if (ModelState.IsValid)
            //{
            //    db.hotel.Add(order_info);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View(order_info);
        }

        //
        // GET: /Order/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Order_info order_info = db.hotel.Find(id);
        //    if (order_info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order_info);
        //}

        //
        // POST: /Order/Edit/5

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
        // GET: /Order/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    Order_info order_info = db.hotel.Find(id);
        //    if (order_info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order_info);
        //}

        //
        // POST: /Order/Delete/5

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