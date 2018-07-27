using SchoolDiary.Filters;
using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using SchoolDiary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SchoolDiary.Controllers
{
    [RoutePrefix("school/teachers")]
    public class TeachersController : ApiController
    {
        private ITeachersService teachersService;
        private ITeacherToSubjectsService teacherToSubjectsService;
        private ISubjectsService subjectsService;
        private IStudentsService studentsService;

        public TeachersController(ITeachersService teachersService,
                                  ITeacherToSubjectsService teacherToSubjectsService,
                                  ISubjectsService subjectsService,
                                  IStudentsService studentsService)
        {
            this.teachersService = teachersService;
            this.teacherToSubjectsService = teacherToSubjectsService;
            this.subjectsService = subjectsService;
            this.studentsService = studentsService;
        }

        [Route("")]
        [ResponseType(typeof(IEnumerable<TeacherModel>))]
        public IHttpActionResult GetAllTeachers()
        {
            return Ok(teachersService.Get());
        }

        [Route("{id}")]
        [ResponseType(typeof(TeacherModel))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult GetTeacherByID(string id)
        {
            if (teachersService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(teachersService.GetByID(id));
        }

        [Route("byusername/{username}")]
        [ResponseType(typeof(TeacherModel))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetTeacherByUsername(string username)
        {
            if (teachersService.ExistsUsername(username) == false)
            {
                return NotFound();
            }
            return Ok(teachersService.GetByUsername(username));
        }

        [Route("{id}/remainingSubjects")]
        [Authorize(Roles = "admins")]
        [ResponseType(typeof(ICollection<SubjectDisplayForTeacherDTO>))]
        public IHttpActionResult GetRemainingSubjects(string id)
        {
            if(teachersService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(teachersService.GetRemainingSubjects(id));
        }

        [Route("{id}")]
        [ResponseType(typeof(TeacherInfoDTO))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutTeacher(string id, TeacherInfoDTO teacher)
        {
            if(teachersService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(teachersService.PutTeacher(id, teacher));
        }

        [Route("{id}/subject/{subjectId}")]
        [ResponseType(typeof(TeacherModel))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutSubjectToTeacher(string id, int subjectId)
        {
            if(teachersService.ExistsID(id) == false || subjectsService.ExistsID(subjectId) == false)
            {
                return NotFound();
            }
            if(teachersService.TeacherHasSubject(id, subjectId) == true)
            {
                return BadRequest("Teacher has this subject in his/hers subject list. Cannot add same subject twice.");
            }
            return Ok(teachersService.PutSubjectToTeacher(id, subjectId));
        }

        [Route("{id}/removesubject/{subjectId}")]
        [ResponseType(typeof(TeacherModel))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteSubjectFromTeacher(string id, int subjectId)
        {
            if(teachersService.ExistsID(id) == false || subjectsService.ExistsID(subjectId) == false)
            {
                return NotFound();
            }
            if(teachersService.TeacherHasSubject(id, subjectId) == false)
            {
                return BadRequest("Teacher does not have refered subject in his subject list");
            }
            return Ok(teachersService.DeleteSubjectFromTeacher(id, subjectId));
        }

        [Route("{id}/putStudent/{studentId}/subject/{subjectId}")]
        [ResponseType(typeof(TeacherModel))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutStudentToTeacher(string id, string studentId, int subjectId)
        {
            if(teachersService.ExistsID(id) == false || studentsService.ExistsID(studentId) == false || subjectsService.ExistsID(subjectId) == false)
            {
                return NotFound();
            }
            if(teachersService.TeacherHasSubject(id, subjectId) == false || studentsService.StudentHasSubject(studentId, subjectId) == false)
            {
                return NotFound();
            }
            return Ok(teachersService.PutStudentToTeacher(id, studentId, subjectId));
        }

        [Route("{id}/givegradetostudent/{studentId}/subject/{subjectId}")]
        [ResponseType(typeof(StudentModel))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult PostGradeToStudent(string id, string studentId, int subjectId, [FromBody] Grade grade)
        {
            if (teachersService.ExistsID(id) == false || studentsService.ExistsID(studentId) == false || subjectsService.ExistsID(subjectId) == false)
            {
                return NotFound();
            }
            if (teachersService.TeacherHasSubject(id, subjectId) == false || studentsService.StudentHasSubject(studentId, subjectId) == false)
            {
                return NotFound();
            }
            if (teachersService.TeacherHasStudent(id, studentId, subjectId) == false)
            {
                return NotFound();
            }
            return Ok(teachersService.PostGradeToStudent(id, studentId, subjectId, grade));
        }

        [Route("{id}")]
        [Authorize(Roles ="admins")]
        [ResponseType(typeof(TeacherModel))]
        public IHttpActionResult DeleteTeacher(string id)
        {
            if (teachersService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(teachersService.DeleteTeacher(id));
        }
    }
}
