using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsWebModels;
namespace StatisticsWebRepository.IRepository
{
    public interface IRepos
    {
        IList<Lesson> getLessonsWithGradeOnSpecificPeriod(string start,string end);
        IList<Lesson> getAllLessonsWithGrade();
        IList<Lesson> getLessonsWithNoGradeOnSpecificPeriod(string start, string end);
        IList<Lesson> getAllLessonsNoWithGrade();
        bool updateLesson(IList<UpdateLesson> lessonList);
        bool lessonListWithIdExists(IList<UpdateLesson> lessonList);
    }
}
