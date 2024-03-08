using Newtonsoft.Json;

namespace WeatherGraphsApp_MVC.Server.Services
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly string _apiUrl = "https://api.openweathermap.org/data/2.5/weather";
        private readonly string _apiKey = "aa0999a6a80f7764e91b473dd87cc54c";
        private readonly List<CityModel> cities = new()
        {
            new CityModel("Rezekne", "LV"),
            new CityModel("Riga", "LV"),
            new CityModel("Marseille", "FR"),
            new CityModel("Paris", "FR")
        };
        private readonly ILogger<OpenWeatherMapService> _logger;
        private readonly IWeatherData _weatherData;

        public OpenWeatherMapService(ILogger<OpenWeatherMapService> logger, IWeatherData weatherData)
        {
            _logger = logger;
            _weatherData = weatherData;
        }

        public void FetchWeatherData()
        {
            foreach (var city in cities)
            {
                BackgroundJob.Enqueue(() => ProcessWeatherData(city));
            }
        }

        public async Task ProcessWeatherData(CityModel city)
        {
            try
            {
                using var httpClient = new HttpClient();

                string url = $"{_apiUrl}?q={city.City},{city.Country}&appid={_apiKey}&units=metric";
                _logger.LogInformation($"Sending weather data request to {url}");

                HttpResponseMessage response = await httpClient.GetAsync(url);
                _logger.LogInformation($"Received response: {response}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    float temperature = data.main.temp;

                    WeatherModel w = new()
                    {
                        City = city.City,
                        Country = city.Country,
                        Temperature = temperature,
                        CreatedDateTime = DateTime.Now
                    };

                    _logger.LogInformation($"Inserting record for {w.City}, {w.Country}");
                    await _weatherData.CreateWeatherRecord(w);
                    _logger.LogInformation($"Successfully inserted record with id {w.Id} for {w.City}, {w.Country}");
                }
                else
                {
                    _logger.LogInformation($"Received response: {response} with status code {response.StatusCode}. No record will be inserted to database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
