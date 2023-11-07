using NewsAndWeather.Models;
using Newtonsoft.Json;

namespace NewsAndWeather.Services;

public class WeatherService : IWeatherService
{
    
    private HttpClient _client = new HttpClient();

    private string url ="https://dataservice.accuweather.com/";
    private string apikey = "Ag2SIT2UK5JJOIoubc8GN1yYYnZUQ1EQ";
    
    public async Task<Weather> Get5DailyForecast(int locationID)
    {
        string requestUrl = $"/forecasts/v1/daily/5day/{locationID}?apikey={apikey}&metric=true";
        
        var responseMsg = _client.GetAsync(url + requestUrl).Result;
        
        responseMsg.EnsureSuccessStatusCode();
        string responseBody = await responseMsg.Content.ReadAsStringAsync();
        
        Weather News = JsonConvert.DeserializeObject<Weather>(responseBody);

        return News;
        
        
    }


}