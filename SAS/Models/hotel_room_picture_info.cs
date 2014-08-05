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
      
        #region Model
        private int _h_r_p_id;
        private int _room_id;
        private string _h_r_p_title;
        private string _h_r_p_pic_original_url;
        private string _h_r_p_pic_thumb_url;
        private int? _h_r_p_sort = 10000;
        private string _h_r_p_tag;
        private DateTime? _h_r_p_time;
        private int? _h_r_p_type;
        private int _h_r_p_size;
        /// <summary>
        /// 酒店房间图片编号
        /// </summary>
        /// 
        [KeyAttribute]
        public int h_r_p_id
        {
            set { _h_r_p_id = value; }
            get { return _h_r_p_id; }
        }
        /// <summary>
        /// 酒店房屋编号
        /// </summary>
        public int room_id
        {
            set { _room_id = value; }
            get { return _room_id; }
        }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string h_r_p_title
        {
            set { _h_r_p_title = value; }
            get { return _h_r_p_title; }
        }
        /// <summary>
        /// 图片路径 原图
        /// </summary>
        public string h_r_p_pic_original_url
        {
            set { _h_r_p_pic_original_url = value; }
            get { return _h_r_p_pic_original_url; }
        }
        /// <summary>
        /// 图片路径 缩略
        /// </summary>
        public string h_r_p_pic_thumb_url
        {
            set { _h_r_p_pic_thumb_url = value; }
            get { return _h_r_p_pic_thumb_url; }
        }
        /// <summary>
        /// 排序 默认为10000
        /// </summary>
        public int? h_r_p_sort
        {
            set { _h_r_p_sort = value; }
            get { return _h_r_p_sort; }
        }
        /// <summary>
        /// 用于SEO优化
        /// </summary>
        public string h_r_p_tag
        {
            set { _h_r_p_tag = value; }
            get { return _h_r_p_tag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? h_r_p_time
        {
            set { _h_r_p_time = value; }
            get { return _h_r_p_time; }
        }
        /// <summary>
        /// 图片类型
        /// </summary>
        public int? h_r_p_type
        {
            set { _h_r_p_type = value; }
            get { return _h_r_p_type; }
        }
        /// <summary>
        /// 图片大小
        /// </summary>
        public int h_r_p_size
        {
            set { _h_r_p_size = value; }
            get { return _h_r_p_size; }
        }
        #endregion Model


        
    }
    public class RoomImageDBContent : DbContext
    {
        public DbSet<hotel_room_picture_info> RoomImage { get; set; }
    }
}