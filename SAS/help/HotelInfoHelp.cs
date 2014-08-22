using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAS.DBC;
using SAS.Models;

namespace SAS.help
{
    public class HotelInfoHelp
    {
        private static HotelDBContent db = new HotelDBContent();
        //酒店的房型列表
        public static List<hotel_room_info> getRooms(string uId)
        {
            uId = "test1";
            int[] rf = (from h in  db.hotel where h.u_id == uId select h.hotel_id).ToArray();
        
            return (from r in  db.rooms where rf.Contains(r.hotel_id) select r).ToList();
        }
        //用户ID所有的酒店
        public static List<hotel_info> getHotlList(string uId)
        {
            
            return (from h in db.hotel where h.u_id == uId select h).ToList();
        }

         //用户ID所有的酒店
        public static List<DrrRules> getDrrList(string uId)
        {
            int[] rf = (from h in  db.hotel where h.u_id == uId select h.hotel_id).ToArray();
            try
            {
                uId = "test1";
               
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
             
            try
            {
                var f = from h in  db.drrmodes select h;
                var g = (from h in  db.drrmodes select h).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }

            return (from h in  db.drrmodes select h).ToList();
        }
    
    
    }
}