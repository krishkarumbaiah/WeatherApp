using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _client;
        private readonly WeatherApiOptions _options;

        public WeatherService(HttpClient client, IOptions<WeatherApiOptions> options)
        {
            _client = client;
            _options = options.Value;
        }

        public async Task<CityWeatherViewModel?> GetCityWeatherAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return null;

            var url = $"{_options.BaseUrl}/weather?q={city}&appid={_options.ApiKey}&units=metric";

            try
            {
                var response = await _client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<OpenWeatherResponse>(
                    json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (data == null)
                    return null;

                var weather = data.Weather.FirstOrDefault();

                return new CityWeatherViewModel
                {
                    City = data.Name,
                    TemperatureC = data.Main.Temp,
                    Humidity = data.Main.Humidity,
                    Condition = weather?.Description ?? "N/A",
                    IconUrl = weather != null
                        ? $"https://openweathermap.org/img/wn/{weather.Icon}@2x.png"
                        : string.Empty
                };
            }
            catch
            {
                // For real app, log error
                return null;
            }
        }
    }
}
