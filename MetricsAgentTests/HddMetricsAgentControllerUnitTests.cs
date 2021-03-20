using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
namespace MetricsManagerTests
{
    public class HddMetricsAgentControllerUnitTests
    {
        private HddMetricsAgentController controller;
        public HddMetricsAgentControllerUnitTests()
        {
            controller = new HddMetricsAgentController();
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            //Act
            var result = controller.GetMetricsFromAgent(agentId);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
