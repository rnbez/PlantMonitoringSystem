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
    [RoutePrefix("api/behavior")]
    public class BehaviorController : ApiController
    {
        // GET api/<controller>/5
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var userId = (int)Core.ApplicationContext.CurrentUser.Id;
            return Request.CreateResponse(HttpStatusCode.OK, Model.Behavior.Get(id, userId));
        }

        // POST api/<controller>
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post([FromBody]Behavior behavior)
        {
            try
            {
                var result = await Model.Behavior.Insert(behavior);
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
        public async Task<HttpResponseMessage> Put([FromBody]Behavior behavior)
        {
            try
            {
                var result = await Model.Behavior.Update(behavior);
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
        public HttpResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }


        // GET api/<controller>/list
        [HttpGet]
        [Route("list")]
        public HttpResponseMessage List()
        {
            var userId = (int)Core.ApplicationContext.CurrentUser.Id;
            var result = Model.Behavior.List(userId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}
