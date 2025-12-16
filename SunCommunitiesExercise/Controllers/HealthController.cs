using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunCommunitiesExercise.Models;
using static SunCommunitiesExercise.Constants;

namespace SunCommunitiesExercise.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController(ILogger<HealthController> logger) : ControllerBase
    {
        private readonly ILogger _logger = logger;

        [HttpGet]
        public async Task<HealthResult> GetHealth()
        {
            //Perform some checks on system health
            await Task.Delay(100);

            //Log and return response, for this if it is running it is healthy
            _logger.Log(LogLevel.Information, Logging.HEALTHY);
            return new() { Status = "ok" };
        }
    }
}
