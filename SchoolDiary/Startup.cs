using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using SchoolDiary.Infrastructure;
using SchoolDiary.Models;
using SchoolDiary.Providers;
using SchoolDiary.Repositories;
using SchoolDiary.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

[assembly: OwinStartup(typeof(SchoolDiary.Startup))]
namespace SchoolDiary
{

    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var container = SetupUnity();
            ConfigureOAuth(app, container);

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new UnityDependencyResolver(container)
            };

            //CamelCase!
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "dd-MM-yyyy hh:mm";

            IsoDateTimeConverter converter = new IsoDateTimeConverter
            {
                //DateTimeStyles = DateTimeStyles.AdjustToUniversal,
                DateTimeFormat = "yyyy-MM-dd" /*HH:mm*/
            };

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(converter);


            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app, UnityContainer container)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new SimpleAuthorizationServerProvider(container)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private UnityContainer SetupUnity()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<DbContext, AuthContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IAuthRepository, AuthRepository>();
            container.RegisterType<IGenericRepository<UserModel>, GenericRepository<UserModel>>();
            container.RegisterType<IGenericRepository<StudentModel>, GenericRepository<StudentModel>>();
            container.RegisterType<IGenericRepository<TeacherModel>, GenericRepository<TeacherModel>>();
            container.RegisterType<IGenericRepository<ParentModel>, GenericRepository<ParentModel>>();
            container.RegisterType<IGenericRepository<AdminModel>, GenericRepository<AdminModel>>();
            container.RegisterType<IGenericRepository<SubjectModel>, GenericRepository<SubjectModel>>();
            container.RegisterType<IGenericRepository<Grade>, GenericRepository<Grade>>();
            container.RegisterType<IGenericRepository<TeacherToSubject>, GenericRepository<TeacherToSubject>>();
            container.RegisterType<IGenericRepository<StudentToSubject>, GenericRepository<StudentToSubject>>();
            container.RegisterType<IGenericRepository<StudentToSubjectService>, GenericRepository<StudentToSubjectService>>();

            container.RegisterType<IAdminsService, AdminsService>();
            container.RegisterType<IStudentsService, StudentsService>();
            container.RegisterType<ITeachersService, TeachersService>();
            container.RegisterType<IParentsService, ParentsService>();
            container.RegisterType<IRegistrationsService, RegistrationsService>();
            container.RegisterType<ISubjectsService, SubjectsService>();
            container.RegisterType<ITeacherToSubjectsService, TeacherToSubjectsService>();
            container.RegisterType<IGradesService, GradesService>();

            return container;
        }
    }
}