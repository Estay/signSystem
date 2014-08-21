using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SAS.DBC;

namespace SAS.Models
{
    public class Hotel_theme_type_info
    {
        public Hotel_theme_type_info()
		{}
		#region Model
		private int _t_id;
		private string _t_name;
		private DateTime _t_ctime;
		private bool _t_stated= true;
		/// <summary>
		/// 酒店主题类型
		/// </summary>
         [KeyAttribute]
		public int t_id
		{
			set{ _t_id=value;}
			get{return _t_id;}
		}
		/// <summary>
		/// 主题类型名称
		/// </summary>
		public string t_name
		{
			set{ _t_name=value;}
			get{return _t_name;}
		}
		/// <summary>
		/// 录入时间
		/// </summary>
		public DateTime t_ctime
		{
			set{ _t_ctime=value;}
			get{return _t_ctime;}
		}
		/// <summary>
		/// 状态（0未启用 1启用）默认1
		/// </summary>
		public bool t_stated
		{
			set{ _t_stated=value;}
			get{return _t_stated;}
		}
		#endregion Model

        public static Dictionary<int, string> allCategory()
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            //Array array = new Array();

            HotelDBContent db = new HotelDBContent();
            var themes = db.curents.ToList();


            foreach (var t in themes)
            {
                list.Add(t._t_id, t._t_name); ;


            }
            return list;
 
        }


    }
  
}