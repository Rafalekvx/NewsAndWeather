using NewsAndWeather.Services;

namespace NewsAndWeather;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        Application.Current.UserAppTheme = AppTheme.Dark;
        DependencyService.Register<NewsService>();
        DependencyService.Register<WeatherService>();
        DependencyService.Register<LocationService>();
        MainPage = new AppShell();
    }
}