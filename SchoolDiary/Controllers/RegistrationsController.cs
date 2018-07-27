using SchoolDiary.Filters;
using SchoolDiary.Models.DTOs;
using SchoolDiary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchoolDiary.Controllers
{
    [RoutePrefix("school/registrations")]
    public class RegistrationsController : ApiController
    {
        private IRegistrationsService registrationsService;

        public RegistrationsController(IRegistrationsService registrationsService)
        {
            this.registrationsService = registrationsService;
        }
        
        [Route("students", Name = "PostStudent")]
        [Authorize(Roles = "admins")]
        [ValidateModel]
        public async Task<IHttpActionResult> PostRegisterStudent(RegisterStudentDTO student)
        {
            try
            {
                var result = await registrationsService.RegisterStudent(student);

                if (result == null)
                {
                    return BadRequest(ModelState);
                }

                return Created("PostStudent", student);
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("parents", Name ="PostParent")]
        [ValidateModel]
        [Authorize(Roles = "admins")]
        public async Task<IHttpActionResult> PostRegisterParent(RegisterParentDTO parent)
        {
            try
            {
                var result = await registrationsService.RegisterParent(parent);

                if (result == null)
                {
                    return BadRequest(ModelState);
                }

                return Created("PostParent", parent);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("teachers", Name = "PostTeacher")]
        [ValidateModel]
        [Authorize(Roles = "admins")]
        public async Task<IHttpActionResult> PostRegisterTeacher(RegisterTeacherDTO teacher)
        {
            try
            {
                var result = await registrationsService.RegisterTeacher(teacher);

                if (result == null)
                {
                    return BadRequest(ModelState);
                }

                return Created("PostTeacher", teacher);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
