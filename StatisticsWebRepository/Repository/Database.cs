using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StatisticsWebModels;
using StatisticsWebRepository.IRepository;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using StatisticsWebDBModel.DBHelpers;
using StatisticsWebModels.DBTableModels.Converters.LessonConverter;
using StatisticsWebModels.ContextParsers;
using StatisticsWebModels.DBTableModels.Converters.LessonInterfaces;

namespace StatisticsWebRepository.Repository
{
    public class Database : IRepos
    {
        public async Task<PaggingRes<IList<LessonInfo>>> getLessonsWithGradeOnSpecificPeriod(int start, int end, int token, Page page)
        {
            PaggingRes<IList<LessonInfo>> res = null;
            res.Items = await DBHelpers.execute<Task<IList<LessonInfo>>>(async (context) =>
            {
                {
                    res.Total = context.Grades.Where(grade => grade.User.Id.Equals(token)
                    && (grade.Lesson.Semester >= start && grade.Lesson.Semester <= end))
                    .Count();
                }
                return await context.Grades.Where(grade => grade.User.Id.Equals(token)
                    && (grade.Lesson.Semester >= start && grade.Lesson.Semester <= end))
               .Skip(page.PageNo * page.Items)
               .Take(page.Items)
               .Select(fromGraded => new LessonInfo
               {
                   Name = fromGraded.Lesson.Name,
                   ECTS = fromGraded.Lesson.ECTS,
                   Semester = fromGraded.Lesson.Semester,
                   Grades = fromGraded.Lesson.Grades.Where(grad => grad.User.Id.Equals(token)).SingleOrDefault().Graded,
                   Type = fromGraded.Lesson.Type
                   ,
                   Id = fromGraded.Lesson.Id
               })
               .ToListAsync();
            });
            return res;
        }
        public async Task<PaggingRes<IList<LessonInfo>>> getLessonsWithNoGradeOnSpecificPeriod(int start, int end, int token, Page page)
        {
            PaggingRes<IList<LessonInfo>> res = new PaggingRes<IList<LessonInfo>>();
            res.Items = await DBHelpers.execute<Task<IList<LessonInfo>>>(async (context) =>
            {
                {
                    res.Total = context.Grades.Where(grade => !grade.User.Id.Equals(token)
                    && (grade.Lesson.Semester >= start && grade.Lesson.Semester <= end))
                    .Count();
                }
                return await context.Grades.Where(grade => grade.User.Id.Equals(token))
                    .Where(le => le.Lesson.Semester >= start && le.Lesson.Semester <= end)
               .Skip(page.PageNo * page.Items)
               .Take(page.Items)
               .Select(fromGraded => new LessonInfo
               {
                   Name = fromGraded.Lesson.Name,
                   ECTS = fromGraded.Lesson.ECTS,
                   Semester = fromGraded.Lesson.Semester,
                   Grades = fromGraded.Lesson.Grades.Where(grad => grad.User.Id.Equals(token)).SingleOrDefault().Graded,
                   Type = fromGraded.Lesson.Type
                   ,
                   Id = fromGraded.Lesson.Id
               })
               .ToListAsync();
            });
            return res;
        }
        public async Task<PaggingRes<IList<LessonInfo>>> getAllLessonsWithGrade(int token, Page page)
        {
            PaggingRes<IList<LessonInfo>> res = new PaggingRes<IList<LessonInfo>>();
            res.Items = await DBHelpers.execute<Task<IList<LessonInfo>>>(async (context) =>
           {
               {
                   res.Total = context.Grades.Where(grade => grade.User.Id.Equals(token)).Count();
               }
               return await context.Grades.Where(grade => grade.User.Id.Equals(token))
              .Skip(page.PageNo * page.Items)
              .Take(page.Items)
              .Select(fromGraded => new LessonInfo
              {
                  Name = fromGraded.Lesson.Name,
                  ECTS = fromGraded.Lesson.ECTS,
                  Semester = fromGraded.Lesson.Semester,
                  Grades = fromGraded.Lesson.Grades.Where(grad => grad.User.Id.Equals(token)).SingleOrDefault().Graded,
                  Type = fromGraded.Lesson.Type
                  ,
                  Id = fromGraded.Lesson.Id
              })
              .ToListAsync();

           });
            return res;
        }
        public async Task<PaggingRes<IList<LessonInfo>>> getAllLessonsWithNoGrade(int token, Page page)
        {
            PaggingRes<IList<LessonInfo>> res = new PaggingRes<IList<LessonInfo>>();
            res.Items = await DBHelpers.execute<Task<IList<LessonInfo>>>(async (context) =>
            {
                {
                    res.Total = context.Grades.Where(grade => !grade.User.Id.Equals(token)).Count();
                }
                return await context.Grades.Where(grade => !grade.User.Id.Equals(token))
              .Skip(page.PageNo * page.Items)
              .Take(page.Items)
              .Select(fromGraded => new LessonInfo
              {
                  Name = fromGraded.Lesson.Name,
                  ECTS = fromGraded.Lesson.ECTS,
                  Semester = fromGraded.Lesson.Semester,
                  Grades = fromGraded.Lesson.Grades.Where(grad => grad.User.Id.Equals(token)).SingleOrDefault().Graded,
                  Type = fromGraded.Lesson.Type
                  ,
                  Id = fromGraded.Lesson.Id
              })
              .ToListAsync();
            });
            return res;
        }
        public async Task<int> updateLesson(int token, IList<UpdateLesson> lessonList)
        {
            return await DBHelpers.execute<Task<int>>(async (context) =>
            {
                await context.Grades.Where(grad => grad.User.Id.Equals(token))
                .Where(grad => lessonList.Select(les => les.Id.Equals(grad.Lesson.Id)).Any())
                .ForEachAsync((item) =>
                {
                    item.Graded = lessonList.Where(les => les.Id.Equals(item.Lesson.Id)).SingleOrDefault().Grades.Graded;
                });
                return await context.SaveChangesAsync();
            });
        }
        public async Task<int> postLesson(int token, IList<UpdateLesson> lessonList)
        {
            return await DBHelpers.execute<Task<int>>(async (context) =>
            {
                foreach (var lesson in lessonList)
                {
                    context.Grades.Add(new Grade
                    {
                        Lesson = new Lesson { Id = lesson.Id },
                        User = new User { Id = token },
                        Graded = lesson.Grades.Graded
                    });
                }
                return await context.SaveChangesAsync();
            });
        }
        public async Task<IList<UpdateLesson>> lessonListWithIdExists(IList<UpdateLesson> lessonList, int token)
        {
            return await DBHelpers.execute<Task<IList<UpdateLesson>>>(async (context) =>
            {
                return (await context.Users.Where(user => user.Id.Equals(token))
                .Select(user => user.Department)
                .Select(dep => dep.Lesson)
                .Where(les => les.Select(act => lessonList.Contains(new UpdateLesson { Id = act.Id })).Any())
                .SingleOrDefaultAsync())
                .Select(les => new UpdateLesson
                {
                    Id = les.Id
                })
                .ToList();
            });
        }
        //extend app
        public async Task<StatisticsWebModels.ContextParsers.MappedSemester> getMappedSemestersRange(int token)
        {
            return await DBHelpers.execute<Task<StatisticsWebModels.ContextParsers.MappedSemester>>(async (context) =>
            {
                return await context.MappedSemesters.Where(map => map.Department.Equals(
                    context.Users.Where(user => user.Id.Equals(token))
                .Select(user => user.Department)
                ))
                .Select(mapped => new StatisticsWebModels.ContextParsers.MappedSemester
                {
                    Start = mapped.Start,
                    End = mapped.End
                }).SingleOrDefaultAsync();
            });
        }
        //end extend
        public async Task<User> userExists(User user)
        {
            return await DBHelpers.execute<Task<User>>(async (context) =>
            {
                User tmp;
                tmp = await context.Users.SingleOrDefaultAsync(u => u.Name.Equals(user.Name) && u.Password.Equals(user.Password));
                return tmp;
            });
        }
        public bool createIfNotExists()
        {
            return DBHelpers.execute<bool>((context) =>
            {
                bool exists = (context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
                if (!exists)
                {
                    var what = context.Database.EnsureCreated();
                    Init();
                }
                return exists;
            });
        }
        public async Task<IList<LessonInfo>> getLessonByList(int token, IList<UpdateLesson> lessons)
        {
            return await DBHelpers.execute<Task<IList<LessonInfo>>>(async (context) =>
            {
                return (await context.Departments.Where(dep => dep.User.Select(user => user.Id.Equals(token)).Any())
                .Select(dep => dep.Lesson.Where(les => lessons.Contains(new UpdateLesson { Id = les.Id }))
                .Select(les => new LessonInfo
                {
                    ECTS = les.ECTS,
                    Grades = les.Grades.Where(grad => grad.User.Id.Equals(token)).SingleOrDefault().Graded,
                    Name = les.Name,
                    Semester = les.Semester,
                    Type = les.Type,
                    Id = les.Id
                })).SingleOrDefaultAsync())
                .ToList();
            });
        }
        public void Init()
        {
            DBHelpers.execute<bool>((context) =>
            {
                var User = new List<User> {
                new User { Name = "test", Password = "test" , Id = 1}
                          , new User { Name = "test2", Password = "test2" , Id = 2 }
                          , new User { Name = "test3", Password = "test3" , Id = 3 }
            };
                var Lesson1 = new Lesson
                {
                    Name = "Databases I"
                                     ,
                    Semester = 1
                                     ,
                    ECTS = 5f
                                     ,
                    Type = "Y"
                };
                var Lesson2 = new Lesson
                {
                    Name = "Databases II"
                                     ,
                    Semester = 3
                                     ,
                    ECTS = 5f
                                     ,
                    Type = "Y"
                };
                var Lessons = new List<Lesson> {
                Lesson1,
                Lesson2
            };
                var Department = new List<Department>() { new Department {
                      Name = "Informatics"
                      , User = User
                       , Lesson = Lessons
            } };
                var MappedSemester = new StatisticsWebModels.MappedSemester { Start = 1, End = 1, Department = Department.First() };
                var University = new University
                {
                    Id = 1,
                    Name = "Attica"
                    ,
                    Departments = Department
                };
                var Grades = new List<Grade> {
                new Grade { Id = 1, User = User.Where(u => u.Id.Equals(1)).SingleOrDefault(), Graded = 5f, Lesson = Lesson1 },
                new Grade { Id = 2, User = User.Where(u => u.Id.Equals(2)).SingleOrDefault(), Graded = 6f, Lesson = Lesson1 },
                new Grade { Id = 3, User = User.Where(u => u.Id.Equals(3)).SingleOrDefault(), Graded = 10f, Lesson = Lesson2},
                new Grade { Id = 4, User = User.Where(u => u.Id.Equals(2)).SingleOrDefault(), Graded = 6f, Lesson = Lesson2 }
            };
                User.ForEach(item => context.Users.Add(item));
                Lessons.ForEach(item => context.Lessons.Add(item));
                Grades.ForEach(item => context.Grades.Add(item));
                Department.ForEach(item => context.Departments.Add(item));
                context.MappedSemesters.Add(MappedSemester);
                context.Universities.Add(University);
                context.SaveChanges();
                return true;
            });
        }
    }
}//drop database statisticswebmaindb
