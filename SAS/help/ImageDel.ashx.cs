using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAS.Models;
using System.IO;
namespace SAS.help
{
    /// <summary>
    /// ImageDel 的摘要说明
    /// </summary>
    public class ImageDel : IHttpHandler
    {
        private PictureDBContent db = new PictureDBContent();
        public void ProcessRequest(HttpContext context)
        {
            int PID = Convert.ToInt32(context.Request.Form[0]);
            var p = (from i in db.room where i.h_p_id == PID select i).Single();
            string path = "";
            if (File.Exists(path))
            {
                File.Delete(path);
                
            }
            db.room.Remove(p);
            if (db.SaveChanges() > 0)
                context.Response.Write("1");
            else
            {
                context.Response.Write("0");
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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