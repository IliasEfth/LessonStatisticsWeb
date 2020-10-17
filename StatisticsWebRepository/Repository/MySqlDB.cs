using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StatisticsWebModels;
using StatisticsWebRepository.IRepository;
using StatisticsWebDBModel.DBRelation;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using StatisticsWebDBModel.DBHelpers;

namespace StatisticsWebRepository.Repository
{
    public class MySqlDB : IRepos
    {        
        public IList<Lesson> getLessonsWithGradeOnSpecificPeriod(string start, string end, string token)
        {
            throw new NotImplementedException();
        }
        public IList<Lesson> getAllLessonsWithGrade(string token)
        {
            throw new NotImplementedException();
        }
        public IList<Lesson> getLessonsWithNoGradeOnSpecificPeriod(string start, string end, string token)
        {
            throw new NotImplementedException();
        }
        public IList<Lesson> getAllLessonsWithNoGrade(string token)
        {
            throw new NotImplementedException();
        }
        public bool updateLesson(IList<Lesson> lessonList)
        {
            throw new NotImplementedException();
        }
        public bool lessonListWithIdExists(IList<Lesson> lessonList)
        {
            throw new NotImplementedException();
        }
        public string getMappedSemester(string semester)
        {
            throw new NotImplementedException();
        }
        public string[] getMappedSemestersRange(string start, string end)
        {
            throw new NotImplementedException();
        }
        public User userExists(User user)
        {
            return DBHelpers.execute<User>((context) => {
                User tmp;               
                tmp = context.Users.SingleOrDefault(u => u.Name.Equals(user.Name) && u.Password.Equals(user.Password));                
                return tmp;
            });
        }
        public bool createIfNotExists()
        {
            return DBHelpers.execute<bool>((context) =>
            {
                bool exists = false;
                if ((context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                {
                    exists = true;
                }
                else
                {
                    context.Database.EnsureCreated();
                    Init();
                }
                return exists;
            });          
        }
        public void Init()
        {
            DBHelpers.execute<bool>((context) => {
                context.Users.Add(new User { Name = "test", Password = "test" });
                context.SaveChanges();
                return true;
            });
        }        
    }
}
