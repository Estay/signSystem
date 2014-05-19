using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class hotel_room_picture_info
    {
        [KeyAttribute]
        public  int h_r_p_id;
        public int room_id;
        public string h_r_p_title;
        public string h_r_p_pic_original_url;

        public class RoomImageDBContent : DbContext
        {
            public DbSet<hotel_room_picture_info> hotel { get; set; }
        }
    }
}