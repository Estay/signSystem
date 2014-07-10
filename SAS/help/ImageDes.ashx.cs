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