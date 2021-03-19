using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly MetricsRepository repository = new MetricsRepository();

        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            repository.Add(date, temperature);
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult ReadTimeInterval([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            List<Metrics> list = repository.Read(fromDate, toDate);
            return Ok(list);
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date , [FromQuery] int temp)
        {
            repository.Update(date, temp);
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult DeleteTimeInterval([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            repository.Delete(fromDate, toDate);
            return Ok();
        }
    }
}
