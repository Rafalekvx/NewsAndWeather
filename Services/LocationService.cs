using Newtonsoft.Json;
using Location = NewsAndWeather.Models.Location;

namespace NewsAndWeather.Services;

public class LocationService : ILocationService
{
    private HttpClient _client = new HttpClient();

    private string url ="http://localhost:5011";
    
    
    public async Task<List<Location>> GetAllLocations()
    {
        string requestUrl = "/api/locations";
        
        var responseMsg = _client.GetAsync(url + requestUrl).Result;
        
        responseMsg.EnsureSuccessStatusCode();
        string responseBody = await responseMsg.Content.ReadAsStringAsync();

        List<Location> listOfLocations = JsonConvert.DeserializeObject<List<Location>>(responseBody);

        return listOfLocations;
    }
}