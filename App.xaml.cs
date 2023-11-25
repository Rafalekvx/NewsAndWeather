using System.Net;
using System.Text.Json;
using NewsAndWeather.Services;

namespace NewsAndWeather;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        string text = LoadMauiAsset("SyncfusionLicense.json");
        SyncfusionLicense license = JsonSerializer.Deserialize<SyncfusionLicense>(text);
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(license.License);
        Application.Current.UserAppTheme = AppTheme.Dark;
        DependencyService.Register<NewsService>();
        DependencyService.Register<WeatherService>();
        DependencyService.Register<LocationService>();
        DependencyService.Register<UserServices>();
        DependencyService.Register<CategoriesService>();
        MainPage = new AppShell();
    }
    
    string LoadMauiAsset(string filename)
    {
        using var stream = FileSystem.OpenAppPackageFileAsync(filename).Result;
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}