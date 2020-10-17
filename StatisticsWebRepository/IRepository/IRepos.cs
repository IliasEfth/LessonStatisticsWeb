using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsWebModels;
namespace StatisticsWebRepository.IRepository
{
    public interface IRepos
    {
        IList<Lesson> getLessonsWithGradeOnSpecificPeriod(string start,string end , string token);
        IList<Lesson> getAllLessonsWithGrade(string token);
        IList<Lesson> getLessonsWithNoGradeOnSpecificPeriod(string start, string end , string token);
        IList<Lesson> getAllLessonsWithNoGrade(string token);
        bool updateLesson(IList<Lesson> lessonList);
        bool lessonListWithIdExists(IList<Lesson> lessonList);
        string getMappedSemester(string semester);
        string[] getMappedSemestersRange(string start, string end);
        User userExists(User user);
        bool createIfNotExists();
        void Init();
    }
}