using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class RoomStatus
    {
        public RoomStatus()
        { }
        #region Model
        private int _r_s_id;
        private int _room_id;
        private string _roomtypeid;
        private DateTime _r_s_time;
        private DateTime? _enddate;
        private int? _r_s_number;
        private bool _overbooking;
        private DateTime? _r_s_utime;
        private DateTime _r_s_ctime;
        private bool _r_s_status;
        private int? _hotel_id;
        private string _hotelcode;
        private int _avieBeds;
        /// <summary>
        /// 可售数
        /// </summary>
        public int AvieBeds
        {
            get { return _avieBeds; }
            set { _avieBeds = value; }
        }
        private int _eBeds;
        /// <summary>
        /// 总房量
        /// </summary>
        public int EBeds
        {
            get { return _eBeds; }
            set { _eBeds = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [KeyAttribute]
        public int r_s_id
        {
            set { _r_s_id = value; }
            get { return _r_s_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int room_id
        {
            set { _room_id = value; }
            get { return _room_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoomTypeId
        {
            set { _roomtypeid = value; }
            get { return _roomtypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime r_s_time
        {
            set { _r_s_time = value; }
            get { return _r_s_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? r_s_number
        {
            set { _r_s_number = value; }
            get { return _r_s_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool OverBooking
        {
            set { _overbooking = value; }
            get { return _overbooking; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? r_s_utime
        {
            set { _r_s_utime = value; }
            get { return _r_s_utime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime r_s_ctime
        {
            set { _r_s_ctime = value; }
            get { return _r_s_ctime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool r_s_status
        {
            set { _r_s_status = value; }
            get { return _r_s_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? hotel_id
        {
            set { _hotel_id = value; }
            get { return _hotel_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HotelCode
        {
            set { _hotelcode = value; }
            get { return _hotelcode; }
        }
        #endregion Model

        //[NotMapped]
        /// <summary>
        /// 房态列表
        /// </summary>
        private List<RoomStatus> roomStatusList = new List<RoomStatus>();
        [NotMapped]
        public List<RoomStatus> RoomStatusList
        {
            get { return roomStatusList; }
            set { roomStatusList = value; }
        }
    }
}