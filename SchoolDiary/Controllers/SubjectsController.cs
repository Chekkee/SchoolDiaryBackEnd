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
    [RoutePrefix("school/subjects")]
    public class SubjectsController : ApiController
    {
        private ISubjectsService subjectsService;

        public SubjectsController(ISubjectsService subjectsService)
        {
            this.subjectsService = subjectsService;
        }

        [Route("")]
        [Authorize(Roles = "admins")]
        [ResponseType(typeof(IEnumerable<SubjectModel>))]
        public IHttpActionResult GetAllSubject()
        {
            return Ok(subjectsService.GetAllSubjects());
        }

        [Route("{id}")]
        [ResponseType(typeof(SubjectModel))]
        [Authorize(Roles = "admins, teachers, students, parents")]
        public IHttpActionResult GetByID(int id)
        {
            if (subjectsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(subjectsService.GetByID(id));
        }

        [Route("{id}/possibleteachers")]
        [ResponseType(typeof(IEnumerable<TeacherInfoDTO>))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetPossibleTeachers(int id)
        {
            if (subjectsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(subjectsService.GetPossibleTeachers(id));
        }

        [Route("")]
        [ValidateModel]
        [ResponseType(typeof(SubjectModel))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PostSubject(SubjectModel subject)
        {
            return Ok(subjectsService.PostSubject(subject));
        }

        [Route("{id}")]
        [ValidateModel]
        [ResponseType(typeof(SubjectModel))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutSubject(int id, SubjectModel subject)
        {
            if (subjectsService.ExistsID(id) == false){
                return NotFound();
            }
            return Ok(subjectsService.PutSubject(id, subject));
        }

        [Route("{id}")]
        [ResponseType(typeof(SubjectModel))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteByID(int id)
        {
            if(subjectsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(subjectsService.DeleteByID(id));
        }

    }
}
