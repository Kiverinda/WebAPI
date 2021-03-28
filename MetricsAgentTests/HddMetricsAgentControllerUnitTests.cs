using MetricsAgent.Controllers;
using System.Collections.Generic;
using MetricsAgent.Models;
using Xunit;
using Moq;
using MetricsAgent.DAL;
using Microsoft.Extensions.Logging;

namespace MetricsAgentTests
{
    public class HddMetricsAgentControllerUnitTests
    {
        private ILogger<HddMetricsAgentController> _logger;

        [Fact]
        public void GetMetricsFreeHddCheckRequestSelect()
        {
            //Arrange
            var mock = new Mock<IHddMetricsRepository>();
            mock.Setup(a => a.GetAll()).Returns(new List<HddMetric>()).Verifiable();
            var controller = new HddMetricsAgentController(mock.Object, _logger);
            //Act
            var result = controller.GetMetricsFreeHdd();
            //Assert
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }
    }
}
