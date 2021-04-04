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
    public class NetworkMetricsAgentControllerUnitTests
    {
        [Fact]
        public void GetMetricsNetworkCheckRequestSelect()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<NetworkMetricsAgentController>>();
            var mock = new Mock<INetworkMetricsRepository>();
            TimeSpan fromTime = TimeSpan.FromSeconds(5);
            TimeSpan toTime = TimeSpan.FromSeconds(10);
            mock.Setup(a => a.GetMetricsFromTimeToTime(fromTime, toTime)).Returns(new List<NetworkMetric>()).Verifiable();
            var controller = new NetworkMetricsAgentController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsNetwork(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
