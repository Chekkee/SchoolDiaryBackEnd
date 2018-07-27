using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SchoolDiary.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using SchoolDiary.Repositories;

namespace SchoolDiary.Infrastructure
{
    public class DataSeedClass : DropCreateDatabaseIfModelChanges<AuthContext>
    {

        protected override void Seed(AuthContext context)
        {
            //Ubacivanje rola

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            RoleManager.Create(new IdentityRole("admins"));
            RoleManager.Create(new IdentityRole("students"));
            RoleManager.Create(new IdentityRole("teachers"));
            RoleManager.Create(new IdentityRole("parents"));

            //Ubacivanje studenata

            string sPassword1 = "Cedapass1";
            string sPassword2 = "Radapass1";
            string sPassword3 = "Jekapass1";

            var UserManager = new UserManager<UserModel>(new UserStore<UserModel>(context));

            StudentModel s1 = new StudentModel()
            {
                FirstName = "Ceda",
                LastName = "Ribic",
                UserName = "Ceki",
                UserRole = EUserRole.ROLE_STUDENT,
                DateOfBirth = new DateTime(2010, 12, 21),
                Email = ""
            };

            StudentModel s2 = new StudentModel()
            {
                FirstName = "Rada",
                LastName = "Ribic",
                UserName = "Raki",
                UserRole = EUserRole.ROLE_STUDENT,
                DateOfBirth = new DateTime(2011, 3, 13),
                Email = ""
            };

            StudentModel s3 = new StudentModel()
            {
                FirstName = "Jelena",
                LastName = "Panic",
                UserName = "Jeka",
                UserRole = EUserRole.ROLE_STUDENT,
                DateOfBirth = new DateTime(2011, 8, 11),
                Email = ""
            };

            UserManager.Create(s1, sPassword1);
            UserManager.Create(s2, sPassword2);
            UserManager.Create(s3, sPassword3);

            UserManager.AddToRole(s1.Id, "students");
            UserManager.AddToRole(s2.Id, "students");
            UserManager.AddToRole(s3.Id, "students");

            //Ubacivanje teachera

            TeacherModel t1 = new TeacherModel()
            {
                FirstName = "Jovica",
                LastName = "Panic",
                UserName = "Jole",
                UserRole = EUserRole.ROLE_TEACHER,
                Email = "jole@gmail.com",
                PhoneNumber = "0603333333"
            };

            TeacherModel t2 = new TeacherModel()
            {
                FirstName = "Enver",
                LastName = "Alagic",
                UserName = "Alaga",
                UserRole = EUserRole.ROLE_TEACHER,
                Email = "alagic@gmail.com",
                PhoneNumber = "0602222222"
            };

            TeacherModel t3 = new TeacherModel()
            {
                FirstName = "Bogdan",
                LastName = "Ilic",
                UserName = "IlicB",
                UserRole = EUserRole.ROLE_TEACHER,
                Email = "bogdan@gmail.com",
                PhoneNumber = "0601111111"
            };

            string tPassword1 = "Jolepass1";
            string tPassword2 = "Alagapass1";
            string tPassword3 = "IlicBpass1";


            UserManager.Create(t1, tPassword1);
            UserManager.Create(t2, tPassword2);
            UserManager.Create(t3, tPassword3);

            UserManager.AddToRole(t1.Id, "teachers");
            UserManager.AddToRole(t2.Id, "teachers");
            UserManager.AddToRole(t3.Id, "teachers");

            //Ubacivanje parenta

            ParentModel p1 = new ParentModel()
            {
                FirstName = "Gordana",
                LastName = "Ribic",
                UserName = "Goga",
                Email = "goga@gmail.com",
                UserRole = EUserRole.ROLE_PARENT
            };

            ParentModel p2 = new ParentModel()
            {
                FirstName = "Snezana",
                LastName = "Panic",
                UserName = "Beba",
                Email = "beba@gmail.com",
                UserRole = EUserRole.ROLE_PARENT
            };

            string pPassword1 = "Gogapass1";
            string pPassword2 = "Bebapass1";

            UserManager.Create(p1, pPassword1);
            UserManager.Create(p2, pPassword2);

            UserManager.AddToRole(p1.Id, "parents");
            UserManager.AddToRole(p2.Id, "parents");

            //Ubacivanje admina

            AdminModel a = new AdminModel()
            {
                FirstName = "AdminName",
                LastName = "AdminLastName",
                UserName = "Adminator",
                UserRole = EUserRole.ROLE_ADMIN,
                Email = "admin@gmail.com"
            };

            string aPassword = "asd123";

            UserManager.Create(a, aPassword);
            UserManager.AddToRole(a.Id, "admins");

            //Ubacivanje subjecta

            List<SubjectModel> subjects = new List<SubjectModel>();

            SubjectModel sub11 = new SubjectModel()
            {
                SubjectName = "Srpski1", SubjectFond = 5, Year = EYear.First
            };

            SubjectModel sub12 = new SubjectModel()
            {
                SubjectName = "Matematika1", SubjectFond = 5, Year = EYear.First
            };

            SubjectModel sub13 = new SubjectModel()
            {
                SubjectName = "Svet oko nas1", SubjectFond = 2,
                Year = EYear.First
            };

            SubjectModel sub14 = new SubjectModel()
            {
                SubjectName = "Fizicko1", SubjectFond = 3,
                Year = EYear.First
            };

            SubjectModel sub15 = new SubjectModel()
            {
                SubjectName = "Muzicko1", SubjectFond = 1,
                Year = EYear.First
            };

            SubjectModel sub16 = new SubjectModel()
            {
                SubjectName = "Likovno1", SubjectFond = 1,
                Year = EYear.First
            };

            SubjectModel sub17 = new SubjectModel()
            {
                SubjectName = "Engleski1", SubjectFond = 2,
                Year = EYear.First
            };

            SubjectModel sub21 = new SubjectModel()
            {
                SubjectName = "Srpski2", SubjectFond = 5,
                Year = EYear.Second
            };

            SubjectModel sub22 = new SubjectModel()
            {
                SubjectName = "Matematika2", SubjectFond = 5,
                Year = EYear.Second
            };

            SubjectModel sub23 = new SubjectModel()
            {
                SubjectName = "Svet oko nas2", SubjectFond = 2,
                Year = EYear.Second
            };

            SubjectModel sub24 = new SubjectModel()
            {
                SubjectName = "Fizicko2", SubjectFond = 3,
                Year = EYear.Second
            };

            SubjectModel sub25 = new SubjectModel()
            {
                SubjectName = "Muzicko2", SubjectFond = 1,
                Year = EYear.Second
            };

            SubjectModel sub26 = new SubjectModel()
            {
                SubjectName = "Likovno2", SubjectFond = 1,
                Year = EYear.Second
            };

            SubjectModel sub27 = new SubjectModel()
            {
                SubjectName = "Engleski2", SubjectFond = 2, Year = EYear.Second
            };

            SubjectModel sub31 = new SubjectModel()
            {
                SubjectName = "Srpski3", SubjectFond = 5,
                Year = EYear.Third
            };

            SubjectModel sub32 = new SubjectModel()
            {
                SubjectName = "Matematika3", SubjectFond = 5,
                Year = EYear.Third
            };

            SubjectModel sub33 = new SubjectModel()
            {
                SubjectName = "Priroda i drustvo3", SubjectFond = 2,
                Year = EYear.Third
            };

            SubjectModel sub34 = new SubjectModel()
            {
                SubjectName = "Fizicko3", SubjectFond = 3,
                Year = EYear.Third
            };

            SubjectModel sub35 = new SubjectModel()
            {
                SubjectName = "Muzicko3", SubjectFond = 1,
                Year = EYear.Third
            };

            SubjectModel sub41 = new SubjectModel()
            {
                SubjectName = "Srpski4", SubjectFond = 5,
                Year = EYear.Fourth
            };

            SubjectModel sub42 = new SubjectModel()
            {
                SubjectName = "Matematika4", SubjectFond = 5,
                Year = EYear.Fourth
            };

            SubjectModel sub43 = new SubjectModel()
            {
                SubjectName = "Priroda i drustvo4", SubjectFond = 2,
                Year = EYear.Fourth
            };

            SubjectModel sub44 = new SubjectModel()
            {
                SubjectName = "Fizicko4", SubjectFond = 3,
                Year = EYear.Fourth
            };

            SubjectModel sub45 = new SubjectModel()
            {
                SubjectName = "Muzicko4", SubjectFond = 1,
                Year = EYear.Fourth
            };

            SubjectModel sub46 = new SubjectModel()
            {
                SubjectName = "Likovno4", SubjectFond = 2,
                Year = EYear.Fourth
            };

            SubjectModel sub47 = new SubjectModel()
            {
                SubjectName = "Engleski4", SubjectFond = 2,
                Year = EYear.Fourth
            };

            SubjectModel sub51 = new SubjectModel()
            {
                SubjectName = "Srpski5",
                SubjectFond = 5,
                Year = EYear.Fifrh
            };

            SubjectModel sub52 = new SubjectModel()
            {
                SubjectName = "Matematika5", SubjectFond = 5,
                Year = EYear.Fifrh
            };

            SubjectModel sub53 = new SubjectModel()
            {
                SubjectName = "Istorija5", SubjectFond = 1,
                Year = EYear.Fifrh
            };

            SubjectModel sub54 = new SubjectModel()
            {
                SubjectName = "Fizicko5", SubjectFond = 3,
                Year = EYear.Fifrh
            };

            SubjectModel sub55 = new SubjectModel()
            {
                SubjectName = "Muzicko5", SubjectFond = 1,
                Year = EYear.Fifrh
            };

            SubjectModel sub56 = new SubjectModel()
            {
                SubjectName = "Likovno5", SubjectFond = 2,
                Year = EYear.Fifrh
            };

            SubjectModel sub57 = new SubjectModel()
            {
                SubjectName = "Geografija5", SubjectFond = 2,
                Year = EYear.Fifrh
            };

            SubjectModel sub58 = new SubjectModel()
            {
                SubjectName = "Biologija5", SubjectFond = 2,
                Year = EYear.Fifrh
            };

            SubjectModel sub59 = new SubjectModel()
            {
                SubjectName = "Tehnicko5",
                SubjectFond = 2,
                Year = EYear.Fifrh
            };

            SubjectModel sub61 = new SubjectModel()
            {
                SubjectName = "Srpski6",
                SubjectFond = 5,
                Year = EYear.Sixth
            };

            SubjectModel sub62 = new SubjectModel()
            {
                SubjectName = "Matematika6",
                SubjectFond = 5,
                Year = EYear.Sixth
            };

            SubjectModel sub63 = new SubjectModel()
            {
                SubjectName = "Istorija6",
                SubjectFond = 1,
                Year = EYear.Sixth
            };

            SubjectModel sub64 = new SubjectModel()
            {
                SubjectName = "Fizicko6",
                SubjectFond = 3,
                Year = EYear.Sixth
            };

            SubjectModel sub65 = new SubjectModel()
            {
                SubjectName = "Muzicko6",
                SubjectFond = 1,
                Year = EYear.Sixth
            };

            SubjectModel sub66 = new SubjectModel()
            {
                SubjectName = "Likovno6",
                SubjectFond = 2,
                Year = EYear.Sixth
            };

            SubjectModel sub67 = new SubjectModel()
            {
                SubjectName = "Geografija6",
                SubjectFond = 2,
                Year = EYear.Sixth
            };

            SubjectModel sub68 = new SubjectModel()
            {
                SubjectName = "Biologija6",
                SubjectFond = 2,
                Year = EYear.Sixth
            };

            SubjectModel sub69 = new SubjectModel()
            {
                SubjectName = "Tehnicko6",
                SubjectFond = 2,
                Year = EYear.Sixth
            };

            SubjectModel sub610 = new SubjectModel()
            {
                SubjectName = "Fizika6",
                SubjectFond = 2,
                Year = EYear.Sixth
            };

            SubjectModel sub71 = new SubjectModel()
            {
                SubjectName = "Srpski7",
                SubjectFond = 5,
                Year = EYear.Seventh
            };

            SubjectModel sub72 = new SubjectModel()
            {
                SubjectName = "Matematika7",
                SubjectFond = 5,
                Year = EYear.Seventh
            };

            SubjectModel sub73 = new SubjectModel()
            {
                SubjectName = "Istorija7",
                SubjectFond = 1,
                Year = EYear.Seventh
            };

            SubjectModel sub74 = new SubjectModel()
            {
                SubjectName = "Fizicko7",
                SubjectFond = 3,
                Year = EYear.Seventh
            };

            SubjectModel sub75 = new SubjectModel()
            {
                SubjectName = "Muzicko7",
                SubjectFond = 1,
                Year = EYear.Seventh
            };

            SubjectModel sub76 = new SubjectModel()
            {
                SubjectName = "Likovno7",
                SubjectFond = 2,
                Year = EYear.Seventh
            };

            SubjectModel sub77 = new SubjectModel()
            {
                SubjectName = "Geografija7",
                SubjectFond = 2,
                Year = EYear.Seventh
            };

            SubjectModel sub78 = new SubjectModel()
            {
                SubjectName = "Biologija7",
                SubjectFond = 2,
                Year = EYear.Seventh
            };

            SubjectModel sub79 = new SubjectModel()
            {
                SubjectName = "Tehnicko7",
                SubjectFond = 2,
                Year = EYear.Seventh
            };

            SubjectModel sub710 = new SubjectModel()
            {
                SubjectName = "Fizika7",
                SubjectFond = 2,
                Year = EYear.Seventh
            };

            SubjectModel sub711 = new SubjectModel()
            {
                SubjectName = "Hemija7",
                SubjectFond = 2,
                Year = EYear.Seventh
            };

            SubjectModel sub81 = new SubjectModel()
            {
                SubjectName = "Srpski8",
                SubjectFond = 5,
                Year = EYear.Eight
            };

            SubjectModel sub82 = new SubjectModel()
            {
                SubjectName = "Matematika8",
                SubjectFond = 5,
                Year = EYear.Eight
            };

            SubjectModel sub83 = new SubjectModel()
            {
                SubjectName = "Istorija8",
                SubjectFond = 1,
                Year = EYear.Eight
            };

            SubjectModel sub84 = new SubjectModel()
            {
                SubjectName = "Fizicko8",
                SubjectFond = 3,
                Year = EYear.Eight
            };

            SubjectModel sub85 = new SubjectModel()
            {
                SubjectName = "Muzicko8",
                SubjectFond = 1,
                Year = EYear.Eight
            };

            SubjectModel sub86 = new SubjectModel()
            {
                SubjectName = "Likovno8",
                SubjectFond = 2,
                Year = EYear.Eight
            };

            SubjectModel sub87 = new SubjectModel()
            {
                SubjectName = "Geografija8",
                SubjectFond = 2,
                Year = EYear.Eight
            };

            SubjectModel sub88 = new SubjectModel()
            {
                SubjectName = "Biologija8",
                SubjectFond = 2,
                Year = EYear.Eight
            };

            SubjectModel sub89 = new SubjectModel()
            {
                SubjectName = "Tehnicko8",
                SubjectFond = 2,
                Year = EYear.Eight
            };

            SubjectModel sub810 = new SubjectModel()
            {
                SubjectName = "Fizika8",
                SubjectFond = 2,
                Year = EYear.Eight
            };

            SubjectModel sub811 = new SubjectModel()
            {
                SubjectName = "Hemija8",
                SubjectFond = 2,
                Year = EYear.Eight
            };

            subjects.Add(sub11);
            subjects.Add(sub12);
            subjects.Add(sub13);
            subjects.Add(sub14);
            subjects.Add(sub15);
            subjects.Add(sub16);
            subjects.Add(sub17);

            subjects.Add(sub21);
            subjects.Add(sub22);
            subjects.Add(sub23);
            subjects.Add(sub24);
            subjects.Add(sub25);
            subjects.Add(sub26);
            subjects.Add(sub27);

            subjects.Add(sub31);
            subjects.Add(sub32);
            subjects.Add(sub33);
            subjects.Add(sub34);
            subjects.Add(sub35);

            subjects.Add(sub41);
            subjects.Add(sub42);
            subjects.Add(sub43);
            subjects.Add(sub44);
            subjects.Add(sub45);
            subjects.Add(sub46);
            subjects.Add(sub47);

            subjects.Add(sub51);
            subjects.Add(sub52);
            subjects.Add(sub53);
            subjects.Add(sub54);
            subjects.Add(sub55);
            subjects.Add(sub56);
            subjects.Add(sub57);
            subjects.Add(sub58);
            subjects.Add(sub59);

            subjects.Add(sub61);
            subjects.Add(sub62);
            subjects.Add(sub63);
            subjects.Add(sub64);
            subjects.Add(sub65);
            subjects.Add(sub66);
            subjects.Add(sub67);
            subjects.Add(sub68);
            subjects.Add(sub69);
            subjects.Add(sub610);

            subjects.Add(sub71);
            subjects.Add(sub72);
            subjects.Add(sub73);
            subjects.Add(sub74);
            subjects.Add(sub75);
            subjects.Add(sub76);
            subjects.Add(sub77);
            subjects.Add(sub78);
            subjects.Add(sub79);
            subjects.Add(sub710);
            subjects.Add(sub711);

            subjects.Add(sub81);
            subjects.Add(sub82);
            subjects.Add(sub83);
            subjects.Add(sub84);
            subjects.Add(sub85);
            subjects.Add(sub86);
            subjects.Add(sub87);
            subjects.Add(sub88);
            subjects.Add(sub89);
            subjects.Add(sub810);
            subjects.Add(sub811);

            context.Subjects.AddRange(subjects);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}