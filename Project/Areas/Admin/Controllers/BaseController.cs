using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Common;
using System.Web.Routing;

namespace Project.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (TaiKhoanDangNhap)Session[HangSo.USER_SESSION];
            if (session == null) { filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "DangNhap", action = "Index", Area = "Admin" })); }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success") { TempData["AlertType"] = "alert-primary"; }
            else if (type == "error") { TempData["AlertType"] = "alert-danger"; }
            else if (type == "warning") { TempData["AlertType"] = "alert-warning"; }
        }
    }
}