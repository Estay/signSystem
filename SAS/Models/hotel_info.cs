using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace SAS.Models
{
    public class hotel_info
    {
        //[key]


        
       // public int id { get; set; }
        [KeyAttribute]
        public int hotel_id { get; set; }
        public string h_name_cn { get; set; }
        public string h_location_cn { get; set; }
        public string h_description_cn { get; set; }
      
        public hotel_room_info room;
        public hotel_room_picture_info roomImage;
    }
    public class hotel_infoDBContent : DbContext
    {
        public DbSet<hotel_info> hotel { get; set; }
    }
}