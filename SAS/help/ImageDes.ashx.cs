using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAS.Models;

namespace SAS.help
{
    /// <summary>
    /// ImageDes 的摘要说明
    /// </summary>
    public class ImageDes : IHttpHandler
    {
        private PictureDBContent db = new PictureDBContent();
        public void ProcessRequest(HttpContext context)
        {
            int PID=Convert.ToInt32(context.Request.Form[0]);
            string DescText = context.Request.Form[0];
            var p = (from i in db.room where i.h_p_id == PID select i).Single();
            p.h_p_title = DescText;
            if (db.SaveChanges() > 0)
                context.Response.Write(1);
            else
                context.Response.Write(0);
           
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