using MetricsLibrary;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerUnitTests
    {
        [Fact] 
        public void GetMetricsFromAgentCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<ICpuMetricsRepository>();
            var mockLogger = new Mock<ILogger<CpuMetricsController>>();
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            int agentId = 1;
            mock.Setup(a => a.GetMetricsFromTimeToTimeFromAgent(fromTime, toTime, agentId)).Returns(new List<CpuMetricModel>()).Verifiable();
            var controller = new CpuMetricsController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTimeFromAgent(fromTime, toTime, agentId), Times.AtMostOnce());
            mockLogger.Verify();
        }

        [Fact]
        public void GetMetricsByPercentileFromAgentCheckRequestSelect()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CpuMetricsController>>();
            var mock = new Mock<ICpuMetricsRepository>();
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            int agentId = 1;
            Percentile percentile = Percentile.P90;
            string sort = "value";
            mock.Setup(a => a.GetMetricsFromTimeToTimeFromAgentOrderBy(fromTime, toTime, sort, agentId))
                .Returns(new List<CpuMetricModel>()).Verifiable();
            var controller = new CpuMetricsController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsByPercentileFromAgent(agentId, fromTime, toTime, percentile);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTimeFromAgentOrderBy(fromTime, toTime, sort, agentId), Times.AtMostOnce());
            mockLogger.Verify();
        }

        [Fact]
        public void GetCpuMetricsFromClusterCheckRequestSelect()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CpuMetricsController>>();
            var mock = new Mock<ICpuMetricsRepository>();
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            mock.Setup(a => a.GetMetricsFromTimeToTime(fromTime, toTime)).Returns(new List<CpuMetricModel>()).Verifiable();
            var controller = new CpuMetricsController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsFromCluster(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
            mockLogger.Verify();
        }

        [Fact]
        public void GetMetricsByPercentileFromClusterCheckRequestSelect()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CpuMetricsController>>();
            var mock = new Mock<ICpuMetricsRepository>();
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            Percentile percentile = Percentile.P90;
            string sort = "value";
            mock.Setup(a => a.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, sort))
                .Returns(new List<CpuMetricModel>()).Verifiable();
            var controller = new CpuMetricsController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsByPercentileFromCluster(fromTime, toTime, percentile);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, sort), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
