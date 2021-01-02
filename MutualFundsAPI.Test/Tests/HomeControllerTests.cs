using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MutualFundsAPI.Controllers;
using MutualFundsAPI.Interfaces;
using MutualFundsAPI.Test;
using MutualFundsAPI.Test.Helpers;
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
            string expectedValue = "1000";

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
                    Amount = 100,
                    Date = "01-01-2019"
                },
                new ValueTrend()
                {
                    Amount = 150,
                    Date = "10-10-2019"
                },
            };

            var okResult = await homeController.GetPortfolioValueTrendAsync("12");
            var actualValue = (OkObjectResult)okResult.Result;

            CollectionAssert.AreEqual(expectedValue, (List<ValueTrend>)actualValue.Value, new ValueTrendComparer());
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
                    Return = "10"
                }
            };

            var okResult = await homeController.GetTopGainersAsync();
            var actualValue = (OkObjectResult)okResult.Result;

            CollectionAssert.AreEqual(expectedValue, (List<FundPerformance>)actualValue.Value, new FundPerformanceComparer());
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
                    Return = "-10"
                }
            };

            var okResult = await homeController.GetTopLosersAsync();
            var actualValue = (OkObjectResult)okResult.Result;

            CollectionAssert.AreEqual(expectedValue, (List<FundPerformance>)actualValue.Value, new FundPerformanceComparer());
        }
    }
}
