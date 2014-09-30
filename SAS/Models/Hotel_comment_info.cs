using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SAS.help;

namespace SAS.Models
{
    public class Hotel_comment_info
    {
        #region Model
        private int _commentid;
        private string _guid;
        private string _orderid;
        private int? _commentsourceid;
        private string _content;
        private DateTime? _createtime;
        private string _locations;
        private string _username;
        private string _tel_mobile;
        private bool _recommendtype;
        private string _room_id;
        private string _room_name;
        private string _title;
        private string _hotel_id;
        private int _commenttypeid;
        private int? _approve_count;
        private bool _status;
        private bool _isreply;
        private string _answer;
        private DateTime? _answer_time;
        private int? _sign;
        /// <summary>
        /// 
        /// </summary>
        /// 
         [KeyAttribute]
        public int commentId
        {
            set { _commentid = value; }
            get { return _commentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string guid
        {
            set { _guid = value; }
            get { return _guid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string orderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? commentSourceId
        {
            set { _commentsourceid = value; }
            get { return _commentsourceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string locations
        {
            set { _locations = value; }
            get { return _locations; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tel_mobile
        {
            set { _tel_mobile = value; }
            get { return _tel_mobile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool recommendType
        {
            set { _recommendtype = value; }
            get { return _recommendtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string room_id
        {
            set { _room_id = value; }
            get { return _room_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string room_name
        {
            set { _room_name = value; }
            get { return _room_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string hotel_id
        {
            set { _hotel_id = value; }
            get { return _hotel_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int commentTypeId
        {
            set { _commenttypeid = value; }
            get { return _commenttypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? approve_count
        {
            set { _approve_count = value; }
            get { return _approve_count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsReply
        {
            set { _isreply = value; }
            get { return _isreply; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string answer
        {
            set { _answer = value; }
            get { return _answer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? answer_time
        {
            set { _answer_time = value; }
            get { return _answer_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sign
        {
            set { _sign = value; }
            get { return _sign; }
        }
        #endregion Model

        //[NotMapped]
        /// <summary>
        /// 点评列表
        /// </summary>
        private List<Hotel_comment_info> commnetList = new List<Hotel_comment_info>();
        [NotMapped]
        public List<Hotel_comment_info> CommnetList
        {
            get { return commnetList; }
            set { commnetList = value; }
        }
        /// <summary>
        /// 账单查询(out decimal totalPrice,out decimal totalGureetePrice,out totalOtherPrice,out int totalPage)
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<Hotel_comment_info> getComments(string hotelId,string isreply,DateTime start,DateTime end, int page, out object totalPage, out List<hotel_info> ListHotels)
        {
            string tableName="hotel_comment_info";
            int pageSize = 30, allCount = 0, count = 0; decimal FreePercent = 0;
            object ototalPrice = 0, ototalGureetePrice = 0, ototalOtherPrice = 0, ototalPage;
            List<hotel_info> ListHotelsTemp = new List<hotel_info>();
            string sqlGetAllHotel = string.Format("select hotel_id,h_name_cn from {0}  where u_id='{1}'", "hotel_info", new HotelInfoHelp().getUId());
           // string hho = comment.createTime.ToString();
            string condition = string.Empty;
            List<Hotel_comment_info> list = new List<Hotel_comment_info>();
            ////酒店ID
            if (!string.IsNullOrEmpty(hotelId))
            {
                if (condition == string.Empty)
                    condition = string.Format("hotel_id={0}", hotelId);
                else
                    condition += string.Format("and hotel_id={0}", hotelId);

            }
            if (!string.IsNullOrEmpty(isreply))
            {
                if (isreply == "1")
                    condition = string.Format("IsReply={0}", hotelId);
                else
                    condition += string.Format("and IsReply={0}", hotelId);
            }
            //入住日期
            if (start != null && start.ToString() != "0001/1/1 0:00:00")
            {
                if (condition == string.Empty)
                    condition = string.Format("createTime>='{0}'", start);
                else
                    condition += string.Format(" and createTime>='{0}'", start);
            }
            //入住日期
            if (end != null && end.ToString() != "0001/1/1 0:00:00")
            {
                if (condition == string.Empty)
                    condition = string.Format("createTime<='{0}'", end);
                else
                    condition += string.Format(" and createTime<='{0}'", end);
            }
          
            if (string.IsNullOrEmpty(condition))
                condition += string.Format("hotel_id in(select hotel_id from hotel_info where u_id='{0}') ", new HotelInfoHelp().getUId());
            else
                condition += string.Format("and hotel_id in(select hotel_id from hotel_info where u_id='{0}') ", new HotelInfoHelp().getUId());
            if (condition != string.Empty)
            {

                string needFild = "orderID,content,userName,room_name,IsReply,commentid,createTime";
                string sql = page == 0 || page == 1 ? string.Format("select top {0} {2} from {3} where {1} ", pageSize, condition, needFild, tableName) : string.Format("select top {0} {3} from {4} where commentid>(select max(commentid) from (select top {1} commentid from {4} where {2} order by commentid) as a) and {2} order by commentid", pageSize, pageSize * (page - 1), condition, needFild, tableName);
                string sqlSum = string.Format("select count(*) from {2} where {0}", condition, new HotelInfoHelp().getUId(),tableName);
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmdSum = new SqlCommand(sqlSum, conn))
                    {
                        using (SqlDataReader drSum = cmdSum.ExecuteReader())
                        {
                            while (drSum.Read())
                            {
                                allCount = Convert.ToInt32(drSum[0]);
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(sql + ";" + sqlGetAllHotel, conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                              //  decimal price = Convert.ToDecimal(dr[6]);
                                Hotel_comment_info o = new Hotel_comment_info() { orderID = dr[0].ToString(), content = dr[1].ToString(), userName = dr[2].ToString(), room_name = dr[3].ToString(), IsReply = Convert.ToBoolean(dr[4]),commentId=Convert.ToInt32(dr[5]),createTime=Convert.ToDateTime(dr[6]) };
                                list.Add(o);
                            }
                            //读取所有酒店信息
                            dr.NextResult();
                            while (dr.Read())
                            {
                                ListHotelsTemp.Add(new hotel_info() { hotel_id = Convert.ToInt32(dr[0]), h_name_cn = dr[1].ToString() });
                            }
                        }
                    }
                }
            }
            // allCount = 60;
            //totalPrice = Convert.ToDecimal(ototalPrice) - Convert.ToDecimal(ototalGureetePrice) + Convert.ToDecimal(ototalGureetePrice); totalGureetePrice = ototalGureetePrice; totalOtherPrice = ototalOtherPrice;
            totalPage = Convert.ToDecimal(allCount) / pageSize > Convert.ToInt32(allCount / pageSize) ? Convert.ToInt32(allCount / pageSize) + 1 : Convert.ToInt32(allCount / pageSize);
            ListHotels = ListHotelsTemp;
            return list;
        }
    }
  
}