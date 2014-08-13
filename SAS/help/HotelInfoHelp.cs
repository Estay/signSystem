using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAS.Models;

namespace SAS.help
{
    public class HotelInfoHelp
    {
        //酒店的房型列表
        public static List<hotel_room_info> getRooms(string uId)
        {
            uId = "test1";
            int[] rf = (from h in new hotel_infoDBContent().hotel where h.u_id == uId select h.hotel_id).ToArray();    
            return (from r in new hotel_room_infoDBContent().room where rf.Contains(r.hotel_id) select r).ToList();
        }
        //用户ID所有的酒店
        public static List<hotel_info> getHotlList(string uId)
        {
            uId = "test1";
            return (from h in new hotel_infoDBContent().hotel  where h.u_id==uId select h).ToList();
        }

         //用户ID所有的酒店
        public static List<DrrRule> getDrrList(string uId)
        {
            uId = "test1";
            int[] rf = (from h in new hotel_infoDBContent().hotel where h.u_id == uId select h.hotel_id).ToArray();  
            return (from h in new DrrRuleDBContent().drrs where rf.Contains(h.hotel_id) select h).ToList();
        }
    
    }
}