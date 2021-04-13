using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentsRepository _repository;

        public AgentsController(IAgentsRepository repository, ILogger<AgentsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentModel agentInfo)
        {
            _repository.Create(agentInfo);

            _logger.LogInformation($"Регистрация агента: " +
                                   $"Id = {agentInfo.Id}" +
                                   $" IpAddress = {agentInfo.Ipaddres}" +
                                   $" Name = {agentInfo.Name}" +
                                   $" Status = {agentInfo.Status}");

            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            AgentModel agent = _repository.GetById(agentId);
            agent.Status = true;
            _repository.Update(agent);

            _logger.LogInformation($"Включение агента Id = {agentId}");

            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            AgentModel agent = _repository.GetById(agentId);
            agent.Status = false;
            _repository.Update(agent);

            _logger.LogInformation($"Отключение агента Id = {agentId}");

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAllAgents()
        {
            var metrics = _repository.GetAll();
            var response = new AllAgentsResponse()
            {
                Metrics = new List<AgentManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new AgentManagerDto
                {
                    Id = metric.Id,
                    Status = metric.Status,
                    Ipaddres = metric.Ipaddres,
                    Name = metric.Name
                });
            }

            _logger.LogInformation("Запрос всех агентов");


            return Ok(response);
        }
    }
}
