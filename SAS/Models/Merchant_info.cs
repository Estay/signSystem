using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
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
        private string sex;

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
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
        /// 菜单ID，对应sasMenu里面的ID，以逗号分隔
        /// </summary>
        public string limit
        {
            set { _limit = value; }
            get { return _limit; }
        }
        /// <summary>
        /// 菜单名称，对应sasMenu里面的title，以逗号分隔
        /// </summary>
        public string limitName
        {
            set { _limitname = value; }
            get { return _limitname; }
        }
        /// <summary>
        /// 操作的酒店名称,对应的酒店名称,以逗号分隔
        /// </summary>
        public string limitHotelName
        {
            set { _limithotelname = value; }
            get { return _limithotelname; }
        }
        /// <summary>
        /// 操作的酒店Id,对应的酒店id,以逗号分隔
        /// </summary>
        public string limitHotelId
        {
            set { _limithotelid = value; }
            get { return _limithotelid; }
        }
        #endregion Model

        List<Merchant_info> list_Mer = new List<Merchant_info>();
        [NotMapped]
        public List<Merchant_info> List_Mer
        {
            get { return list_Mer; }
            set { list_Mer = value; }
        }
        List<SasMenu> list_Menu = new List<SasMenu>();
        [NotMapped]
        public List<SasMenu> List_Menu
        {
            get { return list_Menu; }
            set { list_Menu = value; }
        }
        List<hotel_info> list_hotel = new List<hotel_info>();
        [NotMapped]
        public List<hotel_info> List_hotel
        {
            get { return list_hotel; }
            set { list_hotel = value; }
        }
        

        /// <summary>
        /// 读取酒店，用户，菜单数据
        /// </summary>
        /// <param name="listMer"></param>
        /// <param name="listMenu"></param>
        /// <param name="listHotel"></param>
        public void getMemberInfo(out List<Merchant_info> listMer,out List<SasMenu> listMenu,out List<hotel_info> listHotel)
        {
            List<Merchant_info> List_Mer=new List<Merchant_info>(); List<SasMenu> list_Menu=new List<SasMenu>();List<hotel_info> list_hotel=new List<hotel_info>();
            string uId=new help.HotelInfoHelp().getUId();
            string sqlMer=string.Format("select name,tel,limit,limithotelName from  merchant_info where parents='{0}'",uId), sqlHotel=string.Format("select hotel_Id,h_name_cn from hotel_info where u_id='{0}'",uId),sqlmenu=string.Format("select id,title,controlename from sasMenu",uId);
           
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                  

                    using (SqlCommand cmd = new SqlCommand(sqlHotel + ";" + sqlMer+";"+sqlmenu, conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())//读取所有酒店信息
                            {
                              //  decimal price = Convert.ToDecimal(dr[6]);
                                 list_hotel.Add(new hotel_info() { hotel_id=Convert.ToInt32(dr[0]),h_name_cn=dr[1].ToString() });
                            }
                            
                            dr.NextResult(); 
                            while (dr.Read()) //读取用户
                            {
                                List_Mer.Add(new Merchant_info() { name=dr[0].ToString(),tel=dr[1].ToString(),limit=dr[3].ToString(),limitHotelName=dr[4].ToString() });
                            }
                            dr.NextResult();
                             while (dr.Read()) //读取菜单
                            {
                                list_Menu.Add(new SasMenu() {id=Convert.ToInt32(dr[0]), title=dr[1].ToString(),controleName =dr[2].ToString() });
                            }
                        }
                    }
                }
                listMer = List_Mer; listMenu = list_Menu; listHotel = list_hotel;
            }
        }
    
}