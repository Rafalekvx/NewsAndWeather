

using System.Text.Json;

namespace NewsAndWeather.Services.Sup;

public class ApiUrls
{ 
        
        private static string _newsApi = "https://newsandweatherapi1.azurewebsites.net";
   // private static string _newsApi = "http://10.0.2.2:5011";
    private static string _weatherApi ="https://dataservice.accuweather.com";
    private static string _weatherApiKey = "Ag2SIT2UK5JJOIoubc8GN1yYYnZUQ1EQ";

    public static string NewsApi()
    {
            return _newsApi;
    }
    public static string WeatherApi()
    {
            return _weatherApi;
    }
    public static string WeatherApiKey()
    {
            return _weatherApiKey;
    }
}