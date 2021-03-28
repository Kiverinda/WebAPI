using MetricsAgent.Controllers;
using System.Collections.Generic;
using MetricsAgent.Models;
using MetricsLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using MetricsAgent.DAL;
using Microsoft.Extensions.Logging;

namespace MetricsAgentTests
{
    public class CpuMetricsAgentControllerUnitTests
    {
        private ILogger<CpuMetricsAgentController> _logger;

        [Fact] 
        public void GetCpuMetricsFromTimeToTimeCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<ICpuMetricsRepository>();
            TimeSpan fromTime = TimeSpan.FromSeconds(5);
            TimeSpan toTime = TimeSpan.FromSeconds(10);
            mock.Setup(a => a.GetMetricsFromTimeToTime(fromTime, toTime)).Returns(new List<CpuMetric>()).Verifiable();
            var controller = new CpuMetricsAgentController(mock.Object, _logger);
            //Act
            var result = controller.GetMetricsFromTimeToTime(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetricsFromTimeToTimeByPercentileCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<ICpuMetricsRepository>();
            TimeSpan fromTime = TimeSpan.FromSeconds(5);
            TimeSpan toTime = TimeSpan.FromSeconds(10);
            Percentile percentile = Percentile.P90;
            string sort = "value";
            mock.Setup(a => a.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, sort)).Returns(new List<CpuMetric>()).Verifiable();
            var controller = new CpuMetricsAgentController(mock.Object, _logger);
            //Act
            var result = controller.GetMetricsFromTimeToTimePercentile(fromTime, toTime, percentile);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromTimeToTime(fromTime, toTime), Times.AtMostOnce());
        }
    }
}
