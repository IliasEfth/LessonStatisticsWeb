using System;
using System.Collections.Generic;
using System.Web.Http;
using StatisticsWebModels;
using StatisticsWebCoreManager.Core;
using StatisticsWebCoreManager.ICore;
using StatisticsWebRepository.Repository;
using System.Security.Claims;
using System.Security.Principal;

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
        
        private int getUserId(IPrincipal user , out bool failed)
        {
            int value = 0;
            failed = true;
            var id = (ClaimsIdentity)user.Identity;
            foreach(var claim in id.Claims)
            {
                if (claim.Type.Equals(ClaimTypes.NameIdentifier))
                {
                    value = int.Parse(claim.Value);
                    failed = false;
                }
            }
            return value;
        }
    }
}
