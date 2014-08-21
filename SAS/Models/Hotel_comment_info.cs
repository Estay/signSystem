using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
    }
  
}