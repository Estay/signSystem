using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace SAS.help
{
    public static class StringHelper
    {
        /// <summary>
        /// 清除HTML标签的多余
        /// </summary>
        /// <param name="el">标签名</param>
        /// <param name="str">源字符串</param>
        ///  <param name="pat">匹配标签名</param>
        /// <param name="str">替换</param>
        /// <returns></returns>
        public static string repElement(string el, string str)
        {
            string pat = @"<" + el + "[^>]+>";
            string rep = "<" + el + ">";
            str = Regex.Replace(str.ToString(), pat, rep);
            return str;
        }

        //public static string ContentGetThumbImg(string Content)
        //{
        //    string OriginalSrc = "";
        //    string reg = "<img[ \f\n\r\t\v]*[^>]*/?>";
        //    //  string reg = "<\"div\" class=\"newshow_pic\"" + "><\"img\" src=" + Img + " " + " style=\"Width:592px ;height:372\" > </\"div\"> ";
        //    Regex regex = new Regex(reg, RegexOptions.IgnoreCase);
        //    MatchCollection array = regex.Matches(Content.ToLower());
        //    for (int i = 0; i < array.Count; i++)
        //    {
        //        string img = array[i].Value.Replace("px", "");
        //        if (img.IndexOf("http://") >= 0 || img.IndexOf("https://") >= 0 || img.IndexOf("ftp:") >= 0 || img.IndexOf("file:") >= 0 || img.IndexOf("width=") == -1 || img.IndexOf("height=") == -1)
        //        {
        //            continue;
        //        }
        //        /*
        //         * 获取图片的宽度
        //         */
        //        regex = new Regex("width=\"[0-9]*(px)?\"", RegexOptions.IgnoreCase);
        //        int a = regex.Match(img).ToString().IndexOf('"') + 1;
        //        int b = regex.Match(img).ToString().LastIndexOf('"');
        //        int width = Convert.ToInt32(regex.Match(img).ToString().Substring(a, b - a));
        //        /*
        //        * 获取图片的高度
        //        */
        //        regex = new Regex("height=\"[0-9]*(px)?\"", RegexOptions.IgnoreCase);
        //        a = regex.Match(img).ToString().IndexOf('"') + 1;
        //        b = regex.Match(img).ToString().LastIndexOf('"');
        //        int height = Convert.ToInt32(regex.Match(img).ToString().Substring(a, b - a));
        //        /*
        //        * 获取图片的Url
        //        */
        //        regex = new Regex("src=\"[^ \f\n\r\t\v][^\"]*\"", RegexOptions.IgnoreCase);
        //        a = regex.Match(img).ToString().IndexOf('"') + 1;
        //        b = regex.Match(img).ToString().LastIndexOf('"');
        //        string src = regex.Match(img).ToString().Substring(a, b - a);

        //        OriginalSrc = src;


        //    }
        //    return OriginalSrc;
        //}

        /// <summary>
        /// 获取内层标签的宽高属性,为内层标签加上外层修饰标签div层
        /// </summary>
        /// <param name="el">内层标签名称</param>
        /// <param name="str">不用</param>
        /// <param name="Img">img的html标签，必须有width和height属性哦</param>
        /// <returns></returns>
        public static string repElement(string el, string str, string Img)
        {
            string div = "div";
            string pat = @"<" + el + "[^>]+>";

            string reg = "<img[ \f\n\r\t\v]*[^>]*/?>";
            Regex regex = new Regex(reg, RegexOptions.IgnoreCase);
            MatchCollection array = regex.Matches(Img.ToLower());
            for (int i = 0; i < array.Count; i++)
            {
                string img = array[i].Value.Replace("px", "");
                if (img.IndexOf("http://") >= 0 || img.IndexOf("https://") >= 0 || img.IndexOf("ftp:") >= 0 || img.IndexOf("file:") >= 0 || img.IndexOf("width=") == -1 || img.IndexOf("height=") == -1)
                {
                    continue;
                }

                regex = new Regex("width=\"[0-9]*(px)?\"", RegexOptions.IgnoreCase);
                int a = regex.Match(img).ToString().IndexOf('"') + 1;
                int b = regex.Match(img).ToString().LastIndexOf('"');
                int width = Convert.ToInt32(regex.Match(img).ToString().Substring(a, b - a));
                a = regex.Match(img).ToString().IndexOf('"') + 1;
                b = regex.Match(img).ToString().LastIndexOf('"');
                width = width = Convert.ToInt32(regex.Match(img).ToString().Substring(a, b - a));
                /*
                * 获取图片的高度
                */
                regex = new Regex("height=\"[0-9]*(px)?\"", RegexOptions.IgnoreCase);
                a = regex.Match(img).ToString().IndexOf('"') + 1;
                b = regex.Match(img).ToString().LastIndexOf('"');
                int height = Convert.ToInt32(regex.Match(img).ToString().Substring(a, b - a));
                a = regex.Match(img).ToString().IndexOf('"') + 1;
                b = regex.Match(img).ToString().LastIndexOf('"');
                height = Convert.ToInt32(regex.Match(img).ToString().Substring(a, b - a));

                /*
                * 获取图片的Url
                */
                regex = new Regex("src=\"[^ \f\n\r\t\v][^\"]*\"", RegexOptions.IgnoreCase);
                a = regex.Match(img).ToString().IndexOf('"') + 1;
                b = regex.Match(img).ToString().LastIndexOf('"');
                string src = regex.Match(img).ToString().Substring(a, b - a);

                /*
                * 获取图片的 IMG
                */
                regex = new Regex(reg, RegexOptions.IgnoreCase);
                string oldimg = regex.Match(img).ToString();
                int c = regex.Match(img).ToString().IndexOf('<') + 1;
                int d = regex.Match(img).ToString().LastIndexOf('>');
                string old = regex.Match(img).ToString().Substring(c, d - c);
                string rep = "<" + div + " class=\"newshow_pic\"" + "><div class=\"new_piccon\"><" + el + " src=\"" + src + "\" Width=\"592px\" height=\"372px\" > </div></" + div + "> ";
                Img = Img.Replace(oldimg, rep);
            }



            //string rep = "<" + div + " class=\"newshow_pic\"" + "><" + el + " src=" + Img + " " + " style=\"Width:592px ;height:372\" > </" + div + "> ";
            //str = Regex.Replace(str.ToString(), pat, rep);
            //return str;
            return Img;
        }

        /// <summary>
        /// 把读取的文件中的所有的html标记去掉，把&nbsp;替换成空格
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ParseHtml(string html)
        {
            html = HttpUtility.HtmlDecode(html);
            string[] el = new string[] { "p", "span", "strong", "table", "div", "tr", "td", "a", "b" };
            foreach (string s in el)
            {
                html = repElement(s, html);
            }
            //替换脚本
            html = Regex.Replace(html, @"<script(.*)</script>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Regex reg = new Regex("<[^>]*>");
            html = reg.Replace(html, "");

            html = Regex.Replace(html, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"-->", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<!--.*", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&nbsp;", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "  ", "", RegexOptions.IgnoreCase);
            html = html.Replace("<", "");
            html = html.Replace(">", "");
            html = html.Replace("\r\n", "");
            return html;
        }
        /// <summary>
        /// 获取web.config文件中<appSetting>配置节的值
        /// </summary>
        /// <param name="Name">key</param>
        /// <returns>value</returns>
        public static string appSettings(string key)
        {
            if (key != "")
            {
                if (ConfigurationManager.AppSettings[key] != null)
                {
                    return ConfigurationManager.AppSettings[key].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 创建不存在的目录,耦合度过高无法测试。
        /// 能够加入单元测试，必须把mappath部分改成文件的路径字符串，而参数则是物理地址，而不是虚拟地址。
        /// </summary>
        /// <param name="Path">需要创建的目录</param>
        public static void Direcotorys(string Path)
        {
            if (Path != "")
            {
                Path = HttpContext.Current.Server.MapPath(Path);
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
            }
        }

        public static string FromatString(string html)
        {
            //html = Regex.Replace(html, @"</div>", "</p>", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<div", "<p", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</span>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<span\s*[^>]*>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</pre>", "</p>", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<pre\s*[^>]*>", "<p>", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</h2>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<h2>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</h4>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<h4>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, "class=[\"][a-zA-Z0-9_]+[^\"][\"]", "", RegexOptions.IgnoreCase);
            ////html = Regex.Replace(html, "style=\\s*[^\"]*", "", RegexOptions.IgnoreCase);
            ////html = Regex.Replace(html, "style=\\s*[^\"]*(text-align: center;)\\s*[^\"]*", "style=\"text-align: center;\"", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"</font>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<font\s*[^>]*>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<p>&nbsp;</p>", "<br />", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"<p><b><o:p></o:p></b></p>", "", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"p><b>&nbsp;</b></p>", "<br>", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"line-height: 150%", "line-height: 250%", RegexOptions.IgnoreCase);
            //html = Regex.Replace(html, @"line-height:150%", "line-height:250%", RegexOptions.IgnoreCase);
            return html;
        }

        /// <summary>
        /// 从字符串中抓取图片.并生成缩略图，
        /// 暂时无法测试，由于耦合与mappath，在重构是，建议分成2个函数，一个用于从字符串获取图片地址，另一个通过传入的实际物理地址生成缩略图。
        /// </summary>
        /// <param name="Content">需要被抓取的内容</param>
        /// <returns>返回替换后的内容</returns>
        public static string GetThumbImg(string Content)
        {
            string OriginalSrc = "";
            string reg = "<img[ \f\n\r\t\v]*[^>]*/?>";
            //  string reg = "<\"div\" class=\"newshow_pic\"" + "><\"img\" src=" + Img + " " + " style=\"Width:592px ;height:372\" > </\"div\"> ";
            Regex regex = new Regex(reg, RegexOptions.IgnoreCase);
            MatchCollection array = regex.Matches(Content.ToLower());
            for (int i = 0; i < array.Count; i++)
            {
                string img = array[i].Value.Replace("px", "");
                if (img.IndexOf("http://") >= 0 || img.IndexOf("https://") >= 0 || img.IndexOf("ftp:") >= 0 || img.IndexOf("file:") >= 0 || img.IndexOf("width=") == -1 || img.IndexOf("height=") == -1)
                {
                    continue;
                }
                /*
                 * 获取图片的宽度
                 */
                regex = new Regex("width=\"[0-9]*(px)?\"", RegexOptions.IgnoreCase);
                int a = regex.Match(img).ToString().IndexOf('"') + 1;
                int b = regex.Match(img).ToString().LastIndexOf('"');
                int width = Convert.ToInt32(regex.Match(img).ToString().Substring(a, b - a));
                /*
                * 获取图片的高度
                */
                regex = new Regex("height=\"[0-9]*(px)?\"", RegexOptions.IgnoreCase);
                a = regex.Match(img).ToString().IndexOf('"') + 1;
                b = regex.Match(img).ToString().LastIndexOf('"');
                int height = Convert.ToInt32(regex.Match(img).ToString().Substring(a, b - a));
                /*
                * 获取图片的Url
                */
                regex = new Regex("src=\"[^ \f\n\r\t\v][^\"]*\"", RegexOptions.IgnoreCase);
                a = regex.Match(img).ToString().IndexOf('"') + 1;
                b = regex.Match(img).ToString().LastIndexOf('"');
                string src = regex.Match(img).ToString().Substring(a, b - a);

                OriginalSrc = src;
                /*
                 *生成图片缩略图
                 */
                if (File.Exists(HttpContext.Current.Server.MapPath(src)))
                {
                    int index = src.LastIndexOf(".");
                    string fileType = src.Substring(index).ToLower();
                    int index2 = src.LastIndexOf("/");
                    string filePath = src.Substring(0, index2 + 1);
                    string fileName = DateTime.Now.ToString("yyyyMMdd");
                    Random r = new Random();
                    string StringRandom = r.Next(999999999).ToString() + i; ;//生成9位随机数字
                    System.Drawing.Image oImage = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(src));
                    int owidth = oImage.Width; //原图宽度 
                    int oheight = oImage.Height; //原图高度
                    if (owidth > width && oheight > height)//原图尺寸大于小缩略图尺寸
                    {
                        if (owidth >= oheight)
                        {
                            height = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(width) / Convert.ToDouble(owidth)));//等比设定高度
                        }
                        else
                        {
                            width = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(height) / Convert.ToDouble(oheight)));//等比设定宽度
                        }
                    }
                    else
                    {
                        height = oheight;
                        width = owidth;
                    }

                    //生成缩略原图 （大图）
                    Bitmap sImage = new Bitmap(width, height);
                    Graphics sg = Graphics.FromImage(sImage);
                    sg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
                    sg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
                    sg.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                    sg.DrawImage(oImage, new Rectangle(0, 0, width, height), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);
                    try
                    {
                        switch (fileType)
                        {
                            case ".pjpeg":
                            case ".jpeg":
                            case ".jpg":
                                {
                                    sImage.Save(HttpContext.Current.Server.MapPath(filePath + fileName + StringRandom + fileType), System.Drawing.Imaging.ImageFormat.Jpeg);
                                    Content = Content.Replace(OriginalSrc, filePath + fileName + StringRandom + fileType);
                                    break;
                                }
                            case ".bmp":
                                {
                                    sImage.Save(HttpContext.Current.Server.MapPath(filePath + fileName + StringRandom + fileType), System.Drawing.Imaging.ImageFormat.Bmp);
                                    Content = Content.Replace(OriginalSrc, filePath + fileName + StringRandom + fileType);
                                    break;
                                }
                            case ".gif":
                                {
                                    sImage.Save(HttpContext.Current.Server.MapPath(filePath + fileName + StringRandom + fileType), System.Drawing.Imaging.ImageFormat.Gif);
                                    Content = Content.Replace(OriginalSrc, filePath + fileName + StringRandom + fileType);
                                    break;
                                }
                            case ".png":
                                {
                                    sImage.Save(HttpContext.Current.Server.MapPath(filePath + fileName + StringRandom + fileType), System.Drawing.Imaging.ImageFormat.Png);
                                    Content = Content.Replace(OriginalSrc, filePath + fileName + StringRandom + fileType);
                                    break;
                                }
                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        //释放资源 
                        sg.Dispose();
                        sImage.Dispose();
                        oImage.Dispose();
                    }
                }
            }
            return Content;
        }
        /// <summary>
        /// 返回内容区第一张图片的缩略图
        /// </summary>
        /// <param name="Content">内容</param>
        /// <param name="width">缩略图的宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static string GetThumbImg(string Content, int width, int height)
        {
            string ThumbImg = "";
            string reg = "<img[ \f\n\r\t\v]*[^>]*/?>";
            Regex regex = new Regex(reg, RegexOptions.IgnoreCase);
            MatchCollection array = regex.Matches(Content);
            for (int i = 0; i < array.Count; i++)
            {
                string img = array[i].Value;
                if (img.IndexOf("http://") >= 0 || img.IndexOf("https://") >= 0 || img.IndexOf("ftp:") >= 0 || img.IndexOf("file:") >= 0)
                {
                    continue;
                }
                /*
                * 获取图片的Url
                */
                regex = new Regex("src=\"[^ \f\n\r\t\v][^\"]*\"", RegexOptions.IgnoreCase);
                int a = regex.Match(img).ToString().IndexOf('"') + 1;
                int b = regex.Match(img).ToString().LastIndexOf('"');
                string src = regex.Match(img).ToString().Substring(a, b - a);

                /*
                 *生成图片缩略图
                 */
                if (File.Exists(HttpContext.Current.Server.MapPath(src)))
                {
                    int index = src.LastIndexOf(".");
                    string fileType = src.Substring(index).ToLower();
                    int index2 = src.LastIndexOf("/");
                    string filePath = src.Substring(0, index2 + 1);

                    int start = src.LastIndexOf('/');
                    int end = src.IndexOf('.');
                    //原图名称
                    string OriginalImgName = src.Substring(start + 1, end - start - 1);
                    //小图名称
                    string ThumbImgName = "small_" + OriginalImgName;
                    //如果存在此缩略图
                    if (File.Exists(HttpContext.Current.Server.MapPath(filePath + ThumbImgName + fileType)))
                    {
                        ThumbImg = filePath + ThumbImgName + fileType;
                        break;
                    }
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(filePath)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(filePath));
                    }
                    System.Drawing.Image oImage = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(src));
                    int owidth = oImage.Width; //原图宽度 
                    int oheight = oImage.Height; //原图高度
                    int height2 = height;
                    int width2 = width;
                    height2 = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(width2) / Convert.ToDouble(owidth)));//等比设定高度
                    if (height2 < height)
                    {
                        height2 = height;
                        width2 = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(height2) / Convert.ToDouble(oheight)));//等比设定宽度
                    }
                    //生成缩略原图 （大图）
                    Bitmap sImage = new Bitmap(width2, height2);
                    Graphics sg = Graphics.FromImage(sImage);
                    sg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
                    sg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
                    sg.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                    sg.DrawImage(oImage, new Rectangle(0, 0, width2, height2), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);
                    try
                    {
                        switch (fileType)
                        {
                            case ".pjpeg":
                            case ".jpeg":
                            case ".jpg":
                                {
                                    sImage.Save(HttpContext.Current.Server.MapPath(filePath + ThumbImgName + fileType), System.Drawing.Imaging.ImageFormat.Jpeg);
                                    break;
                                }
                            case ".bmp":
                                {
                                    sImage.Save(HttpContext.Current.Server.MapPath(filePath + ThumbImgName + fileType), System.Drawing.Imaging.ImageFormat.Bmp);
                                    break;
                                }
                            case ".gif":
                                {
                                    sImage.Save(HttpContext.Current.Server.MapPath(filePath + ThumbImgName + fileType), System.Drawing.Imaging.ImageFormat.Gif);
                                    break;
                                }
                            case ".png":
                                {
                                    sImage.Save(HttpContext.Current.Server.MapPath(filePath + ThumbImgName + fileType), System.Drawing.Imaging.ImageFormat.Png);
                                    break;
                                }
                        }
                        ThumbImg = filePath + ThumbImgName + fileType;
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        //释放资源 
                        sg.Dispose();
                        sImage.Dispose();
                        oImage.Dispose();
                    }
                }
                break;
            }
            return ThumbImg;
        }
        ///// <summary>
        ///// 根据ip获得游客的真实地址
        ///// 目前无法进行单元测试，进行重构后，将mappath部分变成物理路径才行。
        ///// </summary>
        ///// <param name="Ip">游客ip地址</param>
        ///// <returns></returns>
        //public static string GetAddressByIp(string Ip)
        //{
        //    IpScanner objScan = new IpScanner();
        //    objScan.DataPath = HttpContext.Current.Server.MapPath("/qqwry.dat");
        //    objScan.IP = Ip;
        //    return objScan.IPLocation(); ;
        //}
        /// <summary>
        /// 获取ip
        /// </summary>
        /// <returns></returns>
        #region 穿过代理服务器获得Ip地址,如果有多个IP，则第一个是用户的真实IP，其余全是代理的IP，用逗号隔开，这个目前无法单元测试。
        public static string getRealIp()
        {
            string ip = string.Empty;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"].ToString();
                }
                if (!string.IsNullOrEmpty(ip))
                {
                    ip = ip.Split(',').Length > 0 ? ip.Split(',')[0] : ip;
                }
            }
            else
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return ip;
        }
        #endregion


        #region 获取汉子首字母
        /// <summary>
        /// 汉字转拼音缩写
        /// </summary>/// <param name="str">要转换的汉字字符串</param>
        /// /// <returns>拼音缩写</returns>
        public static string GetPYString(string str)
        {
            string tempStr = "";
            foreach (char c in str)
            {
                if ((int)c >= 33 && (int)c <= 126)
                    //字母和符号原样保留          
                    tempStr += c.ToString();
                else
                    //累加拼音声母    
                    tempStr += GetPYChar(c.ToString());
            }
            return tempStr;
        }

        /// <summary>
        /// /// 取单个字符的拼音声母/// Code By MuseStudio@hotmail.com
        /// /// 2004-11-30/// </summary>/// <param name="c">要转换的单个汉字</param>
        /// /// <returns>拼音声母</returns>
        public static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "a";
            if (i < 0xB2C1) return "b";
            if (i < 0xB4EE) return "c";
            if (i < 0xB6EA) return "d";
            if (i < 0xB7A2) return "e";
            if (i < 0xB8C1) return "f";
            if (i < 0xB9FE) return "g";
            if (i < 0xBBF7) return "h";
            if (i < 0xBFA6) return "j";
            if (i < 0xC0AC) return "k";
            if (i < 0xC2E8) return "l";
            if (i < 0xC4C3) return "m";
            if (i < 0xC5B6) return "n";
            if (i < 0xC5BE) return "o";
            if (i < 0xC6DA) return "p";
            if (i < 0xC8BB) return "q";
            if (i < 0xC8F6) return "r";
            if (i < 0xCBFA) return "s";
            if (i < 0xCDDA) return "t";
            if (i < 0xCEF4) return "w";
            if (i < 0xD1B9) return "x";
            if (i < 0xD4D1) return "y";
            if (i < 0xD7FA) return "z";
            return "*";
        } 
        #endregion

        public static bool CheckIsNumber( this string input) { 
            string _pattern ="^\\d+$";
            Regex _regex = new Regex(_pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (_regex.IsMatch(input))
            {
                return true;
            }
            else {
                return false;
            }
        }

        /// 生成验证码
        /// </summary>
        /// <param name="length">指定验证码的长度</param>
        /// <returns></returns>
        public static string CreateValidateCode(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }
        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="containsPage">要输出到的page对象</param>
        /// <param name="validateNum">验证码</param>
        public static byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 12.0), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
