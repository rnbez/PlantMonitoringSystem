using PlantMonitoringSystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace PlantMonitoringSystem.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);


            //if (/*IsDebug*/)
            //{
                System.Net.WebRequest.DefaultWebProxy = new System.Net.WebProxy("127.0.0.1", 8888);
            //}
        }

        protected void Application_BeginRequest()
        {
            PlantMonitoringSystem.Model.ModelContext.DataBaseContext = PlantMonitoringSystem.Model.ModelContext.GetNewInstance();
            if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
            {
                Response.Flush();
            }
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var request = Context.Request;
            if (request.HttpMethod == "OPTIONS" || 
                request.Path.StartsWith("/api/user/authenticate") || 
                request.Path.StartsWith("/api/user/create"))
            {
                return;
            }

            var header = request.Headers["X-Auth-Token"];
            if (string.IsNullOrWhiteSpace(header) || ApplicationContext.GetAuthenticatedUser(header) == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                Response.End();
            }
            
        }

        protected void Application_EndRequest()
        {
            if (PlantMonitoringSystem.Model.ModelContext.DataBaseContext != null)
            {
                PlantMonitoringSystem.Model.ModelContext.DataBaseContext.Dispose();
            }
        }
    }
}
