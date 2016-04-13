using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Shared;

namespace Api.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("statelessCounter")]
        public async Task<IHttpActionResult> Stateless()
        {
            var statelessClient = ServiceProxy.Create<IStateless>(new Uri(FabricRuntime.GetActivationContext().ApplicationName + "/Stateless1"), new ServicePartitionKey(0));
            var value = await statelessClient.GetCurrentValue();

            return Ok(new
            {
                value = value,
                message = "Current Value"
            });
        }

        [HttpGet]
        [Route("statefulCounter")]
        public async Task<IHttpActionResult> Stateful()
        {
            var statefulClient = ServiceProxy.Create<IStateful>(new Uri(FabricRuntime.GetActivationContext().ApplicationName + "/Stateful1"), new ServicePartitionKey(1));
            var value = await statefulClient.GetCurrentValue();

            return Ok(new
            {
                value = value,
                message = "Current Stateful Value"
            });

        }
    }
}
