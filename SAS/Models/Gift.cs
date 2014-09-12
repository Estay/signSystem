using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SAS.DBC;

namespace SAS.Models
{
    public class Gift
    {
        public Gift()
        { }
        #region Model
        private int _giftid;
        private int? _hotelgiftid;
        private string _hotelcode;
        private string _datetype;
        private string _weekset;
        private string _giftcontent;
        private string _giftcontenten;
        private string _gifttypes;
        private string _hourtype;
        private int _hournumber;
        private string _wayofgiving;
        private string _wayofgivingother;
        private string _wayofgivingotheren;
        private int _hotel_id;
        private int? _h_room_rp_id;
        private string _rateplanid;
        private string _roomnames;
        private DateTime? _startdate=DateTime.Now;
        private DateTime? _enddate=DateTime.Now;
        private string _roomids;
        /// <summary>
        /// 
        /// </summary>
        [KeyAttribute]
        public int GiftId
        {
            set { _giftid = value; }
            get { return _giftid; }
        }
        /// <summary>
        /// 送礼活动编号(艺龙)
        /// </summary>
        public int? HotelGiftId
        {
            set { _hotelgiftid = value; }
            get { return _hotelgiftid; }
        }
        /// <summary>
        /// 酒店编码
        /// </summary>
        public string HotelCode
        {
            set { _hotelcode = value; }
            get { return _hotelcode; }
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
        /// 星期设置
        /// </summary>
        public string WeekSet
        {
            set { _weekset = value; }
            get { return _weekset; }
        }
        /// <summary>
        /// 礼包内容
        /// </summary>
        public string GiftContent
        {
            set { _giftcontent = value; }
            get { return _giftcontent; }
        }
        /// <summary>
        /// 礼包内容英文
        /// </summary>
        public string GiftContentEn
        {
            set { _giftcontenten = value; }
            get { return _giftcontenten; }
        }
        /// <summary>
        /// 礼包类型 多个类型将以逗号分隔。
        /// </summary>
        public string GiftTypes
        {
            set { _gifttypes = value; }
            get { return _gifttypes; }
        }
        /// <summary>
        /// 开始时间点类型
        /// </summary>
        public string HourType
        {
            set { _hourtype = value; }
            get { return _hourtype; }
        }
        /// <summary>
        /// 小时数
        /// </summary>
        public int HourNumber
        {
            set { _hournumber = value; }
            get { return _hournumber; }
        }
        /// <summary>
        /// 送礼方式
        /// </summary>
        public string WayOfGiving
        {
            set { _wayofgiving = value; }
            get { return _wayofgiving; }
        }
        /// <summary>
        /// 其他送礼方式的描述
        /// </summary>
        public string WayOfGivingOther
        {
            set { _wayofgivingother = value; }
            get { return _wayofgivingother; }
        }
        /// <summary>
        /// 其他送礼方式的英文描述
        /// </summary>
        public string WayOfGivingOtherEn
        {
            set { _wayofgivingotheren = value; }
            get { return _wayofgivingotheren; }
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
        /// 
        /// </summary>
        public string roomNames
        {
            set { _roomnames = value; }
            get { return _roomnames; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
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
        public string RoomIds
        {
            set { _roomids = value; }
            get { return _roomids; }
        }
        #endregion Model

        public List<Gift> GiftList(int hotelId )
        {
            HotelDBContent db = new HotelDBContent();
            string uId = "test1";
           
            //用户ID所有的酒店
           
            return (from h in db.gifts where h.hotel_id==hotelId select h).ToList();

        }
    }

}