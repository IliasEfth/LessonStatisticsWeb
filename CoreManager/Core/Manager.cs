using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatisticsWebCoreManager.ICore;
using StatisticsWebRepository.IRepository;
using StatisticsWebModels;
using StatisticsWebModels.DBTableModels.Converters.LessonConverter;
using StatisticsWebModels.ContextParsers;
using StatisticsWebModels.DBTableModels.Converters.LessonInterfaces;
using StatisticsWebCoreManager.CoreValidator.Validator;

namespace StatisticsWebCoreManager.Core
{
    public class Manager : IManager
    {
        private static IRepos database;
        public Manager(IRepos repos)
        {
            if (repos.Equals(null))
            {
                throw new NullReferenceException("Manager initialize cannot have null reference on repository");
            }
            database = repos;
        }

        public async Task<ApiResponse<PaggingRes<IList<LessonInfo>>>> getLessonsWithGrade(int? start, int? end, int? token, Page page)
        {
            ApiResponse<PaggingRes<IList<LessonInfo>>> response = new ApiResponse<PaggingRes<IList<LessonInfo>>>();
            List<string> errors = Validator.getLessonsWithGrade(start, end, token, page);
            if (errors.Count > 0)
            {
                response.Error = new Error() { Msg = errors };
            }
            else
            {
                PaggingRes<IList<LessonInfo>> result = null;
                if (start == null && end == null)
                {
                    result = await database.getAllLessonsWithGrade(token.GetValueOrDefault(), page);
                }
                else
                {
                    result = await database.getLessonsWithNoGradeOnSpecificPeriod(start.GetValueOrDefault(), end.GetValueOrDefault(), token.GetValueOrDefault(), page);
                }
                response.Item = result;
            }
            return response;
        }
        public async Task<ApiResponse<PaggingRes<IList<LessonInfo>>>> getLessonsWithNoGrade(int? start, int? end, int? token, Page page)
        {
            ApiResponse<PaggingRes<IList<LessonInfo>>> response = new ApiResponse<PaggingRes<IList<LessonInfo>>>();
            List<string> errors = Validator.getLessonsWithNoGrade(start, end, token, page);
            if (errors.Count > 0)
            {
                response.Error = new Error() { Msg = errors };
            }
            else
            {
                PaggingRes<IList<LessonInfo>> result = null;
                if (start == null && end == null)
                {
                    result = await database.getAllLessonsWithNoGrade(token.GetValueOrDefault(), page);
                }
                else
                {
                    result = await database.getLessonsWithNoGradeOnSpecificPeriod(start.GetValueOrDefault(), end.GetValueOrDefault(), token.GetValueOrDefault(), page);
                }
                response.Item = result;
            }
            return response;
        }
        public async Task<ApiResponse<IList<LessonInfo>>> postLesson(int? token, IList<UpdateLesson> lessonList)
        {
            ApiResponse<IList<LessonInfo>> response = new ApiResponse<IList<LessonInfo>>();
            List<string> errors = await Validator.postLesson(token, lessonList, database);
            if (errors.Count > 0)
            {
                response.Error = new Error() { Msg = errors };
            }
            else
            {
                await database.postLesson(token.GetValueOrDefault(), lessonList);
                var result = await database.getLessonByList(token.GetValueOrDefault(), lessonList);
                response.Item = result;
            }
            return response;
        }
        public async Task<ApiResponse<IList<LessonInfo>>> updateLesson(int? token, IList<UpdateLesson> lessonList)
        {
            ApiResponse<IList<LessonInfo>> response = new ApiResponse<IList<LessonInfo>>();
            List<string> errors = await Validator.updateLesson(token, lessonList, database);
            if (errors.Count > 0)
            {
                response.Error = new Error() { Msg = errors };
            }
            else
            {
                await database.updateLesson(token.GetValueOrDefault(), lessonList);
                var result = await database.getLessonByList(token.GetValueOrDefault(), lessonList);
                response.Item = result;
            }
            return response;
        }
    }
}
