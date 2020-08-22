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
        IList<Lesson> getLessonsWithGrade(string start , string end , ref Error error);
        IList<Lesson> getLessonsWithNoGrade(string start , string end , ref Error error);
        bool updateLesson(IList<UpdateLesson> lessonList , ref Error error);       
    }
}
