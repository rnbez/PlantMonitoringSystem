using PlantMonitoringSystem.Core;
using PlantMonitoringSystem.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlantMonitoringSystem.WebApi.Controllers
{
    [RoutePrefix("api/view")]
    public class ViewController : ApiController
    {

        [HttpGet]
        [Route("nodes")]
        public HttpResponseMessage ViewNodes()
        {
            var resposne = Request.CreateResponse(HttpStatusCode.OK, NodesView.GetNodeList());
            resposne.Headers.Add("Access-Control-Allow-Origin", "*");
            return resposne;
        }


        [HttpGet]
        [Route("sensor/{id}/readings")]
        public HttpResponseMessage ViewSensorReadings(int id)
        {
            var resposne = Request.CreateResponse(HttpStatusCode.OK, SensorViewReadings.GetLastReadings(id));
            resposne.Headers.Add("Access-Control-Allow-Origin", "*");
            return resposne;
        }
    }
}
