using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Filters
{
    public class RoleBasedAccessFilter : ActionFilterAttribute
    {
        public string subModule { get; set; }
        public int accessLevel { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var subModules = HttpContext.Current.Session["subModules"] != null ? (List<QMS.Data.SubModule>)HttpContext.Current.Session["subModules"] : new List<QMS.Data.SubModule>();
            int subModuleId = subModules.Where(m => m.Name == subModule).FirstOrDefault().Id;
            bool hasAccess = QMS.Utilities.PermissionChecker.HasPermission(subModuleId, accessLevel);
            if (hasAccess)
            {
                //HttpContext.Current.Response.RedirectPermanent("~/User/InvalidURLRequest", true);
                //HttpContext.Current.Response.RedirectToRoute("InvalidURL");
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary
                {
                    {"controller","User" },
                    {"action","InvalidURLRequest" }
                });
            }
            base.OnActionExecuting(filterContext);
        }

    }
}