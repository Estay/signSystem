using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public partial class Merchant_info
    {
        public Merchant_info()
        { }
        #region Model
        private string _id;
        private string _guid;
        private string _name;
        private string _password;
        private string _company;
        private string _address;
        private string _tel;
        private string _email;
        private DateTime _ctime;
        private DateTime? _utime;
        private string _website;
        private DateTime? _deadlines;
        private string _operator_id;
        private bool _status;
        private bool _display = true;
        private bool _admin;
        private string _parents;
        private DateTime? _starttime;
        private DateTime? _endtime;
        private string _limit;
        private string _limitname;
        private string _limithotelname;
        private string _limithotelid;
        /// <summary>
        /// 商户编号
        /// </summary>
        public string id
        {
            set { _id = value; }
            get { return _id; }
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
        /// 商户名
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 企业/公司
        /// </summary>
        public string company
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime ctime
        {
            set { _ctime = value; }
            get { return _ctime; }
        }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime? utime
        {
            set { _utime = value; }
            get { return _utime; }
        }
        /// <summary>
        /// 商户站点
        /// </summary>
        public string website
        {
            set { _website = value; }
            get { return _website; }
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? deadlines
        {
            set { _deadlines = value; }
            get { return _deadlines; }
        }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string operator_id
        {
            set { _operator_id = value; }
            get { return _operator_id; }
        }
        /// <summary>
        /// 状态  true：启用| false： 待启用
        /// </summary>
        public bool status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// true：展示 | false：隐藏
        /// </summary>
        public bool display
        {
            set { _display = value; }
            get { return _display; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool admin
        {
            set { _admin = value; }
            get { return _admin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string parents
        {
            set { _parents = value; }
            get { return _parents; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? startTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? endTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string limit
        {
            set { _limit = value; }
            get { return _limit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string limitName
        {
            set { _limitname = value; }
            get { return _limitname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string limitHotelName
        {
            set { _limithotelname = value; }
            get { return _limithotelname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string limitHotelId
        {
            set { _limithotelid = value; }
            get { return _limithotelid; }
        }
        #endregion Model

    }
}