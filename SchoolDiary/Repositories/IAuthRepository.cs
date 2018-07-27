using Microsoft.AspNet.Identity;
using SchoolDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Repositories
{
    public interface IAuthRepository : IDisposable
    {
        Task<IdentityResult> RegisterStudent(StudentModel student, string password);
        Task<IdentityResult> RegisterTeacher(TeacherModel teacher, string password);
        Task<IdentityResult> RegisterParent(ParentModel parent, string password);
        Task<IdentityResult> RegisterAdmin(AdminModel admin, string password);
        Task<UserModel> FindUser(string userName, string password);
        Task<IList<string>> FindRoles(string userId);
    }
}
