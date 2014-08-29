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
    public class ImageController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Image/

      

        //
        // GET: /Image/Details/5

    

        //
        // GET: /Image/Create

        public ActionResult Create(string hotelId)
        {
           // hotelId = "48385";
            ViewBag.HotelId = hotelId;
            ViewData["ImageTypes"] = new hotel_picture_info().getImageType();
            getRooms(Convert.ToInt32(hotelId));
            return View();
        }

        //
        // POST: /Image/Create

        [HttpPost]
        public ActionResult CreateSub(string hotelId)
        {
            try
            {
                int hotel_id;
                if (int.TryParse(hotelId, out hotel_id))
                {
                    if ((from h in  db.hotel where h.hotel_id == hotel_id select h).Count() > 0)
                    {
                        string sql = string.Format("update hotel_room_picture_info set state=1 where room_id in(select room_id from hotel_room_info where hotel_id in({0}))", hotel_id);
                        if (DBhelp.ExcuteTableBySQL(sql) > 0)
                            return View("Success");
                        else
                            DBhelp.log("图片提交失败" + hotelId); return View("Faiture");


                    }
                }
                else
                {
                    DBhelp.log("图片提交转换出错");
                    return View("Faiture");
                }
            }
            catch (Exception ex)
            {

                DBhelp.log("图片提交" + ex.ToString());
                return View("Faiture");
            }
         
            //if (ModelState.IsValid)
            //{
            //    db.room.Add(hotel_picture_info);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View(new hotel_picture_info());
        }

        //
        // GET: /Image/Edit/5

     

        //
        // POST: /Image/Edit/5

    

        //
        // GET: /Image/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    hotel_picture_info hotel_picture_info = db.room.Find(id);
        //    if (hotel_picture_info == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hotel_picture_info);
        //}

        //
        // POST: /Image/Delete/5

       // [HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    hotel_picture_info hotel_picture_info = db.room.Find(id);
        //    db.room.Remove(hotel_picture_info);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public void getRooms(int hotel_id)
        {
            //List<hotel_room_info> roomsList = (from r in db.room where r.hotel_id == hotel_id select r).ToList();
            ViewData["rooms"] = DBhelp.getRooms(hotel_id);
        }
    }
}