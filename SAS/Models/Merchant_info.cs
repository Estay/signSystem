using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private int _id;
        private string _guid;
        private string _username;
        private string _name;
        private string _password;
        private string _company;
        private string _address;
        private string _mobliephone;
        private string _tel;
        private string _email;
        private DateTime _ctime;
        private DateTime? _utime;
        private string _website;
        private DateTime? _deadlines;
        private string _operator_id;
        private int? _checkstate = 2;
        private bool _status;
        private string _notthroughexplain;
        private bool _display = true;
        private bool _admin;
        private string _parents;
        private string _limit;
        private string _limitname;
        private string _limithotelname;
        private string _limithotelid;
        private string _sex;
        private string _starttime;
        private string _endtime;
        /// <summary>
        /// 商户编号
        /// </summary>
        public int id
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
        /// 用户名
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
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
        /// 移动电话
        /// </summary>
        public string mobliephone
        {
            set { _mobliephone = value; }
            get { return _mobliephone; }
        }
        /// <summary>
        /// 固定电话
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
        /// 1为审核通过 0为不通过 2为正在审核中  默认2  为0则补充notThroughExplain
        /// </summary>
        public int? CheckState
        {
            set { _checkstate = value; }
            get { return _checkstate; }
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
        public string notThroughExplain
        {
            set { _notthroughexplain = value; }
            get { return _notthroughexplain; }
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
        /// <summary>
        /// 
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string startTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string endTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        #endregion Model
        /// <summary>
        /// 用户列表
        /// </summary>
        List<Merchant_info> list_Mer = new List<Merchant_info>();
        [NotMapped]
        public List<Merchant_info> List_Mer
        {
            get { return list_Mer; }
            set { list_Mer = value; }
        }
        /// <summary>
        /// 菜单列表
        /// </summary>
        List<SasMenu> list_Menu = new List<SasMenu>();
        [NotMapped]
        public List<SasMenu> List_Menu
        {
            get { return list_Menu; }
            set { list_Menu = value; }
        }
        /// <summary>
        /// 酒地列表
        /// </summary>
        List<hotel_info> list_hotel = new List<hotel_info>();
        [NotMapped]
        public List<hotel_info> List_hotel
        {
            get { return list_hotel; }
            set { list_hotel = value; }
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="hotel_room_info"></param>
        /// <returns></returns>
        public int updateUser(Merchant_info mer)
        {
            int result = 0;
            try
            {
                using (DBC.HotelDBContent db = new DBC.HotelDBContent())
                {
                    var merchant = (from m in db.Merchant_infos where m.id == mer.id select m).Single();
                    if (merchant != null)
                    {
                        //Merchant_info m = new Merchant_info();

                          // string f = mer.password != "******" ? new help.HotelInfoHelp().Md5(mer.password) : merchant.password; merchant.utime = DateTime.Now;
                            merchant.password = mer.password != "******" ? new help.HotelInfoHelp().Md5(mer.password) : merchant.password; merchant.utime = DateTime.Now;
                            merchant.name = mer.name; merchant.tel = mer.tel; merchant.Sex = mer.Sex; merchant.startTime = mer.startTime; merchant.endTime = mer.endTime; merchant.limit = mer.limit; merchant.limitName = mer.limitName; merchant.limitHotelId = mer.limitHotelId; merchant.limitHotelName = mer.limitHotelName; 
                      
                        result = db.SaveChanges() > 0 ? 1 : 0; ;
                    }
                }
            }
            catch (Exception)
            {
                result = 0;
                throw;
            }
            return result;

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
            string sqlMer = string.Format("select name,tel,limitName,limithotelName,id,limit,admin from  merchant_info where operator_id='{0}' or tel='{0}'", uId), sqlHotel = string.Format("select hotel_Id,h_name_cn from hotel_info where u_id='{0}'", uId), sqlmenu = string.Format("select id,title,controlename from sasMenu where id!=10 and id!=1", uId);
           
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
                                List_Mer.Add(new Merchant_info() { name = dr[0].ToString(), tel = dr[1].ToString(), limitName = dr[2].ToString(), limitHotelName = dr[3].ToString(), id = Convert.ToInt32(dr[4]), limit =dr[5].ToString(),admin=Convert.ToInt32(dr[6])==1?true:false});
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