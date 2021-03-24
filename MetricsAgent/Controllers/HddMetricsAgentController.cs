using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsAgentController : ControllerBase
    {
        private readonly IHddInfoProvider _hddInfoProvider;

        [HttpGet("left")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId)
        {
            double freeHdd = _hddInfoProvider.GetFreeHdd();
            return Ok(freeHdd);
        }
    }
}
