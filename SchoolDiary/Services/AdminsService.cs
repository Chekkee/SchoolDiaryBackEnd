using NLog;
using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using SchoolDiary.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace SchoolDiary.Services
{
    public class AdminsService : IAdminsService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IUnitOfWork db;
        private IStudentsService studentsService;

        public AdminsService(IUnitOfWork db, IStudentsService studentsService)
        {
            this.db = db;
            this.studentsService = studentsService;
        }

        public bool ExistsID(string id)
        {
            if(db.AdminsRepository.GetByID(id) == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<AdminModel> GetAll()
        {
            return db.AdminsRepository.Get();
        }

        public AdminModel GetByID(string id)
        {
            return db.AdminsRepository.GetByID(id);
        }

        public ICollection<string> GetLogs()
        {
            StreamReader sr;
            string fileLocation = $"D:/Kurs/School - B&F/1. Back - SchoolDiary/SchoolDiary/logs/app-log.txt";
            List<string> logs = new List<string>();

            try
            {
                sr = new StreamReader(fileLocation);
                while (true)
                {
                    string line = sr.ReadLine();
                    logs.Add(line);
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                }
                logs.Reverse();
                return logs;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public StudentInfoDTO PostGradeToStudent(string studentId, int subjectId, Grade grade)
        {
            StudentModel studentFromDB = db.StudentsRepository.GetByID(studentId);
            SubjectModel subjectFromDB = db.SubjectsRepository.GetByID(subjectId);

            StudentToSubject sts = db.StudentToSubjectsRepository.Get().FirstOrDefault(x => x.Student == studentFromDB && x.Subject == subjectFromDB);

            sts.Grades.Add(grade);
            db.StudentToSubjectsRepository.Update(sts);
            db.Save();

            SendEmailToParents(studentId, subjectId, grade);

            logger.Info($"Grade {grade.GradeValue} was posted to student {studentFromDB.FirstName} {studentFromDB.LastName} for subject {subjectFromDB.SubjectName}");
            return studentsService.MakeStudentInfoDTO(studentFromDB);
        }

        private void SendEmailToParents(string studentId, int subjectId, Grade grade)
        {
            StudentModel studentFromDB = db.StudentsRepository.GetByID(studentId);

            if (studentFromDB.Parents.Count() > 0)
            {
                SubjectModel subjectFromDB = db.SubjectsRepository.GetByID(subjectId);

                string subject = String.Format($"Student {studentFromDB.FirstName} {studentFromDB.LastName} just got new grade in school");

                string body = $"Dear parents of {studentFromDB.FirstName} {studentFromDB.LastName}," + Environment.NewLine + Environment.NewLine +
                    $"today, at {grade.GradeDate} your child just received {grade.GradeValue} from subject {subjectFromDB.SubjectName}";
                string FromMail = ConfigurationManager.AppSettings["from"];


                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);

                mail.From = new MailAddress(FromMail);

                foreach (ParentModel parent in studentFromDB.Parents)
                {
                    mail.To.Add(parent.Email);
                }

                mail.Subject = subject;
                mail.Body = body;
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["smtpPort"]);
                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["password"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["smtpSsl"]);
                SmtpServer.Send(mail);

                logger.Info($"Student {studentFromDB.FirstName} {studentFromDB.LastName} received new grade, so proper email was sent to all parents associated with this student." +
                    $"New student grade was: {grade.GradeValue}, subject was: {subjectFromDB.SubjectName}");
            }
        }

    }
}