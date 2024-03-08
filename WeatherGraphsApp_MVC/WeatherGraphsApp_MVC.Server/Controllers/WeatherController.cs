namespace WeatherGraphsApp_MVC.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherData _weatherData;
        public WeatherController(IWeatherData weatherData)
        {
            _weatherData = weatherData;
        }
        [HttpGet]
        public async Task<IActionResult> GetWeatherDataForToday()
        {
            try
            {
                var weatherRecords = await _weatherData.GetWeatherRecordsByDate(DateTime.Today);
                return Ok(weatherRecords);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
