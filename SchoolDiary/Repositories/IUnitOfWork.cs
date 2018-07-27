using SchoolDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Repositories
{
    public interface IUnitOfWork
    {
        IAuthRepository AuthRepository { get; }
        IGenericRepository<UserModel> UsersRepository { get; }
        IGenericRepository<StudentModel> StudentsRepository { get; }
        IGenericRepository<TeacherModel> TeachersRepository { get; }
        IGenericRepository<ParentModel> ParentsRepository { get; }
        IGenericRepository<AdminModel> AdminsRepository { get; }
        IGenericRepository<SubjectModel> SubjectsRepository { get; set; }
        IGenericRepository<Grade> GradesRepository { get; set; }
        IGenericRepository<TeacherToSubject> TeacherToSubjectsRepository { get; set; }
        IGenericRepository<StudentToSubject> StudentToSubjectsRepository { get; set; }

        void Save();
    }
}
