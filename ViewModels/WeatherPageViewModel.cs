using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NewsAndWeather.Models;
using NewsAndWeather.Services;
using Location = NewsAndWeather.Models.Location;

namespace NewsAndWeather.ViewModels;

public partial class WeatherPageViewModel : BaseViewModel
{
    public ObservableCollection<DailyForecast> Forecasts { get; }
    public IWeatherService _weatherService => DependencyService.Get<IWeatherService>();
    public ILocationService _locationService => DependencyService.Get<ILocationService>();
    

    public ObservableCollection<Location> LocationPicker
    {
        get;
        set;
    }

    [ObservableProperty]
    private Location _locationPick;

    public Location SelectedLocation
    {
        get { return LocationPick; }
        set
        {
            LocationPick = value;
            OnPropertyChanged();
        }
    }


    [ObservableProperty] public string _sourcesGet;
    
    
    public WeatherPageViewModel()
    {
        Forecasts = new ObservableCollection<DailyForecast>();
        LocationPicker = new ObservableCollection<Location>();
        PrepareLocationPicker();
    }
    
    [RelayCommand]
    public async void GetList()
    {

        Weather weather = await _weatherService.Get5DailyForecast(SelectedLocation.Id);
        
        List<DailyForecast> helperList = weather.DailyForecasts;
    
        if (helperList?.Count > 0)
        {
            Forecasts.Clear();
            List<string> sourceHelperList = new List<string>(); 
            foreach (DailyForecast forecast in helperList)
            {
                forecast.Day.IconString = "i" + forecast.Day.Icon.ToString() + ".png";
                
                forecast.Night.IconString = "i" +forecast.Night.Icon.ToString() + ".png";
                
                Forecasts.Add(forecast);
                
                foreach (string source in forecast.Sources)
                {
                    if (!(sourceHelperList.Contains(source)))
                    {
                        SourcesGet = "";
                        sourceHelperList.Add(source);
                        SourcesGet += source;
                    }
                }
                
            }
            
            
        }
    }


    public async void PrepareLocationPicker()
    {
        List<Location> listOfLocations = await _locationService.GetAllLocations();
        
            listOfLocations = listOfLocations.OrderBy(r => r.Name).ToList();
            foreach (Location loc in listOfLocations)
            {
                LocationPicker.Add(loc);
            }

    }
    
}