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
    
    [RoutePrefix("api/handshake")]
    public class HandshakeController : ApiController

    {
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Handshake(Node node)
        {
            if (node == null) throw new ArgumentException("node");

            if (node.Id == null)
            {
                node = await Model.Node.Insert(node);
            }
            else
            {
                node = await Model.Node.Update(node);
            }

            node.Sensors = Node.ListSensors((int)node.Id);

            return Request.CreateResponse(HttpStatusCode.OK, node);
        }
    }
}
