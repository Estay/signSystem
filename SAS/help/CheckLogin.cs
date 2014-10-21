﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace SAS.help
{
    public class CheckLogin : ActionFilterAttribute
    {
        public bool IsCheck { get; set; } 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            if (filterContext.HttpContext.Session != null)
            {
                base.OnActionExecuting(filterContext); 
                if (IsCheck)    
                {
                    //if (filterContext.HttpContext.Session.IsNewSession)
                    //{
                   
                        var sessionCookie = filterContext.HttpContext.Request.Headers["Cookie"];
                        if (filterContext.HttpContext.Session["uid"] == null)
                        {

                            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Login", Action = "MyLogin" }));//这里是跳转到Account下的LogOff,自己定义
                        }
                        else
                        {
                            //控制器名称
                            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
                            //控制器根限
                            string limit = filterContext.HttpContext.Session["limit"].ToString().ToLower();
                            if (controllerName == "all")
                               return;

                            if (!controllerName.ToLower().Contains("login".ToLower()))
                            {
                                //如果控制器名称不在limit中
                                if (!filterContext.HttpContext.Session["limit"].ToString().ToLower().Contains(controllerName))
                                {
                                    filterContext.HttpContext.Session.Remove("uid");
                                    filterContext.HttpContext.Session.Remove("userName");
                                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Login", Action = "MyLogin" }));//这里是跳转到Account下的LogOff,自己定义
                                }
                                else
                                {
                                    
                                    if (filterContext.ActionParameters.Count > 0)
                                    {
                                        //for (int i = 0; i < filterContext.ActionParameters.Count; i++)
                                        //{
                                            Dictionary<string, object> dic = filterContext.ActionParameters as Dictionary<string, object>;
                                            foreach (var a in dic)
                                            {
                                                if ((a.Key.Contains("id") || (a.Key.Contains("Id") || a.Key.Contains("hotelId") || a.Key.Contains("hotelId")) && a.Value != null))
                                                {
                                                    string limitHotelId = string.Empty;
                                                    if (filterContext.HttpContext.Session["limitHotelId"] != null)
                                                        limitHotelId = filterContext.HttpContext.Session["limitHotelId"].ToString();
                                                    if(string.IsNullOrEmpty(limitHotelId))
                                                        return;
                                                    if (!limitHotelId.Contains(a.Value.ToString()))
                                                    {
                                                        filterContext.HttpContext.Session.Remove("uid");
                                                          filterContext.HttpContext.Session.Remove("userName");
                                                      
                                                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Login", Action = "MyLogin" }));//这里是跳转到Account下的LogOff,自己定义

                                                    }

                                                }
                                                else
                                                    break;
                                            }
                                        //}
                                    }
                                }
                               
                     
                            }
                        }
                   // }

                }
       
            }
        }
        public void ReMoveSession(ActionExecutingContext filterContext)
        {
                       
        }
    }
}