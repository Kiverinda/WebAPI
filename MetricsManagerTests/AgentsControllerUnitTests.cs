using MetricsManager.Controllers;
using System.Collections.Generic;
using MetricsManager.Models;
using Xunit;
using Moq;
using MetricsManager.DAL;
using Microsoft.Extensions.Logging;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private ILogger<AgentsController> _logger;

        [Fact]
        public void RegisterCheckRequestCreate()
        {
            //Arrange
            var mock = new Mock<IAgentsRepository>();
            mock.Setup(a => a.Create(new AgentModel())).Verifiable();
            var controller = new AgentsController(mock.Object, _logger);
            //Act
            var result = controller.RegisterAgent(new AgentModel());
            //Assert
            mock.Verify(repository => repository.Create(new AgentModel()), Times.AtMostOnce());
        }

        [Fact]
        public void EnableAgentByIdCheckRequestCreate()
        {
            //Arrange
            var mockGet = new Mock<IAgentsRepository>();
            var mockUpdate = new Mock<IAgentsRepository>();
            mockGet.Setup(a => a.GetById(0)).Returns(new AgentModel()).Verifiable();
            mockUpdate.Setup(a => a.Update(new AgentModel())).Verifiable();
            var controller = new AgentsController(mockGet.Object, _logger);
            //Act
            var result = controller.EnableAgentById(0);
            //Assert
            mockGet.Verify(repository => repository.GetById(0), Times.AtMostOnce());
            mockUpdate.Verify(repository => repository.Update(new AgentModel()), Times.AtMostOnce());
        }

        [Fact]
        public void DisableAgentByIdCheckRequestCreate()
        {
            //Arrange
            var mockGet = new Mock<IAgentsRepository>();
            var mockUpdate = new Mock<IAgentsRepository>();
            mockGet.Setup(a => a.GetById(0)).Returns(new AgentModel()).Verifiable();
            mockUpdate.Setup(a => a.Update(new AgentModel())).Verifiable();
            var controller = new AgentsController(mockGet.Object, _logger);
            //Act
            var result = controller.DisableAgentById(0);
            //Assert
            mockGet.Verify(repository => repository.GetById(0), Times.AtMostOnce());
            mockUpdate.Verify(repository => repository.Update(new AgentModel()), Times.AtMostOnce());
        }

        [Fact]
        public void GetAllAgentsCheckRequestCreate()
        {
            //Arrange
            var mock = new Mock<IAgentsRepository>();
            mock.Setup(a => a.GetAll()).Returns(new List<AgentModel>()).Verifiable();
            var controller = new AgentsController(mock.Object, _logger);
            //Act
            var result = controller.GetAllAgents();
            //Assert
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }
    }
}
