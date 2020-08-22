using System;
using Xunit;
using StatisticsWebCoreManager.Core;
using StatisticsWebCoreManager.ICore;
using StatisticsWebRepository.IRepository;
using StatisticsWebRepository.Repository;
using StatisticsWebModels;
using Autofac.Extras.Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace CoreManagerTesting
{
    public class CoreManagerTests
    {        
        [Fact]
        public void getLessonsWithGrade_withNullRepository_ThrowsNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => new Manager(null));
        }
        [Fact]
        public void getLessonsWithGrade_VariableErrorWithNullValue_HandlesHisReference()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepos>()
                    .Setup(db => db.getAllLessonsWithGrade(""))
                    .Returns(new List<Lesson>{
                        new Lesson(),
                        new Lesson(),
                        new Lesson(),
                        new Lesson(),
                    });
                var mockManager = mock.Create<Manager>();
                Error error = null;
                mockManager.getLessonsWithGrade(null, null, ref error);
                Assert.NotNull(error);                
            }
        }
        [Theory]
        [InlineData(null,"4")]
        [InlineData("1",null)]
        public void getLessonsWithGrade_withNullValueOnStartOrOnEndVariablesWhileOneOfThemHasValue_GivesErrorMessagesOnErrorObject(string start , string end)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.True(error.Msg.Length > 0);
                Assert.True((error.Msg.Contains("Start or end variables are wrong")));
                Assert.Null(lessons);
            }
        }
        [Theory]
        [InlineData("4","invalid")]
        [InlineData("invalid","4")]
        public void getLessonsWithGrade_repositoryDoesnotFindsTheMappedSemesters_GivesErrorMessageOnErrorObject(string start , string end)
        {            
            using (var mock = AutoMock.GetLoose())
            {                
                mock.Mock<IRepos>()
                    .Setup(db => db.getMappedSemestersRange(start, end))
                    .Returns((new string[2] { (start.Equals("invalid") ?null:start), (end.Equals("invalid") ? null : end) }));
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithGrade(start, end, ref error);
                Assert.Null(lessons);
                Assert.NotNull(error);
                Assert.True(error.Msg.Length > 0);
                Assert.True((error.Msg.Contains("The selected semester range is not mapped")));
            }
        }
        [Fact]
        public void getLessonsWithGrade_startVariableGreatterThanEndVariable_GivesErrorMessageOnErrorObject()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = "5";
                string end = "4";
                mock.Mock<IRepos>()
                    .Setup(db => db.getMappedSemestersRange(start,end))
                    .Returns((new string[2]{start,end }));
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.Null(lessons);
                Assert.True(error.Msg.Length > 0);
                Assert.True((error.Msg.Contains("Start variable was greater than end variable")));
            }
        }
        [Fact]
        public void getLessonsWithGrade_onSpecifiedPeriod_ReturnsLessonsWithoutError()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = "1";
                string end = "2";
                mock.Mock<IRepos>()
                    .Setup(db => db.getMappedSemestersRange(start, end))
                    .Returns((new string[2] { start, end }));
                mock.Mock<IRepos>()
                    .Setup(db => db.getLessonsWithGradeOnSpecificPeriod(start,end,""))
                    .Returns(new List<Lesson>() { 
                        new Lesson(),
                        new Lesson(),
                        new Lesson(),
                    });
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.NotNull(lessons);
                Assert.True(error.Msg.Length == 0);
                Assert.True(lessons.Count > 0);
            }
        }
        [Fact]
        public void getLessonsWithGrade_getAllLessons_ReturnsLessonsWithoutError()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = null;
                string end = null;
                mock.Mock<IRepos>()
                    .Setup(db => db.getAllLessonsWithGrade(""))
                    .Returns(new List<Lesson>() {
                        new Lesson(),
                        new Lesson(),
                        new Lesson(),
                    });
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.NotNull(lessons);
                Assert.True(error.Msg.Length == 0);
                Assert.True(lessons.Count > 0);
            }
        }
        [Fact]
        public void getLessonsWithGrade_DatabaseCrashesOnMethodWhileTryingToGetMappedRange_ReturnsNullListLessonObject()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = "1";
                string end = "3";
                mock.Mock<IRepos>()
                    .Setup(db => db.getMappedSemestersRange(start, end))
                    .Throws<Exception>();                
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.True(error.Msg.Length == 0);
                Assert.True(lessons == null);
            }
        }
        [Fact]
        public void getLessonsWithGrade_DatabaseCrasheOnMethodWhileTryingToRetrieveDataOnSpecifiedPeriod_ReturnsNullListLessonObject()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = "1";
                string end = "3";
                mock.Mock<IRepos>()
                    .Setup(db => db.getMappedSemestersRange(start, end))
                    .Returns(new string[2] { start, end });
                mock.Mock<IRepos>()
                    .Setup(db => db.getLessonsWithGradeOnSpecificPeriod(start, end,""))
                    .Throws<Exception>();
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithGrade(start, end, ref error);
                Assert.NotNull(error);                
                Assert.True(error.Msg.Length == 0);
                Assert.True(lessons == null);
            }
        }
        [Fact]
        public void getLessonsWithGrade_DatabaseCrashesOnMethodWhileTryingToRetrieveAllLessons_ReturnsNullListLessonObject()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = null;
                string end = null;               
                mock.Mock<IRepos>()
                    .Setup(db => db.getAllLessonsWithGrade(""))
                    .Throws<Exception>();
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.True(error.Msg.Length == 0);
                Assert.True(lessons == null);
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void getLessonsWithNoGrade_VariableErrorWithNullValue_HandlesHisReference()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepos>()
                    .Setup(db => db.getAllLessonsWithGrade(""))
                    .Returns(new List<Lesson>{
                        new Lesson(),
                        new Lesson(),
                        new Lesson(),
                        new Lesson(),
                    });
                var mockManager = mock.Create<Manager>();
                Error error = null;
                mockManager.getLessonsWithNoGrade(null, null, ref error);
                Assert.NotNull(error);
            }
        }
        [Theory]
        [InlineData(null, "4")]
        [InlineData("1", null)]
        public void getLessonsWithNoGrade_withNullValueOnStartOrOnEndVariablesWhileOneOfThemHasValue_GivesErrorMessagesOnErrorObject(string start, string end)
        {
            using (var mock = AutoMock.GetLoose())
            {
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithNoGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.True(error.Msg.Length > 0);
                Assert.True((error.Msg.Contains("Start or end variables are wrong")));
                Assert.Null(lessons);
            }
        }
        [Theory]
        [InlineData("4", "invalid")]
        [InlineData("invalid", "4")]
        public void getLessonsWithNoGrade_repositoryDoesnotFindsTheMappedSemesters_GivesErrorMessageOnErrorObject(string start, string end)
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepos>()
                    .Setup(db => db.getMappedSemestersRange(start, end))
                    .Returns((new string[2] { (start.Equals("invalid") ? null : start), (end.Equals("invalid") ? null : end) }));
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithNoGrade(start, end, ref error);
                Assert.Null(lessons);
                Assert.NotNull(error);
                Assert.True(error.Msg.Length > 0);
                Assert.True((error.Msg.Contains("The selected semester range is not mapped")));
            }
        }
        [Fact]
        public void getLessonsWithNoGrade_startVariableGreatterThanEndVariable_GivesErrorMessageOnErrorObject()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = "5";
                string end = "4";
                mock.Mock<IRepos>()
                    .Setup(db => db.getMappedSemestersRange(start, end))
                    .Returns((new string[2] { start, end }));
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithNoGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.Null(lessons);
                Assert.True(error.Msg.Length > 0);
                Assert.True((error.Msg.Contains("Start variable was greater than end variable")));
            }
        }
        [Fact]
        public void getLessonsWithNoGrade_onSpecifiedPeriod_ReturnsLessonsWithoutError()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = "1";
                string end = "2";
                mock.Mock<IRepos>()
                    .Setup(db => db.getMappedSemestersRange(start, end))
                    .Returns((new string[2] { start, end }));
                mock.Mock<IRepos>()
                    .Setup(db => db.getLessonsWithNoGradeOnSpecificPeriod(start, end,""))
                    .Returns(new List<Lesson>() {
                        new Lesson(),
                        new Lesson(),
                        new Lesson(),
                    });
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithNoGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.NotNull(lessons);
                Assert.True(error.Msg.Length == 0);
                Assert.True(lessons.Count > 0);
            }
        }
        [Fact]
        public void getLessonsWithNoGrade_getAllLessons_ReturnsLessonsWithoutError()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = null;
                string end = null;
                mock.Mock<IRepos>()
                    .Setup(db => db.getAllLessonsWithNoGrade(""))
                    .Returns(new List<Lesson>() {
                        new Lesson(),
                        new Lesson(),
                        new Lesson(),
                    });
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithNoGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.NotNull(lessons);
                Assert.True(error.Msg.Length == 0);
                Assert.True(lessons.Count > 0);
            }
        }
        [Fact]
        public void getLessonsWithNoGrade_DatabaseCrashesOnMethodWhileTryingToGetMappedRange_ReturnsNullListLessonObject()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = "1";
                string end = "3";
                mock.Mock<IRepos>()
                    .Setup(db => db.getMappedSemestersRange(start, end))
                    .Throws<Exception>();
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithNoGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.True(error.Msg.Length == 0);
                Assert.True(lessons == null);
            }
        }
        [Fact]
        public void getLessonsWithNoGrade_DatabaseCrasheOnMethodWhileTryingToRetrieveDataOnSpecifiedPeriod_ReturnsNullListLessonObject()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = "1";
                string end = "3";
                mock.Mock<IRepos>()
                    .Setup(db => db.getMappedSemestersRange(start, end))
                    .Returns(new string[2] { start, end });
                mock.Mock<IRepos>()
                    .Setup(db => db.getLessonsWithGradeOnSpecificPeriod(start, end,""))
                    .Throws<Exception>();
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithNoGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.True(error.Msg.Length == 0);
                Assert.True(lessons == null);
            }
        }
        [Fact]
        public void getLessonsWithNoGrade_DatabaseCrashesOnMethodWhileTryingToRetrieveAllLessons_ReturnsNullListLessonObject()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string start = null;
                string end = null;
                mock.Mock<IRepos>()
                    .Setup(db => db.getAllLessonsWithGrade(""))
                    .Throws<Exception>();
                var mockedManager = mock.Create<Manager>();
                Error error = null;
                IList<Lesson> lessons = mockedManager.getLessonsWithNoGrade(start, end, ref error);
                Assert.NotNull(error);
                Assert.True(error.Msg.Length == 0);
                Assert.True(lessons == null);
            }
        }
    }
}
