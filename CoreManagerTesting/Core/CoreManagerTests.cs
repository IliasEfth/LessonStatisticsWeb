// using System;
// using Xunit;
// using StatisticsWebCoreManager.Core;
// using StatisticsWebCoreManager.ICore;
// using StatisticsWebRepository.IRepository;
// using StatisticsWebRepository.Repository;
// using StatisticsWebModels;
// using Autofac.Extras.Moq;
// using System.Collections.Generic;
// using System.Linq;

// namespace CoreManagerTesting
// {
//     public class CoreManagerTests
//     {
//         Manager manager = new Manager((AutoMock.GetLoose()).Create<InMemory>());   
//         [Fact]
//         public void ManagerConstructor_WithNullRepository_ThrowsNullReferenceException()
//         {
//             Assert.Throws<NullReferenceException>(() => new Manager(null));
//         }
//         [Fact]
//         public void getLessonsWithGrade_WithNullValueonStart_ThrowsArgumentNullException()
//         {
//             Assert.Throws<ArgumentNullException>("start", () => manager.getLessonsWithGrade(null, "4"));
//         }
//         [Fact]
//         public void getLessonsWithGrade_WithNullValueonEnd_ThrowsArgumentNullException()
//         {
//             Assert.Throws<ArgumentNullException>("end", () => manager.getLessonsWithGrade("1", null));
//         }
//         [Fact]
//         public void getLessonsWithGrade_WithStartAndEndParametersWithNull_returnsLessonsList()
//         {
//             IList<Lesson> expect = new List<Lesson>();
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.getAllLessonsWithGrade())
//                     .Returns(expect);
//                 var tmpManager = mock.Create<Manager>();
//                 var answer = tmpManager.getLessonsWithGrade(null,null);
//                 Assert.Empty(answer);
//                 Assert.NotNull(answer);
//             }
//         }
//         [Fact]
//         public void getLessonsWithGrade_WithStartAndEndParametersWithNonNull_returnsLessonsList()
//         {
//             IList<Lesson> expect = new List<Lesson>();
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.getLessonsWithGradeOnSpecificPeriod("1","3"))
//                     .Returns(expect);
//                 var tmpManager = mock.Create<Manager>();
//                 var answer = tmpManager.getLessonsWithGrade("1", "3");
//                 Assert.Empty(answer);
//                 Assert.NotNull(answer);
//             }
//         }
//         [Fact]
//         public void getLessonsWithGrade_DatabaseCrashesOngetAllLessonsWithGrade_catchException()
//         {
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.getAllLessonsWithGrade())
//                     .Throws<TimeoutException>();
//                 var tmpManager = mock.Create<Manager>();
//                 Assert.Throws<Exception>(() => tmpManager.getLessonsWithGrade(null, null));
//             }
//         }
//         [Fact]
//         public void getLessonsWithGrade_DatabaseCrashesOngetLessonsWithGradeOnSpecificPeriod_catchException()
//         {
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.getLessonsWithGradeOnSpecificPeriod("1", "3"))
//                     .Throws<TimeoutException>();

//                 var tmpManager = mock.Create<Manager>();                                
//                 Assert.Throws<Exception>(()=>tmpManager.getLessonsWithGrade("1","3"));
//             }
//         }
//         [Fact]
//         public void getLessonsWithNoGrade_WithNullValueonStart_ThrowsArgumentNullException()
//         {
//             Assert.Throws<ArgumentNullException>("start", () => manager.getLessonsWithNoGrade(null, "4"));
//         }
//         [Fact]
//         public void getLessonsWithNoGrade_WithNullValueonEnd_ThrowsArgumentNullException()
//         {
//             Assert.Throws<ArgumentNullException>("end", () => manager.getLessonsWithNoGrade("1", null));
//         }
//         [Fact]
//         public void getLessonsWithNoGrade_WithStartAndEndParametersWithNull_returnsLessonsList()
//         {
//             IList<Lesson> expect = new List<Lesson>();
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.getAllLessonsNoWithGrade())
//                     .Returns(expect);
//                 var tmpManager = mock.Create<Manager>();
//                 var answer = tmpManager.getLessonsWithNoGrade(null, null);
//                 Assert.Empty(answer);
//                 Assert.NotNull(answer);
//             }
//         }
//         [Fact]
//         public void getLessonsWithNoGrade_WithStartAndEndParametersWithNonNull_returnsLessonsList()
//         {
//             IList<Lesson> expect = new List<Lesson>();
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.getLessonsWithNoGradeOnSpecificPeriod("1", "3"))
//                     .Returns(expect);
//                 var tmpManager = mock.Create<Manager>();
//                 var answer = tmpManager.getLessonsWithNoGrade("1", "3");
//                 Assert.Empty(answer);
//                 Assert.NotNull(answer);
//             }
//         }
//         [Fact]
//         public void getLessonsWithNoGrade_DatabaseCrashesOngetAllLessonsWithGrade_catchException()
//         {
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.getAllLessonsNoWithGrade())
//                     .Throws<TimeoutException>();
//                 var tmpManager = mock.Create<Manager>();
//                 Assert.Throws<Exception>(() => tmpManager.getLessonsWithNoGrade(null, null));
//             }
//         }
//         [Fact]
//         public void getLessonsWithNoGrade_DatabaseCrashesOngetLessonsWithGradeOnSpecificPeriod_catchException()
//         {
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.getLessonsWithNoGradeOnSpecificPeriod("1", "3"))
//                     .Throws<TimeoutException>();
//                 var tmpManager = mock.Create<Manager>();
//                 Assert.Throws<Exception>(() => tmpManager.getLessonsWithNoGrade("1", "3"));
//             }
//         }
//         [Fact]
//         public void updateLesson_ListOfLessonsWithNull_ThrowsNullReferenceException()
//         {
//             Assert.Throws<NullReferenceException>(() => manager.updateLesson(null));
//         }
//         [Fact]
//         public void updateLesson_ListofLessonsEmpty_ThrowsArgumentException()
//         {
//             Assert.Throws<ArgumentException>("lessonList",()=>manager.updateLesson(new List<UpdateLesson>()));
//         }
//         [Fact]
//         public void updateLesson_ListOfLessonsWithIdNull_ThrowsArgumentNullException()
//         {
//             IList<UpdateLesson> lessons = new List<UpdateLesson>();
//             lessons.Add(new UpdateLesson() { Id = null , Grade = 5f});
//             lessons.Add(new UpdateLesson() { Id = null, Grade = 10f });
//             Assert.Throws<ArgumentNullException>("lessonListValues_Null",()=>manager.updateLesson(lessons));
//         }
//         [Fact]
//         public void updateLesson_ListOfLessonsWithGradeNull_ThrowsArgumentNullException()
//         {
//             IList<UpdateLesson> lessons = new List<UpdateLesson>();
//             lessons.Add(new UpdateLesson() { Id = "1", Grade = null });
//             lessons.Add(new UpdateLesson() { Id = "2", Grade = null });
//             Assert.Throws<ArgumentNullException>("lessonListValues_Null", () => manager.updateLesson(lessons));
//         }
//         [Fact]
//         public void updateLesson_ListOfLessonsWithIdDoesntExists_Fails()
//         {
//             IList<UpdateLesson> lessons = new List<UpdateLesson>();
//             lessons.Add(new UpdateLesson() { Id = "100", Grade = 5f });
//             lessons.Add(new UpdateLesson() { Id = "2", Grade = 10f });
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.lessonListWithIdExists(lessons))
//                     .Returns(false);
//                 var tmpManager = mock.Create<Manager>();
//                 Assert.Equal(UpdateLessonsValues.LessonExistsFails, tmpManager.updateLesson(lessons));
//             }
//         }
//         [Fact]
//         public void updateLesson_updateLessonRepositoryFail_Fails()
//         {
//             IList<UpdateLesson> lessons = new List<UpdateLesson>();
//             lessons.Add(new UpdateLesson() { Id = "100", Grade = 5f });
//             lessons.Add(new UpdateLesson() { Id = "2", Grade = 10f });
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.lessonListWithIdExists(lessons))
//                     .Returns(true);
//                 mock.Mock<IRepos>()
//                    .Setup(db => db.updateLesson(lessons))
//                    .Returns(false);
//                 var tmpManager = mock.Create<Manager>();
//                 Assert.Equal(UpdateLessonsValues.UpdateLessonFails, tmpManager.updateLesson(lessons));
//             }
//         }
//         [Fact]
//         public void updateLesson_updateLessonRepositoryPass_Pass()
//         {
//             IList<UpdateLesson> lessons = new List<UpdateLesson>();
//             lessons.Add(new UpdateLesson() { Id = "100", Grade = 5f });
//             lessons.Add(new UpdateLesson() { Id = "2", Grade = 10f });
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.lessonListWithIdExists(lessons))
//                     .Returns(true);
//                 mock.Mock<IRepos>()
//                    .Setup(db => db.updateLesson(lessons))
//                    .Returns(true);
//                 var tmpManager = mock.Create<Manager>();
//                 Assert.Equal(UpdateLessonsValues.UpdateLessonPass, tmpManager.updateLesson(lessons));
//             }
//         }
//         [Fact]
//         public void updateLesson_RepositoryCrashesOnMethodlessonListWithIdExists_ThrowsTimeoutException()
//         {
//             IList<UpdateLesson> lessons = new List<UpdateLesson>();
//             lessons.Add(new UpdateLesson() { Id = "100", Grade = 5f });
//             lessons.Add(new UpdateLesson() { Id = "2", Grade = 10f });
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.lessonListWithIdExists(lessons))
//                     .Throws<TimeoutException>();
                
//                 var tmpManager = mock.Create<Manager>();
//                 Assert.Throws<Exception>(() => tmpManager.updateLesson(lessons));
//             }
//         }
//         [Fact]
//         public void updateLesson_RepositoryCrashesOnMethodupdateLesson_ThrowsTimeoutException()
//         {
//             IList<UpdateLesson> lessons = new List<UpdateLesson>();
//             lessons.Add(new UpdateLesson() { Id = "100", Grade = 5f });
//             lessons.Add(new UpdateLesson() { Id = "2", Grade = 10f });
//             using (var mock = AutoMock.GetLoose())
//             {
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.lessonListWithIdExists(lessons))
//                     .Returns(true);
//                 mock.Mock<IRepos>()
//                     .Setup(db => db.updateLesson(lessons))
//                     .Throws<TimeoutException>();
//                 var tmpManager = mock.Create<Manager>();
//                 Assert.Throws<Exception>(() => tmpManager.updateLesson(lessons));
//             }
//         }
//     }
// }
