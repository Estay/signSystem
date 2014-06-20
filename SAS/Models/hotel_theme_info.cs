using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAS.Models
{
    public class hotel_theme_info
    {
       
            #region Model
            
           
            /// <summary>
            /// 酒店主题编号自增 增量为1 种子1000
            /// </summary>
            [KeyAttribute]
        public int hotel_theme_id { get; set; }
          
            /// <summary>
            /// 酒店主题名称
            /// </summary>
            public string hotel_theme_title { get; set; }
           
            /// <summary>
            /// 状态 关闭 0 开启1   默认为1
            /// </summary>
            public bool hotel_theme_stated { get; set; }
           

            #endregion Model

            ////get all Theme
            //public static SelectList AllTheme()
            //{
            //    List<SelectListItem> list = new List<SelectListItem>();
            //    khotel_theme_infoDBContent db = new khotel_theme_infoDBContent();
            //    var themes = db.Themes.ToList();
            //    foreach (var t in themes)
            //    {
            //        list.Add(new SelectListItem() { Text = t.hotel_theme_title, Value = t.hotel_theme_id.ToString() });


            //    }
            //    SelectList item = new SelectList(list, "Value", "Text");
            //    return item;
            //}
            //get all Theme
            public static Dictionary<int,string> AllTheme()
            {
                Dictionary<int, string> list = new Dictionary<int, string>();
                //Array array = new Array();
               
                khotel_theme_infoDBContent db = new khotel_theme_infoDBContent();
                var themes = db.Themes.ToList();
               
               
                foreach (var t in themes)
                {
                    list.Add(t.hotel_theme_id, t.hotel_theme_title); ;


                }
                
                return list;
            }


       
    }
    public class khotel_theme_infoDBContent : DbContext
    {
        public DbSet<hotel_theme_info> Themes{ get; set; }
    }
}