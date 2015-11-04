using PlantMonitoringSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlantMonitoringSystem.WebApi.Controllers
{
     [RoutePrefix("api/sensor")]
    public class SensorController : ApiController
    {
        // GET api/<controller>/5
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var result = Model.Sensor.Get(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post([FromBody]Sensor sensor)
        {
            try
            {
                var result = await Model.Sensor.Insert(sensor);
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put([FromBody]Sensor sensor)
        {
            try
            {
                var result = await Model.Sensor.Update(sensor);
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                var result = await Model.Sensor.Delete(id);
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        // GET api/<controller>/5/readings
        [HttpGet]
        [Route("{id}/readings")]
        public HttpResponseMessage ListReadings(int id)
        {
            var result = Model.Sensor.ListReadings(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}