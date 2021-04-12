using AutoMapper;
using MetricsLibrary;
using MetricsManager.Client;
using MetricsManager.Client.ApiRequests;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Client.ApiResponses;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMetricsManagerClient _metricsAgentClient;

        public CpuMetricsController(IMapper mapper, ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger, IMetricsManagerClient metricsAgentClient)
        {
            _repository = repository;
            _logger = logger;
            _metricsAgentClient = metricsAgentClient;
            _mapper = mapper;
        }


        [HttpGet("all")]
        public IActionResult GetMetricsFromAgent()
        {
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(16181676);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(20000000000);

            var metrics = _metricsAgentClient.GetAllCpuMetrics(new GetAllCpuMetricsApiRequest
            {
                FromTime = fromTime,
                ToTime = toTime,
                Addres = "http://localhost:5000"
            });

            return Ok(metrics);
        }


        [HttpGet("agent/{idAgent}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int idAgent,
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
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

            _logger.LogInformation($"Запрос метрик Cpu FromTime = {fromTime} ToTime = {toTime} для агента Id = {idAgent}");

            return Ok(response);
        }

        [HttpGet("agent/{idAgent}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent(
            [FromRoute] int idAgent,
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            var metrics = _repository.GetMetricsFromTimeToTimeFromAgentOrderBy(fromTime, toTime, "value", idAgent);
            if (metrics.Count == 0) return NoContent();

            int percentileThisList = (int)percentile;
            percentileThisList = percentileThisList * metrics.Count / 100;

            var response = metrics[percentileThisList].Value;

            _logger.LogInformation($"Запрос percentile Cpu FromTime = {fromTime} ToTime = {toTime} percentile = {percentile} для агента Id = {idAgent}");

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromCluster(
                    [FromRoute] DateTimeOffset fromTime,
                    [FromRoute] DateTimeOffset toTime)
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

            _logger.LogInformation($"Запрос метрик Cpu FromTime = {fromTime} ToTime = {toTime} для кластера");

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromCluster(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            var metrics = _repository.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, "value");
            if (metrics.Count == 0) return NoContent();

            int percentileThisList = (int)percentile;
            percentileThisList = percentileThisList * metrics.Count / 100;

            var response = metrics[percentileThisList].Value;

            _logger.LogInformation($"Запрос percentile = {percentile} Cpu FromTime = {fromTime} ToTime = {toTime}");

            return Ok(response);
        }
    }
}
