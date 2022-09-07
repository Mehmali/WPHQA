namespace QMS.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QAData : DbContext
    {
        public QAData()
            : base("name=QAData")
        {
        }

        public virtual DbSet<ComplaintMainType> ComplaintMainTypes { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<JobTitle> JobTitles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SystemUser> SystemUsers { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<SubModule> SubModules { get; set; }
        public virtual DbSet<ComplaintSubType> ComplaintSubTypes { get; set; }

        public virtual DbSet<Complaint> Complaints { get; set; }

        public virtual DbSet<ComplaintDefect> ComplaintDefects { get; set; }

        public virtual DbSet<ComplaintDocument> ComplaintDocuments { get; set; }

        public virtual DbSet<FaultType> FaultTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(e => e.Dept_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Dept_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.CompId)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Emp_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Emp_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CompId)
                .IsFixedLength();

            modelBuilder.Entity<JobTitle>()
                .Property(e => e.JobTitle_Code)
                .IsUnicode(false);

            modelBuilder.Entity<JobTitle>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<JobTitle>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<JobTitle>()
                .Property(e => e.CompId)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .Property(e => e.Role_Name)
                .IsUnicode(false);

            modelBuilder.Entity<SystemUser>()
                .Property(e => e.Login_Id)
                .IsUnicode(false);

            modelBuilder.Entity<SubModule>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
