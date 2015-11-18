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
    [RoutePrefix("api/reading")]
    public class SensorReadingController : ApiController
    {
        // GET api/<controller>/5
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var result = Model.SensorReading.Get(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post([FromBody]SensorReading reading)
        {
            try
            {
                var result = await Model.SensorReading.Insert(reading);
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
        public async Task<HttpResponseMessage> Put([FromBody]SensorReading reading)
        {
            try
            {
                var result = await Model.SensorReading.Update(reading);
                return Request.CreateResponse(HttpStatusCode.OK, result);
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
                var result = await Model.SensorReading.Delete(id);
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}