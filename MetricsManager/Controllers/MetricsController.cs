using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly MetricsRepository repository = MetricsRepository.getInstance();

        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int temp)
        {
            repository.Add(date, temp);
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(repository);
        }

        [HttpGet("readtimeinterval")]
        public IActionResult ReadTimeInterval([FromQuery] string date1, [FromQuery] string date2)
        {
            DateTime dateTime1 = DateTime.Parse(date1);
            DateTime dateTime2 = DateTime.Parse(date2);
            List<Metrics> list = repository.Read(dateTime1, dateTime2);
            return Ok(list);
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] string date , [FromQuery] int temp)
        {
            DateTime dateTime = DateTime.Parse(date);
            repository.Update(dateTime, temp);
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] int index)
        {
            repository.Delete(index);
            return Ok();
        }

        [HttpDelete("deltimeinterval")]
        public IActionResult DeleteTimeInterval([FromQuery] string date1, [FromQuery] string date2)
        {
            DateTime dateTime1 = DateTime.Parse(date1);
            DateTime dateTime2 = DateTime.Parse(date2);
            repository.Delete(dateTime1, dateTime2);
            return Ok();
        }
    }
}
