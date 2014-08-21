using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class hotel_picture_info
    {
        public hotel_picture_info()
        { }
        #region Model
        private int _h_p_id;
        private int _hotel_id;
        private string _h_p_title;
        private string _h_p_pic_original_url;
        private string _h_p_pic_thumb_url;
        private int? _h_p_sort = 10000;
        private DateTime _h_p_time;
        private string _h_p_tag;
        private int? _h_p_type;
        private int _h_p_size;
        private int width;
        private bool status;

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        /// <summary>
        /// 酒店图片编号
        /// </summary>
        /// 
         [KeyAttribute]
        public int h_p_id
        {
            set { _h_p_id = value; }
            get { return _h_p_id; }
        }
        /// <summary>
        /// 酒店ID
        /// </summary>
        public int hotel_id
        {
            set { _hotel_id = value; }
            get { return _hotel_id; }
        }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string h_p_title
        {
            set { _h_p_title = value; }
            get { return _h_p_title; }
        }
        /// <summary>
        /// 原图 （大）
        /// </summary>
        public string h_p_pic_original_url
        {
            set { _h_p_pic_original_url = value; }
            get { return _h_p_pic_original_url; }
        }
        /// <summary>
        /// 缩略（小）
        /// </summary>
        public string h_p_pic_thumb_url
        {
            set { _h_p_pic_thumb_url = value; }
            get { return _h_p_pic_thumb_url; }
        }
        /// <summary>
        /// 排序（默认10000 ）
        /// </summary>
        public int? h_p_sort
        {
            set { _h_p_sort = value; }
            get { return _h_p_sort; }
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime h_p_time
        {
            set { _h_p_time = value; }
            get { return _h_p_time; }
        }
        /// <summary>
        /// 关键字，(用于SEO)
        /// </summary>
        public string h_p_tag
        {
            set { _h_p_tag = value; }
            get { return _h_p_tag; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public int? h_p_type
        {
            set { _h_p_type = value; }
            get { return _h_p_type; }
        }
        /// <summary>
        /// 大小
        /// </summary>
        public int h_p_size
        {
            set { _h_p_size = value; }
            get { return _h_p_size; }
        }
        #endregion Model

        public Dictionary<string, int> getImageType()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            string[] str = System.Configuration.ConfigurationManager.AppSettings["ImageType"].Split(',');
            for (int i =0 ; i <str.Length; i++)
            {
                dic.Add(str[i], i);
            }
            return dic;
        }
    }
  
}