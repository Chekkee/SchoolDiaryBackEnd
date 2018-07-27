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
    [RoutePrefix("school/students")]
    public class StudentsController : ApiController
    {
        private IStudentsService studentsService;
        private IParentsService parentsService;
        private ISubjectsService subjectsService;

        public StudentsController(IStudentsService studentsService, IParentsService ps, ISubjectsService subservice)
        {
            this.studentsService = studentsService;
            this.parentsService = ps;
            this.subjectsService = subservice;
        }

        [Route("")]
        [Authorize(Roles = "admins, teachers")]
        [ResponseType(typeof(ICollection<StudentInfoDTO>))]
        public IHttpActionResult GetAllStudents()
        {
            return Ok(studentsService.Get());
        }

        [Route("{id}")]
        [ResponseType(typeof(StudentInfoDTO))]
        [Authorize(Roles = "admins, teachers, parents, students")]
        public IHttpActionResult GetStudentByID(string id)
        {
            if (studentsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(studentsService.GetByID(id));
        }

        [Route("byusername/{username}")]
        [ResponseType(typeof(StudentInfoDTO))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetStudentByUsername(string username)
        {
            if (studentsService.IsInDB(username) == false)
            {
                return NotFound();
            }
            return Ok(studentsService.GetByUsername(username));
        }

        [Route("{id}/allsubjectsandgrades")]
        [ResponseType(typeof(ICollection<SubjectWithGradesDTO>))]
        [Authorize(Roles = "admins, teachers, students, parents")]
        public IHttpActionResult GetStudentsSubjectsWithGrades(string id)
        {
            if (studentsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(studentsService.GetAllSubjectsWithGrades(id));
        }

        
        [Route("{id}/remainingsubjects")]
        [ResponseType(typeof(ICollection<SubjectWithGradesDTO>))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetSubjectThatStudentDoesNotHave(string id)
        {
            if(studentsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(studentsService.GetSubjectsThatStudenDoesNotHave(id));
        }

        [Route("{id}/allteachers")]
        [ResponseType(typeof(IEnumerable<TeachersAndSubjectsOfStudent>))]
        public IHttpActionResult GetStudentsTeachersWithSubjects(string id)
        {
            if (studentsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(studentsService.GetStudentsTeachersWithSubjects(id));
        }

        [Route("{id}")]
        [ResponseType(typeof(StudentInfoDTO))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutStudent(string id, StudentInfoDTO student)
        {
            if (studentsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(studentsService.PutStudent(id, student));
        }

        [Route("{id}/parent/{parentId}")]
        [ResponseType(typeof(StudentInfoDTO))]
        [Authorize(Roles ="admins")]
        public IHttpActionResult PutParentToStudent(string id, string parentId)
        {
            if(studentsService.ExistsID(id) == false || parentsService.ExistsID(parentId) == false)
            {
                return NotFound();
            }
            if(studentsService.StudentHasParent(id, parentId) == true)
            {
                return BadRequest("Student allready has this parent in his parent list");
            }
            return Ok(studentsService.PutParentToStudent(id, parentId));
        }

        [Route("{id}/subject/{subjectId}")]
        [ResponseType(typeof(StudentInfoDTO))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutSubjectToStudent(string id, int subjectId)
        {
            if(studentsService.ExistsID(id) == false || subjectsService.ExistsID(subjectId) == false)
            {
                return NotFound();
            }

            if(studentsService.StudentHasSubject(id, subjectId) == true)
            {
                return BadRequest("Student allready has subject in his/hers subject list. Same subject cannot be added two times");
            }

            return Ok(studentsService.PutSubjectToStudent(id, subjectId));
        }

        [Route("{id}")]
        [Authorize(Roles ="admins")]
        [ResponseType(typeof(StudentModel))]
        public IHttpActionResult DeleteStudent(string id)
        {
            if (studentsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(studentsService.DeleteStudent(id));
        }

        [Route("{id}/removesubject/{subjectId}")]
        [ResponseType(typeof(StudentModel))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteSubjectFromStudent(string id, int subjectId)
        {
            if (studentsService.ExistsID(id) == false || subjectsService.ExistsID(subjectId) == false)
            {
                return NotFound();
            }

            if(studentsService.StudentHasSubject(id, subjectId) == false)
            {
                return BadRequest("Student does not have that subject in his/hers subject list");
            }

            return Ok(studentsService.DeleteSubjectFromStudent(id, subjectId));
        }

        [Route("{id}/removeparent/{parentId}")]
        [ResponseType(typeof(StudentInfoDTO))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteParentFromStudent(string id, string parentId)
        {
            if (studentsService.ExistsID(id) == false || parentsService.ExistsID(parentId) == false)
            {
                return NotFound();
            }
            if (studentsService.StudentHasParent(id, parentId) == false)
            {
                return BadRequest("Student does not have that parent in his parent list");
            }
            return Ok(studentsService.DeleteParentFromStudent(id, parentId));
        }
    }
}
