using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SAS.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using System.IO;
using SAS.DBC;

namespace SAS.help
{
    public class DBhelp
    {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      
        public static Dictionary<int, string> GetSelectDataByTable(string tName)
        {

            Dictionary<int, string> list = new Dictionary<int, string>();
            using(SqlConnection conn=new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(string.Format("select * from {0}",tName),conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(Convert.ToInt16(dr[0]),dr[1].ToString());
                }
            }
            return list;
        }

        public static string GetJsonBySQL(string sql)
        {

            List<Location> list = new List<Location>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Location(dr[0], dr[1]));
                }
            }
            var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string str = JsonConvert.SerializeObject(list, settings);
            return str;
        }
        
        
        public static List<hotel_room_info> getRooms(int hotel_id)
        {
            using (HotelDBContent db=new HotelDBContent())
            {
                return (from r in db.rooms where r.hotel_id == hotel_id select r).ToList();
            }
        }

       

        public static void InserDataTable(ref DataTable dt, Type t, hotel_room_RP_price_info price)
        {
            object values = null;
          
            Type type = t;



            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 1; i < 365; i++)
            {

                DataRow tdr = dt.NewRow();
                foreach (PropertyInfo p in props)
                {

                    if (!dt.Columns.Contains(p.Name) || p.Name.Contains("rp_price_id"))
                        continue;
               
                
                    string  f=GetObjectPropertyValue(price, p.Name);;
                    Type DataType= p.PropertyType;
                   string  str= p.PropertyType.ToString();
                   if (str == "System.Nullable`1[System.Int32]" && f == "")
                       continue;
                    tdr[p.Name] = f;
                    //foreach (var mi in type.Attributes)
                    //{
                    //    object obj = Activator.CreateInstance(type);
                    //    Console.WriteLine("{0}\t{1}", mi.GetValue(obj),mi.Name);
                    //}


                  }
                tdr["room_rp_start_time"] =DateTime.Now.AddDays(i);
                tdr["room_rp_end_time"] = DateTime.Now.AddDays(i);
                tdr["h_room_rp_id"] = 37353;
                tdr["room_rp_time"] = DateTime.Now;
                
                dt.Rows.Add(tdr);
            }
        }
        //通过反射得到属性值
        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }
        //得到表结构
        public static string getTStructByTName(string tName)
        {
            return "select * from " + tName + " where 1=2";
        }
        public DataTable getDataTable(string sql)
        {
            //  string connectionString = "Data Source=202.104.150.119;Initial Catalog=estay_ecs_1210;Persist Security Info=True;User ID=sa;Password=~estay123estay";
            //  string connectionString = "Data Source=192.168.0.188\\MSSQLSERVER12,1455;Initial Catalog=estay_ecs_1210;Persist Security Info=True;User ID=sa;Password=~estay123estay";


            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandTimeout = 50000;
                SqlDataReader dr = command.ExecuteReader();
                data.Load(dr);
            }
            return data;
        }
        //批量发送的数据库
        public static bool copyDataToServer(DataTable table, string tableName)
        {
            // DataRow[] dr = table.Select(string.Format("{0}='{1}'",addColoumsName(),insertSign()));
            // if (dr.Count() <= 0)
            // {
            //     DBHelper.log(table+"无新增数据");
            //     return true;
            // }
            //DataTable  dt = dr.CopyToDataTable();
            //removeColum(ref dt);
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlTransaction trans = sqlConnection.BeginTransaction();
            try
            {


                using (SqlBulkCopy SqlBulk = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    SqlBulk.BulkCopyTimeout = 1800;
                    SqlBulk.DestinationTableName = tableName;
                    SqlBulk.BatchSize = table.Rows.Count;



                    if (table != null && table.Rows.Count != 0)
                    {
                        SqlBulk.WriteToServer(table);
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                //DBHelper.log("copyDataToServer" + "----" + ex.ToString());
                return false;
            }
            finally
            {

                trans.Dispose();
                sqlConnection.Close();
            }
            return true;

        }
        //记录日志
        public static void log(string content)
        {

            string directory = @"D:\al"; ;

            if (!Directory.Exists(directory))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(directory);
            }
            string path = directory + "\\log.txt";
            StreamWriter sw = null;

            //指定日志文件的目录
            if (!File.Exists(path))
            {
                sw = File.CreateText(path);

            }
            else
            {
                sw = new StreamWriter(path, true);
                sw.BaseStream.Seek(0, SeekOrigin.End);
            }

            sw.WriteLine(DateTime.Now + "---The date is:------" + content);

            //sw.WriteLine(ex);
            //sw.WriteLine();
            sw.Close();
        }


        public static int ExcuteTableBySQL(string sql)
        {
            int rows = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                rows = command.ExecuteNonQuery();

            }
            return rows;
        }
        /// <summary>
        /// 调用存储过程，用于房态房价修改
        /// </summary>
        /// <returns></returns>
        public static bool CallProc(int roomId,string procName)
        {
            bool result=true;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(procName, connection);
                    connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@roomid", roomId));
                    cmd.ExecuteScalar(); 
                }
               
                //cmd.ex
            }
            catch (Exception ex)
            {
                help.DBhelp.log("修改房价房态存储过程调用失败");
                result = false;
            }
            return result;
           
        }

        public void getIndexData(out decimal orderCount, out decimal noreplyComment, out decimal totalPrice, out decimal commission, out decimal guranteePrice)
        {
              string uId=new help.HotelInfoHelp().getUId(); decimal _orderCount=0, _noreplyComment=0, _totalPrice=0,  _commission=0, _guranteePrice=0;
              DateTime now = DateTime.Now.AddMonths(-1); DateTime s = now.AddDays(1 - now.Day).Date ;DateTime e = now.AddDays(1 - now.Day).AddMonths(1).AddDays(-1).Date;
              string sql_order=string.Format("select count(*) from order_info where hotel_id in(select hotel_id from hotel_info where u_id='{0}') and o_state_id=1",uId);
              string sql_comment=string.Format("select count(*) from hotel_comment_info where  hotel_id in(select hotel_id from hotel_info where u_id='{0}') and IsReply=0",uId);
              string sql_bill = string.Format("select count(*),sum(o_total_price),sum(o_total_price)*(select sum(value) from temp where uid='{0}'),sum(o_guaranteeprice),(select sum(value) from temp where uid='{0}') from order_info where o_check_in_date>='{1}' and o_check_out_date<='{2}' and (o_state_id= 3 or o_state_id =4 or o_state_id=6) and hotel_id in(select hotel_id from hotel_info where u_id='{0}')  group by hotel_id", uId, s, e);
               using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                  

                    using (SqlCommand cmd = new SqlCommand(sql_order + ";" + sql_comment+";"+sql_bill, conn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())//读取所有酒店信息
                            {
                              _orderCount=Convert.ToDecimal(dr[0]);
                            }
                            
                            dr.NextResult(); 
                            while (dr.Read()) //读取用户
                            {
                                _noreplyComment = Convert.ToDecimal(dr[0]);
                            }
                            dr.NextResult();
                             while (dr.Read()) //读取菜单
                            {
                                _totalPrice = Convert.ToDecimal(dr[1]); commission = Convert.ToDecimal(dr[2]); _guranteePrice =Convert.ToDecimal(dr[3]);
                            }
                        }
                    }
                }
               orderCount = _orderCount; noreplyComment = _noreplyComment; totalPrice = _totalPrice; commission = _commission; guranteePrice = _guranteePrice;
            
  
        }
    }
}