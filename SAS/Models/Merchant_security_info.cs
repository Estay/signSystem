using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAS.Models
{
    public class Merchant_security_info
    {
        #region Model
        private int _id;
        private string _merchant_id;
        private DateTime _time;
        private string _location;
        private string _ip;
        private string _platform;
        private string _application;
        private bool _status;
        /// <summary>
        /// login
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 商户编号
        /// </summary>
        public string merchant_id
        {
            set { _merchant_id = value; }
            get { return _merchant_id; }
        }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime time
        {
            set { _time = value; }
            get { return _time; }
        }
        /// <summary>
        /// 位置/地址
        /// </summary>
        public string location
        {
            set { _location = value; }
            get { return _location; }
        }
        /// <summary>
        /// 网络地址
        /// </summary>
        public string ip
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 设备/平台
        /// </summary>
        public string platform
        {
            set { _platform = value; }
            get { return _platform; }
        }
        /// <summary>
        /// 浏览器/应用
        /// </summary>
        public string application
        {
            set { _application = value; }
            get { return _application; }
        }
        /// <summary>
        /// 登录状态  true： 成功| false： 失败
        /// </summary>
        public bool status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model


    }
}