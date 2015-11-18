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
        public HttpResponseMessage Get(int id, bool includeSensors = false, bool light = false, bool water = false)
        {
            var result = Model.Node.Get(id);
            if (includeSensors)
            {
                result.Sensors = Model.Node.ListSensors(id);
            }

            if (light || water)
            {
                Dictionary<string, bool> dic = new Dictionary<string, bool>();
                if (light) dic.Add("lightOn", result.IsLightOn);
                if (water) dic.Add("waterOn", result.IsWaterOn);

                var r = Request.CreateResponse(HttpStatusCode.OK, dic);
                r.Headers.Add("Access-Control-Allow-Origin", "*");
                return r;
            }

            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
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

        // PUT api/<controller>/
        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> Put([FromBody]Node node)
        {
            HttpResponseMessage response = null;
            try
            {
                var result = await Model.Node.Update(node);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
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


        // POST api/<controller>/5/water
        [HttpPost]
        [Route("{id}/water/{status}")]
        public async Task<HttpResponseMessage> SetWater(int id, bool status)
        {
            HttpResponseMessage response = null;
            try
            {
                var node = Model.Node.Get(id);
                if (node.IsWaterOn != status)
                {
                    node.IsWaterOn = status;
                    var result = await Model.Node.Update(node);
                    response = Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotModified, node);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }


        // POST api/<controller>/5/water
        [HttpPost]
        [Route("{id}/light/{status}")]
        public async Task<HttpResponseMessage> SetLight(int id, bool status)
        {
            HttpResponseMessage response = null;
            try
            {
                var node = Model.Node.Get(id);
                if (node.IsLightOn != status)
                {
                    node.IsLightOn = status;
                    var result = await Model.Node.Update(node);
                    response = Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotModified, node);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }

    }
}