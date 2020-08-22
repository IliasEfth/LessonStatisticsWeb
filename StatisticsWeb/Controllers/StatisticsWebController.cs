using System;
using System.Collections.Generic;
using System.Web.Http;
using StatisticsWebModels;
using StatisticsWebCoreManager.Core;
using StatisticsWebCoreManager.ICore;
using StatisticsWebRepository.Repository;
namespace StatisticsWeb.Controllers
{
    [RoutePrefix("api/StatisticsWeb")]
    [Authorize]
    public class StatisticsWebController : ApiController
    {
        private static IManager manager;
        static StatisticsWebController()
        {            
            manager = new Manager(new InMemory());
        }
        [Route("lessons")]        
        [HttpGet]       
        public IHttpActionResult getLessonsWithGrade([FromUri]string start , [FromUri]string end)
        {
            throw new NotImplementedException();                        
        }//done manager
        [HttpPut]
        public IHttpActionResult updateLesson([FromBody]IList<UpdateLesson> lessonList)
        {
            throw new NotImplementedException();                        
        }
        [Route("lessons/nograde")]
        [HttpGet]
        public IHttpActionResult getLessonsWithNoGrade([FromUri]string start, [FromUri]string end)
        {
            throw new NotImplementedException();                        
        }
        [Route("AvgGrade")]
        [HttpGet]
        public IHttpActionResult avgGrade()
        {            
            throw new NotImplementedException();                        
        }
    }
}
