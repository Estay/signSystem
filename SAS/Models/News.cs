using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace SAS.Models
{
    public class News
    {
        public int ID { get; set; }
        public string Ttitle { get; set; }
        public string Descripiton{ get; set; }

    }
    public class NewsDBContent:DbContext
    {
        public DbSet<News> news { get; set; }
    }
}