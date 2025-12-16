using Microsoft.Extensions.Options;
using SunCommunitiesExercise.Models;
using static SunCommunitiesExercise.Constants;

namespace SunCommunitiesExercise.Services
{
    public class FeeCalculator(ILogger<FeeCalculator> logger, IOptions<FeeOptions> options) 
        : IFeeCalculator
    {
        private readonly ILogger<FeeCalculator> _logger = logger;
        private readonly IOptions<FeeOptions> _options = options;

        public FeeResult Calculate(decimal amount, bool preferredCustomer)
        {
            FeeResult result = new()
            {
                BaseRate = _options.Value.BaseRate,
                Amount = amount,
                Preferred = preferredCustomer,
                EffectiveRate = _options.Value.BaseRate
            };

            if(preferredCustomer)
            {
                result.EffectiveRate -= _options.Value.PreferredCustomerDiscount;
            }

            result.CalculatedFee = result.Amount * result.EffectiveRate;

            if(result.CalculatedFee > _options.Value.MaxFee)
            {
                result.CalculatedFee = _options.Value.MaxFee;
                result.Capped = true;
                _logger.Log(LogLevel.Warning, Logging.FEE_CAPPED);
            }

            return result;
        }
    }
}
