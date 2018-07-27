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
    [RoutePrefix("school/admins")]
    public class AdminController : ApiController
    {
        private IAdminsService adminsService;
        private IStudentsService studentsService;
        private ISubjectsService subjectsService;

        public AdminController(IAdminsService adminsService, IStudentsService studServ, ISubjectsService subServ)
        {
            this.adminsService = adminsService;
            this.studentsService = studServ;
            this.subjectsService = subServ;
        }

        [Route("")]
        [ResponseType(typeof(IEnumerable<AdminModel>))]
        public IHttpActionResult GetAllAdmins()
        {
            return Ok(adminsService.GetAll());
        }

        [Route("{id}")]
        [ResponseType(typeof(AdminModel))]
        public IHttpActionResult GetByID(string id)
        {
            if (adminsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(adminsService.GetByID(id));
        }

        [Route("getLogs")]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetLogs()
        {
            return Ok(adminsService.GetLogs());
        }

        [Route("student/{studentId}/subject/{subjectId}/givegrade/", Name = "AdminGaveGrade")]
        [ResponseType(typeof(StudentInfoDTO))]
        [Authorize(Roles ="admins, teachers")]
        public IHttpActionResult PostGradeByAdmin(string studentId, int subjectId, [FromBody] Grade grade)
        {
            if(studentsService.ExistsID(studentId) == false || subjectsService.ExistsID(subjectId)== false)
            {
                return NotFound();
            }

            if(studentsService.StudentHasSubject(studentId, subjectId) == false)
            {
                return NotFound();
            }

            return Created("AdminGaveGrade", adminsService.PostGradeToStudent(studentId, subjectId, grade));
        }
    }
}
