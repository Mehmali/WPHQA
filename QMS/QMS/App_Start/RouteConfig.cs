using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            //    routes.MapRoute(
            //    name: "Download",
            //    url: "{controller}/{action}/{FileName}",
            //    defaults: new { controller = "ComplaintDocument", action = "DownloadFile", FileName = UrlParameter.Optional }
            //);

            routes.MapRoute(
            name: "Default1",
            url: "Department/GetDepartmentsByComplaintType/{param}",
            defaults: new { controller = "Department", action = "GetDepartmentsByComplaintType", param = UrlParameter.Optional }

        );

            //routes.MapRoute(
            //    name: "InvalidURL",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "User", action = "InvalidURLRequest", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
           name: "Default",
           url: "{controller}/{action}/{id}",
           defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
       );
        }
    }
}
