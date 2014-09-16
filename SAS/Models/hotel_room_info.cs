using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAS.Models
{
    
    public class hotel_room_info
    {
        
      
        public hotel_room_info()
        { }
        #region Model
        private int _room_id;
        private string _h_r_id;
        private int _hotel_id = 0;
        private string _h_r_name_cn;
        private string _h_r_name_en;
        private string _h_r_description_cn;
        private string _h_r_description_en;
        private string _h_r_acreage;
        private int? _h_r_people_number;
        private int? _sitting_room_number = 0;
        private int? _h_r_bedroom_number = 1;
        private int? _kitchennumber = 0;
        private int? _h_r_bathroom_number;
        private string _h_r_house_number = "1";
        private int _h_r_reserve = 1;
        private string _h_r_bed_number = "1";
        private string _h_r_bed_type;
        private string _h_r_floor;
        private int? _h_r_least_day = 1;
        private int? _h_r_longest_day;
        private DateTime _h_r_ctime;
        private DateTime? _h_r_utime;
        private bool _h_r_state = true;
        private int? _h_r_sort = 10000;
        private int? _broadnetaccess;
        private int? _broadnetfee;
        private string _comments;
        private int? _capacity = 0;
        private string _house_service;
        private int? _study;
        private int? _kitchen;
        private int _sitting_room;
        private decimal defaultPrice;

        /// <summary>
        /// 默认价格用于我的公寓增加房型
        /// </summary>
        public decimal DefaultPrice
        {
            get { return defaultPrice; }
            set { defaultPrice = value; }
        }

        public int Sitting_room
        {
            get { return _sitting_room; }
            set { _sitting_room = value; }
        }
        private int _balcony;

        public int Balcony
        {
            get { return _balcony; }
            set { _balcony = value; }
        }
        /// <summary>
        /// 我库房型编号
        /// </summary>
        [KeyAttribute]
        public int room_id
        {
            set { _room_id = value; }
            get { return _room_id; }
        }
        /// <summary>
        /// 房屋编号（艺龙 ）
        /// </summary>
        public string h_r_id
        {
            set { _h_r_id = value; }
            get { return _h_r_id; }
        }
        /// <summary>
        /// 所属酒店编号（我库）
        /// </summary>
        public int hotel_id
        {
            set { _hotel_id = value; }
            get { return _hotel_id; }
        }
        /// <summary>
        /// 房屋名称（中文）
        /// </summary>
        public string h_r_name_cn
        {
            set { _h_r_name_cn = value; }
            get { return _h_r_name_cn; }
        }
        /// <summary>
        /// 房屋名称（英文）
        /// </summary>
        public string h_r_name_en
        {
            set { _h_r_name_en = value; }
            get { return _h_r_name_en; }
        }
        /// <summary>
        /// 房屋描述（中文） 包括大床、双床、宽带等一类描述
        /// </summary>
        public string h_r_description_cn
        {
            set { _h_r_description_cn = value; }
            get { return _h_r_description_cn; }
        }
        /// <summary>
        /// 房屋描述（英文）
        /// </summary>
        public string h_r_description_en
        {
            set { _h_r_description_en = value; }
            get { return _h_r_description_en; }
        }
        /// <summary>
        /// 房屋面积
        /// </summary>
        public string h_r_acreage
        {
            set { _h_r_acreage = value; }
            get { return _h_r_acreage; }
        }
        /// <summary>
        /// 最大可住人数 (暂不用)
        /// </summary>
        public int? h_r_people_number
        {
            set { _h_r_people_number = value; }
            get { return _h_r_people_number; }
        }
        /// <summary>
        /// 客厅数  默认0
        /// </summary>
        public int? sitting_room_number
        {
            set { _sitting_room_number = value; }
            get { return _sitting_room_number; }
        }
        /// <summary>
        /// 卧室数 (暂不用) 几室
        /// </summary>
        public int? h_r_bedroom_number
        {
            set { _h_r_bedroom_number = value; }
            get { return _h_r_bedroom_number; }
        }
        /// <summary>
        /// 厨房数  默认1
        /// </summary>
        public int? KitchenNumber
        {
            set { _kitchennumber = value; }
            get { return _kitchennumber; }
        }
        /// <summary>
        /// 卫生间数(暂不用) 默认 1
        /// </summary>
        public int? h_r_bathroom_number
        {
            set { _h_r_bathroom_number = value; }
            get { return _h_r_bathroom_number; }
        }
        /// <summary>
        /// 房屋套数 (库存来源数值)
        /// </summary>
        public string h_r_house_number
        {
            set { _h_r_house_number = value; }
            get { return _h_r_house_number; }
        }
        /// <summary>
        /// 库存 （实时房态）
        /// </summary>
        public int h_r_reserve
        {
            set { _h_r_reserve = value; }
            get { return _h_r_reserve; }
        }
        /// <summary>
        /// 床数
        /// </summary>
        public string h_r_bed_number
        {
            set { _h_r_bed_number = value; }
            get { return _h_r_bed_number; }
        }
        /// <summary>
        /// 床型 （单，双/大）
        /// </summary>
        public string h_r_bed_type
        {
            set { _h_r_bed_type = value; }
            get { return _h_r_bed_type; }
        }
        /// <summary>
        /// 楼层
        /// </summary>
        public string h_r_floor
        {
            set { _h_r_floor = value; }
            get { return _h_r_floor; }
        }
        /// <summary>
        /// 最少天数 （默认1天）(暂不用)
        /// </summary>
        public int? h_r_least_day
        {
            set { _h_r_least_day = value; }
            get { return _h_r_least_day; }
        }
        /// <summary>
        /// 最长天数 (暂不用)
        /// </summary>
        public int? h_r_longest_day
        {
            set { _h_r_longest_day = value; }
            get { return _h_r_longest_day; }
        }
        /// <summary>
        /// 房间录入时间
        /// </summary>
        public DateTime h_r_ctime
        {
            set { _h_r_ctime = value; }
            get { return _h_r_ctime; }
        }
        /// <summary>
        /// 房间编辑时间
        /// </summary>
        public DateTime? h_r_utime
        {
            set { _h_r_utime = value; }
            get { return _h_r_utime; }
        }
        /// <summary>
        /// 是否启用(0未启用 1启用)默认1 
        /// </summary>
        public bool h_r_state
        {
            set { _h_r_state = value; }
            get { return _h_r_state; }
        }
        /// <summary>
        /// 排序 
        /// </summary>
        public int? h_r_sort
        {
            set { _h_r_sort = value; }
            get { return _h_r_sort; }
        }
        /// <summary>
        /// 是否有宽带 0表示无宽带，1 表示有宽带
        /// </summary>
        public int? BroadnetAccess
        {
            set { _broadnetaccess = value; }
            get { return _broadnetaccess; }
        }
        /// <summary>
        /// 宽带是否收费 0表示免费，1 表示收费
        /// </summary>
        public int? BroadnetFee
        {
            set { _broadnetfee = value; }
            get { return _broadnetfee; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comments
        {
            set { _comments = value; }
            get { return _comments; }
        }
        /// <summary>
        /// 房间最大入住人数  如没有提供请根据房间名称判断：单人间或有单字的为1人，三人间的为3人，其他的默认2人
        /// </summary>
        public int? Capacity
        {
            set { _capacity = value; }
            get { return _capacity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string house_service
        {
            set { _house_service = value; }
            get { return _house_service; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? study
        {
            set { _study = value; }
            get { return _study; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Kitchen
        {
            set { _kitchen = value; }
            get { return _kitchen; }
        }
        #endregion Model 

        //[NotMapped]
        private List<hotel_room_info> roomList = new List<hotel_room_info>();
        [NotMapped]
        public List<hotel_room_info> RoomList
        {
            get { return roomList; }
            set { roomList = value; }
        }

        private Hotel_room_RP_price prices = new Hotel_room_RP_price();
        [NotMapped]
        public Hotel_room_RP_price Prices
        {
            get { return prices; }
            set { prices = value; }
        }
        private DrrRules drrs = new DrrRules();
        [NotMapped]
        public DrrRules Drrs
        {
            get { return drrs; }
            set { drrs = value; }
        }

        private RoomStatus roomStatus = new RoomStatus();
        [NotMapped]
        public RoomStatus RoomStatus
        {
            get { return roomStatus; }
            set { roomStatus = value; }
        }

        //床型
        public List<string> getBedType()
        {
            List<string> dic = new  List<string>();
            string[] str = System.Configuration.ConfigurationManager.AppSettings["BedType"].Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                dic.Add(str[i].Trim());
            }
            return dic;
        }
        public List<hotel_room_info> getRoomsByHoltelId(int hotelId)
        {
            using (DBC.HotelDBContent db = new DBC.HotelDBContent())
            {
                return (from r in db.rooms where r.hotel_id == hotelId select r).ToList();
            }
        }
        /// <summary>
        /// 修改房型
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="hotel_room_info"></param>
        /// <returns></returns>
        public bool updateRoom(hotel_room_info hotel_room_info)
        {
            bool result = false;
            try
            {
                using (DBC.HotelDBContent db = new DBC.HotelDBContent())
                {
                    var room = (from r in db.rooms where r.room_id == hotel_room_info.room_id select r).Single();
                    if (room != null)
                    {
                        room.h_r_name_cn = hotel_room_info.h_r_name_cn;
                        room.h_r_bed_type = hotel_room_info.h_r_bed_type;
                        room.h_r_description_cn = hotel_room_info.h_r_description_cn;
                        room.h_r_floor = hotel_room_info.h_r_floor;
                        room.Comments = hotel_room_info.Comments;
                        room.house_service = hotel_room_info.house_service;

                        room.sitting_room_number = hotel_room_info.sitting_room_number;
                        room.study = hotel_room_info.study;
                        room.h_r_bathroom_number = hotel_room_info.h_r_bathroom_number;
                        room.KitchenNumber = hotel_room_info.KitchenNumber;
                        room.Balcony = hotel_room_info.Balcony;
                       
                        room.h_r_bedroom_number = hotel_room_info.h_r_bedroom_number;

                        room.h_r_utime = DateTime.Now;
                        room.h_r_people_number = hotel_room_info.h_r_people_number;
                        room.h_r_house_number = hotel_room_info.h_r_house_number;
                      
                        room.h_r_acreage = hotel_room_info.h_r_acreage;
                        room.defaultPrice = hotel_room_info.DefaultPrice;
                        result = db.SaveChanges() > 0 ? true : false; ;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
                throw;
            }
            return result;
           
        }

    }
   
}