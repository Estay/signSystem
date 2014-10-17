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
        int result = 1,Id=0;
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
            merchant_info.status = true;
            using (db = new HotelDBContent())
            {
                if (merchant_info.id > 0)
                {
                     //if(merchant_info.password=="******")

                    merchant_info.updateUser(merchant_info);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        merchant_info.ctime = DateTime.Now; merchant_info.status = true; merchant_info.guid = Guid.NewGuid().ToString(); merchant_info.operator_id = new help.HotelInfoHelp().getUId(); merchant_info.status = true;
                        //using (db=new HotelDBContent())
                        //{
                        db.Merchant_infos.Add(merchant_info);
                        result = db.SaveChanges() > 0 ? 1 : 0;
                        //}

                    }
                }
            }
           

            return View("MyUser", getData());
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

        //修改用户
        public ActionResult update(string id)
        {

            ViewBag.Tag = "修改用户资料"; int.TryParse(id, out Id);
            Merchant_info mer = (from m in db.Merchant_infos where m.id == Id select m).Single();
            List<Merchant_info> list_Mer = new List<Merchant_info>(); List<SasMenu> list_Menu = new List<SasMenu>(); List<hotel_info> list_hotel = new List<hotel_info>();
            mer.getMemberInfo(out list_Mer, out list_Menu, out list_hotel);
            mer.List_hotel = list_hotel; mer.List_Menu = list_Menu; mer.List_Mer = list_Mer;
            return View("MyUser", mer);
        }

        //删除用户
        public ActionResult remove(string id)
        {
            ViewBag.Tag = "创建用户";int.TryParse(id, out Id);
            Merchant_info m = db.Merchant_infos.Find(Id);
            if (m != null)
            {

                db.Merchant_infos.Remove(m);
                ViewBag.sign=db.SaveChanges() > 0?1:0;
                
            }
            return View("MyUser",getData());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}