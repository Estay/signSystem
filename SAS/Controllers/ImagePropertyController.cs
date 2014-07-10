using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.help;
using SAS.Models;
using f=System.IO;

namespace SAS.Controllers
{
    public class ImagePropertyController : Controller
    {
        //
        // GET: /ImageProperty/
        int sign = 0;
        private PictureDBContent db = new PictureDBContent();
        public ActionResult Index()
        {
            return View();
        }
        public int ImageDes(string PID, string text)
        {

            try
            {
                int ID = Convert.ToInt32(PID);
                string DescText = text;
                var p = (from i in db.room where i.h_p_id == ID select i).Single();
                p.h_p_title = DescText;
                if (db.SaveChanges() > 0)
                    sign = 1;
                else
                {
                    sign = 0;
                    DBhelp.log("图片描述修改失败PID=" + PID);
                }
            }
            catch (Exception ex)
            {

                DBhelp.log("修改图片" + ex.ToString());
            }
            return sign;
        }
        public int ImageDel(string PID, string text)
        {
            string pth=System.Web. HttpContext.Current.Server.MapPath("..");
            string path = pth+text;
            hotel_picture_info p = null;
            int ID = Convert.ToInt32(PID);
            if (ID == 0)
                path =pth+ text;
            else
            {
                p = (from i in db.room where i.h_p_id == ID select i).Single();
                path = pth+p.h_p_pic_original_url;

            }
           path= path.Remove(path.IndexOf(".."), 2);
            if (f.File.Exists(path))
            {
                f.File.Delete(path);

            }
            if(p!=null)
            db.room.Remove(p);
            if (db.SaveChanges() > 0)
                sign = 1;
            else
                sign = 0;
           
            return sign;
        }

        public int ImageType(string PID, string text)
        {
            try
            {
                int ID = Convert.ToInt32(PID);
                int DescText =Convert.ToInt32(text);
                var p = (from i in db.room where i.h_p_id == ID select i).Single();
                p.h_p_type = DescText;
                if (db.SaveChanges() > 0)
                    sign = 1;
                else
                {
                    sign = 0;
                    DBhelp.log("图片类型修改失败PID=" + PID);
                }
            }
            catch (Exception ex)
            {

                DBhelp.log("修改图片" + ex.ToString());
            }
            return sign;
        }
    }
}
