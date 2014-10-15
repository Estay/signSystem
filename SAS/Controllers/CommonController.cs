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
    public class CommonController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        public CommonController()
        { }
     
        //验证房型是否存在
        public int isOkRoom(string text,string hotelId)
        {
            int hotel_id = Convert.ToInt32(hotelId);
            using (db = new HotelDBContent())
            {
                if ((from h in db.rooms where h.h_r_name_cn == text && h.hotel_id == hotel_id select h).Count() > 0)
                    return 0;
                else
                    return 1;
            }
        }
        /// <summary>
        /// 验证酒店名是否存在
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int IsOkHotel(string text)
        {
            using (db = new HotelDBContent())
            {
                if ((from h in db.hotel where h.h_name_cn == text select h).Count() > 0 || (from h in new HotelDBContent("").hotel where h.h_name_cn == text select h).Count() > 0)
                    return 0;
                else
                    return 1;
            }

        }
        ///// <summary>
        ///// 验证酒店名称是否存在
        ///// </summary>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public int IsOk(string text)
        //{

        //    using (db = new HotelDBContent())
        //    {

        //        if ((from h in db.hotel where h.h_name_cn == text select h).Count() > 0)
        //            return 0;
        //        else
        //            return 1;
        //    }
            

        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}