using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsWebCoreManager.ICore;
using StatisticsWebRepository.IRepository;
using StatisticsWebRepository.Repository;
using StatisticsWebModels;
namespace StatisticsWebCoreManager.Core
{
    public class Manager : IManager
    {
        private static IRepos database;
        public Manager(IRepos repos)
        {            
        }
        public IList<Lesson> getLessonsWithGrade(string start,string end)
        {
            throw new NotImplementedException();
        }
        public IList<Lesson> getLessonsWithNoGrade(string start,string end)
        {
            throw new NotImplementedException();
        }
        public void updateLesson(IList<UpdateLesson> lessonList)
        {
            throw new NotImplementedException();
        }                    
        public void avgGrade()
        {
            throw new NotImplementedException();
        }      
    }
}
