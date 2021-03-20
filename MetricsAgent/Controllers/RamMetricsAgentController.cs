using MetricsAgent.Enums;
using Microsoft.AspNetCore.Mvc;
using System;


namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsAgentController : ControllerBase
    {
        [HttpGet("agent/{agentId}/available")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            double freeRam = new ServiceRam().GetFreeRam();
            return Ok(freeRam);
        }
    }
}
