using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsWebModels;
namespace StatisticsWebCoreManager.ICore
{
    public interface IManager
    {
        IList<Lesson> getLessonsWithGrade(string start,string end);
        IList<Lesson> getLessonsWithNoGrade(string start, string end);
        void updateLesson(IList<UpdateLesson> lessonList);
        void avgGrade();
    }
}
