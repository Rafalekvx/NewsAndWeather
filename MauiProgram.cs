using Microsoft.Extensions.Logging;
using NewsAndWeather.Services;
using NewsAndWeather.ViewModels;
using NewsAndWeather.Views;

namespace NewsAndWeather;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        //Views
        builder.Services.AddScoped<NewsPage>();
        builder.Services.AddScoped<WeatherPage>();
        
        //ViewModels
        builder.Services.AddScoped<NewsPageViewModel>();
        builder.Services.AddScoped<WeatherPageViewModel>();
        
        //Services
        builder.Services.AddScoped<INewsService, NewsService>();
        builder.Services.AddScoped<IWeatherService, WeatherService>();
        
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}