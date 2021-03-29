using System;
using MetricsManager.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Responses;

namespace MetricsManager.Controllers
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private IAgentsRepository _repository;

        public AgentsController(IAgentsRepository repository, ILogger<AgentsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentModel agentInfo)
        {
            _repository.Create(agentInfo);

            if (_logger != null)
            {
                _logger.LogInformation("Добавление в базу агента");
            }
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            AgentModel agent = _repository.GetById(agentId);
            agent.Status = true;
            _repository.Update(agent);

            if (_logger != null)
            {
                _logger.LogInformation($"Включение агента {agentId}");
            }
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            AgentModel agent = _repository.GetById(agentId);
            agent.Status = false;
            _repository.Update(agent);

            if (_logger != null)
            {
                _logger.LogInformation($"Отключение агента {agentId}");
            }
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
                    IpAddress = metric.IpAddress,
                    Name = metric.Name
                });
            }

            if (_logger != null)
            {
                _logger.LogInformation("Запрос всех агентов");
            }

            return Ok(response);
        }
    }
}
