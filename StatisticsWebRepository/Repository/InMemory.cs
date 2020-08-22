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
        public IList<Lesson> getLessonsWithGradeOnSpecificPeriod(string start, string end)
        {
            throw new NotImplementedException();
        }
        public IList<Lesson> getAllLessonsWithGrade()
        {
            throw new NotImplementedException();
        }
        public IList<Lesson> getLessonsWithNoGradeOnSpecificPeriod(string start, string end)
        {
            throw new NotImplementedException();        
        }
        public IList<Lesson> getAllLessonsNoWithGrade()
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

    }
}
