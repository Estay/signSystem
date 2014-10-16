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
    public class UserController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /User/
        int result = 1;
        public ActionResult queryUser()
        {

            return View("MyUser", getData());
        }
        public Merchant_info getData()
        {
            List<Merchant_info> list_Mer = new List<Merchant_info>(); List<SasMenu> list_Menu = new List<SasMenu>(); List<hotel_info> list_hotel = new List<hotel_info>();

            Merchant_info mermber = new Merchant_info();
            mermber.getMemberInfo(out list_Mer, out list_Menu, out list_hotel);
            mermber.List_hotel = list_hotel; mermber.List_Menu = list_Menu; mermber.List_Mer = list_Mer;
            return mermber;
        }

       
  
       
        [HttpPost]
        public ActionResult CreateUser(Merchant_info merchant_info)
        {
            if(string.IsNullOrEmpty(merchant_info.id))
            {
              
                if (ModelState.IsValid)
                {
                    using (db=new HotelDBContent())
                    {
                      db.Merchant_infos.Add(merchant_info);
                      result=db.SaveChanges()>0?1:0;
                    }
                    
                }
            }else
            {
                 
            }
           

            return View("MyUser", getData());
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(string id = null)
        {
            Merchant_info merchant_info = db.Merchant_infos.Find(id);
            if (merchant_info == null)
            {
                return HttpNotFound();
            }
            return View(merchant_info);
        }

      

        /// <summary>
        /// 电话
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public int isOkTel(string text)
        {
          // int hotel_id = Convert.ToInt32(hotelId);
            using (db = new HotelDBContent())
            {
                if ((from m in db.Merchant_infos where m.tel== text  select m).Count() > 0)
                    return 0;
                else
                    return 1;
            }
        }
        //
        // GET: /User/Delete/5

        public ActionResult Delete(string id = null)
        {
            Merchant_info merchant_info = db.Merchant_infos.Find(id);
            if (merchant_info == null)
            {
                return HttpNotFound();
            }
            return View(merchant_info);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Merchant_info merchant_info = db.Merchant_infos.Find(id);
            db.Merchant_infos.Remove(merchant_info);
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