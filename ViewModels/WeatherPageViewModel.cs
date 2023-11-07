using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using NewsAndWeather.Models;
using NewsAndWeather.Services;

namespace NewsAndWeather.ViewModels;

public partial class WeatherPageViewModel : BaseViewModel
{
    public ObservableCollection<DailyForecast> Forecasts { get; }
    public IWeatherService _weatherService => DependencyService.Get<IWeatherService>();

    public WeatherPageViewModel()
    {
        Forecasts = new ObservableCollection<DailyForecast>();
    }
    
    [RelayCommand]
    public async void GetList()
    {
        // List<Post> posts = new List<Post>() { new Post() { ID = 1, Title = "Jeden", Description="Wiecej niz jeden", ImageLink="Linkacz" },
        //     new Post() { ID = 2, Title = "Dwa", Description = "Wiecej niz dwa", ImageLink = "Linkacz" } ,
        //     new Post() { ID = 3, Title = "Trzy", Description="Wiecej niz trzy", ImageLink="Linkacz" } };
        //
        // Posts.Clear();
        // foreach (Post post in posts)
        // {
        //     Posts.Add(post);
        // }

        Weather weather = await _weatherService.Get5DailyForecast(274663);
        
        List<DailyForecast> helperList = weather.DailyForecasts;
    
        if (helperList?.Count > 0)
        {
            Forecasts.Clear();
            foreach (DailyForecast forecast in helperList)
            {
                Forecasts.Add(forecast);
            }
        }
    }
    
}