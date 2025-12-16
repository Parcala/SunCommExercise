using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SunCommunitiesExercise.Models;
using SunCommunitiesExercise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunCommunitiesExerciseTests.ServiceTests
{
    public class FeeCalculatorTests
    {
        private IFeeCalculator _feeCalculator;
        private Mock<ILogger<FeeCalculator>> _logger;
        private IOptions<FeeOptions> _options;

        public FeeCalculatorTests()
        {
            _logger = new Mock<ILogger<FeeCalculator>>();
            _options = Options.Create(new FeeOptions()
            {
                BaseRate = 0.05m,
                PreferredCustomerDiscount = 0.01m,
                MaxFee = 250m
            });

            _feeCalculator = new FeeCalculator(_logger.Object, _options);
        }

        [Fact]
        public async Task NonPreferred__FeeCalculations()
        {
            //Arrange
            decimal amount = 100m;
            bool preferred = false;
            decimal expectedFee = 5m;

            //Act
            var result = _feeCalculator.Calculate(amount, preferred);

            //Assert
            Assert.Equal(result.BaseRate, result.EffectiveRate);
            Assert.False(result.Preferred);
            Assert.False(result.Capped);
            Assert.Equal(result.CalculatedFee, expectedFee);
        }

        [Fact]
        public async Task Preferred__FeeCalculations()
        {
            //Arrange
            decimal amount = 100m;
            bool preferred = true;
            decimal expectedFee = 4m;

            //Act
            var result = _feeCalculator.Calculate(amount, preferred);

            //Assert
            Assert.NotEqual(result.BaseRate, result.EffectiveRate);
            Assert.True(result.Preferred);
            Assert.False(result.Capped);
            Assert.Equal(result.CalculatedFee, expectedFee);
        }

        [Fact]
        public async Task Cap__Calculations()
        {
            //Arrange
            decimal amount = 10000m;
            bool preferred = false;
            decimal expectedFee = 250m;

            //Act
            var result = _feeCalculator.Calculate(amount, preferred);

            //Assert
            Assert.Equal(result.BaseRate, result.EffectiveRate);
            Assert.False(result.Preferred);
            Assert.True(result.Capped);
            Assert.Equal(result.CalculatedFee, expectedFee);
        }
    }
}
