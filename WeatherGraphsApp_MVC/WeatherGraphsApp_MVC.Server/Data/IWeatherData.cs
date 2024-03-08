using WeatherGraphsApp_MVC.Server.Models;

namespace WeatherGraphsApp_MVC.Server.Data
{
    public interface IWeatherData
    {
        Task CreateWeatherRecord(WeatherModel w);
        Task<List<WeatherModel>> GetAllWeatherRecords();
        Task<List<WeatherModel>> GetWeatherRecordsByDate(DateTime date);
    }
}