using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.DBC;
using SAS.help;
using SAS.Models;
using f = System.IO;

namespace SAS.Controllers
{
    public class ImagePropertyController : Controller
    {
        //
        // GET: /ImageProperty/
        int sign = 0;
        private HotelDBContent db = new HotelDBContent();
        public ActionResult Index()
        {
            return View();
        }
        //修改图片标题
        public int ImageDes(string PID, string text)
        {

            try
            {
                int ID =0;   int.TryParse( PID, out ID);
                string DescText = text;
                var p = (from i in db.roomImages where i.h_r_p_id == ID select i).SingleOrDefault();
                if (p != null)
                {
                    p.h_r_p_title = DescText;
                    if (db.SaveChanges() > 0)
                        sign = 1;
                    else
                    {
                        sign = 0;
                        DBhelp.log("图片描述修改失败PID=" + PID);
                    }
                }
            }
            catch (Exception ex)
            {

                DBhelp.log("修改图片" + ex.ToString());
            }
            return sign;
        }
        public int ImageDel(string PID, string text1,string text2)
        {
            try
            {
                string pth=System.Web. HttpContext.Current.Server.MapPath("..");
                string oPath, tPath;
               // string tPath = pth + text2;
                hotel_room_picture_info p = null;
                int ID = 0;int.TryParse(PID,out ID);
               
                    p = (from i in db.roomImages where i.h_r_p_id == ID select i).SingleOrDefault();
                    oPath = pth + p.h_r_p_pic_original_url;
                    tPath = pth + p.h_r_p_pic_thumb_url;
                    if (p != null)
                        db.roomImages.Remove(p);
                    if (db.SaveChanges() > 0)
                        sign = 1;
                    else
                        sign = 0;
                
             //  path= path.Remove(path.IndexOf(".."), 2);
              if (f.File.Exists(oPath))
               {
                   f.File.Delete(oPath);
                   sign = 1;
               }
               if (f.File.Exists(tPath))
               {
                   f.File.Delete(tPath);
                   sign = 1;
               }
            }
            catch (Exception ex)
            {

                sign = 0;
                DBhelp.log("删除图片" + ex.ToString());
            }
           
            return sign;
        }
        //修改图片类型
        public int ImageType(string PID, string text)
        {
            try
            {
                int ID = Convert.ToInt32(PID);
                int DescText =Convert.ToInt32(text);
                var p = (from i in db.roomImages where i.h_r_p_id == ID select i).SingleOrDefault();
                p.h_r_p_type = DescText;
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
