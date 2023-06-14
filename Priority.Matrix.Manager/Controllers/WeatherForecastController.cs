using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Priority.Matrix.Manager.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly ILoggerManager _logger;

		public WeatherForecastController(ILoggerManager logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<string> Get()
		{
			_logger.LogInfo("Here is info message from our values controller.");
			_logger.LogDebug("Here is debug message from our values controller.");
			_logger.LogWarn("Here is warn message from our values controller.");
			_logger.LogError("Here is an error message from our values controller.");

			return new string[] { "value1", "value2" };
		}
	}
}