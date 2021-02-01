using System;
using System.Collections.Generic;
using System.Web.Http;
using StatisticsWebModels;
using StatisticsWebCoreManager.Core;
using StatisticsWebCoreManager.ICore;
using StatisticsWebRepository.Repository;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using StatisticsWebModels.DBTableModels.Converters.LessonInterfaces;
using StatisticsWebModels.ContextParsers;
using Newtonsoft.Json.Linq;

namespace StatisticsWeb.Controllers
{
    [RoutePrefix("api/StatisticsWeb")]
    [Authorize]
    public class StatisticsWebController : ApiController
    {
        private static IManager manager;
        static StatisticsWebController()
        {
            manager = new Manager(new Database());
        }
        [Route("lessons")]
        [HttpPut]
        public async Task<IHttpActionResult> updateLesson([FromBody] IList<UpdateLesson> lessonList)
        {
            bool failed = false;
            var token = getUserId(RequestContext.Principal, out failed);
            if (failed)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var result = await manager.updateLesson(token, lessonList);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError();
                }
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> postLesson([FromBody] IList<UpdateLesson> lessonList, [FromBody] Page page)
        {
            bool failed = false;
            var token = getUserId(RequestContext.Principal, out failed);
            if (failed)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var result = await manager.postLesson(token, lessonList);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError();
                }
            }
        }
        [Route("lessons/graded")]
        [HttpGet]
        public async Task<IHttpActionResult> getLessonsWithGrade([FromBody] Page page, [FromUri(Name = "start")] int? start = null, [FromUri(Name = "end")] int? end = null)
        {
            bool failed = false;
            var token = getUserId(RequestContext.Principal, out failed);
            if (failed)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var result = await manager.getLessonsWithGrade(start, end, token, page);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError();
                }
            }
        }
        [Route("lessons/nograded")]
        [HttpGet]
        public async Task<IHttpActionResult> getLessonsWithNoGrade([FromUri] int? start, [FromUri] int? end, [FromBody] Page page)
        {
            bool failed = false;
            var token = getUserId(RequestContext.Principal, out failed);
            if (failed)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var result = await manager.getLessonsWithNoGrade(start, end, token, page);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return InternalServerError();
                }
            }
        }

        private int getUserId(IPrincipal user, out bool failed)
        {
            int value = 0;
            failed = true;
            var id = (ClaimsIdentity)user.Identity;
            foreach (var claim in id.Claims)
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
