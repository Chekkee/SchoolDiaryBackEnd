using Microsoft.AspNet.Identity.EntityFramework;
using SchoolDiary.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Infrastructure
{
    public class AuthContext : IdentityDbContext<UserModel>
    {
        public AuthContext() : base("SchoolDiaryConnection")
        {
            Database.SetInitializer<AuthContext>(new DataSeedClass());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentModel>().ToTable("Students");
            modelBuilder.Entity<TeacherModel>().ToTable("Teachers");
            modelBuilder.Entity<ParentModel>().ToTable("Parents");
            modelBuilder.Entity<AdminModel>().ToTable("Admins");
            modelBuilder.Entity<SubjectModel>().ToTable("Subjects");
            modelBuilder.Entity<StudentToSubject>().ToTable("StudentAttendsSubject");
            modelBuilder.Entity<TeacherToSubject>().ToTable("TeacherTeachesSubject");
        }

        public DbSet<StudentModel> Students { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<ParentModel> Parents { get; set; }
        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<SubjectModel> Subjects { get; set; }
        public DbSet<TeacherToSubject> TeacherToSubjects { get; set; }
        public DbSet<StudentToSubject> StudentToSubjects { get; set; }
    }
}