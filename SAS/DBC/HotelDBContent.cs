using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SAS.Models;

namespace SAS.DBC
{
    public class HotelDBContent : DbContext
    {
        public HotelDBContent() : base("DefaultConnection") { }
        public HotelDBContent(string second) : base("SecondConnection") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }
        public DbSet<hotel_info> hotel { get; set; }
        public DbSet<DrrRules> drrs { get; set; }
        public DbSet<Gift> gifts { get; set; }
       // public DbSet<GuaranteeRule> guarantees { get; set; }
        public DbSet<GuaranteeRule> gu { get; set; }
        public DbSet<Hotel_comment_info> coments { get; set; }
        public DbSet<hotel_picture_info> pics { get; set; }
        public DbSet<hotel_room_RP_price_info> price { get; set; }
        public DbSet<hotel_theme_info> themes { get; set; }
        public DbSet<Hotel_theme_type_info> curents { get; set; }
        public DbSet<Order_info> orders { get; set; }
        public DbSet<Room_status_info> rstatus { get; set; }
        public DbSet<hotel_room_info> rooms { get; set; }
        public DbSet<hotel_room_picture_info> roomImages { get; set; }
        public DbSet<DrrModes> drrmodes { get; set; }
        public DbSet<Hotel_room_RP_info> rps { get; set; }
        public DbSet<Hotel_room_RP_price> realPrices { get; set; }
        public DbSet<Hotel_room_RP_price_batch> publicPrices { get; set; }
    }
}