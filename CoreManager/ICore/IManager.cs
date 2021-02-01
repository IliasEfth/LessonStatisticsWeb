using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsWebModels;
using StatisticsWebModels.ContextParsers;
using StatisticsWebModels.DBTableModels.Converters.LessonConverter;
using StatisticsWebModels.DBTableModels.Converters.LessonInterfaces;
namespace StatisticsWebCoreManager.ICore
{
    public interface IManager
    {
        Task<ApiResponse<PaggingRes<IList<LessonInfo>>>> getLessonsWithGrade(int? start , int? end , int? token  , Page page);
        Task<ApiResponse<PaggingRes<IList<LessonInfo>>>> getLessonsWithNoGrade(int? start , int? end , int? token , Page page);
        Task<ApiResponse<IList<LessonInfo>>> updateLesson(int? token , IList<UpdateLesson> lessonList);
        Task<ApiResponse<IList<LessonInfo>>> postLesson(int? token, IList<UpdateLesson> lessonList);
    }
}
