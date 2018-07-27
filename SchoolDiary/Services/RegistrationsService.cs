using Microsoft.AspNet.Identity;
using NLog;
using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using SchoolDiary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SchoolDiary.Services
{
    public class RegistrationsService : IRegistrationsService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public RegistrationsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public async Task<IdentityResult> RegisterStudent(RegisterStudentDTO newStudent)
        {
            StudentModel student = new StudentModel
            {
                UserRole = EUserRole.ROLE_STUDENT,
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                UserName = newStudent.UserName,
                DateOfBirth = newStudent.DateOfBirth,
                Email = ""
            };

            SetYearAndSubjects(student, student.DateOfBirth);

            logger.Info($"New student created. Student information - Name: {student.FirstName}, LastName: {student.LastName}, Username: {student.UserName}, DateOfBirth: {student.DateOfBirth}" +
                $"student was placed into year {student.Year} according to birthday");

            return await db.AuthRepository.RegisterStudent(student, newStudent.Password);
        }

        public async Task<IdentityResult> RegisterParent(RegisterParentDTO newParent)
        {
            ParentModel parent = new ParentModel
            {
                UserRole = EUserRole.ROLE_PARENT,
                FirstName = newParent.FirstName,
                LastName = newParent.LastName,
                UserName = newParent.UserName,
                Email = newParent.Email,
                PhoneNumber = newParent.MobilePhone
            };

            logger.Info($"New parent created. Parent information - Name: {parent.FirstName}, LastName: {parent.LastName}, Username: {parent.UserName}, DateOfBirth: {parent.Email}");

            return await db.AuthRepository.RegisterParent(parent, newParent.Password);
        }

        public async Task<IdentityResult> RegisterTeacher(RegisterTeacherDTO newTeacher)
        {
            TeacherModel teacher = new TeacherModel
            {
                UserRole = EUserRole.ROLE_TEACHER,
                FirstName = newTeacher.FirstName,
                LastName = newTeacher.LastName,
                UserName = newTeacher.UserName,
                Email = newTeacher.Email,
                PhoneNumber = newTeacher.MobilePhone
            };

            logger.Info($"New teacher created. Teacher information - Name: {teacher.FirstName}, LastName: {teacher.LastName}, Username: {teacher.UserName}, DateOfBirth: {teacher.Email}");

            return await db.AuthRepository.RegisterTeacher(teacher, newTeacher.Password);
        }

        private void SetYearAndSubjects(StudentModel student, DateTime DateOfBirth)
        {
            if(DateOfBirth >= new DateTime(2011, 1, 1) && DateOfBirth <= new DateTime(2011, 12, 31))
            {
                student.Year = EYear.First;
                List<SubjectModel> OkSubjects = db.SubjectsRepository.Get(x => x.Year == EYear.First).ToList();
                foreach (SubjectModel subject in OkSubjects)
                {
                    StudentToSubject sts = new StudentToSubject()
                    {
                        Student = student,
                        Subject = subject,
                    };
                    student.StudentToSubjects.Add(sts);
                    subject.StudentToSubjects.Add(sts);
                }
            }
            else if (DateOfBirth >= new DateTime(2010, 1, 1) && DateOfBirth <= new DateTime(2010, 12, 31))
            {
                student.Year = EYear.Second;
                foreach (SubjectModel subject in db.SubjectsRepository.Get(x => x.Year == EYear.Second))
                {
                    StudentToSubject sts = new StudentToSubject()
                    {
                        Student = student,
                        Subject = subject,
                    };
                    student.StudentToSubjects.Add(sts);
                    subject.StudentToSubjects.Add(sts);
                }
            }
            else if (DateOfBirth >= new DateTime(2009, 1, 1) && DateOfBirth <= new DateTime(2009, 12, 31))
            {
                student.Year = EYear.Third;
                foreach (SubjectModel subject in db.SubjectsRepository.Get(x => x.Year == EYear.Third))
                {
                    StudentToSubject sts = new StudentToSubject()
                    {
                        Student = student,
                        Subject = subject,
                    };
                    student.StudentToSubjects.Add(sts);
                    subject.StudentToSubjects.Add(sts);
                }
            }
            else if (DateOfBirth >= new DateTime(2008, 1, 1) && DateOfBirth <= new DateTime(2008, 12, 31))
            {
                student.Year = EYear.Fourth;
                foreach (SubjectModel subject in db.SubjectsRepository.Get(x => x.Year == EYear.Fourth))
                {
                    StudentToSubject sts = new StudentToSubject()
                    {
                        Student = student,
                        Subject = subject,
                    };
                    student.StudentToSubjects.Add(sts);
                    subject.StudentToSubjects.Add(sts);
                }
            }
            else if (DateOfBirth >= new DateTime(2007, 1, 1) && DateOfBirth <= new DateTime(2007, 12, 31))
            {
                student.Year = EYear.Fifrh;
                foreach (SubjectModel subject in db.SubjectsRepository.Get(x => x.Year == EYear.Fifrh))
                {
                    StudentToSubject sts = new StudentToSubject()
                    {
                        Student = student,
                        Subject = subject,
                    };
                    student.StudentToSubjects.Add(sts);
                    subject.StudentToSubjects.Add(sts);
                }
            }
            else if (DateOfBirth >= new DateTime(2006, 1, 1) && DateOfBirth <= new DateTime(2006, 12, 31))
            {
                student.Year = EYear.Sixth;
                foreach (SubjectModel subject in db.SubjectsRepository.Get(x => x.Year == EYear.Sixth))
                {
                    StudentToSubject sts = new StudentToSubject()
                    {
                        Student = student,
                        Subject = subject,
                    };
                    student.StudentToSubjects.Add(sts);
                    subject.StudentToSubjects.Add(sts);
                }
            }
            else if (DateOfBirth >= new DateTime(2005, 1, 1) && DateOfBirth <= new DateTime(2005, 12, 31))
            {
                student.Year = EYear.Seventh;
                foreach (SubjectModel subject in db.SubjectsRepository.Get(x => x.Year == EYear.Seventh))
                {
                    StudentToSubject sts = new StudentToSubject()
                    {
                        Student = student,
                        Subject = subject,
                    };
                    student.StudentToSubjects.Add(sts);
                    subject.StudentToSubjects.Add(sts);
                }
            }
            else if (DateOfBirth >= new DateTime(2004, 1, 1) && DateOfBirth <= new DateTime(2004, 12, 31))
            {
                student.Year = EYear.Eight;
                foreach (SubjectModel subject in db.SubjectsRepository.Get(x => x.Year == EYear.Eight))
                {
                    StudentToSubject sts = new StudentToSubject()
                    {
                        Student = student,
                        Subject = subject,
                    };
                    student.StudentToSubjects.Add(sts);
                    subject.StudentToSubjects.Add(sts);
                }
            }
        }

    }
}