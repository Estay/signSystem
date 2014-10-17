using System;
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
                            string f = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
                            string c = filterContext.HttpContext.Session["limit"].ToString().ToLower();
                            if (!f.ToLower().Contains("login".ToLower()))
                            {
                                if (!filterContext.HttpContext.Session["limit"].ToString().ToLower().Contains(f))
                                {
                                    filterContext.HttpContext.Session.Remove("uid");
                                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Login", Action = "MyLogin" }));//这里是跳转到Account下的LogOff,自己定义
                                }
                     
                            }
                        }
                   // }

                }
       
            }
        }
    }
}