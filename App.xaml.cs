using System.Net;
using NewsAndWeather.Models;
using NewsAndWeather.Services;
using NewsAndWeather.Views;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace NewsAndWeather;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        string text = LoadMauiAsset("SyncfusionLicense.json");
        SyncfusionLicense license = JsonSerializer.Deserialize<SyncfusionLicense>(text);
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(license.License);
        LoadSettings();
        if (Preferences.Get("DarkTheme", true))
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        DependencyService.Register<NewsService>();
        DependencyService.Register<WeatherService>();
        DependencyService.Register<LocationService>();
        DependencyService.Register<UserServices>();
        DependencyService.Register<CategoriesService>();
        MainPage = new NavigationPage(new StartPage());
    }
    
    string LoadMauiAsset(string filename)
    {
        using var stream = FileSystem.OpenAppPackageFileAsync(filename).Result;
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }

    void LoadSettings()
    {
        if (!Preferences.Default.ContainsKey("DarkTheme") || !Preferences.Default.ContainsKey("Location"))
        {
            Preferences.Default.Set("DarkTheme", true);
            Preferences.Default.Set("Location", "nothing");
        }
        var startPath = FileSystem.Current.AppDataDirectory;
        var combinedPath = Path.Combine(startPath, "ListOfUserTags.json");
        List<UserCategory> fileContent = new List<UserCategory>();
        if (!(File.Exists(combinedPath)))
        {
            File.WriteAllText(combinedPath, JsonConvert.SerializeObject(fileContent));
        }
    }
}