using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MutualFundsAPI.Controllers;
using MutualFundsAPI.Interfaces;
using MutualFundsAPI.Models;
using MutualFundsAPI.Test;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutualFundsAPI.Tests
{
    public class HomeControllerTests
    {
        readonly HomeController homeController;
        readonly IDashboardService dashboardService;

        public HomeControllerTests()
        {
            var logger = Mock.Of<ILogger<HomeController>>();
            dashboardService = new DummyDashboardService();
            homeController = new HomeController(dashboardService, logger);
        }

        [Test]
        public async Task TotalValue_ReturnsOk()
        {
            var okResult = await homeController.GetTotalValueAsync();
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        public async Task TotalValue_ReturnValuePass()
        {
            double expectedValue = 1000;

            var okResult = await homeController.GetTotalValueAsync();
            var actualValue =(OkObjectResult)okResult.Result; 

            Assert.AreEqual(expectedValue, actualValue.Value);
        }

        [Test]
        public async Task ValueTrend_ReturnsOk()
        {
            var okResult = await homeController.GetPortfolioValueTrendAsync("12");
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        public async Task ValueTrend_ReturnValuePass()
        {
            List<ValueTrend> expectedValue = new List<ValueTrend>()
            {
                new ValueTrend()
                {
                    CurrentValue = 100,
                    InvestedValue = 80,
                    Date = "01-01-2019"
                },
                new ValueTrend()
                {
                    CurrentValue = 150,
                    InvestedValue = 80,
                    Date = "10-10-2019"
                },
            };

            var okResult = await homeController.GetPortfolioValueTrendAsync("12");
            var actualValue = (OkObjectResult)okResult.Result;

            expectedValue.Should().BeEquivalentTo((List<ValueTrend>)actualValue.Value);
        }

        [Test]
        public async Task TopGainers_ReturnsOk()
        {
            var okResult = await homeController.GetTopGainersAsync();
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        public async Task TopGainers_ReturnValuePass()
        {
            List<FundPerformance> expectedValue = new List<FundPerformance>()
            {
                new FundPerformance()
                {
                    FundName = "HDFC Fund",
                    Return = 10
                }
            };

            var okResult = await homeController.GetTopGainersAsync();
            var actualValue = (OkObjectResult)okResult.Result;

            expectedValue.Should().BeEquivalentTo((List<FundPerformance>)actualValue.Value);
        }

        [Test]
        public async Task TopLosers_ReturnsOk()
        {
            var okResult = await homeController.GetTopLosersAsync();
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        public async Task TopLosers_ReturnValuePass()
        {
            List<FundPerformance> expectedValue = new List<FundPerformance>()
            {
                new FundPerformance()
                {
                    FundName = "HDFC Fund",
                    Return = -10
                }
            };

            var okResult = await homeController.GetTopLosersAsync();
            var actualValue = (OkObjectResult)okResult.Result;

            expectedValue.Should().BeEquivalentTo((List<FundPerformance>)actualValue.Value);
        }

        [Test]
        public async Task AvailableFunds_ReturnValuePass()
        {
            List<Funds> expectedValue = new List<Funds>()
            {
                new Funds()
                {
                    FundName = "HDFC Fund",
                    Id = "1"
                }
            };

            var okResult = await homeController.GetAvailableFundsAsync();
            var actualValue = (OkObjectResult)okResult.Result;

            expectedValue.Should().BeEquivalentTo((List<Funds>)actualValue.Value);
        }
    }
}
