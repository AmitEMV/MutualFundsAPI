using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MutualFundsAPI.Controllers;
using MutualFundsAPI.Interfaces;
using MutualFundsAPI.Models;
using MutualFundsAPI.Test.Helpers;
using MutualFundsAPI.Test.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutualFundsAPI.Test.Tests
{
    public class PortfolioControllerTests
    {
        readonly PortfolioController portfolioController;
        readonly IPortfolioService portfolioService;

        public PortfolioControllerTests()
        {
            var logger = Mock.Of<ILogger<PortfolioController>>();
            portfolioService = new DummyPortfolioService();
            portfolioController = new PortfolioController(portfolioService, logger);
        }

        [Test]
        public async Task InvestmentReturns_ReturnsOk()
        {
            var okResult = await portfolioController.GetInvestmentReturnsValueAsync();
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        public async Task InvestmentReturns_ReturnValuePass()
        {
            (string, string) expectedValue = ("1000", "1500");

            var okResult = await portfolioController.GetInvestmentReturnsValueAsync();
            var actualValue = (OkObjectResult)okResult.Result;

            Assert.AreEqual(expectedValue, actualValue.Value);
        }

        [Test]
        public async Task GetFundDistributionAsync_ReturnsOk()
        {
            var okResult = await portfolioController.GetFundDistributionAsync();
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        public async Task GetFundDistributionAsync_ReturnValuePass()
        {
            List<FundDistribution> expectedValue = new List<FundDistribution>()
            {
                new FundDistribution
                {
                    FundType = "Equity",
                    FundValue = 1000
                }
            };

            var okResult = await portfolioController.GetFundDistributionAsync();
            var actualValue = (OkObjectResult)okResult.Result;

            CollectionAssert.AreEqual(expectedValue, (List<FundDistribution>)actualValue.Value, new FundDistributionComparer());
        }

        [Test]
        public async Task GetPortfolioSnapshotAsync_ReturnsOk()
        {
            var okResult = await portfolioController.GetPortfolioSnapshotAsync();
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        public async Task GetPortfolioSnapshotAsync_ReturnValuePass()
        {
            List<PortfolioSnapshot> expectedValue = new List<PortfolioSnapshot>()
            {
                new PortfolioSnapshot()
                {
                    FundName = "HDFC Fund",
                    InvestmentValue = "1000",
                    CurrentValue = "1500",
                    Return = "50"
                }
            };

            var okResult = await portfolioController.GetPortfolioSnapshotAsync();
            var actualValue = (OkObjectResult)okResult.Result;

            CollectionAssert.AreEqual(expectedValue, (List<PortfolioSnapshot>)actualValue.Value, new PortfolioSnapshotComparer());
        }
    }
}
