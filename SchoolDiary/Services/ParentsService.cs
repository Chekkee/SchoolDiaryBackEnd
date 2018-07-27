using NLog;
using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using SchoolDiary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Services
{
    public class ParentsService : IParentsService
    {
        private IUnitOfWork db;
        private IStudentsService studentsService;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ParentsService(IUnitOfWork db, IStudentsService studentsService)
        {
            this.db = db;
            this.studentsService = studentsService;
        }

        public IEnumerable<ParentInfoDTO> Get()
        {
            IEnumerable<ParentModel> AllParents = db.ParentsRepository.Get();
            ICollection<ParentInfoDTO> parentDTOs = new List<ParentInfoDTO>();
            foreach (ParentModel parent in AllParents)
            {
                parentDTOs.Add(MakeParentInfoDTO(parent));
            }

            return parentDTOs;
        }

        public bool ExistsID(string Id)
        {
            string id = Id.ToString();
            if(db.ParentsRepository.GetByID(id) == null)
            {
                return false;
            }
            return true;
        }

        public bool ExistsUsername(string username)
        {
            if(db.ParentsRepository.Get().FirstOrDefault(x=>x.UserName == username) == null)
            {
                return false;
            }
            return true;
        }

        public ParentInfoDTO GetByID(string id)
        {
            return MakeParentInfoDTO(db.ParentsRepository.GetByID(id));
        }

        public ParentInfoDTO GetByUsername(string username)
        {
            return MakeParentInfoDTO(db.ParentsRepository.Get().FirstOrDefault(x => x.UserName == username));
        }

        public ICollection<StudentInfoDTO> GetRemainingStudents(string id)
        {
            ParentModel parentFromDB = db.ParentsRepository.GetByID(id);
            IEnumerable<StudentModel> remainingStudents = db.StudentsRepository.Get().Except(parentFromDB.Students);

            ICollection<StudentInfoDTO> remainingDTOs = new List<StudentInfoDTO>();

            foreach (StudentModel student in remainingStudents)
            {
                remainingDTOs.Add(new StudentInfoDTO(student.Id, student.FirstName, student.LastName, student.UserName, student.Year, student.DateOfBirth));
            }

            return remainingDTOs;
        }

        public ParentInfoDTO PutParent(string id, ParentInfoDTO parent)
        {
            ParentModel parentFromDB = db.ParentsRepository.GetByID(id);
            parentFromDB.FirstName = parent.FirstName;
            parentFromDB.LastName = parent.LastName;
            parentFromDB.UserName = parent.UserName;
            parentFromDB.Email = parent.Email;
            parentFromDB.PhoneNumber = parent.MobilePhone;

            db.ParentsRepository.Update(parentFromDB);
            db.Save();

            logger.Info($"Informations about parent with id: {parentFromDB.Id} were changed");

            return MakeParentInfoDTO(parentFromDB);
        }

        public ParentModel DeleteParent(string id)
        {
            ParentModel ParentFromDB = db.ParentsRepository.GetByID(id);
            db.ParentsRepository.Delete(ParentFromDB);
            db.Save();

            logger.Warn($"Parent with id: {ParentFromDB.Id} was deleted");
            
            return ParentFromDB;
        }

        public ParentInfoDTO MakeParentInfoDTO(ParentModel parentModel)
        {
            ParentInfoDTO parent = new ParentInfoDTO(parentModel.Id, parentModel.FirstName, parentModel.LastName, parentModel.UserName, parentModel.Email, parentModel.PhoneNumber);

            foreach(StudentModel sm in parentModel.Students)
            {
                parent.StudentsWithSubjectsAndGrades.Add(studentsService.MakeStudentInfoDTO(sm));
            }

            return parent;
        }
    }
}