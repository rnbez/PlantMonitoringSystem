using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PlantMonitoringSystem.WebApi.Controllers
{
    [System.Runtime.Serialization.DataContract]
    public class SystemUser
    {
        [System.Runtime.Serialization.DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "username")]
        public string Username { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "token", EmitDefaultValue=false)]
        public string AuthToken { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        [System.Runtime.Serialization.DataMember(Name = "pass", EmitDefaultValue = false)]
        public string Password { get; set; }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }

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
            
            if (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password) &&
                user.Username == "root" && user.Password == "admin")
            {
                var authUser = new SystemUser
                {
                    Id = 1,
                    Email = "rafael@mail.edu",
                    Username = user.Username,
                    Password = user.Password,
                    AuthToken = SystemUser.Base64Encode(user.Username + DateTime.Now.ToString())
                };
                return Request.CreateResponse(HttpStatusCode.OK, authUser);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }


        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var user = new SystemUser
            {
                Id = id,
                Email = "rafael@mail.edu",
                Password = "abc123",
                Username = "rafa"
            };
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
    }
}
