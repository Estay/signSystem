using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAS.DBC;
using SAS.Models;
using SAS.EstayMobileService;

namespace SAS.Controllers
{
    public class OrderController : Controller
    {
        private HotelDBContent db = new HotelDBContent();
       
        //
        // GET: /Order/
        Order_info order =null;
        public ActionResult MyOrder()
        {

            return View(getOrder());
        }
        public Order_info getOrder()
        {
            order = new Order_info();
            order.OrderList = order.getOrderInfos(Order_info.orderStatus.newOrder);
            return order;
        }

        //查询新单
        public ActionResult CheckOrder()
        {
            order = new Order_info();
            order.OrderList = order.getOrderInfos(Order_info.orderStatus.confirmed);
            return View("CheckOrderinfo", order);
        }
        public ActionResult QueryOrder()
        {
            // ViewData["hotels"] = help.HotelInfoHelp.getHotlList("");
            Order_info order = QueryOrder(new Order_info() { o_check_in_date = DateTime.Now, o_check_out_date = DateTime.Now.AddDays(30) });
            order.o_check_in_date = Convert.ToDateTime("0001/1/1 0:00:00");

            order.o_check_out_date = Convert.ToDateTime("0001/1/1 0:00:00");
            return View("QueryOrderInfo", order);
        }
       
        //
        // POST: /Order/Create
        /// <summary>
        /// 订单确认提交
        /// </summary>
        /// <param name="order_info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult orderSubmit(Order_info order_info)
        {

            try
            {
                EstayMobileService.MobileContractClient _client = new MobileContractClient();
                _client.ClientCredentials.UserName.UserName = help.StringHelper.appSettings("WCFUserName");
                _client.ClientCredentials.UserName.Password = help.StringHelper.appSettings("WCFPassWord");
                object f = _client.confirmOrderStatus(order_info.order_id).ResultCode;
                if (order_info.o_state_id == 1)
                  order_info.room_id = _client.confirmOrderStatus(order_info.order_id).ResultCode == SAS.EstayMobileService.EnumResultCode.Success ? 1 : 0;
                else
                    order_info.room_id = _client.ChangeOrderState(new OrderAndStateChangeParamsDTO() { OrderID = order_info.order_id, NewOrderStateInfoID = order_info.o_state_id }).ResultCode == SAS.EstayMobileService.EnumResultCode.Success ? 1 : 0;

            }
            catch (Exception ex)
            {
                 //ViewBag.sign =
                help.DBhelp.log("确认订单失败"+ex.ToString()); order_info.room_id = 0;
            }
            ViewBag.sign = order_info.room_id;
          
       

           return View("MyOrder", getOrder());
        }
        /// <summary>
        /// 订单审核提交
        /// </summary>
        /// <param name="order_info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult orderCheckSubmit(Order_info order_info)
        {

            try
            {
                EstayMobileService.MobileContractClient _client = new MobileContractClient();
                _client.ClientCredentials.UserName.UserName = help.StringHelper.appSettings("WCFUserName");
                _client.ClientCredentials.UserName.Password = help.StringHelper.appSettings("WCFPassWord");
              // object  ffff= _client.ChangeOrderState(new OrderAndStateChangeParamsDTO() { OrderID = order_info.order_id, NewOrderStateInfoID = order_info.o_state_id }).ResultCode;
                 order_info.room_id = _client.ChangeOrderState(new OrderAndStateChangeParamsDTO() { OrderID = order_info.order_id, NewOrderStateInfoID = order_info.o_state_id }).ResultCode == SAS.EstayMobileService.EnumResultCode.Success ? 1 : 0;

            }
            catch (Exception ex)
            {
                //ViewBag.sign =
                help.DBhelp.log("确认订单失败" + ex.ToString()); order_info.room_id = 0;
            }
            ViewBag.sign = order_info.room_id;

            return RedirectToAction("CheckOrder","Order");
           // return View("CheckOrderInfo", getOrder());
        }

        /// <summary>
        /// 订单查询提交
        /// </summary>
        /// <param name="order_info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult orderQuerySubmit(Order_info order_info)
        {

            
            ViewBag.sign = order_info.room_id;
            Order_info order = QueryOrder(order_info);


            return View("QueryOrderInfo", order);
        }

        private static Order_info QueryOrder(Order_info order_info)
        {
            Order_info order = new Order_info();
            order_info.OrderList = order.getOrderInfos(order_info);
            //if( order.OrderList.Count>0)
            //    return order_info;
            //else
                return order_info;
        }
   
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}