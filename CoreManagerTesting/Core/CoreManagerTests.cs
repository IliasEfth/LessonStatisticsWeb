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
using System.Threading.Tasks;
using StatisticsWebModels.ContextParsers;
using StatisticsWebModels.DBTableModels.Converters.LessonConverter;
using Moq;
using StatisticsWebModels.DBTableModels.Converters.LessonInterfaces;
using StatisticsWebModels.DBTableModels.Converters.GradeInterfaces;

namespace CoreManagerTesting
{
    public class CoreManagerTests
    {
        [Fact]
        public void managerInitialize_withNullRepository_ThrowsNullReferenceException()
        {
            //init, act , assert
            Assert.Throws<NullReferenceException>(() => new Manager(null));
        }
        [Fact]
        public void getLessonsWithGrade_withOutToken_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                mock.Mock<IRepos>();
                var mockManager = mock.Create<Manager>();
                int? token = null;
                Page page = new Page()
                {
                    Items = 10,
                    PageNo = 1
                };
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithGrade(null, null, token, page); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void getLessonsWithGrade_withOutPageReference_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                mock.Mock<IRepos>();
                var mockManager = mock.Create<Manager>();
                int? token = 10;
                Page page = null;
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithGrade(null, null, token, page); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void getLessonsWithGrade_PageObjectWithoutRange_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                mock.Mock<IRepos>();
                var mockManager = mock.Create<Manager>();
                int? token = 10;
                Page page = new Page()
                {
                    Items = 0,
                    PageNo = 0
                };
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithGrade(null, null, token, page); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Theory]
        [InlineData(1, null)]
        [InlineData(null, 2)]
        public void getLessonsWithGrade_withoutStartOrEnd_whileOtherExists_returnsMessage(int? start, int? end)
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                mock.Mock<IRepos>();
                var mockManager = mock.Create<Manager>();
                int? token = 10;
                Page page = new Page()
                {
                    Items = 10,
                    PageNo = 1
                }; ;
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithGrade(start, end, token, page); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void getLessonsWithGrade_withoutStartOrEnd_returnsDataProperly()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                Page page = new Page()
                {
                    Items = 10,
                    PageNo = 1
                }; ;
                PaggingRes<IList<LessonInfo>> items = new PaggingRes<IList<LessonInfo>>()
                {
                    Items = new List<LessonInfo>() {
                                new LessonInfo() {
                                    ECTS = 5f
                                   , Grades = 5f
                                   , Name = "Databases II"
                                   , Semester = 1
                                   , Type = "y"
                                }
                            },
                    Total = 10
                };
                mock.Mock<IRepos>()
                    .Setup(db => db.getAllLessonsWithGrade(token.GetValueOrDefault(), page))
                    .ReturnsAsync(items);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithGrade(null, null, token, page); }).Result;
                //assert
                Assert.Null(result.Error);
                Assert.Equal(result.Item.Items, items.Items);
            }
        }
        [Fact]
        public void getLessonsWithGrade_withStartOrEnd_returnsDataProperly()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                Page page = new Page()
                {
                    Items = 10,
                    PageNo = 1
                }; ;
                PaggingRes<IList<LessonInfo>> items = new PaggingRes<IList<LessonInfo>>()
                {
                    Items = new List<LessonInfo>() {
                                new LessonInfo() {
                                    ECTS = 5f
                                   , Grades = 5f
                                   , Name = "Databases II"
                                   , Semester = 1
                                   , Type = "y"
                                }
                            },
                    Total = 10
                };
                mock.Mock<IRepos>()
                    .Setup(db => db.getLessonsWithNoGradeOnSpecificPeriod(1,1,token.GetValueOrDefault(), page))
                    .ReturnsAsync(items);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithGrade(1, 1, token, page); }).Result;
                //assert
                Assert.Null(result.Error);
                Assert.Equal(result.Item.Items, items.Items);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void getLessonsWithNoGrade_withOutToken_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                mock.Mock<IRepos>();
                var mockManager = mock.Create<Manager>();
                int? token = null;
                Page page = new Page()
                {
                    Items = 10,
                    PageNo = 1
                };
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithNoGrade(null, null, token, page); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void getLessonsWithNoGrade_withOutPageReference_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                mock.Mock<IRepos>();
                var mockManager = mock.Create<Manager>();
                int? token = 10;
                Page page = null;
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithNoGrade(null, null, token, page); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void getLessonsWithNoGrade_PageObjectWithoutRange_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                mock.Mock<IRepos>();
                var mockManager = mock.Create<Manager>();
                int? token = 10;
                Page page = new Page()
                {
                    Items = 0,
                    PageNo = 0
                };
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithNoGrade(null, null, token, page); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Theory]
        [InlineData(1, null)]
        [InlineData(null, 2)]
        public void getLessonsWithNoGrade_withoutStartOrEnd_whileOtherExists_returnsMessage(int? start, int? end)
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                mock.Mock<IRepos>();
                var mockManager = mock.Create<Manager>();
                int? token = 10;
                Page page = new Page()
                {
                    Items = 10,
                    PageNo = 1
                }; ;
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithNoGrade(start, end, token, page); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void getLessonsWithNoGrade_withoutStartOrEnd_returnsDataProperly()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                Page page = new Page()
                {
                    Items = 10,
                    PageNo = 1
                }; ;
                PaggingRes<IList<LessonInfo>> items = new PaggingRes<IList<LessonInfo>>()
                {
                    Items = new List<LessonInfo>() {
                                new LessonInfo() {
                                    ECTS = 5f
                                   , Name = "Databases II"
                                   , Semester = 1
                                   , Type = "y"
                                }
                            },
                    Total = 10
                };
                mock.Mock<IRepos>()
                    .Setup(db => db.getAllLessonsWithNoGrade(token.GetValueOrDefault(), page))
                    .ReturnsAsync(items);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithNoGrade(null, null, token, page); }).Result;
                //assert
                Assert.Null(result.Error);
                Assert.Equal(result.Item.Items, items.Items);
            }
        }
        [Fact]
        public void getLessonsWithNoGrade_withStartOrEnd_returnsDataProperly()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                Page page = new Page()
                {
                    Items = 10,
                    PageNo = 1
                }; ;
                PaggingRes<IList<LessonInfo>> items = new PaggingRes<IList<LessonInfo>>()
                {
                    Items = new List<LessonInfo>() {
                                new LessonInfo() {
                                    ECTS = 5f
                                   , Name = "Databases II"
                                   , Semester = 1
                                   , Type = "y"
                                }
                            },
                    Total = 10
                };
                mock.Mock<IRepos>()
                    .Setup(db => db.getLessonsWithNoGradeOnSpecificPeriod(1, 1, token.GetValueOrDefault(), page))
                    .ReturnsAsync(items);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.getLessonsWithNoGrade(1, 1, token, page); }).Result;
                //assert
                Assert.Null(result.Error);
                Assert.Equal(result.Item.Items, items.Items);
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////
        [Fact]
        public void postLesson_withoutToken_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = null;
                IList<UpdateLesson> lessonList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                mock.Mock<IRepos>()
                    .Setup(database => database.lessonListWithIdExists(lessonList , token.GetValueOrDefault()))
                    .ReturnsAsync(lessonList);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.postLesson( token, lessonList); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void postLesson_withoutLessonList_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                mock.Mock<IRepos>();
                var mockManager = mock.Create<Manager>();
                int? token = 10;
                IList<UpdateLesson> lessonList = null;
                //act
                var result = Task.Run(async () => { return await mockManager.postLesson(token, lessonList); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void postLesson_duplicatestLessonList_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                IList<UpdateLesson> lessonList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 10
                   }
                };
                mock.Mock<IRepos>()
                   .Setup(database => database.lessonListWithIdExists(lessonList, token.GetValueOrDefault()))
                   .ReturnsAsync(lessonList);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.postLesson(token, lessonList); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void postLesson_dataDoesNotMapWithAllLessonstLessonList_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                IList<UpdateLesson> lessonList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                IList<UpdateLesson> dbList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   }
                };
                mock.Mock<IRepos>()
                    .Setup(database => database.lessonListWithIdExists( lessonList , token.GetValueOrDefault()))
                    .ReturnsAsync(dbList);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.postLesson(token, lessonList); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void postLesson_gradesAreWrongRangeAtLessonList_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                IList<UpdateLesson> lessonList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 11f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                IList<UpdateLesson> dbList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                IList<LessonInfo> lessonInfos = new List<LessonInfo>() {
                    new LessonInfo(){
                         ECTS = 5
                         , Grades = 5f
                         , Name = "Databases I"
                         , Semester = 1
                         , Type = "y"
                         ,Id = 10
                    }
                    ,new LessonInfo(){
                         ECTS = 5
                         , Grades = 5f
                         , Name = "Databases II"
                         , Semester = 1
                         , Type = "y"
                         , Id = 11
                    }
                };
                mock.Mock<IRepos>()
                    .Setup(database => database.lessonListWithIdExists(lessonList, token.GetValueOrDefault()))
                    .ReturnsAsync(dbList);
                mock.Mock<IRepos>()
                    .Setup(database => database.postLesson(token.GetValueOrDefault(), lessonList))
                    .ReturnsAsync(lessonList.Count);
                mock.Mock<IRepos>()
                    .Setup(database => database.getLessonByList(token.GetValueOrDefault(), lessonList))
                    .ReturnsAsync(lessonInfos);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.postLesson(token, lessonList); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void postLesson_correctDataTryToPost_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                IList<UpdateLesson> lessonList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                IList<UpdateLesson> dbList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                IList<LessonInfo> lessonInfos = new List<LessonInfo>() {
                    new LessonInfo(){
                         ECTS = 5
                         , Grades = 5f
                         , Name = "Databases I"
                         , Semester = 1
                         , Type = "y"
                         ,Id = 10
                    }
                    ,new LessonInfo(){
                         ECTS = 5
                         , Grades = 5f
                         , Name = "Databases II"
                         , Semester = 1
                         , Type = "y"
                         , Id = 11
                    }
                };
                mock.Mock<IRepos>()
                    .Setup(database => database.lessonListWithIdExists(lessonList, token.GetValueOrDefault()))
                    .ReturnsAsync(dbList);
                mock.Mock<IRepos>()
                    .Setup(database => database.postLesson(token.GetValueOrDefault() , lessonList))
                    .ReturnsAsync(lessonList.Count);
                mock.Mock<IRepos>()
                    .Setup(database => database.getLessonByList(token.GetValueOrDefault(), lessonList))
                    .ReturnsAsync(lessonInfos);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.postLesson(token, lessonList); }).Result;
                //assert
                Assert.Null(result.Error);
                Assert.Equal(result.Item, lessonInfos);
            }
        }
        //////////////////////////////////////////////
        [Fact]
        public void updateLesson_withoutToken_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = null;
                IList<UpdateLesson> lessonList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                mock.Mock<IRepos>()
                    .Setup(database => database.lessonListWithIdExists(lessonList, token.GetValueOrDefault()))
                    .ReturnsAsync(lessonList);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.updateLesson(token, lessonList); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void updateLesson_withoutLessonList_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                mock.Mock<IRepos>();
                var mockManager = mock.Create<Manager>();
                int? token = 10;
                IList<UpdateLesson> lessonList = null;
                //act
                var result = Task.Run(async () => { return await mockManager.updateLesson(token, lessonList); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void updateLesson_duplicatestLessonList_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                IList<UpdateLesson> lessonList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 10
                   }
                };
                mock.Mock<IRepos>()
                   .Setup(database => database.lessonListWithIdExists(lessonList, token.GetValueOrDefault()))
                   .ReturnsAsync(lessonList);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.updateLesson(token, lessonList); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void updateLesson_dataDoesNotMapWithAllLessonstLessonList_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                IList<UpdateLesson> lessonList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                IList<UpdateLesson> dbList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   }
                };
                mock.Mock<IRepos>()
                    .Setup(database => database.lessonListWithIdExists(lessonList, token.GetValueOrDefault()))
                    .ReturnsAsync(dbList);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.updateLesson(token, lessonList); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void updateLesson_gradesAreWrongRangeAtLessonList_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                IList<UpdateLesson> lessonList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 11f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                IList<UpdateLesson> dbList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                IList<LessonInfo> lessonInfos = new List<LessonInfo>() {
                    new LessonInfo(){
                         ECTS = 5
                         , Grades = 5f
                         , Name = "Databases I"
                         , Semester = 1
                         , Type = "y"
                         ,Id = 10
                    }
                    ,new LessonInfo(){
                         ECTS = 5
                         , Grades = 5f
                         , Name = "Databases II"
                         , Semester = 1
                         , Type = "y"
                         , Id = 11
                    }
                };
                mock.Mock<IRepos>()
                    .Setup(database => database.lessonListWithIdExists(lessonList, token.GetValueOrDefault()))
                    .ReturnsAsync(dbList);
                mock.Mock<IRepos>()
                    .Setup(database => database.updateLesson(token.GetValueOrDefault(), lessonList))
                    .ReturnsAsync(lessonList.Count);
                mock.Mock<IRepos>()
                    .Setup(database => database.getLessonByList(token.GetValueOrDefault(), lessonList))
                    .ReturnsAsync(lessonInfos);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.updateLesson(token, lessonList); }).Result;
                //assert
                Assert.NotNull(result.Error);
                Assert.Single(result.Error.Msg);
            }
        }
        [Fact]
        public void updateLesson_correctDataTryToPost_returnsMessage()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //init
                int? token = 10;
                IList<UpdateLesson> lessonList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                IList<UpdateLesson> dbList = new List<UpdateLesson>()
                {
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 5f
                       }
                       , Id = 10
                   },
                   new UpdateLesson()
                   {
                       Grades = new UpdateGrade()
                       {
                            Graded = 6f
                       }
                       , Id = 11
                   }
                };
                IList<LessonInfo> lessonInfos = new List<LessonInfo>() {
                    new LessonInfo(){
                         ECTS = 5
                         , Grades = 5f
                         , Name = "Databases I"
                         , Semester = 1
                         , Type = "y"
                         ,Id = 10
                    }
                    ,new LessonInfo(){
                         ECTS = 5
                         , Grades = 5f
                         , Name = "Databases II"
                         , Semester = 1
                         , Type = "y"
                         , Id = 11
                    }
                };
                mock.Mock<IRepos>()
                    .Setup(database => database.lessonListWithIdExists(lessonList, token.GetValueOrDefault()))
                    .ReturnsAsync(dbList);
                mock.Mock<IRepos>()
                    .Setup(database => database.updateLesson(token.GetValueOrDefault(), lessonList))
                    .ReturnsAsync(lessonList.Count);
                mock.Mock<IRepos>()
                    .Setup(database => database.getLessonByList(token.GetValueOrDefault(), lessonList))
                    .ReturnsAsync(lessonInfos);
                var mockManager = mock.Create<Manager>();
                //act
                var result = Task.Run(async () => { return await mockManager.updateLesson(token, lessonList); }).Result;
                //assert
                Assert.Null(result.Error);
                Assert.Equal(result.Item, lessonInfos);
            }
        }
    }
}
