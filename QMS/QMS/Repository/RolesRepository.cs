using QMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class RolesRepository
    {
        public List<Role> GetRolesList()
        {
            try
            {
                using (var db = new QAData())
                {
                    var roles = db.Roles.ToList();
                    return roles;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int AddRole(Role role)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.Roles.Add(role);
                    db.SaveChanges();
                    return role.Id;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int UpdateRole(Role role)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return role.Id;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Role GetRoleById(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var role = new Role();
                    role = db.Roles.Find(id);
                    return role;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public bool DeleteRole(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var role = db.Roles.Find(id);
                    db.Roles.Remove(role);
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