using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SchoolDiary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SchoolDiary.Repositories
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private DbContext _ctx;

        private UserManager<UserModel> _userManager;

        public AuthRepository(DbContext context)
        {
            _ctx = context;
            _userManager = new UserManager<UserModel>(new UserStore<UserModel>(_ctx));
        }


        public async Task<IdentityResult> RegisterStudent(StudentModel student, string password)
        {
            var result = await _userManager.CreateAsync(student, password);
            _userManager.AddToRole(student.Id, "students");
            
            return result;
        }

        public async Task<IdentityResult> RegisterTeacher(TeacherModel teacher, string password)
        {
            var result = await _userManager.CreateAsync(teacher, password);
            _userManager.AddToRole(teacher.Id, "teachers");
            return result;
        }

        public async Task<IdentityResult> RegisterParent(ParentModel parent, string password)
        {
            var result = await _userManager.CreateAsync(parent, password);
            _userManager.AddToRole(parent.Id, "parents");
            return result;
        }

        public async Task<IdentityResult> RegisterAdmin(AdminModel admin, string password)
        {
            var result = await _userManager.CreateAsync(admin, password);
            _userManager.AddToRole(admin.Id, password);
            return result;
        }

        public async Task<UserModel> FindUser(string userName, string password)
        {
            UserModel user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public async Task<IList<string>> FindRoles(string userId)
        {
            return await _userManager.GetRolesAsync(userId);
        }

        public void Dispose()
        {
            if (_userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
        }
    }
}