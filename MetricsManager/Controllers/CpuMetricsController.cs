﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Responses;
using MetricsLibrary;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private ICpuMetricsRepository _repository;

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("agent/{idAgent}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int idAgent,
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            var metrics = _repository.GetMetricsFromTimeToTimeFromAgent(fromTime, toTime, idAgent);
            var response = new AllCpuMetricsFromAgentResponse()
            {
                Metrics = new List<CpuMetricManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricManagerDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id,
                    IdAgent = metric.IdAgent
                });
            }

            if (_logger != null)
            {
                _logger.LogInformation("Запрос метрик Cpu FromTimeToTime для агента");
            }

            return Ok(response);
        }

        [HttpGet("agent/{idAgent}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent(
            [FromRoute] int idAgent,
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime,
            [FromRoute] Percentile percentile)
        {
            var metrics = _repository.GetMetricsFromTimeToTimeFromAgentOrderBy(fromTime, toTime, "value", idAgent);
            if (metrics.Count == 0) return NoContent();

            int percentileThisList = (int)percentile;
            percentileThisList = percentileThisList * metrics.Count / 100;

            var response = new AllCpuMetricsFromAgentResponse()
            {
                Metrics = new List<CpuMetricManagerDto>()
            };

            response.Metrics.Add(new CpuMetricManagerDto
            {
                Time = metrics[percentileThisList].Time,
                Value = metrics[percentileThisList].Value,
                Id = metrics[percentileThisList].Id,
                IdAgent = metrics[percentileThisList].IdAgent
            });

            if (_logger != null)
            {
                _logger.LogInformation("Запрос percentile Cpu FromTimeToTime");
            }

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromCluster(
                    [FromRoute] TimeSpan fromTime,
                    [FromRoute] TimeSpan toTime)
        {
            var metrics = _repository.GetMetricsFromTimeToTime(fromTime, toTime);
            var response = new AllCpuMetricsFromAgentResponse()
            {
                Metrics = new List<CpuMetricManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricManagerDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id,
                    IdAgent = metric.IdAgent
                });
            }

            if (_logger != null)
            {
                _logger.LogInformation("Запрос метрик Cpu FromTimeToTime для кластера");
            }

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromCluster(
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime,
            [FromRoute] Percentile percentile)
        {
            var metrics = _repository.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, "value");
            if (metrics.Count == 0) return NoContent();

            int percentileThisList = (int)percentile;
            percentileThisList = percentileThisList * metrics.Count / 100;

            var response = new AllCpuMetricsFromAgentResponse()
            {
                Metrics = new List<CpuMetricManagerDto>()
            };

            response.Metrics.Add(new CpuMetricManagerDto
            {
                Time = metrics[percentileThisList].Time,
                Value = metrics[percentileThisList].Value,
                Id = metrics[percentileThisList].Id,
                IdAgent = metrics[percentileThisList].IdAgent
            });

            if (_logger != null)
            {
                _logger.LogInformation("Запрос percentile Cpu FromTimeToTime");
            }

            return Ok(response);
        }
    }
}
