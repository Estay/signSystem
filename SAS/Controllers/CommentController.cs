﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.Models;
using SAS.DBC;
using SAS.help;
using SAS.EstayMobileService;

namespace SAS.Controllers
{
    public class CommentController : Controller
    {
        private HotelDBContent db = new HotelDBContent();

        //
        // GET: /Comment/

        public ActionResult QueryComment(string hotelId, string IsReply, string page, string startTime, string endTime)
        {
            int currentPage = 1; int.TryParse(page, out currentPage);
            if (currentPage < 0)
                return View("MyComment");
       //     Hotel_comment_info ment = new Hotel_comment_info();
            return View("MyComment", getdata(hotelId, IsReply, page, startTime, endTime));
        }
        public Hotel_comment_info getdata(string hotelId, string IsReply, string page, string startTime, string endTime)
        {
            int currentPage = 1; int.TryParse(page, out currentPage);

            //List<hotel_info> list = new HotelInfoHelp().getHotlList("");

            //string hotelIds = string.Empty;
            //if (string.IsNullOrEmpty(hotelId))
            //{
            //    foreach (var item in list)
            //    {
            //        if (string.IsNullOrEmpty(hotelId))
            //            hotelId += item.hotel_id;
            //        else
            //            hotelId += "," + item.hotel_id;
            //    }

            //}
            //MobileContractClient client = new MobileContractClient();
            //client.ClientCredentials.UserName.UserName = help.StringHelper.appSettings("WCFUserName");
            //client.ClientCredentials.UserName.Password = help.StringHelper.appSettings("WCFPassWord");
            DateTime s = new HotelInfoHelp().getStartDate(startTime); DateTime e = new HotelInfoHelp().getEndDate(endTime);
            //var coments = client.GetHotelCommentList(new CommentHParamsDTO() { Hotel_id = hotelId, IsReply = false, currentPage = currentPage, pageSize = HotelInfoHelp.pageSize, StartTime = s, EndTime = e });
            List<hotel_info> list; object allpage;
            Hotel_comment_info ment = new Hotel_comment_info();
            ment.CommnetList = ment.getComments(hotelId, IsReply, s, e, currentPage, out allpage, out list);
            ViewData["hotels"] = list;
            ViewBag.startTime = s.ToString("yyyy-MM-dd");
            ViewBag.endTime = e.ToString("yyyy-MM-dd");
            int t = string.IsNullOrEmpty(hotelId) ? 0 : Convert.ToInt32(hotelId);
            ViewBag.currentHotelId = string.IsNullOrEmpty(hotelId) ?0: Convert.ToInt32(hotelId);
            ViewBag.allPage = allpage;
            ViewBag.IsReply = IsReply;
            ViewBag.curentPage = currentPage == 0 ? currentPage + 1 : currentPage;
           
            
            return ment;
        }
    
        //
        // GET: /Comment/Details/5

        public ActionResult Details(int id = 0)
        {
            Hotel_comment_info hotel_comment_info = db.coments.Find(id);
            if (hotel_comment_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_comment_info);
        }
        /// <summary>
        /// 提交评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CommentSubmit(Hotel_comment_info comment,string page)
        {
          //  var  result= new help.RefrenceHelp().GetMobileContractClientTest().ReplyReview(new ReplyReviewRequest(){commentId=comment.commentId,answer=comment.content});
            ViewBag.sign = help.DBhelp.ExcuteTableBySQL(string.Format("update Hotel_comment_info set answer='{0}',isreply=1 where commentid in({1})", comment.content, comment.commentId)) > 0 ? 1 : 0;

            return View("MyComment", getdata(comment.hotel_id.ToString(), "", page, null, null));
        }
        //
        // GET: /Comment/Create

        public ActionResult Create()
        {
            return View();
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}