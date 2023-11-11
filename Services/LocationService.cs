using Newtonsoft.Json;
using Location = NewsAndWeather.Models.Location;

namespace NewsAndWeather.Services;

public class LocationService : ILocationService
{
    private HttpClient _client = new HttpClient();

    private string url ="https://newsandweatherapi1.azurewebsites.net";
    
    
    public async Task<List<Location>> GetAllLocations()
    {
        string requestUrl = "/api/locations";
        try
        {
            var responseMsg = _client.GetAsync(url + requestUrl).Result;
            
            string responseBody = await responseMsg.Content.ReadAsStringAsync();
        
            if (!(responseMsg.IsSuccessStatusCode))
            {
                List<Location> secondList = new List<Location>() { new Location() { Id = 1, Name = "Api off" } };

                return secondList;
            }

            List<Location> listOfLocations = JsonConvert.DeserializeObject<List<Location>>(responseBody);

            return listOfLocations;
        }

        catch(Exception ex)
        {
            List<Location> secondList = new List<Location>() { new Location() { Id = 1, Name = "Api off" } };

            return secondList;
        }
        
    }
}