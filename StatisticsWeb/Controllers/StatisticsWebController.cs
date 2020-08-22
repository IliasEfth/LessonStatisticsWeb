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
            IList<Lesson> lessons = null;
            try
            {
                lessons = manager.getLessonsWithGrade(start, end);
            }            
            catch(ArgumentNullException ex)
            {
                return BadRequest();
            }
            catch(Exception)
            {
                return NotFound();
            }            
            return Ok(lessons);
        }//done manager
        [HttpPut]
        public IHttpActionResult updateLesson([FromBody]IList<UpdateLesson> lessonList)
        {
            UpdateLessonsValues value;
            try
            {
                value = manager.updateLesson(lessonList);                
            } 
            catch(NullReferenceException)
            {
                return BadRequest(UpdateErrorCodes.NoInstance.ToString());
            }
            catch(ArgumentException)
            {
                return BadRequest(UpdateErrorCodes.InvalidArguments.ToString());
            }
            catch(Exception)
            {
                return NotFound();
            }
            if(value.Equals(UpdateLessonsValues.LessonExistsFails))
            {
                return NotFound();
            }
            return Ok();
        }
        [Route("lessons/nograde")]
        [HttpGet]
        public IHttpActionResult getLessonsWithNoGrade([FromUri]string start, [FromUri]string end)
        {
            IList<Lesson> lessons = null;
            try
            {
                lessons = manager.getLessonsWithNoGrade(start, end);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(lessons);
        }//done manager
        [Route("AvgGrade")]
        [HttpGet]
        public IHttpActionResult avgGrade()
        {            
            throw new NotImplementedException();                        
        }
    }
}
