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
            try
            {
                 int PID=Convert.ToInt32(context.Request.QueryString[0]);
                 string DescText = context.Request.QueryString[1];
                 var p = (from i in db.room where i.h_p_id == PID select i).Single();
                 p.h_p_title = DescText;
                 if (db.SaveChanges() > 0)
                    context.Response.Write(1);
                else
                {
                    context.Response.Write(0);
                    DBhelp.log("图片修改失败PID" + PID);
                }
             }
            catch (Exception ex)
            {
                
              DBhelp.log("修改图片"+ex.ToString());
            }
           
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