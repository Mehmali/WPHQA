using QMS.Data;
using QMS.Data;
using QMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class UserRepository
    {
        public bool Validate(User user)
        {
            try
            {
                using (var db = new QAData())
                {
                    return db.SystemUsers.Any(u => u.Login_Id.Equals(user.UserName) && u.Password.Equals(user.Password) && u.IsActive != false);
                    
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public SystemUser GetSystemUser(User user)
        {
            try
            {
                using (var db = new QAData())
                {
                    return db.SystemUsers.Where(u => u.Login_Id.Equals(user.UserName) && u.Password.Equals(user.Password) && u.IsActive != false).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool ChangePassword(int loggedInUserId, int id, string password)
        {
            try
            {
                using (var db = new QAData())
                {
                    var user = db.SystemUsers.Find(id);
                    user.Password = password;
                    user.PasswordLastModifiedOn = DateTime.Now;
                    user.PasswordLastModifiedBy = loggedInUserId;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<UserPermission> GetPermissions(int roleId)
        {
            try
            {
                using (var db = new QAData())
                {
                    return db.UserPermissions.Where(u => u.Role_Id.Equals(roleId)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}