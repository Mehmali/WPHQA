using QMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class FaultTypeRepository
    {
        public int AddFaultType(FaultType faultType)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.FaultTypes.Add(faultType);
                    db.SaveChanges();
                    return faultType.Id;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }

        }

        public int UpdateFaultType(FaultType faultType)
        {
            try
            {
                using(var db=new QAData())
                {
                    db.Entry(faultType).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return faultType.Id;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public FaultType GetFaultTypeById(int id)
        {
            try
            {
                using(var db=new QAData())
                {
                    var faultType = new FaultType();
                    faultType = db.FaultTypes.Find(id);
                    return faultType;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<FaultType> GetFaultTypeList()
        {
            try
            {
                using (var db = new QAData())
                {
                    var faultTypes = db.FaultTypes.ToList();
                    return faultTypes;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool DeleteFaultType(int id)
        {
            try
            {
                using(var db=new QAData())
                {
                    var faultType = db.FaultTypes.Find(id);
                    db.FaultTypes.Remove(faultType);
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<FaultType> GetFaultTypeListBySubTypeId(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var faultTypes = db.FaultTypes.Where(x => x.SubType_Id.Equals(id)).ToList();
                    return faultTypes;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}