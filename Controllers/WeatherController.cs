using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Empty page model initially
            return View(new WeatherPageViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string city)
        {
            var model = new WeatherPageViewModel();

            var result = await _weatherService.GetCityWeatherAsync(city);

            if (result == null)
            {
                model.ErrorMessage = $"Could not find weather for '{city}'.";
            }
            else
            {
                model.CurrentCity = result;
            }

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Compare(string city1, string city2)
        {
            var model = new WeatherPageViewModel();

            var w1 = await _weatherService.GetCityWeatherAsync(city1);
            var w2 = await _weatherService.GetCityWeatherAsync(city2);

            if (w1 == null || w2 == null)
            {
                model.ErrorMessage = "Could not fetch weather for one or both cities.";
            }
            else
            {
                model.City1 = w1;
                model.City2 = w2;
            }

            return View("Index", model);
        }
    }
}
