using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SAS.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;

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
    }
}