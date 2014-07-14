using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace SAS.Models
{
    public class hotel_info
    {
        //[key]


        
       // public int id { get; set; }
        
        #region Model
        private int _hotel_id;
        private string _h_id;
        private string _u_id;
        private int? _h_t_id;
        private string _h_name_cn;
        private string _h_name_en;
        private string _h_location_cn;
        private string _h_location_en;
        private string _h_description_cn;
        private string _h_description_en;
        private int? _h_star = 0;
        private string _h_organization;
        private string _h_tel;
        private string _h_mobile_phone;
        private string _h_fax;
        private string _h_email;
        private string _h_contact;
        private string _h_price_range;
        private string _h_traffic_position;
        private string _h_longitude;
        private string _h_latitude;
        private string _h_check_in;
        private string _h_check_out;
        private DateTime? _h_opening_time;
        private int? _h_room_count;
        private string _h_province;
        private string _h_city;
        private string _h_administrative_region;
        private string _h_business_zone;
        private string _district;
        private string _h_postcode;
        private bool _h_outer_network_display = true;
        private bool _h_state = true;
        private string _h_state_reason;
        private string _h_hotel_website;
        private DateTime? _h_ctime;
        private DateTime? _h_utime;
        private int? _h_sort = 10000;
        private int? _source_id;
        private string _generalamenities;
        private string _roomamenities;
        private string _diningamenities;
        private string _recreationamenities;
        private string _availpolicy;
        private int? _commenttotal = 0;
        private int? _commentgood = 0;
        private int? _commentbad = 0;
        private string _creditcards;
        private string _score;
        private string _introeditor;
        private string _facilities;
        private string _baidulat;
        private string _baidulon;
        private int? _brandid;
        private string _surroundings;
        private string _features;
        private string _helpfultips;
        private string _hotel_theme_id;
        private DateTime decorateTime;
        private int floor;

        public int Floor
        {
            get { return floor; }
            set { floor = value; }
        }

        public DateTime DecorateTime
        {
            get { return decorateTime; }
            set { decorateTime = value; }
        }
        /// <summary>
        /// 
        /// </summary>
       [KeyAttribute]
        public int hotel_id
        {
            set { _hotel_id = value; }
            get { return _hotel_id; }
        }
        /// <summary>
        /// 酒店编号 有 自营和非自营的
        /// </summary>
        public string h_id
        {
            set { _h_id = value; }
            get { return _h_id; }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string u_id
        {
            set { _u_id = value; }
            get { return _u_id; }
        }
        /// <summary>
        /// 主题类型分类 对应 hotel_theme_type_info 的 t_id
        /// </summary>
        public int? h_t_id
        {
            set { _h_t_id = value; }
            get { return _h_t_id; }
        }
        /// <summary>
        /// 酒店名称（中文）
        /// </summary>
        public string h_name_cn
        {
            set { _h_name_cn = value; }
            get { return _h_name_cn; }
        }
        /// <summary>
        /// 酒店名称（英文）
        /// </summary>
        public string h_name_en
        {
            set { _h_name_en = value; }
            get { return _h_name_en; }
        }
        /// <summary>
        /// 具体位置（中文）
        /// </summary>
        public string h_location_cn
        {
            set { _h_location_cn = value; }
            get { return _h_location_cn; }
        }
        /// <summary>
        /// 具体位置（英文）
        /// </summary>
        public string h_location_en
        {
            set { _h_location_en = value; }
            get { return _h_location_en; }
        }
        /// <summary>
        /// 酒店描述（中文）
        /// </summary>
        public string h_description_cn
        {
            set { _h_description_cn = value; }
            get { return _h_description_cn; }
        }
        /// <summary>
        /// 酒店描述（英文）
        /// </summary>
        public string h_description_en
        {
            set { _h_description_en = value; }
            get { return _h_description_en; }
        }
        /// <summary>
        /// 酒店星级 旅游局指认 0 1 2 3 4 5 (0为普通，后面相对)
        /// </summary>
        public int? h_star
        {
            set { _h_star = value; }
            get { return _h_star; }
        }
        /// <summary>
        /// 所属机构,公司,集团
        /// </summary>
        public string h_organization
        {
            set { _h_organization = value; }
            get { return _h_organization; }
        }
        /// <summary>
        /// 前台电话
        /// </summary>
        public string h_tel
        {
            set { _h_tel = value; }
            get { return _h_tel; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string h_mobile_phone
        {
            set { _h_mobile_phone = value; }
            get { return _h_mobile_phone; }
        }
        /// <summary>
        /// 前台传真
        /// </summary>
        public string h_fax
        {
            set { _h_fax = value; }
            get { return _h_fax; }
        }
        /// <summary>
        /// 前台Email
        /// </summary>
        public string h_email
        {
            set { _h_email = value; }
            get { return _h_email; }
        }
        /// <summary>
        /// 联系人 （酒店负责人）
        /// </summary>
        public string h_contact
        {
            set { _h_contact = value; }
            get { return _h_contact; }
        }
        /// <summary>
        /// 价格范围 （暂不用）
        /// </summary>
        public string h_price_range
        {
            set { _h_price_range = value; }
            get { return _h_price_range; }
        }
        /// <summary>
        /// 交通位置（推荐三条以上 各条以；分开）如 距标志性建筑物几分钟 多少路程
        /// </summary>
        public string h_traffic_position
        {
            set { _h_traffic_position = value; }
            get { return _h_traffic_position; }
        }
        /// <summary>
        /// 经度 google
        /// </summary>
        public string h_longitude
        {
            set { _h_longitude = value; }
            get { return _h_longitude; }
        }
        /// <summary>
        /// 纬度 google
        /// </summary>
        public string h_latitude
        {
            set { _h_latitude = value; }
            get { return _h_latitude; }
        }
        /// <summary>
        /// 入店办理 (暂不用 )
        /// </summary>
        public string h_check_in
        {
            set { _h_check_in = value; }
            get { return _h_check_in; }
        }
        /// <summary>
        /// 离店办理(暂不用 )
        /// </summary>
        public string h_check_out
        {
            set { _h_check_out = value; }
            get { return _h_check_out; }
        }
        /// <summary>
        /// 开业时间 （挂牌营业）
        /// </summary>
        public DateTime? h_opening_time
        {
            set { _h_opening_time = value; }
            get { return _h_opening_time; }
        }
        /// <summary>
        /// 可销售房数量（非库存）
        /// </summary>
        public int? h_room_count
        {
            set { _h_room_count = value; }
            get { return _h_room_count; }
        }
        /// <summary>
        /// 所在省份（用户酒店搜索）
        /// </summary>
        public string h_province
        {
            set { _h_province = value; }
            get { return _h_province; }
        }
        /// <summary>
        /// 所在城市（用户酒店搜索）
        /// </summary>
        public string h_city
        {
            set { _h_city = value; }
            get { return _h_city; }
        }
        /// <summary>
        /// 所在行政区（用户酒店搜索）（暂不用）
        /// </summary>
        public string h_administrative_region
        {
            set { _h_administrative_region = value; }
            get { return _h_administrative_region; }
        }
        /// <summary>
        /// 所在商业区
        /// </summary>
        public string h_business_zone
        {
            set { _h_business_zone = value; }
            get { return _h_business_zone; }
        }
        /// <summary>
        /// 行政区 （暂用）
        /// </summary>
        public string district
        {
            set { _district = value; }
            get { return _district; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string h_postcode
        {
            set { _h_postcode = value; }
            get { return _h_postcode; }
        }
        /// <summary>
        /// 外网显示（0不显示 1显示）默认1 (暂不用 )
        /// </summary>
        public bool h_outer_network_display
        {
            set { _h_outer_network_display = value; }
            get { return _h_outer_network_display; }
        }
        /// <summary>
        /// 酒店状态（0 不启用 1启用）默认启用 搜索条件  为0则不检索
        /// </summary>
        public bool h_state
        {
            set { _h_state = value; }
            get { return _h_state; }
        }
        /// <summary>
        /// 状态原因（如：开始营业  挂牌中 工商审核中 装修中 等等） 
        /// </summary>
        public string h_state_reason
        {
            set { _h_state_reason = value; }
            get { return _h_state_reason; }
        }
        /// <summary>
        /// 酒店网址 （http://）
        /// </summary>
        public string h_hotel_website
        {
            set { _h_hotel_website = value; }
            get { return _h_hotel_website; }
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? h_ctime
        {
            set { _h_ctime = value; }
            get { return _h_ctime; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? h_utime
        {
            set { _h_utime = value; }
            get { return _h_utime; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int? h_sort
        {
            set { _h_sort = value; }
            get { return _h_sort; }
        }
        /// <summary>
        /// 关联来源类型编号   如 1 自营，4 艺龙 
        /// </summary>
        public int? source_id
        {
            set { _source_id = value; }
            get { return _source_id; }
        }
        /// <summary>
        /// 服务设施
        /// </summary>
        public string GeneralAmenities
        {
            set { _generalamenities = value; }
            get { return _generalamenities; }
        }
        /// <summary>
        /// 房间设施
        /// </summary>
        public string RoomAmenities
        {
            set { _roomamenities = value; }
            get { return _roomamenities; }
        }
        /// <summary>
        /// 餐饮设施
        /// </summary>
        public string diningAmenities
        {
            set { _diningamenities = value; }
            get { return _diningamenities; }
        }
        /// <summary>
        /// 休闲设施
        /// </summary>
        public string recreationAmenities
        {
            set { _recreationamenities = value; }
            get { return _recreationamenities; }
        }
        /// <summary>
        /// 特殊政策  请把此信息展示给用户，以便用户预订
        /// </summary>
        public string availPolicy
        {
            set { _availpolicy = value; }
            get { return _availpolicy; }
        }
        /// <summary>
        /// 评论总数
        /// </summary>
        public int? commentTotal
        {
            set { _commenttotal = value; }
            get { return _commenttotal; }
        }
        /// <summary>
        /// 好评数
        /// </summary>
        public int? commentGood
        {
            set { _commentgood = value; }
            get { return _commentgood; }
        }
        /// <summary>
        /// 差评数
        /// </summary>
        public int? commentBad
        {
            set { _commentbad = value; }
            get { return _commentbad; }
        }
        /// <summary>
        /// 酒店支持的信用卡  关联bank_card_info 多个用，号隔开
        /// </summary>
        public string CreditCards
        {
            set { _creditcards = value; }
            get { return _creditcards; }
        }
        /// <summary>
        /// 评率 %
        /// </summary>
        public string score
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 介绍信息
        /// </summary>
        public string IntroEditor
        {
            set { _introeditor = value; }
            get { return _introeditor; }
        }
        /// <summary>
        /// 用于检索的设施 对应Facilities_info  id编号  多个用"，"隔开
        /// </summary>
        public string Facilities
        {
            set { _facilities = value; }
            get { return _facilities; }
        }
        /// <summary>
        /// 百度纬度
        /// </summary>
        public string BaiduLat
        {
            set { _baidulat = value; }
            get { return _baidulat; }
        }
        /// <summary>
        /// 百度经度
        /// </summary>
        public string BaiduLon
        {
            set { _baidulon = value; }
            get { return _baidulon; }
        }
        /// <summary>
        /// 品牌编号 
        /// </summary>
        public int? BrandId
        {
            set { _brandid = value; }
            get { return _brandid; }
        }
        /// <summary>
        /// 周边信息
        /// </summary>
        public string Surroundings
        {
            set { _surroundings = value; }
            get { return _surroundings; }
        }
        /// <summary>
        /// 特色信息 用户优化的设施-检索 对应Facilities_info 表 [id] 多个字段用逗号隔开
        /// </summary>
        public string Features
        {
            set { _features = value; }
            get { return _features; }
        }
        /// <summary>
        /// 温馨提示 
        /// </summary>
        public string HelpfulTips
        {
            set { _helpfultips = value; }
            get { return _helpfultips; }
        }
        /// <summary>
        /// 酒店精确主题搜索 多个用逗号隔开 关联 hotel_theme_info id
        /// </summary>
        public string hotel_theme_id
        {
            set { _hotel_theme_id = value; }
            get { return _hotel_theme_id; }
        }
        #endregion Model

        public hotel_theme_info theme;
        //装修时间
        public List<string> getDecorationTime()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 30; i++)
            {
                list.Add(DateTime.Now.Date.AddYears(-i).Year+"年");
            }
            return list;
        }
        
    }
    public class hotel_infoDBContent : DbContext
    {
        public DbSet<hotel_info> hotel { get; set; }
       
    }
}