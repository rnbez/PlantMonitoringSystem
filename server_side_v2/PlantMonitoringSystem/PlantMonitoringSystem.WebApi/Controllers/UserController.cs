using PlantMonitoringSystem.Core;
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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("authenticate")]
        public HttpResponseMessage Authenticate([FromBody] SystemUser user)
        {
            if (user == null)
            {
                var ex = new ArgumentNullException("user");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            if ((user = SystemUser.Authenticate(user.Username, user.Password)) != null)
            {
                var authUser = new SystemUser
                {
                    Id = user.Id,
                    Username = user.Username,
                };
                authUser.GenerateAuthToken();
                ApplicationContext.AddAuthenticatedUser(authUser);

                return Request.CreateResponse(HttpStatusCode.OK, authUser);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }


        [HttpPost]
        [Route("authenticate/remove")]
        public HttpResponseMessage RevemoAuthenticatedUser([FromBody] SystemUser user)
        {
            if (user == null)
            {
                var ex = new ArgumentNullException("user");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            
            ApplicationContext.RemoveAuthenticatedUser(user.AuthToken);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Model.SystemUser.Get(id));
        }

        // POST api/<controller>/create
        [HttpPost]
        [Route("create")]
        public async Task<HttpResponseMessage> Post([FromBody]SystemUser user)
        {
            try
            {
                if (SystemUser.UserExists(user.Username))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = "Username already used" });
                }

                user.Password = SystemUser.Base64Encode(user.Password);
                var result = await Model.SystemUser.Insert(user);
                result.GenerateAuthToken();
                ApplicationContext.AddAuthenticatedUser(result);
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
        public async Task<HttpResponseMessage> Put([FromBody]SystemUser user)
        {
            try
            {
                var result = await Model.SystemUser.Update(user);
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
            //try
            //{
            //    var result = await Model.SystemUser.Delete(id);
            //    return Request.CreateResponse(HttpStatusCode.Created, result);
            //}
            //catch (Exception ex)
            //{
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            //}
        }

    }
}
