using Microsoft.AspNetCore.Mvc;
using System;


namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsAgentController : ControllerBase
    {
        private readonly IRamInfoProvider _ramInfoProvider;

        [HttpGet("available")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            double freeRam = _ramInfoProvider.GetFreeRam();
            return Ok(freeRam);
        }
    }
}
