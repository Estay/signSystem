using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SAS.help
{
    /// <summary>
    /// location 的摘要说明
    /// </summary>
    public class location : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           context.Response.ContentType = "text/plain";
           string type= context.Request.QueryString["type"];
           string value = context.Request.QueryString["value"];
           
            //context.Response.Write("Hello World");
            //var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            //settings.Converters.Add(enumConverter);
            context.Response.Write(getValue(type,value));
        }

        public string getValue(string type,string value)
        {
            string sql = string.Empty;
            switch (type)
            {
                case "city":
                    sql = string.Format("select city_id,city_name from city_info where province_id={0}", value);
                    break;
                case "commercial":
                    sql = string.Format("select id,Locations_name from commerical_locations_info where city_id={0}", value);
                    break;
                default:
                    sql = string.Format("select id,district_name from district_info where city_id={0}", value);
                    break;
            }
           return   DBhelp.GetJsonBySQL(sql);
        
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}