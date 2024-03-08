namespace WeatherGraphsApp_MVC.Server.Models
{
    public class CityModel
    {
        public string City { get; set; }
        public string Country { get; set; }
        public CityModel(string city, string country)
        {
            City = city;
            Country = country;
        }
    }
}
