using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class Room_status_info
    {
        #region Model
        private int _r_s_id;
        private int _room_id;
        private string _roomtypeid;
        private DateTime _r_s_time;
        private DateTime? _enddate;
        private int? _r_s_number = 0;
        private bool _overbooking = false;
        private DateTime? _r_s_utime;
        private DateTime _r_s_ctime;
        private bool _r_s_status;
        private int _hotel_id;
        private string _hotelcode;
        /// <summary>
        /// 房态自增编号
        /// </summary>
        /// 
        [KeyAttribute]
        public int r_s_id
        {
            set { _r_s_id = value; }
            get { return _r_s_id; }
        }
        /// <summary>
        /// 房型编号 我库
        /// </summary>
        public int room_id
        {
            set { _room_id = value; }
            get { return _room_id; }
        }
        /// <summary>
        /// 艺龙可售库存
        /// </summary>
        public string RoomTypeId
        {
            set { _roomtypeid = value; }
            get { return _roomtypeid; }
        }
        /// <summary>
        /// 房态日期 
        /// </summary>
        public DateTime r_s_time
        {
            set { _r_s_time = value; }
            get { return _r_s_time; }
        }
        /// <summary>
        /// 可用结束日期
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 房量 暂不用
        /// </summary>
        public int? r_s_number
        {
            set { _r_s_number = value; }
            get { return _r_s_number; }
        }
        /// <summary>
        /// 是否超售  0不可 1可以 默认0
        /// </summary>
        public bool OverBooking
        {
            set { _overbooking = value; }
            get { return _overbooking; }
        }
        /// <summary>
        /// 编辑时间 
        /// </summary>
        public DateTime? r_s_utime
        {
            set { _r_s_utime = value; }
            get { return _r_s_utime; }
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime r_s_ctime
        {
            set { _r_s_ctime = value; }
            get { return _r_s_ctime; }
        }
        /// <summary>
        /// 房态标识  1为可用   0为不可用
        /// </summary>
        public bool r_s_status
        {
            set { _r_s_status = value; }
            get { return _r_s_status; }
        }
        /// <summary>
        /// 关联酒店编号
        /// </summary>
        public int hotel_id
        {
            set { _hotel_id = value; }
            get { return _hotel_id; }
        }
        /// <summary>
        /// 酒店编号
        /// </summary>
        public string HotelCode
        {
            set { _hotelcode = value; }
            get { return _hotelcode; }
        }
        #endregion Model

    }
  
}