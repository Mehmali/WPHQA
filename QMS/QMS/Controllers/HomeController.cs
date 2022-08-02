using QMS.Data;
using QMS.Filters;
using QMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Controllers
{
    [CustomAuthorizeFilter]
    public class HomeController : Controller
    {
        ModuleRepository _repoModule = new ModuleRepository();
        // GET: Home
        public ActionResult Index()
        {           
            return View();
        }
    }
}