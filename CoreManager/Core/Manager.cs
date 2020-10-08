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
            throw new NotImplementedException();
        }
        public IList<Lesson> getLessonsWithGrade(string start, string end, ref Error error)
        {
            throw new NotImplementedException();
        }
        public IList<Lesson> getLessonsWithNoGrade(string start, string end, ref Error error)
        {
            throw new NotImplementedException();
        }
        public bool updateLesson(IList<UpdateLesson> lessonList, ref Error error)
        {
            throw new NotImplementedException();
        }
    }
}
