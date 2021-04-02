using MetricsAgent.Controllers;
using System.Collections.Generic;
using MetricsAgent.Models;
using Xunit;
using Moq;
using MetricsAgent.DAL;
using Microsoft.Extensions.Logging;

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
            mock.Setup(a => a.GetAll()).Returns(new List<RamMetric>()).Verifiable();
            var controller = new RamMetricsAgentController(mock.Object, mockLogger.Object);
            //Act
            var result = controller.GetMetricsAvailableRam();
            //Assert
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
            mockLogger.Verify();
        }
    }
}
