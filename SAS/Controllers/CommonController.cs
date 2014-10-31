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
     
        /// <summary>
        /// 验证房型是否存在
        /// </summary>
        /// <param name="text"></param>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public int isOkRoom(string text,string hotelId)
        {
            int hotel_id = Convert.ToInt32(hotelId);
            string strRooms = string.Empty;
            using (db = new HotelDBContent())
            {
                if ((from r in db.rooms where r.h_r_name_cn == text.Trim() && r.hotel_id == hotel_id select r).Count() > 0)
                    return 0;
                else
                    return 1;
              
            }
        
        }
        /// <summary>
        /// 查找房型
        /// </summary>
        /// <param name="text"></param>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public string qureyRoom(string text, string hotelId)
        {
            text = "IW1号测试房";
            int hotel_id=0;int.TryParse(hotelId,out hotel_id); 
            string strRooms = string.Empty;

           
            using (db = new HotelDBContent())
            {
               
                var f = (from helong in db.hotel where (from o in db.hotel where o.hotel_id == hotel_id select o.h_name_cn).Contains(helong.h_name_cn) where helong.source_id == 4 select helong).SingleOrDefault();
                if (f != null)
                {
                    var room = from o in db.rooms where o.h_r_name_cn.Contains(text.Trim()) && o.hotel_id == f.hotel_id select o;
                    foreach (var r in room)
                    {
                        strRooms += string.Format("{0},{1}", r.h_r_name_cn, r.room_id) + "|";
                    }
                }

            }
            return strRooms;
        }
   
        ///// <summary>
        ///// 验证酒店名是否存在
        ///// </summary>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public int IsOkHotel(string text)
        //{
        //    using (db = new HotelDBContent())
        //    {
        //        if ((from h in db.hotel where h.h_name_cn == text select h).Count() > 0 || (from h in new HotelDBContent("").hotel where h.h_name_cn == text select h).Count() > 0)
        //            return 0;
        //        else
        //            return 1;
        //    }

        //}
        /// <summary>
        /// 验证酒店名是否存在
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int IsOkHotel(string text)
        {
            string strResult = string.Empty;
            using (db = new HotelDBContent())
            {
               
                if((from h in db.hotel where  h.h_name_cn==text.Trim() select h).Count()>0)
                return 0;
                else
                return 1;
               
            }
           

        }
        //}
        /// <summary>
        /// 查询酒店
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string queryHotel(string text)
        {
            string strResult = string.Empty;
            using (db = new HotelDBContent())
            {

                var result = from h in db.hotel join b in db.citys on h.h_city equals b.City_id where h.h_name_cn.Contains(text.Trim()) && h.source_id == 4 select new { name = h.h_name_cn, city = b.City_name, f = h.source_id };
                foreach (var r in result)
                {

                    strResult += string.Format("{0}[{1}])", r.name, r.city) + "|";
                }
            }
           
            return strResult;

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