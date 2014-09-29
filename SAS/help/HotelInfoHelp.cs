using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAS.DBC;
using SAS.Models;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Web.SessionState;

namespace SAS.help
{
    public class HotelInfoHelp : System.Web.UI.Page, IRequiresSessionState
    {

        string uId = string.Empty;
        private static HotelDBContent db =null;
        public static int pageSize = 30;
        public HotelInfoHelp()
        {
            uId = getUId();
        }

        //酒店的房型列表
        public static List<hotel_room_info> getRooms(int hotelId)
        {

            using(db = new HotelDBContent())
            {
              return (from r in db.rooms where r.hotel_id == hotelId select r).ToList();
            }

        }

        //用户ID所有的酒店
        public  List<hotel_info> getHotlList(string iuId)
        {
           // uId = "test1";
            //c = new help.HotelInfoHelp().getUId();
           // uId=
            using (db = new HotelDBContent())
            {
                return (from h in db.hotel where h.u_id == uId && h.h_state == true select h).ToList();
            }
        }


         //用户ID所有的酒店
        public  List<DrrRules> getDrrList(string iuId)
        {
            int[] rf = (from h in  db.hotel where h.u_id == uId select h.hotel_id).ToArray();
            try
            {
               // uId = "test1";
               
               // var f = from h in db.drrs where rf.Contains(h.hotel_id) select h;
                var rfr=(from h in  db.drrs where rf.Contains(h.hotel_id) select h).ToList();
            }
            catch (Exception e)
            {
                
                throw e;
            }

            return (from h in  db.drrs where rf.Contains(h.hotel_id) select h).ToList();
        }

        //所有促销类型
        public static List<DrrModes> getDrrModeList(string uId)
        {

            using (db = new HotelDBContent())
            {
                return (from h in db.drrmodes select h).ToList();
            }
           
        }
        //得到查询日期
        public static Dictionary<string, string> getDate(DateTime start, DateTime end)
        {
            Dictionary<string, string> dates = new Dictionary<string, string>();
            int t = Convert.ToInt32((end - start).TotalDays) + 1;
            for (int i = 0; i < t; i++)
            {
                DateTime d = start.AddDays(i);
                string day = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(d.DayOfWeek).Substring(2);
                dates.Add(d.ToString("MM-dd"), day);
            }
            return dates;
        }
        /// <summary>
        /// 得到RatePlanId
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        public static int getRatePlanId(Hotel_room_RP_info rp)
        {
            int ratePlanId = 0;
            rp.RatePlanId = Guid.NewGuid().ToString();
            rp.h_room_rp_is_to_store_pay = true;
            rp.h_room_rp_check_in = "00:00:00";
            rp.h_room_rp_check_out = "23:59:00";
            rp.h_room_rp_least_day = 1;
            rp.h_room_rp_longest_day = 365;
            rp.h_room_rp_ctime = DateTime.Now;
           // rp.h_room_rp_name_cn = "";
            using (HotelDBContent db = new HotelDBContent())
            {
                var tempRp = (from r in db.rps where r.h_room_rp_name_cn == rp.h_room_rp_name_cn && r.hotel_id == rp.hotel_id select r).SingleOrDefault();
                if (tempRp != null)
                {
                    ratePlanId = tempRp.h_room_rp_id; ;
                }
                else
                {
                    db.rps.Add(rp);
                    db.SaveChanges();
                    //取rpId
                    ratePlanId = (from r in db.rps where r.RatePlanId == rp.RatePlanId select r.h_room_rp_id).SingleOrDefault();
               
                }
            }
        
            return ratePlanId;
        }
        /// <summary>
        /// 开始时间(为空上个月的1号)
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public DateTime getStartDate(string startTime)
        {
            DateTime tempS, tempE; DateTime.TryParse(startTime, out tempS); 
            DateTime now = DateTime.Now.AddMonths(-1);
            return string.IsNullOrEmpty(startTime) ? now.AddDays(1 - now.Day).Date : tempS;
          
        }
        /// <summary>
        /// 结速日期(上个月的最后一天)
        /// </summary>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DateTime getEndDate(string endTime)
        {
            DateTime tempS, tempE;
            DateTime.TryParse(endTime, out tempE);
            DateTime now = DateTime.Now.AddMonths(-1);
            return string.IsNullOrEmpty(endTime) ? now.AddDays(1 - now.Day).AddMonths(1).AddDays(-1).Date : tempE;
        }
        /// <summary>
        /// 或得用户账号,用于hotel_info里面的u_id字段
        /// </summary>
        /// <returns></returns>
        public  string getUId()
        {
            return "13528873363";
            //HttpContext.Current.Session["uid"] = "admintest";
            //HttpContext.Current.Session["userName"] = "测试账号";
           // Session["uid"];
           
            //return HttpContext.Current.Session["uid"].ToString();
           // return Session["uid"].ToString();
        }
    }

}
