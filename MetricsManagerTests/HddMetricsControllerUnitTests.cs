using MetricsManager.Controllers;
using System.Collections.Generic;
using MetricsManager.Models;
using MetricsLibrary;
using System;
using Xunit;
using Moq;
using MetricsManager.DAL;
using Microsoft.Extensions.Logging;

namespace MetricsManagerTests
{
    public class HddMetricsControllerUnitTests
    {
        private ILogger<HddMetricsController> _logger;

        [Fact] 
        public void GetMetricsFromAgentCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<IHddMetricsRepository>();
            TimeSpan fromTime = TimeSpan.FromSeconds(5);
            TimeSpan toTime = TimeSpan.FromSeconds(10);
            int agentId = 1;
            mock.Setup(a => a.GetMetricsFromTimeToTimeFromAgent(fromTime, toTime, agentId)).Returns(new List<HddMetricModel>()).Verifiable();
            var controller = new HddMetricsController(mock.Object, _logger);
            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTimeFromAgent(fromTime, toTime, agentId), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetricsByPercentileFromAgentCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<IHddMetricsRepository>();
            TimeSpan fromTime = TimeSpan.FromSeconds(5);
            TimeSpan toTime = TimeSpan.FromSeconds(10);
            int agentId = 1;
            Percentile percentile = Percentile.P90;
            string sort = "value";
            mock.Setup(a => a.GetMetricsFromTimeToTimeFromAgentOrderBy(fromTime, toTime, sort, agentId))
                .Returns(new List<HddMetricModel>()).Verifiable();
            var controller = new HddMetricsController(mock.Object, _logger);
            //Act
            var result = controller.GetMetricsByPercentileFromAgent(agentId, fromTime, toTime, percentile);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTimeFromAgentOrderBy(fromTime, toTime, sort, agentId), Times.AtMostOnce());
        }

        [Fact]
        public void GetHddMetricsFromClusterCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<IHddMetricsRepository>();
            TimeSpan fromTime = TimeSpan.FromSeconds(5);
            TimeSpan toTime = TimeSpan.FromSeconds(10);
            mock.Setup(a => a.GetMetricsFromTimeToTime(fromTime, toTime)).Returns(new List<HddMetricModel>()).Verifiable();
            var controller = new HddMetricsController(mock.Object, _logger);
            //Act
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetricsByPercentileFromClusterCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<IHddMetricsRepository>();
            TimeSpan fromTime = TimeSpan.FromSeconds(5);
            TimeSpan toTime = TimeSpan.FromSeconds(10);
            Percentile percentile = Percentile.P90;
            string sort = "value";
            mock.Setup(a => a.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, sort))
                .Returns(new List<HddMetricModel>()).Verifiable();
            var controller = new HddMetricsController(mock.Object, _logger);
            //Act
            var result = controller.GetMetricsByPercentileFromCluster(fromTime, toTime, percentile);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, sort), Times.AtMostOnce());
        }
    }
}
