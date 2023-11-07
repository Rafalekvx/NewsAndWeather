using NewsAndWeather.Models;
using Newtonsoft.Json;

namespace NewsAndWeather.Services;

public class WeatherService : IWeatherService
{
    
    private HttpClient _client = new HttpClient();

    private string url ="https://dataservice.accuweather.com/";
    
    public async Task<Weather> Get5DailyForecast(int locationID)
    {
        string one = $"/forecasts/v1/daily/5day/{locationID}?apikey=Ag2SIT2UK5JJOIoubc8GN1yYYnZUQ1EQ&metric=true";
        
        var responseMsg = _client.GetAsync(url + one).Result;
        
        responseMsg.EnsureSuccessStatusCode();
        string responseBody = await responseMsg.Content.ReadAsStringAsync();
        
        Weather News = JsonConvert.DeserializeObject<Weather>(responseBody);

        return News;
        
        
    }


}