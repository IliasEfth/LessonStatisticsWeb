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
        public bool updateLesson(IList<UpdateLesson> lessonList)
        {
            throw new NotImplementedException();
        }
        public bool lessonListWithIdExists(IList<UpdateLesson> lessonList)
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
        public bool userExists(User user)
        {
            return execute<bool>((context) => {
                bool exists = false;                
                exists = context.Users.SingleOrDefault(u => u.Name.Equals(user.Name) && u.Password.Equals(user.Password)) != null ? true : false;                
                return exists;
            });
        }
        public bool createIfNotExists()
        {
            return execute<bool>((context) =>
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
            execute<bool>((context) => {
                context.Users.Add(new User { Name = "test", Password = "test" });
                context.SaveChanges();
                return true;
            });
        }
        private T execute<T>(Func<StatisticsWebDB, T> lambda)
        {
            T value;
            using (var context = new StatisticsWebDB())
            {
               value = lambda(context);
            }
            return value;
        }
    }
}
