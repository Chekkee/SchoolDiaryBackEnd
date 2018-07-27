using SchoolDiary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity.Attributes;

namespace SchoolDiary.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        [Dependency]
        public IAuthRepository AuthRepository { get; set; }

        [Dependency]
        public IGenericRepository<UserModel> UsersRepository { get; set; }

        [Dependency]
        public IGenericRepository<StudentModel> StudentsRepository { get; set; }

        [Dependency]
        public IGenericRepository<TeacherModel> TeachersRepository { get; set; }

        [Dependency]
        public IGenericRepository<ParentModel> ParentsRepository { get; set; }

        [Dependency]
        public IGenericRepository<AdminModel> AdminsRepository { get; set; }
        
        [Dependency]
        public IGenericRepository<SubjectModel> SubjectsRepository { get; set; }

        [Dependency]
        public IGenericRepository<Grade> GradesRepository { get; set; }

        [Dependency]
        public IGenericRepository<TeacherToSubject> TeacherToSubjectsRepository { get; set; }

        [Dependency]
        public IGenericRepository<StudentToSubject> StudentToSubjectsRepository { get; set; }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}