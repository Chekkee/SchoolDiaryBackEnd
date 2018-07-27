using SchoolDiary.Filters;
using SchoolDiary.Models;
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
    [RoutePrefix("school/grades")]
    public class GradeController : ApiController
    {
        private IGradesService gradesService;

        public GradeController(IGradesService gradesService)
        {
            this.gradesService = gradesService;
        }

        [Route("{id}")]
        [ResponseType(typeof(Grade))]
        [Authorize(Roles = "admins, teachers, students, parents")]
        public IHttpActionResult GetGrade(int id)
        {
            if(gradesService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(gradesService.GetByID(id));
        }

        [Route("{id}")]
        [ValidateModel]
        [ResponseType(typeof(Grade))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult PutGrade(int id, Grade grade)
        {
            if (gradesService.ExistsID(id) == false)
            {
                return NotFound();
            }

            return Ok(gradesService.PutGrade(id, grade));
        }

        [Route("{id}")]
        [Authorize(Roles = "admins, teachers")]
        [ResponseType(typeof(Grade))]
        public IHttpActionResult DeleteGrade(int id)
        {
            if(gradesService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(gradesService.DeleteByID(id));
        }
    }
}
