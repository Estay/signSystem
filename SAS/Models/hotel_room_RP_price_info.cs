using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class hotel_room_RP_price_info
    {
        #region Model
        private int _rp_price_id;
        private int _h_room_rp_id;
        private int? _priceid;
        private int _room_id;
        private string _roomtypeid;
        private DateTime _room_rp_start_time;
        private DateTime _room_rp_end_time;
        private decimal _room_rp_price;
        private decimal? _weekend;
        private decimal? _membercost=-1;
        private decimal? _weekendcost=-1;
        private DateTime _room_rp_time;
        private bool _status = true;
        private string _currencycode;
        private decimal? _addbed = -1M;
        private int _hotel_id;
        /// <summary>
        /// RP Price编号
        /// </summary>
        /// 
         [KeyAttribute]
        public int rp_price_id
        {
            set { _rp_price_id = value; }
            get { return _rp_price_id; }
        }
        /// <summary>
        /// 关联 hotel_room_RP_info 的 h_room_rp_id 字段 
        /// </summary>
        public int h_room_rp_id
        {
            set { _h_room_rp_id = value; }
            get { return _h_room_rp_id; }
        }
        /// <summary>
        /// 艺龙价格编号
        /// </summary>
        public int? PriceID
        {
            set { _priceid = value; }
            get { return _priceid; }
        }
        /// <summary>
        /// 关联房型编号   （只单个） 我库
        /// </summary>
        public int room_id
        {
            set { _room_id = value; }
            get { return _room_id; }
        }
        /// <summary>
        /// 艺龙可售房型编号
        /// </summary>
        public string RoomTypeId
        {
            set { _roomtypeid = value; }
            get { return _roomtypeid; }
        }
        /// <summary>
        /// 价格开始时间
        /// </summary>
        public DateTime room_rp_start_time
        {
            set { _room_rp_start_time = value; }
            get { return _room_rp_start_time; }
        }
        /// <summary>
        /// 价格结束时间
        /// </summary>
        public DateTime room_rp_end_time
        {
            set { _room_rp_end_time = value; }
            get { return _room_rp_end_time; }
        }
        /// <summary>
        /// 价格 平日价格 Member
        /// </summary>
        public decimal room_rp_price
        {
            set { _room_rp_price = value; }
            get { return _room_rp_price; }
        }
        /// <summary>
        /// 周末卖价
        /// </summary>
        public decimal? Weekend
        {
            set { _weekend = value; }
            get { return _weekend; }
        }
        /// <summary>
        /// 平日结算价 
        /// </summary>
        public decimal? MemberCost
        {
            set { _membercost = value; }
            get { return _membercost; }
        }
        /// <summary>
        /// 周末结算价
        /// </summary>
        public decimal? WeekendCost
        {
            set { _weekendcost = value; }
            get { return _weekendcost; }
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime room_rp_time
        {
            set { _room_rp_time = value; }
            get { return _room_rp_time; }
        }
        /// <summary>
        /// 状态  1启用 0关闭
        /// </summary>
        public bool Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 货币类型
        /// </summary>
        public string CurrencyCode
        {
            set { _currencycode = value; }
            get { return _currencycode; }
        }
        /// <summary>
        /// -1代表不能加床，0-免费加床，大于0表示加床的费用
        /// </summary>
        public decimal? AddBed
        {
            set { _addbed = value; }
            get { return _addbed; }
        }
        /// <summary>
        /// 酒店ID 
        /// </summary>
        public int hotel_id
        {
            set { _hotel_id = value; }
            get { return _hotel_id; }
        }
        #endregion Model

        //[NotMapped]
        private List<hotel_room_RP_price_info> priceList = new List<hotel_room_RP_price_info>();
        [NotMapped]
        public List<hotel_room_RP_price_info> PriceList
        {
            get { return priceList; }
            set { priceList = value; }
        }
    }
  
}