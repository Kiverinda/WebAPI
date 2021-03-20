using MetricsAgent.Enums;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsAgentController : ControllerBase
    {
        [HttpGet("agent/{agentId}/left")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            double freeHdd = new ServiceHdd().GetFreeMemoryToAllHdd();
            return Ok(freeHdd);
        }
    }
}
