using NewsAndWeather.Models;
using Location = NewsAndWeather.Models.Location;

namespace NewsAndWeather.Services;

public interface ILocationService
{
    Task<List<Location>> GetAllLocations();
}