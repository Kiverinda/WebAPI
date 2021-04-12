using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Responses;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class RamMetricsAgentControllerUnitTests
    {
        [Fact]
        public void GetMetricsAvailableCheckRequestSelect()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<RamMetricsAgentController>>();
            var mock = new Mock<IRamMetricsRepository>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RamMetricModel, RamMetricDto>());
            IMapper mapper = config.CreateMapper();
            mock.Setup(a => a.GetAll()).Returns(new List<RamMetricModel>()).Verifiable();
            var controller = new RamMetricsAgentController(mapper, mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsAvailableRam();
            //Assert
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
