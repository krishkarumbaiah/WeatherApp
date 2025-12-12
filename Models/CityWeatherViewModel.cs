namespace WeatherApp.Models
{
    public class CityWeatherViewModel
    {
        public string City { get; set; } = string.Empty;
        public double TemperatureC { get; set; }
        public int Humidity { get; set; }
        public string Condition { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
    }
}
