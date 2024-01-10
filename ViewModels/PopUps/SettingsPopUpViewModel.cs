using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NewsAndWeather.Services;
using Location = NewsAndWeather.Models.Location;

namespace NewsAndWeather.ViewModels.PopUps;

public partial class SettingsPopUpViewModel : BaseViewModel
{
    private ILocationService _locationService => DependencyService.Get<ILocationService>();
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
    [ObservableProperty]
    private bool _isLightTheme;
    public bool IsLightThemeToggled
    {
        get { return _isLightTheme; }
        set
        {
            _isLightTheme = value;
            OnPropertyChanged();
        }
    }

    public SettingsPopUpViewModel()
    {
        LocationPicker = new ObservableCollection<Location>();
        PrepareLocationPicker();
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

    [RelayCommand]
    public async void Save()
    {
        if (!(SelectedLocation is null))
        {
            ChangeLocation();
        }
        ChangeTheme();
        Application.Current.MainPage.DisplayAlert("Restart App to chenge Theme",
            "Restart App to chenge Theme.", "OK");

    }
    
    public async void ChangeLocation()
    {
        Preferences.Default.Set("Location", SelectedLocation.Name);
    }
    
    public async void ChangeTheme()
    {
        if (IsLightThemeToggled)
        {
            Preferences.Default.Set("DarkTheme", false);
        }
        else
        {
            Preferences.Default.Set("DarkTheme", true);
        }
    }
}