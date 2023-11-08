using Microsoft.AspNetCore.Mvc;
using NewsAndWeather.Models;
using NewsAndWeatherAPI.Services;

namespace NewsAndWeatherAPI.Controllers;

[Route("api/locations")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;
    
    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }
    
    [HttpGet]
    public ActionResult<List<Location>> GetAll()
    {
        List<Location> listOfLocations = _locationService.GetAll();

        return Ok(listOfLocations);
    }
    
}