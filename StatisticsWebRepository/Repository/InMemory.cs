using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsWebModels;
using StatisticsWebRepository.IRepository;

namespace StatisticsWebRepository.Repository
{
    public class InMemory : IRepos
    {
        
        public IList<Lesson> getLessonsWithNoGradeOnSpecificPeriod(string start, string end , string token)
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
            throw new NotImplementedException();
        }
        public bool createIfNotExists() {
            throw new NotImplementedException();
        }

        public IList<Lesson> getLessonsWithGradeOnSpecificPeriod(string start, string end, string token)
        {
            throw new NotImplementedException();
        }

        public IList<Lesson> getAllLessonsWithGrade(string token)
        {
            throw new NotImplementedException();
        }
        public void Init()
        {
            throw new NotImplementedException();
        }
    }
}
