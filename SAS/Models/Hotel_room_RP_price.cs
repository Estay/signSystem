using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class Hotel_room_RP_price
    {
        public Hotel_room_RP_price()
		{}
		#region Model
		private int _rp_price_id;
		private int _room_rp_id;
		private int _hotel_id;
		private int _room_id;
		private int? _roomtypeid;
		private int? _priceid;
		private bool _status= true;
		private decimal _room_rp_price;
		private decimal? _weekend;
		private decimal? _membercost;
		private decimal? _weekendcost;
		private decimal? _addbed=-1M;
		private DateTime _effectdate;
		private int? _ebeds=-1;
		private int? _aviebeds=-1;
		private decimal? _cost=-1M;
		private decimal? _commission=-1M;
		private DateTime _lastuptime;
		/// <summary>
		/// 
		/// </summary>
        [KeyAttribute]
        public int Rp_price_id
		{
			set{ _rp_price_id=value;}
			get{return _rp_price_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Room_rp_id
		{
			set{ _room_rp_id=value;}
			get{return _room_rp_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Hotel_id
		{
			set{ _hotel_id=value;}
			get{return _hotel_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Room_id
		{
			set{ _room_id=value;}
			get{return _room_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomTypeId
		{
			set{ _roomtypeid=value;}
			get{return _roomtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PriceID
		{
			set{ _priceid=value;}
			get{return _priceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Room_rp_price
		{
			set{ _room_rp_price=value;}
			get{return _room_rp_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Weekend
		{
			set{ _weekend=value;}
			get{return _weekend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MemberCost
		{
			set{ _membercost=value;}
			get{return _membercost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? WeekendCost
		{
			set{ _weekendcost=value;}
			get{return _weekendcost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Addbed
		{
			set{ _addbed=value;}
			get{return _addbed;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Effectdate
		{
			set{ _effectdate=value;}
			get{return _effectdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Ebeds
		{
			set{ _ebeds=value;}
			get{return _ebeds;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Aviebeds
		{
			set{ _aviebeds=value;}
			get{return _aviebeds;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Cost
		{
			set{ _cost=value;}
			get{return _cost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Commission
		{
			set{ _commission=value;}
			get{return _commission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime LastUpTime
		{
			set{ _lastuptime=value;}
			get{return _lastuptime;}
		}
		#endregion Model
        //[NotMapped]
        private List<Hotel_room_RP_price> priceList = new List<Hotel_room_RP_price>();
        [NotMapped]
        public List<Hotel_room_RP_price> PriceList
        {
            get { return priceList; }
            set { priceList = value; }
        }
    }
}