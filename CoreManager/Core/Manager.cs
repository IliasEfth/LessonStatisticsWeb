using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsWebCoreManager.ICore;
using StatisticsWebRepository.IRepository;
using StatisticsWebRepository.Repository;
using StatisticsWebModels;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace StatisticsWebCoreManager.Core
{
    public class Manager : IManager
    {
        private static IRepos database;
        public Manager(IRepos repos)
        {
            if (repos == null)
            {
                throw new NullReferenceException();
            }
            database = repos;
        }
        public IList<Lesson> getLessonsWithGrade(string start, string end, ref Error error)
        {
            List<string> errorMessages = new List<string>();
            IList<Lesson> lessons = null;
            if (error == null)
            {
                error = new Error();
            }
            if (start == null && end == null)
            {
                try
                {
                    lessons = database.getAllLessonsWithGrade("");
                }
                catch (Exception)
                {
                    Console.WriteLine("Database crashes while trying to retrieve data\n\tManager method: getLessonsWithGrade\n\tDatabase method: getAllLessonsWithGrade");
                }
            }
            else if (start == null || end == null)
            {
                errorMessages.Add("Start or end variables are wrong");
            }
            else
            {                
                string[] tmpStartEndStorage = null;
                try
                {
                    tmpStartEndStorage = database.getMappedSemestersRange(start, end);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database crashes while trying to retrieve data\n\tManager method: getLessonsWithGrade\n\tDatabase method: getMappedSemestersRange");
                }
                if (tmpStartEndStorage != null)
                {
                    int tmpStart = 0;
                    int tmpEnd = 0;
                    if (!(int.TryParse(tmpStartEndStorage[0], out tmpStart) && int.TryParse(tmpStartEndStorage[1], out tmpEnd)))
                    {
                        errorMessages.Add("The selected semester range is not mapped");
                    }
                    else if (tmpStart > tmpEnd)
                    {
                        errorMessages.Add("Start variable was greater than end variable");
                    }
                    else
                    {
                        try
                        {
                            lessons = database.getLessonsWithGradeOnSpecificPeriod(start, end , "");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Database crashes while trying to retrieve data\n\tManager method: getLessonsWithGrade\n\tDatabase method: getLessonsWithGradeOnSpecificPeriod");
                        }
                    }
                }
            }
            error.Msg = errorMessages.ToArray();
            return lessons;
        }
        public IList<Lesson> getLessonsWithNoGrade(string start, string end, ref Error error)
        {
            List<string> errorMessages = new List<string>();
            IList<Lesson> lessons = null;
            if (error == null)
            {
                error = new Error();
            }
            if (start == null && end == null)
            {
                try
                {
                    lessons = database.getAllLessonsWithNoGrade("");
                }
                catch (Exception)
                {
                    Console.WriteLine("Database crashes while trying to retrieve data\n\tManager method: getLessonsWithNoGrade\n\tDatabase method: getAllLessonsWithNoGrade");
                }
            }
            else if (start == null || end == null)
            {
                errorMessages.Add("Start or end variables are wrong");
            }
            else
            {
                string[] tmpStartEndStorage = null;
                try
                {
                    tmpStartEndStorage = database.getMappedSemestersRange(start, end);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database crashes while trying to retrieve data\n\tManager method: getLessonsWithNoGrade\n\tDatabase method: getMappedSemestersRange");
                }
                if (tmpStartEndStorage != null)
                {
                    int tmpStart = 0;
                    int tmpEnd = 0;
                    if (!(int.TryParse(tmpStartEndStorage[0], out tmpStart) && int.TryParse(tmpStartEndStorage[1], out tmpEnd)))
                    {
                        errorMessages.Add("The selected semester range is not mapped");
                    }
                    else if (tmpStart > tmpEnd)
                    {
                        errorMessages.Add("Start variable was greater than end variable");
                    }
                    else
                    {
                        try
                        {
                            lessons = database.getLessonsWithNoGradeOnSpecificPeriod(start, end , "");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Database crashes while trying to retrieve data\n\tManager method: getLessonsWithNoGrade\n\tDatabase method: getLessonsWithNoGradeOnSpecificPeriod");
                        }
                    }
                }
            }
            error.Msg = errorMessages.ToArray();
            return lessons;
        }
        public bool updateLesson(IList<UpdateLesson> lessonList, ref Error error)
        {
            throw new NotImplementedException();
        }
    }
}
