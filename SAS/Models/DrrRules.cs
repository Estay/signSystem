using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAS.Models
{
    public class DrrRules
    {
       
        public DrrRules()
        { }
        #region Model
        private int _id;
        private int? _drrruleid;
        private string _typecode;
        private string _description;
        private string _datetype ="CheckInDay";
        private DateTime? _startdate=DateTime.Now;
        private DateTime? _enddate = DateTime.Now;
        private int _daynum;
        private int _checkinnum;
        private int? _everycheckinnum;
        private int? _lastdaynum;
        private int? _whichdaynum;
        private string _cashscale;
        private decimal? _deductnum;
        private string _weekset;
        private string _feetype = "WeekdayFee";
        private int _hotel_id;
        private int? _h_room_rp_id;
        private string _rateplanid;
        private string _roomids;
        private string _roomnames;
        private string _drrname;
        /// <summary>
        /// 产品编号
        /// </summary>
       [KeyAttribute]
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 艺龙产品编号
        /// </summary>
        public int? DrrRuleId
        {
            set { _drrruleid = value; }
            get { return _drrruleid; }
        }
        /// <summary>
        /// 产品促销规则类型代码
        /// </summary>
        public string TypeCode
        {
            set { _typecode = value; }
            get { return _typecode; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 日期类型
        /// </summary>
        public string DateType
        {
            set { _datetype = value; }
            get { return _datetype; }
        }
        /// <summary>
        /// 促销生效开始日期
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 促销生效结束日期
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 提前几天
        /// </summary>
        public int DayNum
        {
            set { _daynum = value; }
            get { return _daynum; }
        }
        /// <summary>
        /// 连住几天
        /// </summary>
        public int CheckInNum
        {
            set { _checkinnum = value; }
            get { return _checkinnum; }
        }
        /// <summary>
        /// 每连住几晚
        /// </summary>
        public int? EveryCheckInNum
        {
            set { _everycheckinnum = value; }
            get { return _everycheckinnum; }
        }
        /// <summary>
        /// 最后几天
        /// </summary>
        public int? LastDayNum
        {
            set { _lastdaynum = value; }
            get { return _lastdaynum; }
        }
        /// <summary>
        /// 第几晚及以后优惠
        /// </summary>
        public int? WhichDayNum
        {
            set { _whichdaynum = value; }
            get { return _whichdaynum; }
        }
        /// <summary>
        /// 按金额或按比例来优惠
        /// </summary>
        public string CashScale
        {
            set { _cashscale = value; }
            get { return _cashscale; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DeductNum
        {
            set { _deductnum = value; }
            get { return _deductnum; }
        }
        /// <summary>
        /// 星期有效设置
        /// </summary>
        public string WeekSet
        {
            set { _weekset = value; }
            get { return _weekset; }
        }
        /// <summary>
        /// 价格类型
        /// </summary>
        public string FeeType
        {
            set { _feetype = value; }
            get { return _feetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int hotel_id
        {
            set { _hotel_id = value; }
            get { return _hotel_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? h_room_rp_id
        {
            set { _h_room_rp_id = value; }
            get { return _h_room_rp_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RatePlanId
        {
            set { _rateplanid = value; }
            get { return _rateplanid; }
        }
        /// <summary>
        /// 房型编号(以逗号分隔）
        /// </summary>
        public string RoomIds
        {
            set { _roomids = value; }
            get { return _roomids; }
        }
        /// <summary>
        /// 房型名称(以逗号分隔）
        /// </summary>
        public string RoomNames
        {
            set { _roomnames = value; }
            get { return _roomnames; }
        }
        /// <summary>
        /// 促销名称
        /// </summary>
        public string DrrName
        {
            set { _drrname = value; }
            get { return _drrname; }
        }

        #endregion Model

        //[NotMapped]
        private List<DrrRules> drrList = new List<DrrRules>();
        [NotMapped]
        public List<DrrRules> DrrList
        {
            get { return drrList; }
            set { drrList = value; }
        }

        public List<DrrRules> getDrrsByHoltelId(int hotelId)
        {
            using (DBC.HotelDBContent db = new DBC.HotelDBContent())
            {
                return (from r in db.drrs where r.hotel_id == hotelId select r).ToList();
            }
        }
    }
  
}