﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAS.Models;

namespace SAS.help
{
    public class HotelInfoHelp
    {
        //酒店的房型列表
        public static List<hotel_room_info> getRooms(int hotel_id)
        {
            return (from r in new hotel_room_infoDBContent().room where r.hotel_id == hotel_id select r).ToList();
        }
        //用户ID所有的酒店
        public static List<hotel_info> getHotlList(string uId)
        {
            return (from h in new hotel_infoDBContent().hotel  where h.u_id==uId select h).ToList();
        }
    }
}