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

    [RoutePrefix("api/node")]
    public class NodeController : ApiController
    {
        // GET api/<controller>/5
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id, bool includeSensors = false)
        {
            var result = Model.Node.Get(id);
            if (includeSensors)
            {
                result.Sensors = Model.Node.ListSensors(id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post([FromBody]Node node)
        {
            try
            {
                var result = await Model.Node.Insert(node);
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
        public async Task<HttpResponseMessage> Put([FromBody]Node node)
        {
            try
            {
                var result = await Model.Node.Update(node);
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
                var result = await Model.Node.Delete(id);
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        
        // GET api/<controller>/5/sensors
        [HttpGet]
        [Route("{id}/sensors")]
        public HttpResponseMessage ListSensors(int id)
        {
            var result = Model.Node.ListSensors(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}