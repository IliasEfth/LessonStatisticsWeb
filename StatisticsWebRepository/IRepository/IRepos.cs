using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsWebModels;
using StatisticsWebModels.DBTableModels.Converters.LessonConverter;
using StatisticsWebModels.ContextParsers;
using StatisticsWebModels.DBTableModels.Converters.LessonInterfaces;
namespace StatisticsWebRepository.IRepository
{
    public interface IRepos
    {
        Task<PaggingRes<IList<LessonInfo>>> getLessonsWithGradeOnSpecificPeriod(int start, int end, int token, Page page);
        Task<PaggingRes<IList<LessonInfo>>> getAllLessonsWithGrade(int token, Page page);
        Task<PaggingRes<IList<LessonInfo>>> getLessonsWithNoGradeOnSpecificPeriod(int start, int end, int token, Page page);
        Task<PaggingRes<IList<LessonInfo>>> getAllLessonsWithNoGrade(int token, Page page);
        Task<IList<LessonInfo>> getLessonByList(int token, IList<UpdateLesson> lessons);
        Task<int> updateLesson(int token, IList<UpdateLesson> lessonList);
        Task<int> postLesson(int token, IList<UpdateLesson> lessonList);
        Task<IList<UpdateLesson>> lessonListWithIdExists(IList<UpdateLesson> lessonList , int token);
        Task<StatisticsWebModels.ContextParsers.MappedSemester> getMappedSemestersRange(int token);//TODO: extend app
        Task<User> userExists(User user);
        bool createIfNotExists();
        void Init();
    }
}