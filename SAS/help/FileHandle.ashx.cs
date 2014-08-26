using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using SAS.DBC;
using SAS.Models;

namespace SAS.help
{
    /// <summary>
    /// FileHandle 的摘要说明
    /// </summary>
    public class FileHandle : IHttpHandler
    {
        //最大10M
        int maxSize = 104857600;
        int ThumbImages_Size = Convert.ToInt32(StringHelper.appSettings("ThumbImages_Size"));
        string ThumbImages_SavePath = StringHelper.appSettings("ThumbImages_SavePath");
        string OriginalImages_SavePath = StringHelper.appSettings("OriginalImages_SavePath");
        int ThumbImages_Height = Convert.ToInt32(StringHelper.appSettings("ThumbImages_Height"));
        int ThumbImages_Wight = Convert.ToInt32(StringHelper.appSettings("ThumbImages_Wight"));
        int Images_Height = Convert.ToInt32(StringHelper.appSettings("Images_Height"));
        int Images_Wight = Convert.ToInt32(StringHelper.appSettings("Images_Wight"));
        public void ProcessRequest(HttpContext context)
        {
            HotelDBContent db = new HotelDBContent();
            string NewsFilePath = HttpContext.Current.Server.MapPath("\\") + "UploadFiles\\TitleImage\\";
            string AdminFilePath = HttpContext.Current.Server.MapPath("\\") + "admin\\UploadFiles\\TitleImage\\";
            context.Response.ContentType = "text/html";
            HttpFileCollection files = context.Request.Files;              // From中获取文件对象
            string tag = Guid.NewGuid().ToString();
            int roomId=Convert.ToInt32(context.Request.Form[0]);
            List<Image> list1 = new List<Image>();
            if (files.Count > 0)
            {
                string path = "";
                string fileD = "../";
                if (!Directory.Exists(AdminFilePath))
                    Directory.CreateDirectory(AdminFilePath);
                if (!Directory.Exists(NewsFilePath))
                    Directory.CreateDirectory(NewsFilePath);
                //路径字符串
                Random rnd = new Random();
                for (int i = 0; i < files.Count; i++)
                {
                    #region
                    HttpPostedFile file = files[i];
                    //得到文件对象
                    if (file.ContentLength > 0)
                    {
                        int size = file.ContentLength;
                        string fileName = file.FileName;
                        Stream stream = file.InputStream;
                        System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                        int w = image.Width;
                        int h = image.Height;
                        string extension = Path.GetExtension(fileName);
                        //int num = rnd.Next(5000, 10000);                            //文件名称
                        ////  path = ;
                        ////保存文件。
                        //string fileI = "UploadFiles\\TitleImage\\" + num.ToString() + extension;

                        //string thumbImgPath = context.Server.MapPath("/") + fileI;
                        //string AdminthumbImgPath = context.Server.MapPath("/") + "admin/" + fileI;
                        ////thumbImg.Save();
                        //file.SaveAs(thumbImgPath);
                        //file.SaveAs(AdminthumbImgPath);
                        //filePath = context.Server.MapPath(filePath + "/" + fileName);
                        ////将上传来的 文件数据 保存在 对应的 物理路径上   
                        //hpFile.SaveAs(filePath);
                        
                      //  string showImagePath = "..\\" + fileI;
                        string showImagePath = string.Empty; ;
                        // && w > 500 && h > 300
                        if (size < maxSize && w > 500 && h > 300 )
                        {
                            string fileName1,message,oPath,tPath=string.Empty;
                          //  string message=
                            if (new help.ImgHelper().MoreImgUpload(file, out fileName1, out message, ThumbImages_SavePath, OriginalImages_SavePath, ThumbImages_Height, ThumbImages_Wight, Images_Height, Images_Wight, ThumbImages_Size, out oPath, out tPath))
                            {
                                hotel_room_picture_info pic = new hotel_room_picture_info();
                                pic.h_r_p_pic_original_url = oPath;
                                pic.h_r_p_pic_thumb_url = tPath;
                                pic.souce_id = Convert.ToInt32(help.StringHelper.appSettings("source_id"));
                               
                                pic.room_id = roomId;
                             
                                pic.h_r_p_tag = tag;
                                pic.h_r_p_time = DateTime.Now;

                                db.roomImages.Add(pic);
                                db.SaveChanges();
                            }

                        }
                        else
                        {
                            Image IM = new Image();
                            IM.URL = showImagePath;
                            string message = string.Empty;
                            if (size > maxSize)
                                message = "图片大小小于10M";
                            if (w < 500 ||h < 300)
                            {
                                if (message == string.Empty)
                                    message += "图片像素宽大于500px,高大于300px";
                                else
                                    message += ",图片像素宽大于500px,高大于300px";
                            }
                            IM.Message = message;
                            IM.PID = 0;
                            list1.Add(IM);
                        }


                    }
                    #endregion
                }
                //  List<hotel_picture_info> list = (from p in db.room where p.h_p_tag ==tag select new { p.h_p_pic_original_url, p.h_p_id }).ToList();
                var pics = from p in db.pics
                           where p.h_p_tag == tag
                           select new
                               {
                                   p.h_p_id,
                                   p.h_p_pic_original_url,
                                   p.h_p_title

                               };
                foreach (var item in pics)
                {
                    Image pic = new Image();
                    pic.URL = item.h_p_pic_original_url;
                    pic.Message ="";
                    pic.PID = item.h_p_id;
                    list1.Add(pic);
                }

                string str = JsonConvert.SerializeObject(list1, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                context.Response.Write(str);
                //context.Response.Write(showImagePath + "$" + "1089023");            //返回文件存储后的路径，用于回显。
                //context.Response.End();

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