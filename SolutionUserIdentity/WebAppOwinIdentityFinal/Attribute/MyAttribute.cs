using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebAppOwinIdentityFinal.Models.ViewModels;

namespace WebAppOwinIdentityFinal.Attribute
{
    public class MyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            var action = routeValues["action"].ToString();
            var controller = routeValues["controller"].ToString();

            
            //var RoleName =claimsIdentity.FindFirst(ClaimTypes.Role).Value;




            var user = (ClaimsPrincipal)filterContext.HttpContext.User;
            var claimsIdentity = user.Identity as ClaimsIdentity;
            var RoleName = claimsIdentity.FindFirst(ClaimTypes.Role).Value;
            if (!(RoleName =="Admin"))
            {
                filterContext.Result = new RedirectToRouteResult(
              new RouteValueDictionary(
                  new
                  {
                      controller = controller,
                      action = action
                  }));
                
            }
            else
            {
  filterContext.Result = new RedirectToRouteResult(
              new RouteValueDictionary(
                  new
                  {
                      controller = "Account",
                      action = "Login"
                  }));
            }
            return;
            //var userRole=user.Claims.Where(x=)
            //var userMenus = user.Claims.Where(x => x.Type == UserInformation.Menus).FirstOrDefault().Value;



            //UserData _UserData = new UserData();
            //var _FullName = _UserData.FullName;
            //var controller = routeValues["action"].ToString(); //RouteAttributes.GetRouteParam(routeValues, "controller");

            //var action = RouteAttributes.GetRouteParam(routeValues, "action");

            //var id = RouteAttributes.GetRouteParam(routeValues, "id");
        }


    }
}