using QMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class ModuleRepository
    {
        public List<SubModule> GetSubModulesList()
        {
            try
            {
                using (var db = new QAData())
                {
                    var subModules = db.SubModules.ToList();
                    return subModules;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}