using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class Order_info
    {
        #region Model
        private int _order_id;
        private string _o_serialid;
        private int _hotel_id = 0;
        private string _hotel_name;
        private string _hotel_address;
        private string _hotel_tel;
        private string _hotel_description;
        private string _u_id = "0";
        private string _o_userfromdb = "ECS";
        private int _room_id = 0;
        private string _room_name;
        private string _room_descript;
        private int? _r_number_id;
        private int? _h_room_rp_id = 0;
        private string _rp_name;
        private string _rp_description;
        private int? _rp_price_id = 0;
        private string _rp_price_description;
        private int _o_state_id=1;
        private string _o_state_change_log;
        private string _o_title;
        private int _o_number;
        private decimal _o_total_price;
        private decimal? _o_unit_price;
        private bool _o_isbuy = false;
        private DateTime _o_ctime = DateTime.Now;
        private DateTime? _o_buy_time;
        private string _o_user_name;
        private string _o_user_phone;
        private string _o_user_email;
        private DateTime _o_check_in_date;
        private DateTime _o_check_out_date;
        private int _o_night = 1;
        private int _o_buy_type = 0;
        private string _o_other_guest_info;
        private int? _order_from_id;
        private string _o_guest_remark;
        private string _o_remark;
        private string _o_associateorderid;
        private DateTime? _lastestarrivetime;
        private DateTime? _earliestarrivetime;
        private bool _o_isinstantorder;
        private decimal? _o_guaranteeprice = 0M;
        private DateTime? _o_roomretain;
        private string _o_orderway = "web";
        private DateTime? _paylasesttime;
        private string _ordercreateoperatorguid;
        private string _ordercreateoperator;
        private string _channelid;
        /// <summary>
        /// 订单编码
        /// </summary>
        /// 
          [KeyAttribute]
        public int order_id
        {
            set { _order_id = value; }
            get { return _order_id; }
        }
        /// <summary>
        /// 订单流水号
        /// </summary>
        public string o_SerialId
        {
            set { _o_serialid = value; }
            get { return _o_serialid; }
        }
        /// <summary>
        /// 酒店ID 可以联表查询hotel_info不是外键关系
        /// </summary>
        public int hotel_id
        {
            set { _hotel_id = value; }
            get { return _hotel_id; }
        }
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string hotel_name
        {
            set { _hotel_name = value; }
            get { return _hotel_name; }
        }
        /// <summary>
        /// 酒店地址
        /// </summary>
        public string hotel_address
        {
            set { _hotel_address = value; }
            get { return _hotel_address; }
        }
        /// <summary>
        /// 酒店电话
        /// </summary>
        public string hotel_Tel
        {
            set { _hotel_tel = value; }
            get { return _hotel_tel; }
        }
        /// <summary>
        /// 酒店的简单描述，如星级，位置等
        /// </summary>
        public string hotel_description
        {
            set { _hotel_description = value; }
            get { return _hotel_description; }
        }
        /// <summary>
        /// 用户编号，可以联表查询user_info，不是外键关系
        /// </summary>
        public string u_id
        {
            set { _u_id = value; }
            get { return _u_id; }
        }
        /// <summary>
        /// 用户来自的数据库,目前有 ECS,anyhome
        /// </summary>
        public string o_userFromdb
        {
            set { _o_userfromdb = value; }
            get { return _o_userfromdb; }
        }
        /// <summary>
        /// 房型可以链表查询room info，但是不是外键关系
        /// </summary>
        public int room_id
        {
            set { _room_id = value; }
            get { return _room_id; }
        }
        /// <summary>
        /// 房型名称
        /// </summary>
        public string room_name
        {
            set { _room_name = value; }
            get { return _room_name; }
        }
        /// <summary>
        /// 房型的简单描述，如双人，大床
        /// </summary>
        public string room_descript
        {
            set { _room_descript = value; }
            get { return _room_descript; }
        }
        /// <summary>
        /// 房间编号
        /// </summary>
        public int? r_number_id
        {
            set { _r_number_id = value; }
            get { return _r_number_id; }
        }
        /// <summary>
        /// rate plan ID,可以联表查询room_rp_info 但是不是外键关系
        /// </summary>
        public int? h_room_rp_id
        {
            set { _h_room_rp_id = value; }
            get { return _h_room_rp_id; }
        }
        /// <summary>
        /// rate plan的名字
        /// </summary>
        public string rp_name
        {
            set { _rp_name = value; }
            get { return _rp_name; }
        }
        /// <summary>
        /// rate plan的简要描述
        /// </summary>
        public string rp_description
        {
            set { _rp_description = value; }
            get { return _rp_description; }
        }
        /// <summary>
        /// rp的价格可以联表查询room_rp_price表，但是不是外键关系
        /// </summary>
        public int? rp_price_id
        {
            set { _rp_price_id = value; }
            get { return _rp_price_id; }
        }
        /// <summary>
        /// rateplan价格的文字描述
        /// </summary>
        public string rp_price_description
        {
            set { _rp_price_description = value; }
            get { return _rp_price_description; }
        }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int o_state_id
        {
            set { _o_state_id = value; }
            get { return _o_state_id; }
        }
        /// <summary>
        /// 订单状态转变日志
        /// </summary>
        public string o_state_change_log
        {
            set { _o_state_change_log = value; }
            get { return _o_state_change_log; }
        }
        /// <summary>
        /// 订单名称（对应物品名称）
        /// </summary>
        public string o_title
        {
            set { _o_title = value; }
            get { return _o_title; }
        }
        /// <summary>
        /// 定房数量
        /// </summary>
        public int o_number
        {
            set { _o_number = value; }
            get { return _o_number; }
        }
        /// <summary>
        /// 总价，支付以此价格为准
        /// </summary>
        public decimal o_total_price
        {
            set { _o_total_price = value; }
            get { return _o_total_price; }
        }
        /// <summary>
        /// 单价，参考价格
        /// </summary>
        public decimal? o_unit_price
        {
            set { _o_unit_price = value; }
            get { return _o_unit_price; }
        }
        /// <summary>
        /// 是否支付（0 未支付  1 已支付）（默认为 0）
        /// </summary>
        public bool o_isbuy
        {
            set { _o_isbuy = value; }
            get { return _o_isbuy; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime o_ctime
        {
            set { _o_ctime = value; }
            get { return _o_ctime; }
        }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? o_buy_time
        {
            set { _o_buy_time = value; }
            get { return _o_buy_time; }
        }
        /// <summary>
        /// 订房用户名字
        /// </summary>
        public string o_user_name
        {
            set { _o_user_name = value; }
            get { return _o_user_name; }
        }
        /// <summary>
        /// 订购用户电话
        /// </summary>
        public string o_user_phone
        {
            set { _o_user_phone = value; }
            get { return _o_user_phone; }
        }
        /// <summary>
        /// 订购用户email
        /// </summary>
        public string o_user_email
        {
            set { _o_user_email = value; }
            get { return _o_user_email; }
        }
        /// <summary>
        /// 预约入住时间
        /// </summary>
        public DateTime o_check_in_date
        {
            set { _o_check_in_date = value; }
            get { return _o_check_in_date; }
        }
        /// <summary>
        /// 预约离店时间
        /// </summary>
        public DateTime o_check_out_date
        {
            set { _o_check_out_date = value; }
            get { return _o_check_out_date; }
        }
        /// <summary>
        /// 间夜
        /// </summary>
        public int o_night
        {
            set { _o_night = value; }
            get { return _o_night; }
        }
        /// <summary>
        /// 付款类型： 1 网络支付，2 到店支付 3 ...
        /// </summary>
        public int o_buy_type
        {
            set { _o_buy_type = value; }
            get { return _o_buy_type; }
        }
        /// <summary>
        /// 其他入住用户信息，用逗号分隔
        /// </summary>
        public string o_other_guest_info
        {
            set { _o_other_guest_info = value; }
            get { return _o_other_guest_info; }
        }
        /// <summary>
        /// 订单来源
        /// </summary>
        public int? order_from_id
        {
            set { _order_from_id = value; }
            get { return _order_from_id; }
        }
        /// <summary>
        /// 客人订单时候，可以填写
        /// </summary>
        public string o_guest_remark
        {
            set { _o_guest_remark = value; }
            get { return _o_guest_remark; }
        }
        /// <summary>
        /// 订单备注，管理员批复时候，留下说明
        /// </summary>
        public string o_remark
        {
            set { _o_remark = value; }
            get { return _o_remark; }
        }
        /// <summary>
        /// 关联的订单ID，根据来源分类处理，如来源艺龙，则此ID号表示是艺龙中保存的订单号ID
        /// </summary>
        public string o_associateOrderID
        {
            set { _o_associateorderid = value; }
            get { return _o_associateorderid; }
        }
        /// <summary>
        /// 最晚到店时间
        /// </summary>
        public DateTime? lastestArriveTime
        {
            set { _lastestarrivetime = value; }
            get { return _lastestarrivetime; }
        }
        /// <summary>
        /// 最早到店时间
        /// </summary>
        public DateTime? earliestArriveTime
        {
            set { _earliestarrivetime = value; }
            get { return _earliestarrivetime; }
        }
        /// <summary>
        /// elong关联的订单，是否调用即时确认接口检测过是即时确认订单，默认为null，表示未做任何检测，只有true才是经过检测后为即时确认订单，false为非即时确认订单或者未检测的
        /// </summary>
        public bool o_IsInstantOrder
        {
            set { _o_isinstantorder = value; }
            get { return _o_isinstantorder; }
        }
        /// <summary>
        /// 本订单的担保金额，0和空表示无需担保金额，担保的规则见规则表
        /// </summary>
        public decimal? o_guaranteePrice
        {
            set { _o_guaranteeprice = value; }
            get { return _o_guaranteeprice; }
        }
        /// <summary>
        /// 房间保留到的时间
        /// </summary>
        public DateTime? o_roomRetain
        {
            set { _o_roomretain = value; }
            get { return _o_roomretain; }
        }
        /// <summary>
        /// 用户从什么途径下订单的，目前支持 tel（电话）,web（网络），handphone（手机） 
        /// </summary>
        public string o_orderWay
        {
            set { _o_orderway = value; }
            get { return _o_orderway; }
        }
        /// <summary>
        /// 支付的最后时刻，这个时刻后，订单自动进入取消状态。
        /// </summary>
        public DateTime? payLasestTime
        {
            set { _paylasesttime = value; }
            get { return _paylasesttime; }
        }
        /// <summary>
        /// 订单创建人Guid,对应account表记录
        /// </summary>
        public string orderCreateOperatorGuid
        {
            set { _ordercreateoperatorguid = value; }
            get { return _ordercreateoperatorguid; }
        }
        /// <summary>
        /// 订单的创建人登录名，后台订单必须会有此项目，登录名字可以对差account，admin为超级管理员
        /// </summary>
        public string orderCreateOperator
        {
            set { _ordercreateoperator = value; }
            get { return _ordercreateoperator; }
        }
        /// <summary>
        /// 渠道号，2014-06-13
        /// </summary>
        public string channelID
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        #endregion Model

        //[NotMapped]
        private List<Order_info> orderList = new List<Order_info>();
        [NotMapped]
        public List<Order_info> OrderList
        {
            get { return orderList; }
            set { orderList = value; }
        }
     
        public enum orderStatus
        {
           newOrder=1, //新单
           confirmed=2   //已确认 
            
        }
        public List<Order_info> getOrderInfos(orderStatus state)
        {
           
            int orderState = (int)state;;
            List<Order_info> list=new List<Order_info>();
            try
            {
                using (DBC.HotelDBContent db = new DBC.HotelDBContent())
                {
                    string uId=help.HotelInfoHelp.getUId();
                    int[] rf = (from h in db.hotel where h.u_id == uId select h.hotel_id).ToArray();
                   
                        list = (from o in db.orders where rf.Contains(o.hotel_id) && o.o_state_id == orderState select o).ToList();
                    
                }
                // uId = "test1";

                // var f = from h in db.drrs where rf.Contains(h.hotel_id) select h;
                
            }
            catch (Exception e)
            {

                throw e;
            }
            return list;
           
        }
        public List<Order_info> getOrderInfos(string start,string end, Order_info order)
        {
            int orderState = 0; DateTime s, e; DateTime.TryParse(start, out s); DateTime.TryParse(end, out e);
            List<Order_info> list = new List<Order_info>();
           // int.TryParse(state.ToString(), out orderState);
            try
            {
                using (DBC.HotelDBContent db = new DBC.HotelDBContent())
                {
                    string uId = help.HotelInfoHelp.getUId();
                    int[] rf = (from h in db.hotel where h.u_id == uId select h.hotel_id).ToArray();

                    list = (from o in db.orders where rf.Contains(o.hotel_id) && o.o_ctime >= s && o.o_ctime<=e select o).ToList();

                }
                // uId = "test1";

                // var f = from h in db.drrs where rf.Contains(h.hotel_id) select h;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return list;

        }
       
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<Order_info> getOrderInfos( Order_info order)
        {
            string hho = order.o_check_in_date.ToString();
            string condition=string.Empty;
            if (!string.IsNullOrEmpty(order.o_SerialId ))
            {
                if(condition==string.Empty)
                condition=string.Format("o_SerialId='{0}'",order.o_SerialId);
                else
                  condition+=string.Format("and o_SerialId='{0}'",order.o_SerialId);

            }
            if(!string.IsNullOrEmpty(order.o_other_guest_info))
            {
                if (condition == string.Empty)
                    condition = string.Format("o_other_guest_info='{0}'", order._o_other_guest_info);
                else
                    condition += string.Format(" and o_other_guest_info='{0}'", order._o_other_guest_info);
            } if (!string.IsNullOrEmpty(order.o_user_phone))
            {
                if (condition == string.Empty)
                    condition = string.Format("o_user_phone='{0}'", order.o_user_phone);
                else
                    condition += string.Format(" and o_user_phone='{0}'", order.o_user_phone);
            }
            //预定开始日期
            if (order.earliestArriveTime != null && order.earliestArriveTime.ToString() != "0001/1/1 0:00:00")
            {
                if (condition == string.Empty)
                    condition = string.Format("o_ctime>='{0}'", order.earliestArriveTime);
                else
                    condition += string.Format(" and o_ctime>='{0}'", order.earliestArriveTime);
            }
            //预定结束日期
            if (order.lastestArriveTime != null && order.lastestArriveTime.ToString() != "0001/1/1 0:00:00")
            {
                if (condition == string.Empty)
                    condition = string.Format("o_ctime<='{0}'", order.lastestArriveTime);
                else
                    condition += string.Format(" and o_ctime<='{0}'", order.lastestArriveTime);
            }
            //入住日期
            if (order._o_check_in_date != null && order._o_check_in_date.ToString() != "0001/1/1 0:00:00")
            {
                if (condition == string.Empty)
                    condition = string.Format("o_check_in_date>='{0}'", order._o_check_in_date);
                else
                    condition += string.Format(" and o_check_in_date>='{0}'", order._o_check_in_date);
            }
            //离店日期
            if (order._o_check_out_date != null && order._o_check_out_date.ToString() != "0001/1/1 0:00:00")
            {
                if (condition == string.Empty)
                    condition = string.Format("o_check_out_date<='{0}'", order._o_check_out_date);
                else
                    condition += string.Format(" and o_check_out_date<='{0}'", order._o_check_out_date);
            }
            List<Order_info> list = new List<Order_info>();
            if (condition != string.Empty)
            {
                string sql = string.Format("select o_SerialId, hotel_name,o_other_guest_info,o_user_phone,o_check_in_date,o_check_out_date,o_total_price ,room_name,(select  o_state_title from  order_state_type_info as s where a.o_state_id=s.o_state_id) from order_info as a where {0} ", condition);
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        DateTime s,e;
                        DateTime.TryParse(dr[4].ToString(),out s);
                        DateTime.TryParse(dr[5].ToString(),out e);
                        Order_info o = new Order_info() { o_SerialId = dr[0].ToString(), hotel_name = dr[1].ToString(), o_other_guest_info = dr[2].ToString(), o_user_phone = dr[3].ToString(), o_check_in_date = s, o_check_out_date = e, o_total_price = Convert.ToDecimal(dr[6]), room_name=dr[7].ToString(),_o_title=dr[8].ToString() };
                        list.Add(o);
                    }
                }
            }
            return list;
              

        }
        public string convertDate(DateTime t)
        {
            return t.ToString();
        }
    }

   
 
}