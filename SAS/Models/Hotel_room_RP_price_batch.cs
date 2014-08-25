using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class Hotel_room_RP_price_batch
    {
        #region Model
        private int _hpbid;
        private int _hotel_id;
        private int _room_id;
        private int _room_rp_id;
        private int _roomtypeid = 0;
        private int _priceid = 0;
        private decimal _price;
        private decimal _cost = -1M;
        private decimal _weekend;
        private decimal _membercost;
        private decimal _weekendcost;
        private decimal _addbed;
        private string _uid;
        private string _hoteluid;
        private DateTime _idate;
        private DateTime _room_rp_start_time;
        private DateTime _room_rp_end_time;
        private DateTime _hpdate;
        private int _status = 1;
        private int _hpstatus = 0;
        private bool _isaudit = false;
        private string _audituid;
        private DateTime _auditdate;
        private int _auditstatus = -1;
        private decimal _commission = -1M;
        /// <summary>
        /// 
        /// </summary>
        [KeyAttribute]
        public int Hpbid
        {
            set { _hpbid = value; }
            get { return _hpbid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Hotel_id
        {
            set { _hotel_id = value; }
            get { return _hotel_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Room_id
        {
            set { _room_id = value; }
            get { return _room_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Room_rp_id
        {
            set { _room_rp_id = value; }
            get { return _room_rp_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RoomTypeId
        {
            set { _roomtypeid = value; }
            get { return _roomtypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PriceID
        {
            set { _priceid = value; }
            get { return _priceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Cost
        {
            set { _cost = value; }
            get { return _cost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Weekend
        {
            set { _weekend = value; }
            get { return _weekend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MemberCost
        {
            set { _membercost = value; }
            get { return _membercost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal WeekendCost
        {
            set { _weekendcost = value; }
            get { return _weekendcost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Addbed
        {
            set { _addbed = value; }
            get { return _addbed; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HotelUid
        {
            set { _hoteluid = value; }
            get { return _hoteluid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Idate
        {
            set { _idate = value; }
            get { return _idate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Room_rp_start_time
        {
            set { _room_rp_start_time = value; }
            get { return _room_rp_start_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Room_rp_end_time
        {
            set { _room_rp_end_time = value; }
            get { return _room_rp_end_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Hpdate
        {
            set { _hpdate = value; }
            get { return _hpdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int HpStatus
        {
            set { _hpstatus = value; }
            get { return _hpstatus; }
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
        public DateTime AuditDate
        {
            set { _auditdate = value; }
            get { return _auditdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AuditStatus
        {
            set { _auditstatus = value; }
            get { return _auditstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Commission
        {
            set { _commission = value; }
            get { return _commission; }
        }
        #endregion Model
    }
}