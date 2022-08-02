using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Utilities
{
    public static class PermissionChecker
    {
        public static bool HasPermission(int moduleId, int accessLevel)
        {
            var user = HttpContext.Current.Session["user"] != null ? (QMS.Data.SystemUser)HttpContext.Current.Session["user"] : new QMS.Data.SystemUser();
            var permissions = HttpContext.Current.Session["permissions"] != null ? (List<QMS.Data.UserPermission>)HttpContext.Current.Session["permissions"] : new List<QMS.Data.UserPermission>();
            var userRole = user.Role_Id;
            if (permissions.Any(p => p.Role_Id == userRole && p.SubModule_Id == moduleId && p.AccessLevel >= accessLevel))
                return true;
            else
                return false;
        }
    }
}