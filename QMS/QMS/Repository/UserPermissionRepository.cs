using QMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class UserPermissionRepository
    {
        public int AddUserPermission(UserPermission userpermission)
        {
            try
            {
                using (var db = new QAData())
                {
                    var dbPermission = db.UserPermissions.Where(p => p.Role_Id.Equals(userpermission.Role_Id) && p.SubModule_Id.Equals(userpermission.SubModule_Id)).FirstOrDefault();
                    if (dbPermission != null && dbPermission.Id > 0)
                    {
                        dbPermission.AccessLevel = userpermission.AccessLevel;
                        dbPermission.LastUpdatedBy = userpermission.LastUpdatedBy;
                        dbPermission.LastUpdatedOn = userpermission.LastUpdatedOn;
                        db.SaveChanges();
                        return dbPermission.Id;
                    }
                    else
                    {
                        db.UserPermissions.Add(userpermission);
                        db.SaveChanges();
                        return userpermission.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int UpdateUserPermission(UserPermission userpermission)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.Entry(userpermission).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return userpermission.Id;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public UserPermission GetUserPermissionById(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var userpermission = new UserPermission();
                    userpermission = db.UserPermissions.Find(id);
                    return userpermission;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<UserPermission> GetUserPermissionList()
        {
            try
            {
                using (var db = new QAData())
                {
                    var userpermissions = db.UserPermissions.ToList();
                    return userpermissions;
                }
            }
            catch (Exception ex)
            {
                return new List<UserPermission>();
            }
        }

        public bool DeleteUserPermission(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var userpermission = db.UserPermissions.Find(id);
                    db.UserPermissions.Remove(userpermission);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteUserPermission(int roleId, int moduleId)
        {
            try
            {
                using (var db = new QAData())
                {
                    var userpermission = db.UserPermissions.Where(p => p.Role_Id.Equals(roleId) && p.SubModule_Id.Equals(moduleId)).FirstOrDefault();
                    db.UserPermissions.Remove(userpermission);
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