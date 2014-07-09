using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SAS.help
{
    /// <summary>
    /// FileHandle 的摘要说明
    /// </summary>
    public class FileHandle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string NewsFilePath = HttpContext.Current.Server.MapPath("\\") + "UploadFiles\\TitleImage\\";
            string AdminFilePath = HttpContext.Current.Server.MapPath("\\") + "admin\\UploadFiles\\TitleImage\\";
            context.Response.ContentType = "text/html";
            HttpFileCollection files = context.Request.Files;              // From中获取文件对象
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
                    HttpPostedFile file = files[i];                                        //得到文件对象
                    if (file.ContentLength > 0)
                    {
                        string fileName = file.FileName;
                        string extension = Path.GetExtension(fileName);
                        int num = rnd.Next(5000, 10000);                            //文件名称
                        //  path = ;
                        //保存文件。
                        string fileI = "UploadFiles\\TitleImage\\" + num.ToString() + extension;

                        string thumbImgPath = context.Server.MapPath("/") + fileI;
                        string AdminthumbImgPath = context.Server.MapPath("/") + "admin/" + fileI;
                        //thumbImg.Save();
                        file.SaveAs(thumbImgPath);
                        file.SaveAs(AdminthumbImgPath);
                        //filePath = context.Server.MapPath(filePath + "/" + fileName);
                        ////将上传来的 文件数据 保存在 对应的 物理路径上   
                        //hpFile.SaveAs(filePath);
                        string showImagePath = "..\\" + fileI;

                        context.Response.Write(showImagePath +"$"+"1089023");            //返回文件存储后的路径，用于回显。
                        context.Response.End();
                        break;

                    }
                }

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