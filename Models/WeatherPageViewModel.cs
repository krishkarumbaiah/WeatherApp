namespace WeatherApp.Models
{
    public class WeatherPageViewModel
    {
        public CityWeatherViewModel? CurrentCity { get; set; }

        public CityWeatherViewModel? City1 { get; set; }
        public CityWeatherViewModel? City2 { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
