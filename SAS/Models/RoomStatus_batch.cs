using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class RoomStatus_batch
    {
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
        private int? _aviebeds;
        private int? _ebeds;
        private int? _hpstatus;
        private string _uid;
        private string _hoteluid;
        private bool _isaudit = false;
        private string _audituid;
        private DateTime? _auditdate;
        private int? _auditstatus = -1;
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
        /// <summary>
        /// 
        /// </summary>
        public int? avieBeds
        {
            set { _aviebeds = value; }
            get { return _aviebeds; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? eBeds
        {
            set { _ebeds = value; }
            get { return _ebeds; }
        }
        /// <summary>
        /// 发布状态
        /// </summary>
        public int? HpStatus
        {
            set { _hpstatus = value; }
            get { return _hpstatus; }
        }
        /// <summary>
        /// ECS后台操作人
        /// </summary>
        public string Uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 第三方签约后台操作人
        /// </summary>
        public string HotelUid
        {
            set { _hoteluid = value; }
            get { return _hoteluid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsAudit
        {
            set { _isaudit = value; }
            get { return _isaudit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AuditUid
        {
            set { _audituid = value; }
            get { return _audituid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AuditDate
        {
            set { _auditdate = value; }
            get { return _auditdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AuditStatus
        {
            set { _auditstatus = value; }
            get { return _auditstatus; }
        }
        #endregion Model
    }
}