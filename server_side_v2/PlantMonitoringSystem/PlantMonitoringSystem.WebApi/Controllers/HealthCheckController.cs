using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantMonitoringSystem.WebApi.Controllers
{
    [RoutePrefix("api/healthcheck")]
    public class HealthCheckController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "PlantMonitor Is ON";
        }

        [HttpGet]
        [Route("detail")]
        public HttpResponseMessage Detail()
        {
            var dic = new Dictionary<string, object>();
            try
            {
                dic.Add("Initializing", "Ok");

                Model.Sensor.List();

                dic.Add("Database Access", "Ok");


                return Request.CreateResponse(HttpStatusCode.OK, dic);
            }
            catch (Exception ex)
            {
                dic.Add("exception", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, dic);
            }
        }
    }
}
