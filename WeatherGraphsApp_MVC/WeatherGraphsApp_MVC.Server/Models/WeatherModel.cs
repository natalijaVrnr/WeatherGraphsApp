using System.ComponentModel.DataAnnotations;

namespace WeatherGraphsApp_MVC.Server.Models
{
    public class WeatherModel
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public float Temperature { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
