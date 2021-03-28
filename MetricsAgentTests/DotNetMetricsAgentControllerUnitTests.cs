using MetricsAgent.Controllers;
using System.Collections.Generic;
using MetricsAgent.Models;
using System;
using Xunit;
using Moq;
using MetricsAgent.DAL;
using Microsoft.Extensions.Logging;

namespace MetricsAgentTests
{
    public class DotNetMetricsAgentControllerUnitTests
    {
        private ILogger<DotNetMetricsAgentController> _logger;

        [Fact]
        public void GetDotNetMetricsFromTimeToTimeCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<IDotNetMetricsRepository>();
            TimeSpan fromTime = TimeSpan.FromSeconds(5);
            TimeSpan toTime = TimeSpan.FromSeconds(10);
            mock.Setup(a => a.GetMetricsFromTimeToTime(fromTime, toTime)).Returns(new List<DotNetMetric>()).Verifiable();
            var controller = new DotNetMetricsAgentController(mock.Object, _logger);
            //Act
            var result = controller.GetMetricsFromTimeToTime(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
        }
    }
}
