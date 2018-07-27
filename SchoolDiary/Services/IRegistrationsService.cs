using Microsoft.AspNet.Identity;
using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services
{
    public interface IRegistrationsService
    {
        Task<IdentityResult> RegisterStudent(RegisterStudentDTO newStudent);
        Task<IdentityResult> RegisterParent(RegisterParentDTO newParent);
        Task<IdentityResult> RegisterTeacher(RegisterTeacherDTO newTeacher);
    }
}
