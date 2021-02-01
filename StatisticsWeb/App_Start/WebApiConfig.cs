using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using StatisticsWebRepository.Repository;

namespace StatisticsWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            try
            {
                var db = new Database();
                db.createIfNotExists();
            }
            catch(Exception ex)
            {
                //make file logger for execute start
                Console.Out.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
