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
    public class HddMetricsAgentControllerUnitTests
    {
        [Fact]
        public void GetMetricsFreeHddCheckRequestSelect()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<HddMetricsAgentController>>();
            var mock = new Mock<IHddMetricsRepository>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetricModel, HddMetricDto>());
            IMapper mapper = config.CreateMapper();
            mock.Setup(a => a.GetAll()).Returns(new List<HddMetricModel>()).Verifiable();
            var controller = new HddMetricsAgentController(mapper, mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsFreeHdd();
            //Assert
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
