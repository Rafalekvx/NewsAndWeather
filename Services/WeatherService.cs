using System.Net;
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

        if (responseMsg.StatusCode == HttpStatusCode.Unauthorized)
        {
            DateTime dateTime = DateTime.Now;
            Weather Example = new Weather() {DailyForecasts =
            {
                new DailyForecast() {Date = dateTime, 
                    Temperature = new Temperature()
                    {
                        Maximum = new Maximum() {Unit = "C",UnitType = 0,Value = 37}, 
                        Minimum = new Minimum(){Unit = "C",UnitType = 0,Value = 21}
                    },
                    Day = new Day()
                    {
                        Icon = 1, HasPrecipitation = false, IconPhrase = "Soo Hot", PrecipitationIntensity = "", PrecipitationType = ""
                    },
                    Night = new Night()
                    {
                        Icon = 33, HasPrecipitation = false, IconPhrase = "Soo Hot", PrecipitationIntensity = "", PrecipitationType = ""
                    }
                    
                }
                
            }};
            


            return Example;
        }
        
        
        Weather News = JsonConvert.DeserializeObject<Weather>(responseBody);

        return News;
        
        
    }


}