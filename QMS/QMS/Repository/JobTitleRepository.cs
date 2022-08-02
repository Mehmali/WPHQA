using QMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMS.Repository
{
    public class JobTitleRepository
    {
        public int AddJobTitle(JobTitle jobTitle)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.JobTitles.Add(jobTitle);
                    db.SaveChanges();
                    return jobTitle.Id;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateJobTitle(JobTitle jobTitle)
        {
            try
            {
                using (var db = new QAData())
                {
                    db.Entry(jobTitle).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return jobTitle.Id;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public JobTitle GetJobTitleById(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var jobTitles = db.JobTitles.Find(id);
                    return jobTitles;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<JobTitle> GetJobTitleList()
        {
            try
            {
                using (var db = new QAData())
                {
                    var jobTitles = db.JobTitles.ToList();
                    return jobTitles;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JobTitle DeleteobTitleById(int id)
        {
            try
            {
                using (var db = new QAData())
                {
                    var jobTitles = db.JobTitles.Find(id);
                    db.JobTitles.Remove(jobTitles);
                    db.SaveChanges();
                    return jobTitles;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}