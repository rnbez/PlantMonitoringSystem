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
            //    System.Net.WebRequest.DefaultWebProxy = new System.Net.WebProxy("127.0.0.1", 8888);
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
                request.Path.Contains("api/user/authenticate") ||
                request.Path.Contains("api/user/create") ||
                request.Path.Contains("api/healthcheck"))
            {
                return;
            }

            var authToken = request.Headers["X-Auth-Token"];
            if (string.IsNullOrWhiteSpace(authToken) || ApplicationContext.GetAuthenticatedUser(authToken) == null)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>()
                {
                    {"method", request.HttpMethod},
                    {"path", request.Path},
                    {"X-Auth-Token", authToken}
                };
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                Response.ContentType = "application/json";
                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dic));
                Response.Flush();
            }
            else
            {
                ApplicationContext.CurrentUser = ApplicationContext.GetAuthenticatedUser(authToken);
            }

        }

        protected void Application_EndRequest()
        {
            if (PlantMonitoringSystem.Model.ModelContext.DataBaseContext != null)
            {
                PlantMonitoringSystem.Model.ModelContext.DataBaseContext.Dispose();
            }

            if (ApplicationContext.CurrentUser != null)
            {
                ApplicationContext.CurrentUser = null;

            }
        }
    }
}
