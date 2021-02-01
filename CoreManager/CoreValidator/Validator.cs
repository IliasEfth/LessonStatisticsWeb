using StatisticsWebModels.ContextParsers;
using StatisticsWebModels.DBTableModels.Converters.LessonInterfaces;
using StatisticsWebRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsWebCoreManager.CoreValidator.Validator
{
    public class Validator
    {
        public static List<string> getLessonsWithGrade(int? start, int? end, int? token, Page page)
        {
            List<string> errors = new List<string>();
            if (!token.HasValue)
            {
                errors.Add("token is empty");
            }
            if ((start == null && end != null)
                || (start != null && end == null))
            {
                errors.Add("ivalid start-end range");
            }
            if (page == null)
            {
                errors.Add("page cannot be empty");
            }
            else
            {
                if (page.Items <= 0 || page.PageNo < 0)
                {
                    errors.Add("not valid page range");
                }
            }
            return errors;
        }
        public static List<string> getLessonsWithNoGrade(int? start, int? end, int? token, Page page)
        {
            List<string> errors = new List<string>();
            if (!token.HasValue)
            {
                errors.Add("token is empty");
            }
            if ((start == null && end != null)
                || (start != null && end == null))
            {
                errors.Add("ivalid start-end range");
            }
            if (page == null)
            {
                errors.Add("page cannot be empty");
            }
            else
            {
                if (page.Items <= 0 || page.PageNo < 0)
                {
                    errors.Add("not valid page range");
                }
            }
            return errors;
        }
        public static async Task<List<string>> postLesson(int? token, IList<UpdateLesson> lessonList, IRepos database)
        {
            List<string> errors = new List<string>();
            if (!token.HasValue)
            {
                errors.Add("token is empty");
            }
            if (lessonList == null)
            {
                errors.Add("list of lessons are empty");
            }
            else
            {
                {
                    //find duplicate lessons!!
                    var duplicates = lessonList.GroupBy(key => key.Id).SelectMany(group => group.Skip(1));
                    if (duplicates.Any())
                    {
                        errors.Add("list of lessons contains duplicates");
                    }
                }
                {
                    //find if lesson list is wrong
                    var list = await database.lessonListWithIdExists(lessonList, token.GetValueOrDefault());
                    if (list.Count < lessonList.Count)
                    {
                        errors.Add("lesson list contains wrong lessons data");
                    }
                }
                {
                    if (lessonList.Where(les => les.Grades.Graded < 0f || les.Grades.Graded > 10f).Any())
                    {
                        errors.Add("some grades are at wrong range");
                    }
                }
            }
            return errors;
        }
        public static async  Task<List<string>> updateLesson(int? token , IList<UpdateLesson> lessonList , IRepos database)
        {
            List<string> errors = new List<string>();
            if (!token.HasValue)
            {
                errors.Add("token is mempty");
            }
            if(lessonList == null)
            {
                errors.Add("list of lessons contains duplicates");
            }
            else
            {
                {
                    //find duplicate lessons!!
                    var duplicates = lessonList.GroupBy(key => key.Id).SelectMany(group => group.Skip(1));
                    if (duplicates.Any())
                    {
                        errors.Add("list of lessons contains duplicates");
                    }
                }
                {
                    //find if lesson list is wrong
                    var list = await database.lessonListWithIdExists(lessonList, token.GetValueOrDefault());
                    if (list.Count < lessonList.Count)
                    {
                        errors.Add("lesson list contains wrong lessons data");
                    }
                }
                {
                    if(lessonList.Where(les => les.Grades.Graded < 0f || les.Grades.Graded > 10f).Any())
                    {
                        errors.Add("some grades are at wrong range");
                    }
                }
            }
            return errors;
        }
    }
}
