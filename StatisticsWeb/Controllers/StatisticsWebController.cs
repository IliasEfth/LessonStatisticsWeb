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
            manager = new Manager(new MySqlDB());
        }
        [Route("lessons")]
        [HttpPut]
        public IHttpActionResult updateLesson([FromBody]IList<UpdateLesson> lessonList )
        {
            throw new NotImplementedException();                        
        }
        [Route("test")]
        [HttpGet]
        public IHttpActionResult test()
        {
            return Ok("mpike");
        }
        [Route("lessons/graded")]        
        [HttpGet]       
        public IHttpActionResult getLessonsWithGrade([FromUri]string start , [FromUri]string end , [FromUri]int? pageOffset, [FromUri]int? itemsCounter)
        {
            throw new NotImplementedException();                        
        }
        [Route("lessons/nograded")]
        [HttpGet]
        public IHttpActionResult getLessonsWithNoGrade([FromUri]string start, [FromUri]string end , [FromUri]int? pageOffset, [FromUri]int? itemsCounter)
        {
            throw new NotImplementedException();                        
        }        
    }
}
