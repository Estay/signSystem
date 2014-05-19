using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class hotel_room_info
    {
        [KeyAttribute]
        public int room_id;
        public string h_r_id;
        public int hotel_id;
        public int h_r_name_cn;
        public class RoomDBContent : DbContext
        {
            public DbSet<hotel_room_info> hotel { get; set; }
        }
    }
}