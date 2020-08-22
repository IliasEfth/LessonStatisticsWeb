using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsWebModels;
using StatisticsWebRepository.IRepository;
namespace StatisticsWebRepository.Repository
{
    public class InMemory : IRepos
    {
        private static List<UserTable> userPool = new List<UserTable>()
        {
            new UserTable
            {
                Id = "1",
                user = new User
                {
                    UserName = "TestingPerposesUser1",
                    Password = "TestingPerposesUser1"
                },
                NickName = "Takis Testakis",
                Role = "student",
                DepId ="1"
            }
            ,new UserTable
            {
                Id ="2",
                user = new User
                {
                    UserName = "TestingPerposesUser2",
                    Password = "TestingPerposesUser2"
                },
                NickName = "Ilias Testerakos",
                Role = "student",
                DepId= "1"
            }

        };
        private class UserTable
        {
            public string Id { get; set; }
            public User user { get; set; }
            public string NickName { get; set; }
            public string Role { get; set; }
            public string DepId { get; set; }
            public string Token { get; set; }
        }
        private static List<DepartementTable> depPool = new List<DepartementTable>()
        {
            new DepartementTable
            {
                Id ="1",
                Name = "Mixanikoi Pliroforikis"
            }
        };
        private class DepartementTable
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
        private static List<LessonTable> LessonsPool = new List<LessonTable>()
        {
            new LessonTable
            {
                Id = "1" ,
                DepId  = "1",
                Name = "Databases1",
                ECTS = 5,
                Semester = "1",
            },
            new LessonTable
            {
                Id = "2" ,
                DepId  = "1",
                Name = "Operating Systems",
                ECTS = 5,
                Semester = "2",
            },
        };
        private class LessonTable
        {
            public string Id { get; set; }
            public string DepId { get; set; }
            public string Name { get; set; }
            public float ECTS { get; set; }
            public string Semester { get; set; }
        }
        private static List<userLessonsTable> userLessonsPool = new List<userLessonsTable>()
        {
            new userLessonsTable
            {
                userId = "1",
                LessId = "1",
                Grade = 5,
                FakeId = "1"
            }
        };
        private class userLessonsTable
        {
            public string LessId { get; set; }
            public string userId { get; set; }
            public float Grade { get; set; }
            public string FakeId { get; set; }
        }
        private static List<MappedSemesterByDepartementTable> mappedSemesterPool = new List<MappedSemesterByDepartementTable>()
        {
            new MappedSemesterByDepartementTable
            {
                DepId= "1",
                Semester = "1",
                SystemSemester = "1"
            },
            new MappedSemesterByDepartementTable
            {
                DepId= "1",
                Semester = "2",
                SystemSemester = "2"
            },
        };
        private class MappedSemesterByDepartementTable
        {
            public string DepId { get; set; }
            public string SystemSemester { get; set; }
            public string Semester { get; set; }
        }
        public IList<Lesson> getLessonsWithGradeOnSpecificPeriod(string start, string end , string token)
        {
            throw new NotImplementedException();
        }
        public IList<Lesson> getAllLessonsWithGrade(string token)
        {
            List<Lesson> lessons = new List<Lesson>();
            string usersId = null;
            usersId = userPool.Find(user => user.Token.Equals(token)).Id;
            (userLessonsPool.FindAll(lessonRow => lessonRow.userId.Equals(usersId)))
                .ForEach(lessonRow =>
                {                    
                    Lesson tmpLesson = new Lesson();
                    tmpLesson.Grade = lessonRow.Grade;
                    tmpLesson.Id = lessonRow.LessId;
                    lessons.Add(tmpLesson);
                });
            lessons.ForEach(lesson =>
            {
                if (LessonsPool.Exists(LessonsPool => LessonsPool.Id.Equals(lesson.Id)))
                {
                    var row = LessonsPool.Find(lessonPool => lessonPool.Id.Equals(lesson.Id));
                    lesson.Name = row.Name;
                    lesson.Semester = row.Semester;
                    lesson.ECTS = row.ECTS;
                }
            });
            return lessons;
        }
        public IList<Lesson> getLessonsWithNoGradeOnSpecificPeriod(string start, string end , string token)
        {
            throw new NotImplementedException();
        }
        public IList<Lesson> getAllLessonsWithNoGrade(string token)
        {
            throw new NotImplementedException();
        }
        public bool updateLesson(IList<UpdateLesson> lessonList)
        {
            throw new NotImplementedException();
        }
        public bool lessonListWithIdExists(IList<UpdateLesson> lessonList)
        {
            throw new NotImplementedException();
        }
        public string getMappedSemester(string semester)
        {
            throw new NotImplementedException();
        }
        public string[] getMappedSemestersRange(string start, string end)
        {
            string[] mapped = new string[2];
            if (mappedSemesterPool.Exists(mappedSemester => mappedSemester.Semester.Equals(start)))
            {
                mapped[0] = mappedSemesterPool.Find(mappedSemester => mappedSemester.Semester.Equals(start)).SystemSemester;
            }
            else
            {
                mapped[0] = null;
            }
            if (mappedSemesterPool.Exists(mappedSemester => mappedSemester.Semester.Equals(end)))
            {
                mapped[1] = mappedSemesterPool.Find(mappedSemester => mappedSemester.Semester.Equals(end)).SystemSemester;
            }
            else
            {
                mapped[1] = null;
            }
            return mapped;
        }
        public bool userExists(User user)
        {
            if (userPool.Exists(userTable => userTable.user.Equals(user)))
            {
                return true;
            }
            return false;
        }
    }
}
