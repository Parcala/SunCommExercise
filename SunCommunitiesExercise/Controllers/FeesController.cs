using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunCommunitiesExercise.Models;
using SunCommunitiesExercise.Services;
using static SunCommunitiesExercise.Constants;

namespace SunCommunitiesExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesController(ILogger<FeesController> logger, IFeeCalculator feeCalculator) 
        : ControllerBase
    {
        private readonly ILogger _logger = logger;
        private readonly IFeeCalculator _feeCalculator = feeCalculator;

        [HttpGet("estimate")]
        public async Task<FeeResult> CalculateFee([FromQuery] decimal amount, [FromQuery] bool preferred)
        {
            _logger.Log(LogLevel.Information, Logging.CALCULATION_STARTED);
            var fee = _feeCalculator.Calculate(amount, preferred);
            return fee;
        }
    }
}
