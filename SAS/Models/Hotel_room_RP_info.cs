using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class Hotel_room_RP_info
    {
        public Hotel_room_RP_info()
		{}
		#region Model
		private int _h_room_rp_id;
		private string _rateplanid;
		private int _hotel_id;
		private string _hotelcode;
		private bool _h_room_rp_state= true;
		private string _room_id_str;
		private string _h_room_rp_name_cn;
		private string _h_room_rp_name_en;
		private bool _h_room_rp_is_pay_online= true;
		private bool _h_room_rp_is_to_store_pay= true;
		private string _h_room_rp_check_in;
		private string _h_room_rp_check_out;
		private int _h_room_rp_least_day=1;
		private int _h_room_rp_longest_day=365;
		private bool _h_room_rp_invoice= true;
		private bool _h_room_rp_isbreakfast= false;
		private int? _h_room_rp_breakfast_count=0;
		private string _h_room_rp_breakfast_title;
		private bool _h_room_rp_is_plus_breakfast= false;
		private bool _h_room_rp_is_plus_bed= false;
		private DateTime _h_room_rp_ctime;
		private DateTime? _h_room_rp_utime;
		private bool _h_room_rp_isguarantee= false;
		private string _suffixname;
		private string _customertype;
		private int? _currentalloment;
		private bool _instantconfirmation;
		private string _paymenttype;
		private string _bookingruleids;
		private string _guaranteeruleids;
		private string _prepayruleids;
		private string _drrruleids;
		private string _valueaddids;
		private string _producttypes;
		private bool _islastminutesale;
		private string _starttime;
		private string _endtime;
		private int? _minamount=1;
		private int? _minadvhours;
		private int? _maxadvhours;
		private string _currencycode;
		/// <summary>
		/// 房型RP编号
		/// </summary>
        [KeyAttribute]
        public int h_room_rp_id
		{
			set{ _h_room_rp_id=value;}
			get{return _h_room_rp_id;}
		}
		/// <summary>
		/// 艺龙RP编号 我库中的需要生成流水号
		/// </summary>
		public string RatePlanId
		{
			set{ _rateplanid=value;}
			get{return _rateplanid;}
		}
		/// <summary>
		/// 所属酒店 我库
		/// </summary>
		public int hotel_id
		{
			set{ _hotel_id=value;}
			get{return _hotel_id;}
		}
		/// <summary>
		/// 供应商酒店编码  在获取房态的时候需要进行关联
		/// </summary>
		public string HotelCode
		{
			set{ _hotelcode=value;}
			get{return _hotelcode;}
		}
		/// <summary>
		/// 销售状态 是否启用   默认启用 1 
		/// </summary>
		public bool h_room_rp_state
		{
			set{ _h_room_rp_state=value;}
			get{return _h_room_rp_state;}
		}
		/// <summary>
		/// 关联房型编号， 多者之间用英文逗号隔开  (已弃用 后删除)
		/// </summary>
		public string room_id_Str
		{
			set{ _room_id_str=value;}
			get{return _room_id_str;}
		}
		/// <summary>
		/// RP名称中文
		/// </summary>
		public string h_room_rp_name_cn
		{
			set{ _h_room_rp_name_cn=value;}
			get{return _h_room_rp_name_cn;}
		}
		/// <summary>
		/// RP名称英文 暂不用
		/// </summary>
		public string h_room_rp_name_en
		{
			set{ _h_room_rp_name_en=value;}
			get{return _h_room_rp_name_en;}
		}
		/// <summary>
		/// 是否网络支付 默认是
		/// </summary>
		public bool h_room_rp_is_pay_online
		{
			set{ _h_room_rp_is_pay_online=value;}
			get{return _h_room_rp_is_pay_online;}
		}
		/// <summary>
		/// 是否到店支付默认是
		/// </summary>
		public bool h_room_rp_is_to_store_pay
		{
			set{ _h_room_rp_is_to_store_pay=value;}
			get{return _h_room_rp_is_to_store_pay;}
		}
		/// <summary>
		/// 入店时间
		/// </summary>
		public string h_room_rp_check_in
		{
			set{ _h_room_rp_check_in=value;}
			get{return _h_room_rp_check_in;}
		}
		/// <summary>
		/// 离店时间
		/// </summary>
		public string h_room_rp_check_out
		{
			set{ _h_room_rp_check_out=value;}
			get{return _h_room_rp_check_out;}
		}
		/// <summary>
		/// 最短天数 默认1
		/// </summary>
		public int h_room_rp_least_day
		{
			set{ _h_room_rp_least_day=value;}
			get{return _h_room_rp_least_day;}
		}
		/// <summary>
		/// 最长天数
		/// </summary>
		public int h_room_rp_longest_day
		{
			set{ _h_room_rp_longest_day=value;}
			get{return _h_room_rp_longest_day;}
		}
		/// <summary>
		/// 是否提供发票
		/// </summary>
		public bool h_room_rp_invoice
		{
			set{ _h_room_rp_invoice=value;}
			get{return _h_room_rp_invoice;}
		}
		/// <summary>
		/// 是否含免早
		/// </summary>
		public bool h_room_rp_isbreakfast
		{
			set{ _h_room_rp_isbreakfast=value;}
			get{return _h_room_rp_isbreakfast;}
		}
		/// <summary>
		/// 含早数量 暂不用
		/// </summary>
		public int? h_room_rp_breakfast_count
		{
			set{ _h_room_rp_breakfast_count=value;}
			get{return _h_room_rp_breakfast_count;}
		}
		/// <summary>
		/// 含早名称 （暂不用）
		/// </summary>
		public string h_room_rp_breakfast_title
		{
			set{ _h_room_rp_breakfast_title=value;}
			get{return _h_room_rp_breakfast_title;}
		}
		/// <summary>
		/// 是否加餐  已弃用
		/// </summary>
		public bool h_room_rp_is_plus_breakfast
		{
			set{ _h_room_rp_is_plus_breakfast=value;}
			get{return _h_room_rp_is_plus_breakfast;}
		}
		/// <summary>
		/// 是否加床  已弃用
		/// </summary>
		public bool h_room_rp_is_plus_bed
		{
			set{ _h_room_rp_is_plus_bed=value;}
			get{return _h_room_rp_is_plus_bed;}
		}
		/// <summary>
		/// 录入时间
		/// </summary>
		public DateTime h_room_rp_ctime
		{
			set{ _h_room_rp_ctime=value;}
			get{return _h_room_rp_ctime;}
		}
		/// <summary>
		/// 编辑时间
		/// </summary>
		public DateTime? h_room_rp_utime
		{
			set{ _h_room_rp_utime=value;}
			get{return _h_room_rp_utime;}
		}
		/// <summary>
		/// 暂不用
		/// </summary>
		public bool h_room_rp_isGuarantee
		{
			set{ _h_room_rp_isguarantee=value;}
			get{return _h_room_rp_isguarantee;}
		}
		/// <summary>
		/// 供应商房型附加名称
//房型信息的补充说明
		/// </summary>
		public string SuffixName
		{
			set{ _suffixname=value;}
			get{return _suffixname;}
		}
		/// <summary>
		/// 客人类型
//All=统一价；
//Chinese =内宾价；
//OtherForeign =外宾价；
//HongKong =港澳台客人价；
//Japanese=日本客人价
		/// </summary>
		public string CustomerType
		{
			set{ _customertype=value;}
			get{return _customertype;}
		}
		/// <summary>
		/// 放量限额
 //入住时间内不能超售的最小值。当大于0小于5时，表示目前仅剩的房量;0表示房量充足
		/// </summary>
		public int? CurrentAlloment
		{
			set{ _currentalloment=value;}
			get{return _currentalloment;}
		}
		/// <summary>
		/// 是否支持即时确认
 //表示这个产品是否支持即时确认。最终的订单是否是即时确认还需调用即时确认接口来验证
		/// </summary>
		public bool InstantConfirmation
		{
			set{ _instantconfirmation=value;}
			get{return _instantconfirmation;}
		}
		/// <summary>
		/// 付款类型  
//SelfPay-前台现付；
//Prepay-预付
		/// </summary>
		public string PaymentType
		{
			set{ _paymenttype=value;}
			get{return _paymenttype;}
		}
		/// <summary>
		/// 对应的预订规则编号
		/// </summary>
		public string BookingRuleIds
		{
			set{ _bookingruleids=value;}
			get{return _bookingruleids;}
		}
		/// <summary>
		/// 对应的担保规则编号 |艺龙标识 1 为担保 0无需担保
		/// </summary>
		public string GuaranteeRuleIds
		{
			set{ _guaranteeruleids=value;}
			get{return _guaranteeruleids;}
		}
		/// <summary>
		/// 对应的预付规则编号
		/// </summary>
		public string PrepayRuleIds
		{
			set{ _prepayruleids=value;}
			get{return _prepayruleids;}
		}
		/// <summary>
		/// 对应的促销规则编号
		/// </summary>
		public string DrrRuleIds
		{
			set{ _drrruleids=value;}
			get{return _drrruleids;}
		}
		/// <summary>
		/// 对应的增值服务编号
		/// </summary>
		public string ValueAddIds
		{
			set{ _valueaddids=value;}
			get{return _valueaddids;}
		}
		/// <summary>
		/// 产品特性类型 
///3-限时抢购；
//4-钟点房；
		/// </summary>
		public string ProductTypes
		{
			set{ _producttypes=value;}
			get{return _producttypes;}
		}
		/// <summary>
		/// 是否今日特价(尾房) 
//如果为true，则要校验当前时间是否在StartTime和EndTime的范围内，从而决定这个RP是否显示在可销售产品中。
		/// </summary>
		public bool IsLastMinuteSale
		{
			set{ _islastminutesale=value;}
			get{return _islastminutesale;}
		}
		/// <summary>
		/// 每天可以销售的开始时间 默认值：00:00
		/// </summary>
		public string StartTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// 每天可以销售的带结束时间
///默认值:23:59

///如果结束时间是00:00至6:00，则表示是次日
		/// </summary>
		public string EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 预定最少数量
 ///默认值：1
		/// </summary>
		public int? MinAmount
		{
			set{ _minamount=value;}
			get{return _minamount;}
		}
		/// <summary>
		/// 最少提前预定小时数 
        ///按checkInDate的23:59:59(一般认为24点)来计算
		/// </summary>
		public int? MinAdvHours
		{
			set{ _minadvhours=value;}
			get{return _minadvhours;}
		}
		/// <summary>
		/// 最多提前预定小时数 
///按checkInDate的23:59:59(一般认为24点)来计算
		/// </summary>
		public int? MaxAdvHours
		{
			set{ _maxadvhours=value;}
			get{return _maxadvhours;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CurrencyCode
		{
			set{ _currencycode=value;}
			get{return _currencycode;}
		}
		#endregion Model

        //public int BuildRatePlan()
        //{
 
        //}
    }
}