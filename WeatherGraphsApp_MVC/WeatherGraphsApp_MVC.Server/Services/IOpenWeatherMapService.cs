namespace WeatherGraphsApp_MVC.Server.Services
{
    public interface IOpenWeatherMapService
    {
        void FetchWeatherData();
        Task ProcessWeatherData(CityModel city);
    }
}