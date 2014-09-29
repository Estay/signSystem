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

            List<hotel_info> list = new HotelInfoHelp().getHotlList("");
            ViewData["hotels"] = list;
            string hotelIds = string.Empty;
            if (string.IsNullOrEmpty(hotelId))
            {
                foreach (var item in list)
                {
                    if (string.IsNullOrEmpty(hotelId))
                        hotelId += item.hotel_id;
                    else
                        hotelId += "," + item.hotel_id;
                }

            }
            MobileContractClient client = new MobileContractClient();
            client.ClientCredentials.UserName.UserName = help.StringHelper.appSettings("WCFUserName");
            client.ClientCredentials.UserName.Password = help.StringHelper.appSettings("WCFPassWord");
            DateTime s = new HotelInfoHelp().getStartDate(startTime); DateTime e = new HotelInfoHelp().getEndDate(endTime);
            var coments = client.GetHotelCommentList(new CommentHParamsDTO() { Hotel_id = hotelId, IsReply = false, currentPage = currentPage, pageSize = HotelInfoHelp.pageSize, StartTime = s, EndTime = e });
            ViewBag.startTime = s.ToString("yyyy-MM-dd");
            ViewBag.endTime = e.ToString("yyyy-MM-dd");
            ViewBag.currentHotelId = hotelId;
            ViewBag.allPage = 100;
            ViewBag.curentPage = page;
            Hotel_comment_info ment = new Hotel_comment_info();
            return View("MyComment", ment);
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
        public ActionResult CommentSubmit(Hotel_comment_info comment)
        {
            var  result= new help.RefrenceHelp().GetMobileContractClientTest().ReplyReview(new ReplyReviewRequest(){commentId=comment.commentId,answer=comment.content});
            ViewBag.sign = result.Contains("") ? 1 : 0;
            return View("MyComment");
        }
        //
        // GET: /Comment/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Comment/Create

        [HttpPost]
        public ActionResult Create(Hotel_comment_info hotel_comment_info)
        {
            if (ModelState.IsValid)
            {
                db.coments.Add(hotel_comment_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotel_comment_info);
        }

        //
        // GET: /Comment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Hotel_comment_info hotel_comment_info = db.coments.Find(id);
            if (hotel_comment_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_comment_info);
        }

        //
        // POST: /Comment/Edit/5

        [HttpPost]
        public ActionResult Edit(Hotel_comment_info hotel_comment_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel_comment_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel_comment_info);
        }

        //
        // GET: /Comment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Hotel_comment_info hotel_comment_info = db.coments.Find(id);
            if (hotel_comment_info == null)
            {
                return HttpNotFound();
            }
            return View(hotel_comment_info);
        }

        //
        // POST: /Comment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel_comment_info hotel_comment_info = db.coments.Find(id);
            db.coments.Remove(hotel_comment_info);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}