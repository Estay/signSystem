using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SAS.DBC;

namespace SAS.Models
{
    public class GuaranteeRule
    {
        #region Model
		private int _id;
		private int _guaranteerulesid;
		private string _description;
		private string _datetype;
		private DateTime? _startdate;
		private DateTime? _enddate;
		private string _weekset;
		private bool _istimeguarantee;
		private string _starttime;
		private string _endtime;
		private int? _istomorrow;
		private bool _isamountguarantee;
		private int? _amount;
		private string _guaranteetype;
		private int? _changerule;
		private DateTime? _day;
		private string _time;
		private int? _hour;
		private int _hotel_id;
		private int? _h_room_rp_id;
		private string _rateplanid;
		/// <summary>
		/// 担保规则编号
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 艺龙担保规则编号
		/// </summary>
		public int GuaranteeRulesId
		{
			set{ _guaranteerulesid=value;}
			get{return _guaranteerulesid;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 日期类型 


		/// </summary>
		public string DateType
		{
			set{ _datetype=value;}
			get{return _datetype;}
		}
		/// <summary>
		/// 开始日期 举例：DateType为CheckInDay：表示当前订单的入住日期落在StartDate和EndDate之间，并且入住日期符合周设置时才需要判断其它条件是否担保，否则不需要担保 DateType为StayDay：表示当前订单的客人只要有住在店里面的日期（[ArrivalDate,DepartureDate））落在StartDate和EndDate之间，并且入住日期符合周设置时才需要判断其它条件是否担保，否则不需要担保

		/// </summary>
		public DateTime? StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 结束时间 举例：DateType为CheckInDay：表示当前订单的入住日期落在StartDate和EndDate之间，并且入住日期符合周设置时才需要判断其它条件是否担保，否则不需要担保 DateType为StayDay：表示当前订单的客人只要有住在店里面的日期（[ArrivalDate,DepartureDate））落在StartDate和EndDate之间，并且入住日期符合周设置时才需要判断其它条件是否担保，否则不需要担保

		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 周有效天数， 一般为周一到周日都有效， 判断日期符合日期段同时也要满足周设置的有效 举例：DateType为CheckInDay：表示当前订单的入住日期落在StartDate和EndDate之间，并且入住日期符合周设置时才需要判断其它条件是否担保，否则不需要担保 DateType为StayDay：表示当前订单的客人只要有住在店里面的日期（[ArrivalDate,DepartureDate））落在StartDate和EndDate之间，并且入住日期符合周设置时才需要判断其它条件是否担保，否则不需要担保

		/// </summary>
		public string WeekSet
		{
			set{ _weekset=value;}
			get{return _weekset;}
		}
		/// <summary>
		/// 是否到店时间担保 False:为不校验到店时间 True:为需要校验到店时间

		/// </summary>
		public bool IsTimeGuarantee
		{
			set{ _istimeguarantee=value;}
			get{return _istimeguarantee;}
		}
		/// <summary>
		/// 到店担保开始时间 用于IsTimeGuarantee ==true进行检查
		/// </summary>
		public string StartTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// 到店担保结束时间 用于IsTimeGuarantee ==true进行检查
		/// </summary>
		public string EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 到店担保的结束时间是否为第二天 ; 0为当天，1为次日 用于IsTimeGuarantee ==true进行检查
		/// </summary>
		public int? IsTomorrow
		{
			set{ _istomorrow=value;}
			get{return _istomorrow;}
		}
		/// <summary>
		/// 是否房量担保 False:为不校验房量条件 True:为校验房量条件

		/// </summary>
		public bool IsAmountGuarantee
		{
			set{ _isamountguarantee=value;}
			get{return _isamountguarantee;}
		}
		/// <summary>
		/// 担保的房间数,预订几间房以上要担保 用于IsAmountGuarantee ==true进行检查
		/// </summary>
		public int? Amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 担保类型 FirstNightCost为首晚房费担保 FullNightCost为全额房费担保
		/// </summary>
		public string GuaranteeType
		{
			set{ _guaranteetype=value;}
			get{return _guaranteetype;}
		}
		/// <summary>
		/// 变更规则 担保规则条数，规则 NoChange、不允许变更取消 NeedSomeDay、允许变更/取消,需在XX日YY时之前通知
//NeedCheckinTime 、允许变更/取消,需在最早到店时间之前几小时通知 NeedCheckin24hour、允许变更/取消,需在到店日期的24点之前几小时通知

		/// </summary>
		public int? ChangeRule
		{
			set{ _changerule=value;}
			get{return _changerule;}
		}
		/// <summary>
		/// 日期参数
//ChangeRule= NeedSomeDay时，对应规则2描述中 “允许变更/取消,需在XX日YY时之前通知” 中的XX日，YY时
		/// </summary>
		public DateTime? Day
		{
			set{ _day=value;}
			get{return _day;}
		}
		/// <summary>
		/// 时间参数 
//		/// </summary>
		public string Time
		{
			set{ _time=value;}
			get{return _time;}
		}
		/// <summary>
		/// 小时参数
//ChangeRule= NeedSomeDay时，对应规则2描述中 “允许变更/取消,需在XX日YY时之前通知” 中的XX日，YY时
		/// </summary>
		public int? Hour
		{
			set{ _hour=value;}
			get{return _hour;}
		}
		/// <summary>
		/// 酒店编号
		/// </summary>
		public int hotel_id
		{
			set{ _hotel_id=value;}
			get{return _hotel_id;}
		}
		/// <summary>
		/// RP编号（我库）
		/// </summary>
		public int? h_room_rp_id
		{
			set{ _h_room_rp_id=value;}
			get{return _h_room_rp_id;}
		}
		/// <summary>
		/// RP编号（艺龙）
		/// </summary>
		public string RatePlanId
		{
			set{ _rateplanid=value;}
			get{return _rateplanid;}
		}
		#endregion Model

        public List<GuaranteeRule> GuraranteeList(int hotelId)
        {
            //用户ID所有的酒店
              HotelDBContent db = new HotelDBContent();

              return (from h in db.gu where h.hotel_id == hotelId select h).ToList();

        }
    }

}