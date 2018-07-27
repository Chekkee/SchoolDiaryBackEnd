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
    [RoutePrefix("school/parents")]
    public class ParentsController : ApiController
    {
        private IParentsService parentsService;

        public ParentsController(IParentsService parentsService)
        {
            this.parentsService = parentsService;
        }

        [Route("")]
        [ResponseType(typeof(IEnumerable<ParentInfoDTO>))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetAllParents()
        {
            return Ok(parentsService.Get());
        }

        [Route("{id}")]
        [ResponseType(typeof(ParentInfoDTO))]
        [Authorize(Roles = "admins, teachers, students, parents")]
        public IHttpActionResult GetByID(string id)
        {
            if (parentsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(parentsService.GetByID(id));
        }

        [Route("byusername/{username}")]
        [ResponseType(typeof(ParentInfoDTO))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetByUsername(string username)
        {
            if (parentsService.ExistsUsername(username) == false)
            {
                return NotFound();
            }
            return Ok(parentsService.GetByUsername(username));
        }

        [Route("{id}/remainingStudents")]
        [ResponseType(typeof(ICollection<StudentInfoDTO>))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetRemainingStudents(string id)
        {
            if(parentsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(parentsService.GetRemainingStudents(id));
        }

        [Route("{id}")]
        [ResponseType(typeof(ParentInfoDTO))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutParent(string id, ParentInfoDTO parent)
        {
            if(parentsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(parentsService.PutParent(id, parent));
        }

        [Route("{id}")]
        [ResponseType(typeof(ParentInfoDTO))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteParent(string id)
        {
            if (parentsService.ExistsID(id) == false)
            {
                return NotFound();
            }
            return Ok(parentsService.DeleteParent(id));
        }

    }
}
