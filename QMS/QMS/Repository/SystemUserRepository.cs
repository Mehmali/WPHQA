using QMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class SystemUserRepository
    {
        public int AddSystemUser(SystemUser sysUser)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.SystemUsers.Add(sysUser);
                    db.SaveChanges();
                    return sysUser.Id;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int UpdateSystemUser(SystemUser sysUser)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.Entry(sysUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return sysUser.Id;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public SystemUser GetSystemUserId(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var sysUser = new SystemUser();
                    sysUser = db.SystemUsers.Where(u => u.Id.Equals(id)).FirstOrDefault();
                    return sysUser;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SystemUser> GetSystemUserList()
        {
            try
            {
                using (var db = new QAData())
                {
                    var sysUsers = db.SystemUsers.ToList();
                    return sysUsers;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteSystemUser(int id, int userId)
        {
            try
            {
                using (var db = new QAData())
                {
                    var sysUser = db.SystemUsers.Find(id);
                    sysUser.IsActive = false;
                    sysUser.DeActivatedOn = DateTime.Now;
                    sysUser.DeActivatedBy = userId;
                    //db.SystemUsers.Remove(sysUser);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}