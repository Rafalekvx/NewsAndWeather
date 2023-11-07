using NewsAndWeather.Models;

namespace NewsAndWeather.Services;

public interface IWeatherService
{ 
    Task<Weather> Get5DailyForecast(int locationID);
}