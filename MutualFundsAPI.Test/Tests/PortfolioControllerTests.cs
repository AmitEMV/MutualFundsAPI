using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MutualFundsAPI.Controllers;
using MutualFundsAPI.Interfaces;
using MutualFundsAPI.Models;
using MutualFundsAPI.Test.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;

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
            InvestmentStatus expectedValue = new InvestmentStatus
            {
                InvestmentValue = 1000,
                CurrentValue = 1500
            };

            var okResult = await portfolioController.GetInvestmentReturnsValueAsync();
            var actualValue = (OkObjectResult)okResult.Result;

            expectedValue.Should().BeEquivalentTo((InvestmentStatus)actualValue.Value);
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

            expectedValue.Should().BeEquivalentTo((List<FundDistribution>)actualValue.Value);
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
                    FundId = 1,
                    PortfolioId = 1000,
                    FundName = "HDFC Fund",
                    InvestmentValue = 1000,
                    CurrentValue = 1500,
                    Return = 50
                }
            };

            var okResult = await portfolioController.GetPortfolioSnapshotAsync();
            var actualValue = (OkObjectResult)okResult.Result;

            expectedValue.Should().BeEquivalentTo((List<PortfolioSnapshot>)actualValue.Value);
        }

        [Test]
        public async Task DeleteFundFromPortfolioAsync_ReturnsOk()
        {
            var okResult = await portfolioController.DeleteFundFromPortfolioAsync(new PortfolioSnapshot());
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
            var actualValue = (OkObjectResult)okResult.Result;
            bool expectedValue = true;
            expectedValue.Should().Equals(actualValue);
        }

        [Test]
        public async Task AddFundToPortfolioAsync_ReturnsOk()
        {
            var okResult = await portfolioController.AddFundToPortfolioAsync(new FundInfo());
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
            var actualValue = (OkObjectResult)okResult.Result;
            bool expectedValue = true;
            expectedValue.Should().Equals(actualValue);
        }
    }
}
