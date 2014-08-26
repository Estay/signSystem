using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;

namespace SAS.help
{
    /// <summary>
    /// 图片处理
    /// </summary>
    public class ImgHelper
    {
        public ImgHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /*
    1.原始图片完整路径 
    2.原始图片名称 
    3.缩略图完整路径 
    4.缩略图名称 
    5.限定宽度 
    6.限定高度 
    7.缩略图宽度 
    8.缩略图高度 
    9.限定大小 
    10.是否等比压缩 
    11.存放路径 
    12.信息输出
          */

        private static string _MSG;
        private string _Original_SavePath = "0";
        private string _Thumb_SavePath = "0";
        private string _ofilename = "0";
        private string _tfilename = "0";

        private int _limitwidth = 3072;
        private int _limitheight = 2304;

        private int _twidth = 130;
        private int _theight = 88;

        private int _size = 3145728;
        private bool _israte = true;

        private string _path = "D:\\We7.CMS.Source\\Upload\\Pic";

        /// <summary>
        /// 信息
        /// </summary>
        public static string MSG
        {
            get { return _MSG; }
            set { _MSG = value; }
        }

        /// <summary>
        /// 保存时的完整路径.原图
        /// </summary>
        public string Original_SavePath
        {
            get { return _Original_SavePath; }
            set { _Original_SavePath = value; }
        }

        /// <summary>
        /// 保存时的完整路径.缩略图
        /// </summary>
        public string Thumb_SavePath
        {
            get { return _Thumb_SavePath; }
            set { _Thumb_SavePath = value; }
        }

        /// <summary>
        /// 保存时的图片名称.原图
        /// </summary>
        public string OFileName
        {
            get { return _ofilename; }
            set { _ofilename = value; }
        }

        /// <summary>
        /// 保存时的图片名称.缩略图
        /// </summary>
        public string TFileName
        {
            get { return _tfilename; }
            set { _tfilename = value; }
        }

        /// <summary>
        /// 限定宽度
        /// </summary>
        public int LimitWidth
        {
            get { return _limitwidth; }
            set { _limitwidth = value; }
        }

        /// <summary>
        /// 限定高度
        /// </summary>
        public int LimitHeight
        {
            get { return _limitheight; }
            set { _limitheight = value; }
        }

        /// <summary>
        /// 缩略图宽度
        /// </summary>
        public int TWidth
        {
            get { return _twidth; }
            set { _twidth = value; }
        }

        /// <summary>
        /// 缩略图高度
        /// </summary>
        public int THeight
        {
            get { return _theight; }
            set { _theight = value; }
        }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// 是否成比例
        /// </summary>
        public bool IsRate
        {
            get { return _israte; }
            set { _israte = value; }
        }

        /// <summary>
        /// 是否生成缩略图
        /// </summary>
        public bool IsCreate
        {
            get { return _israte; }
            set { _israte = value; }
        }

        /// <summary>
        /// 存放图片的文件夹名称
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }


        //GeneralConfigInfo config;
        //public GeneralConfigInfo ImageConfig
        //{
        //    get
        //    {
        //        if (config == null)
        //            config = GeneralConfigs.GetConfig();
        //        return config;
        //    }
        //}
        public bool ArticleImgUpload(FileUpload file, out string ImgName, out string message,
            string OriginalArticleImages_SavePath, string ThumbArticleImages_SavePath, int h, int w)
        {
            TWidth = w;
            THeight = h;
            int SWidth = 283;
            int SHeight = 161;
            int DWidth = 592;
            int DHeight = 372;
            int DDWidth = 0;
            int DDHeight = 0;

            ImgName = "";
            message = "";
            bool Result = false;
            if (file.HasFile)//检查是否已经选择文件
            {
                string filename = file.FileName.ToLower();
                int i = filename.LastIndexOf(".");
                filename = filename.Substring(i).ToLower();
                if (!(filename == ".bmp" ||
                    filename == ".gif" ||
                    filename == ".jpeg" || filename == ".jpg" ||
                    filename == ".png"))
                {
                    MSG = "不受支持的类型,请重新选择!";
                    return false;
                }//检查上传文件的格式是否有效

                //生成原图 
                Stream oStream = file.PostedFile.InputStream;
                System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);

                int owidth = oImage.Width; //原图宽度 
                int oheight = oImage.Height; //原图高度

                if (owidth > LimitWidth || oheight > LimitHeight)
                {
                    message = "超过允许的图片尺寸范围!";
                    return false;
                }//检查是否超出规定尺寸

                if (IsRate)
                {
                    //按比例计算出缩略图的宽度和高度 
                    if (owidth >= oheight)
                    {
                        THeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(TWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    else
                    {
                        TWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(THeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }
                    if (owidth > SWidth)//原图尺寸大于大缩略图尺寸
                    {
                        SHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(SWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }

                    else
                    {
                        SWidth = owidth;
                        SHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(SWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    //如果生成的缩略图的高小于规定的高，则根据规定的高度获取缩略图的宽
                    if (THeight > SHeight)
                    {
                        SHeight = THeight;
                        SWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(SHeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }

                }

                if (IsRate)
                {


                    //按比例计算出缩略图的宽度和高度 
                    if (owidth >= oheight)
                    {
                        DDHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(DWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    else
                    {
                        DDWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(DHeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }
                    if (owidth > DWidth)//原图尺寸大于大缩略图尺寸
                    {
                        DDHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(DWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }

                    else
                    {
                        DDWidth = owidth;
                        DDHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(DWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }

                    //如果生成的缩略图的高小于规定的高，则根据规定的高度获取缩略图的宽
                    if (DDHeight > DHeight)
                    {
                        DDHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(DWidth) / Convert.ToDouble(owidth)));//等比设定高度
                        DDWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(DHeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }
                    else
                    {
                        DDHeight = DHeight;
                        DDWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(DHeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }

                }

                //生成缩略原图 
                Bitmap tImage = new Bitmap(TWidth, THeight);
                Graphics g = Graphics.FromImage(tImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
                g.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                g.DrawImage(oImage, new Rectangle(0, 0, TWidth, THeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);


                //生成缩略原图 （大图）
                Bitmap sImage = new Bitmap(DDWidth, DDHeight);
                Graphics sg = Graphics.FromImage(sImage);
                sg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
                sg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
                sg.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                sg.DrawImage(oImage, new Rectangle(0, 0, DDWidth, DDHeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);


                Random r = new Random();
                string StringRandom = r.Next(9999).ToString();//生成4位随机数字

                string Thumb_SavePath = HttpContext.Current.Server.MapPath(ThumbArticleImages_SavePath + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\");
                string Original_SavePath = HttpContext.Current.Server.MapPath(OriginalArticleImages_SavePath + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\");

                //格式化日期作为文件名
                string StringTime = DateTime.Now.ToString("yyMMdd");
                ImgName = StringTime + StringRandom + filename;
                if (!Directory.Exists(Thumb_SavePath))
                {
                    Directory.CreateDirectory(Thumb_SavePath);//在根目录下建立文件夹
                }
                if (!Directory.Exists(Original_SavePath))
                {
                    Directory.CreateDirectory(Original_SavePath);//在根目录下建立文件夹
                }
                //开始保存图片至服务器
                try
                {
                    switch (filename)
                    {
                        case ".jpeg":
                        case ".jpg":
                            {
                                oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            }

                        case ".gif":
                            {
                                oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Gif);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                            }

                        case ".png":
                            {
                                oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            }

                        case ".bmp":
                            {
                                oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            }
                    }
                    message = "图片上传成功!";
                    Result = true;
                }
                catch (Exception)
                {
                    message = "╮(╯▽╰)╭...出错啦!";
                    Result = false;
                }
                finally
                {
                    //释放资源 
                    oImage.Dispose();
                    g.Dispose();
                    tImage.Dispose();
                }
            }
            else
            {
                message = "请先选择需要上传的图片!";
                Result = false;
            }
            return Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">图片</param>
        /// <param name="ImgName">生成的图片名称</param>
        /// <param name="message">返回消息</param>
        /// <param name="ThumbSavePath">缩略图保存路径</param>
        /// <param name="OriginalSavePath">原图保存路径</param>
        /// <param name="Th">小缩略图高</param>
        /// <param name="Tw">小缩略图宽</param>
        /// <param name="Sh">大缩略图高</param>
        /// <param name="Sw">大缩略图宽</param>
        /// <param name="LimitWidth">限定最大的宽度</param>
        /// <param name="LimitHeight">限定的最大高度</param>
        /// <param name="size">允许上传图片大小的最大值</param>
        /// <returns>是否上传成功</returns>
        /// 保留原图并缩略两张不同大小的缩略图
        public bool MoreImgUpload(HttpPostedFile file, out string ImgName, out string message,string ThumbSavePath, string OriginalSavePath, int Th, int Tw, int Sh, int Sw, int size,out string oPath,out string tPath)
        {
            TWidth = Tw;
            THeight = Th;
            int SWidth = Sw;
            int SHeight = Sh;
            ImgName = "";
            message = "";
            Size = size;
            bool Result = false;
            string filename = file.FileName.ToLower();
            int i = filename.LastIndexOf(".");
            filename = filename.Substring(i).ToLower();
            //原图路径
             string tempOpath=string.Empty;
            //缩略图路径
            string tempTpath=string.Empty;

            //if (file.ContentLength == 0 || file.ContentLength > Size)
            //{
            //    message = "指定的文件大小不符合要求!";
            //    message = "-4";
            //    return false;
            //}//检查图片文件的大小

            //生成原图 
            Stream oStream = file.InputStream;
            System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);

            int owidth = oImage.Width; //原图宽度 
            int oheight = oImage.Height; //原图高度

            //if (owidth > LimitWidth || oheight > LimitHeight)
            //{
            //    message = "超过允许的图片尺寸范围!";
            //    return false;
            //}//检查是否超出规定尺寸

            if (IsRate)
            {
                if (IsRate)
                {
                    if (owidth > TWidth)//原图尺寸大于小缩略图尺寸
                    {
                        THeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(TWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    else
                    {
                        THeight = oheight;
                        TWidth = owidth;
                    }
                    if (owidth > SWidth)//原图尺寸大于大缩略图尺寸
                    {
                        SHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(SWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    else if (oheight > SHeight)//原图尺寸大于大缩略图尺寸
                    {
                        SWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(SHeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }
                    else
                    {
                        SHeight = oheight;
                        SWidth = owidth;
                    }
                }
            }

            //生成缩略原图 (小图)
            Bitmap tImage = new Bitmap(TWidth, THeight);
            Graphics g = Graphics.FromImage(tImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
            g.Clear(Color.Transparent); //清空画布并以透明背景色填充 
            g.DrawImage(oImage, new Rectangle(0, 0, TWidth, THeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);

            //生成缩略原图 （大图）
            Bitmap sImage = new Bitmap(SWidth, SHeight);
            Graphics sg = Graphics.FromImage(sImage);
            sg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
            sg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
            sg.Clear(Color.Transparent); //清空画布并以透明背景色填充 
            sg.DrawImage(oImage, new Rectangle(0, 0, SWidth, SHeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);

            Random r = new Random();
            string StringRandom = r.Next(999999999).ToString();//生成9位随机数字

            string Thumb_SavePath = HttpContext.Current.Server.MapPath(ThumbSavePath + DateTime.Now.ToString("yyyy-MM") + "\\");
            string Original_SavePath = HttpContext.Current.Server.MapPath(OriginalSavePath + DateTime.Now.ToString("yyyy-MM") + "\\");

            //格式化日期作为文件名
            string StringTime = DateTime.Now.ToString("yyyyMMdd");
            ImgName = StringTime + StringRandom + filename;
            if (!Directory.Exists(Thumb_SavePath))
            {
                Directory.CreateDirectory(Thumb_SavePath);//在根目录下建立文件夹
            }
            if (!Directory.Exists(Original_SavePath))
            {
                Directory.CreateDirectory(Original_SavePath);//在根目录下建立文件夹
            }
            //开始保存图片至服务器
            try
            {
              
                file.SaveAs(Original_SavePath + ImgName);//保存原图
                switch (filename)
                {
                    case ".pjpeg":
                    case ".jpeg":
                    case ".jpg":
                        {
                            sImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            tImage.Save(Thumb_SavePath + "T_" + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        }
                    case ".png":
                        {
                            sImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                            tImage.Save(Thumb_SavePath + "T_" + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                        }
                    case ".bmp":
                        {
                            sImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                            tImage.Save(Thumb_SavePath + "T_" + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        }
                }
                //We7.CMS.ArticleHelper.AddWatermarkToImage(ImageConfig, Thumb_SavePath + ImgName, Original_SavePath + ImgName);//生成水印图片
                message = "图片上传成功!";
                message = "1";
                Result = true;
                 tempOpath=Original_SavePath + ImgName;
                tempTpath=Original_SavePath + "T_"+ImgName;
            }
            catch (Exception ex)
            {
                message = "╮(╯▽╰)╭...出错啦!";
                message = "0";
                //ErrorHelper.CreateErrorLog(ex.ToString());
                Result = false;
            }
            finally
            {
                //释放资源 
                oImage.Dispose();
                g.Dispose();
                tImage.Dispose();
            }
            oPath= tempOpath;
            tPath = tempTpath;
            return Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">图片</param>
        /// <param name="ImgName">生成的图片名称</param>
        /// <param name="message">返回消息</param>
        /// <param name="ThumbSavePath">缩略图保存路径</param>
        /// <param name="OriginalSavePath">原图保存路径</param>
        /// <param name="Th">小缩略图高</param>
        /// <param name="Tw">小缩略图宽</param>
        /// <param name="Sh">大缩略图高</param>
        /// <param name="Sw">大缩略图宽</param>
        /// <param name="size">允许上传图片大小的最大值</param>
        /// <returns>是否上传成功</returns>
        public bool ImgUpload(FileUpload file, out string ImgName, out string message,
           string ThumbSavePath, string OriginalSavePath, int Th, int Tw, int Sh, int Sw, int size)
        {
            TWidth = Tw;
            THeight = Th;
            int SWidth = Sw;
            int SHeight = Sh;
            ImgName = "";
            message = "";
            Size = size;
            bool Result = false;
            string filename = file.FileName.ToLower();
            int i = filename.LastIndexOf(".");
            filename = filename.Substring(i).ToLower();

            if (file.PostedFile.ContentLength == 0 || file.PostedFile.ContentLength > Size)
            {
                message = "指定的文件大小不符合要求!";
                message = "-4";
                return false;
            }//检查图片文件的大小

            //生成原图 
            Stream oStream = file.PostedFile.InputStream;
            System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);

            int owidth = oImage.Width; //原图宽度 
            int oheight = oImage.Height; //原图高度

            if (owidth > LimitWidth || oheight > LimitHeight)
            {
                message = "超过允许的图片尺寸范围!";
                return false;
            }//检查是否超出规定尺寸

            if (IsRate)
            {
                if (owidth > TWidth)//原图尺寸大于小缩略图尺寸
                {
                    THeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(TWidth) / Convert.ToDouble(owidth)));//等比设定高度
                }
                else
                {
                    TWidth = owidth;
                    THeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(TWidth) / Convert.ToDouble(owidth)));//等比设定高度
                }
                //如果生成的缩略图的高小于规定的高，则根据规定的高度获取缩略图的宽
                if (Th > THeight)
                {
                    THeight = Th;
                    TWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(THeight) / Convert.ToDouble(oheight)));//等比设定宽度
                }

                if (owidth > SWidth)//原图尺寸大于大缩略图尺寸
                {
                    SHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(SWidth) / Convert.ToDouble(owidth)));//等比设定高度
                }
                else
                {
                    SWidth = owidth;
                    SHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(SWidth) / Convert.ToDouble(owidth)));//等比设定高度
                }
                //如果生成的缩略图的高小于规定的高，则根据规定的高度获取缩略图的宽
                if (Th > SHeight)
                {
                    SHeight = Th;
                    SWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(SHeight) / Convert.ToDouble(oheight)));//等比设定宽度
                }
            }

            //生成缩略原图 (小图)
            Bitmap tImage = new Bitmap(TWidth, THeight);
            Graphics g = Graphics.FromImage(tImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
            g.Clear(Color.Transparent); //清空画布并以透明背景色填充 
            g.DrawImage(oImage, new Rectangle(0, 0, TWidth, THeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);

            //生成缩略原图 （大图）
            Bitmap sImage = new Bitmap(SWidth, SHeight);
            Graphics sg = Graphics.FromImage(sImage);
            sg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
            sg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
            sg.Clear(Color.Transparent); //清空画布并以透明背景色填充 
            sg.DrawImage(oImage, new Rectangle(0, 0, SWidth, SHeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);

            Random r = new Random();
            string StringRandom = r.Next(999999999).ToString();//生成9位随机数字

            string Thumb_SavePath = HttpContext.Current.Server.MapPath(ThumbSavePath + DateTime.Now.ToString("yyyy-MM") + "\\");
            string Original_SavePath = HttpContext.Current.Server.MapPath(OriginalSavePath + DateTime.Now.ToString("yyyy-MM") + "\\");

            //格式化日期作为文件名
            string StringTime = DateTime.Now.ToString("yyyyMMdd");
            ImgName = StringTime + StringRandom + filename;
            if (!Directory.Exists(Thumb_SavePath))
            {
                Directory.CreateDirectory(Thumb_SavePath);//在根目录下建立文件夹
            }
            if (!Directory.Exists(Original_SavePath))
            {
                Directory.CreateDirectory(Original_SavePath);//在根目录下建立文件夹
            }
            //开始保存图片至服务器
            try
            {
                file.SaveAs(Thumb_SavePath + ImgName);//保存原图
                switch (filename)
                {
                    case ".pjpeg":
                    case ".jpeg":
                    case ".jpg":
                        {
                            sImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            tImage.Save(Thumb_SavePath + "T_" + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        }
                    case ".png":
                        {
                            sImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                            tImage.Save(Thumb_SavePath + "T_" + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                        }
                    case ".bmp":
                        {
                            sImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                            tImage.Save(Thumb_SavePath + "T_" + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        }
                }
                //We7.CMS.ArticleHelper.AddWatermarkToImage(ImageConfig, Thumb_SavePath + ImgName, Original_SavePath + ImgName);//生成水印图片
                message = "图片上传成功!";
                message = "1";
                Result = true;
            }
            catch (Exception ex)
            {
                message = "╮(╯▽╰)╭...出错啦!";
                message = "0";
                //ErrorHelper.CreateErrorLog(ex.ToString());
                Result = false;
            }
            finally
            {
                //释放资源 
                oImage.Dispose();
                g.Dispose();
                tImage.Dispose();
            }
            return Result;
        }

        /// <summary>
        /// VideoImage
        /// </summary>
        /// <param name="file"></param>
        /// <param name="ImgName"></param>
        /// <param name="message"></param>
        /// <param name="OriginalVideoImages_SavePath"></param>
        /// <param name="ThumbVideoImages_SavePath"></param>
        /// <param name="h"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool VideoImgUpload(FileUpload file, out string ImgName, out string message,
    string OriginalVideoImages_SavePath, string ThumbVideoImages_SavePath, int h, int w)
        {
            TWidth = w;
            THeight = h;
            int SWidth = 207;
            int SHeight = 180;

            ImgName = "";
            message = "";
            bool Result = false;
            if (file.HasFile)//检查是否已经选择文件
            {
                string filename = file.FileName.ToLower();
                int i = filename.LastIndexOf(".");
                filename = filename.Substring(i).ToLower();
                if (!(filename == ".bmp" ||
                    filename == ".gif" ||
                    filename == ".jpeg" || filename == ".jpg" ||
                    filename == ".png"))
                {
                    MSG = "不受支持的类型,请重新选择!";
                    return false;
                }//检查上传文件的格式是否有效

                //生成原图 
                Stream oStream = file.PostedFile.InputStream;
                System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);

                int owidth = oImage.Width; //原图宽度 
                int oheight = oImage.Height; //原图高度

                if (owidth > LimitWidth || oheight > LimitHeight)
                {
                    message = "超过允许的图片尺寸范围!";
                    return false;
                }//检查是否超出规定尺寸

                if (IsRate)
                {
                    //按比例计算出缩略图的宽度和高度 
                    if (owidth >= oheight)
                    {
                        THeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(TWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    else
                    {
                        TWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(THeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }
                    if (owidth > SWidth)//原图尺寸大于大缩略图尺寸
                    {
                        SHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(SWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }

                    else
                    {
                        SWidth = owidth;
                        SHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(SWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    //如果生成的缩略图的高小于规定的高，则根据规定的高度获取缩略图的宽
                    if (THeight > SHeight)
                    {
                        SHeight = THeight;
                        SWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(SHeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }

                }

                //生成缩略原图 
                Bitmap tImage = new Bitmap(TWidth, THeight);
                Graphics g = Graphics.FromImage(tImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
                g.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                g.DrawImage(oImage, new Rectangle(0, 0, TWidth, THeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);


                //生成缩略原图 （大图）
                //Bitmap sImage = new Bitmap(DDWidth, DDHeight);
                //Graphics sg = Graphics.FromImage(sImage);
                //sg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
                //sg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
                //sg.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                //sg.DrawImage(oImage, new Rectangle(0, 0, DDWidth, DDHeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);


                Random r = new Random();
                string StringRandom = r.Next(9999).ToString();//生成4位随机数字

                string Thumb_SavePath = HttpContext.Current.Server.MapPath(ThumbVideoImages_SavePath + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\");
                string Original_SavePath = HttpContext.Current.Server.MapPath(OriginalVideoImages_SavePath + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\");

                //格式化日期作为文件名
                string StringTime = DateTime.Now.ToString("yyMMdd");
                ImgName = StringTime + StringRandom + filename;
                if (!Directory.Exists(Thumb_SavePath))
                {
                    Directory.CreateDirectory(Thumb_SavePath);//在根目录下建立文件夹
                }
                if (!Directory.Exists(Original_SavePath))
                {
                    Directory.CreateDirectory(Original_SavePath);//在根目录下建立文件夹
                }
                //开始保存图片至服务器
                try
                {
                    switch (filename)
                    {
                        case ".jpeg":
                        case ".jpg":
                            {
                                // oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            }

                        case ".gif":
                            {
                                //oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Gif);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                            }

                        case ".png":
                            {
                                // oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            }

                        case ".bmp":
                            {
                                //oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            }
                    }
                    message = "图片上传成功!";
                    Result = true;
                }
                catch (Exception)
                {
                    message = "╮(╯▽╰)╭...出错啦!";
                    Result = false;
                }
                finally
                {
                    //释放资源 
                    //oImage.Dispose();
                    g.Dispose();
                    tImage.Dispose();
                }
            }
            else
            {
                message = "请先选择需要上传的图片!";
                Result = false;
            }
            return Result;
        }

        /// <summary>
        /// ActivityImageThumbnail
        /// </summary>
        /// <param name="file"></param>
        /// <param name="ImgName"></param>
        /// <param name="message"></param>
        /// <param name="OriginalActivityImages_SavePath"></param>
        /// <param name="ThumbActivityImages_SavePath"></param>
        /// <param name="h"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool ActivityImgUpload(FileUpload file, out string ImgName, out string message,
string OriginalActivityImages_SavePath, string ThumbActivityImages_SavePath, int h, int w)
        {
            TWidth = w;
            THeight = h;
            int SWidth = 207;
            int SHeight = 180;

            ImgName = "";
            message = "";
            bool Result = false;
            if (file.HasFile)//检查是否已经选择文件
            {
                string filename = file.FileName.ToLower();
                int i = filename.LastIndexOf(".");
                filename = filename.Substring(i).ToLower();
                if (!(filename == ".bmp" ||
                    filename == ".gif" ||
                    filename == ".jpeg" || filename == ".jpg" ||
                    filename == ".png"))
                {
                    MSG = "不受支持的类型,请重新选择!";
                    return false;
                }//检查上传文件的格式是否有效

                //生成原图 
                Stream oStream = file.PostedFile.InputStream;
                System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);

                int owidth = oImage.Width; //原图宽度 
                int oheight = oImage.Height; //原图高度

                if (owidth > LimitWidth || oheight > LimitHeight)
                {
                    message = "超过允许的图片尺寸范围!";
                    return false;
                }//检查是否超出规定尺寸

                if (IsRate)
                {
                    //按比例计算出缩略图的宽度和高度 
                    if (owidth >= oheight)
                    {
                        THeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(TWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    else
                    {
                        TWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(THeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }
                    if (owidth > SWidth)//原图尺寸大于大缩略图尺寸
                    {
                        SHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(SWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }

                    else
                    {
                        SWidth = owidth;
                        SHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(SWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    //如果生成的缩略图的高小于规定的高，则根据规定的高度获取缩略图的宽
                    if (THeight > SHeight)
                    {
                        SHeight = THeight;
                        SWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(SHeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }

                }

                //生成缩略原图 
                Bitmap tImage = new Bitmap(TWidth, THeight);
                Graphics g = Graphics.FromImage(tImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
                g.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                g.DrawImage(oImage, new Rectangle(0, 0, TWidth, THeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);


                //生成缩略原图 （大图）
                //Bitmap sImage = new Bitmap(DDWidth, DDHeight);
                //Graphics sg = Graphics.FromImage(sImage);
                //sg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
                //sg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
                //sg.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                //sg.DrawImage(oImage, new Rectangle(0, 0, DDWidth, DDHeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);


                Random r = new Random();
                string StringRandom = r.Next(9999).ToString();//生成4位随机数字

                string Thumb_SavePath = HttpContext.Current.Server.MapPath(ThumbActivityImages_SavePath + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\");
                string Original_SavePath = HttpContext.Current.Server.MapPath(OriginalActivityImages_SavePath + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\");

                //格式化日期作为文件名
                string StringTime = DateTime.Now.ToString("yyMMdd");
                ImgName = StringTime + StringRandom + filename;
                if (!Directory.Exists(Thumb_SavePath))
                {
                    Directory.CreateDirectory(Thumb_SavePath);//在根目录下建立文件夹
                }
                if (!Directory.Exists(Original_SavePath))
                {
                    Directory.CreateDirectory(Original_SavePath);//在根目录下建立文件夹
                }
                //开始保存图片至服务器
                try
                {
                    switch (filename)
                    {
                        case ".jpeg":
                        case ".jpg":
                            {
                                // oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            }

                        case ".gif":
                            {
                                //oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Gif);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                            }

                        case ".png":
                            {
                                // oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            }

                        case ".bmp":
                            {
                                //oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            }
                    }
                    message = "图片上传成功!";
                    Result = true;
                }
                catch (Exception)
                {
                    message = "╮(╯▽╰)╭...出错啦!";
                    Result = false;
                }
                finally
                {
                    //释放资源 
                    //oImage.Dispose();
                    g.Dispose();
                    tImage.Dispose();
                }
            }
            else
            {
                message = "请先选择需要上传的图片!";
                Result = false;
            }
            return Result;
        }

        /// <summary>
        /// 优惠缩略
        /// </summary>
        /// <param name="file"></param>
        /// <param name="ImgName"></param>
        /// <param name="message"></param>
        /// <param name="OriginalPreferentialImages_SavePath"></param>
        /// <param name="ThumbPreferentialImages_SavePath"></param>
        /// <param name="h"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public bool PreferentialImgUpload(FileUpload file, out string ImgName, out string message,
string OriginalPreferentialImages_SavePath, string ThumbPreferentialImages_SavePath, int h, int w)
        {
            TWidth = w;
            THeight = h;
            int SWidth = 207;
            int SHeight = 180;

            ImgName = "";
            message = "";
            bool Result = false;
            if (file.HasFile)//检查是否已经选择文件
            {
                string filename = file.FileName.ToLower();
                int i = filename.LastIndexOf(".");
                filename = filename.Substring(i).ToLower();
                if (!(filename == ".bmp" ||
                    filename == ".gif" ||
                    filename == ".jpeg" || filename == ".jpg" ||
                    filename == ".png"))
                {
                    MSG = "不受支持的类型,请重新选择!";
                    return false;
                }//检查上传文件的格式是否有效

                //生成原图 
                Stream oStream = file.PostedFile.InputStream;
                System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);

                int owidth = oImage.Width; //原图宽度 
                int oheight = oImage.Height; //原图高度

                if (owidth > LimitWidth || oheight > LimitHeight)
                {
                    message = "超过允许的图片尺寸范围!";
                    return false;
                }//检查是否超出规定尺寸

                if (IsRate)
                {
                    //按比例计算出缩略图的宽度和高度 
                    if (owidth >= oheight)
                    {
                        THeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(TWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    else
                    {
                        TWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(THeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }
                    if (owidth > SWidth)//原图尺寸大于大缩略图尺寸
                    {
                        SHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(SWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    else
                    {
                        SWidth = owidth;
                        SHeight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(SWidth) / Convert.ToDouble(owidth)));//等比设定高度
                    }
                    //如果生成的缩略图的高小于规定的高，则根据规定的高度获取缩略图的宽
                    if (THeight > SHeight)
                    {
                        SHeight = THeight;
                        SWidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(SHeight) / Convert.ToDouble(oheight)));//等比设定宽度
                    }
                }

                //生成缩略原图 
                Bitmap tImage = new Bitmap(TWidth, THeight);
                Graphics g = Graphics.FromImage(tImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
                g.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                g.DrawImage(oImage, new Rectangle(0, 0, TWidth, THeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);

                //生成缩略原图 （大图）
                //Bitmap sImage = new Bitmap(DDWidth, DDHeight);
                //Graphics sg = Graphics.FromImage(sImage);
                //sg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法 
                //sg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
                //sg.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                //sg.DrawImage(oImage, new Rectangle(0, 0, DDWidth, DDHeight), new Rectangle(0, 0, owidth, oheight), GraphicsUnit.Pixel);

                Random r = new Random();
                string StringRandom = r.Next(9999).ToString();//生成4位随机数字

                string Thumb_SavePath = HttpContext.Current.Server.MapPath(ThumbPreferentialImages_SavePath + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\");
                string Original_SavePath = HttpContext.Current.Server.MapPath(OriginalPreferentialImages_SavePath + "\\" + DateTime.Now.ToString("yyyy-MM") + "\\");
                string StringTime = DateTime.Now.ToString("yyMMdd");
                ImgName = StringTime + StringRandom + filename;
                if (!Directory.Exists(Thumb_SavePath))
                {
                    Directory.CreateDirectory(Thumb_SavePath);
                }
                if (!Directory.Exists(Original_SavePath))
                {
                    Directory.CreateDirectory(Original_SavePath);
                }
                try
                {
                    switch (filename)
                    {
                        case ".jpeg":
                        case ".jpg":
                            {
                                //oImage 原图
                                //tImage 缩略
                                // oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            }

                        case ".gif":
                            {
                                //oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Gif);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                            }
                        case ".png":
                            {
                                // oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            }
                        case ".bmp":
                            {
                                //oImage.Save(Original_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                                tImage.Save(Thumb_SavePath + ImgName, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            }
                    }
                    message = "图片上传成功!";
                    Result = true;
                }
                catch (Exception)
                {
                    message = "╮(╯▽╰)╭...出错啦!";
                    Result = false;
                }
                finally
                {
                    //oImage.Dispose();
                    g.Dispose();
                    tImage.Dispose();
                }
            }
            else
            {
                message = "请先选择需要上传的图片!";
                Result = false;
            }
            return Result;
        }

        /// <summary>
        /// 友情链接图片处理
        /// </summary>
        /// <param name="file"> </param>
        /// <param name="PicTitle"></param>
        /// <param name="message"></param>
        /// <param name="Pic_SavePath"></param>
        /// <returns></returns>
        public bool LinkPicUpload(FileUpload file, out string PicTitle, out string message, string Pic_SavePath)
        {
            bool res = false;
            PicTitle = "";
            message = "";
            if (file.HasFile)//检查是否已经选择文件
            {
                string filename = file.FileName.ToLower();
                int i = filename.LastIndexOf(".");
                filename = filename.Substring(i).ToLower();
                if (!(filename == ".bmp" ||
                    filename == ".gif" ||
                    filename == ".jpeg" || filename == ".jpg" ||
                    filename == ".png"))
                {
                    MSG = "不受支持的类型,请重新选择!";
                    return false;
                }//检查上传文件的格式是否有效
                //格式化日期作为文件名
                Random r = new Random();
                string StringRandom = r.Next(9999).ToString();//生成4位随机数字
                string StringTime = DateTime.Now.ToString("yyMMdd");
                PicTitle = StringTime + StringRandom + filename;
                string pic1 = string.IsNullOrEmpty(file.FileName.Trim()) ? "" : PicTitle +
                    System.IO.Path.GetExtension(file.FileName); //得到名字+扩展名（例如20001265466.jpg）
                if (!string.IsNullOrEmpty(pic1)) file.SaveAs(Thumb_SavePath + pic1);//保存图片

            }
            return res;
        }


    }
}
