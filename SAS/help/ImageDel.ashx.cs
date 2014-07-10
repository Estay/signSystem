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