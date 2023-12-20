#if ANDROID
using Android.Content.Res;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NewsAndWeather.Services;
using NewsAndWeather.ViewModels;
using NewsAndWeather.ViewModels.User;
using NewsAndWeather.Views;
using NewsAndWeather.Views.UserPages;
using Syncfusion.Maui.Core.Hosting;

namespace NewsAndWeather;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SourceSans3-Italic-VariableFont_wght.ttf", "SourceSans3-Italic");
                fonts.AddFont("SourceSans3-VariableFont_wght.ttf", "SourceSans3");
                fonts.AddFont("NunitoSans.ttf", "NunitoSans");
                fonts.AddFont("NunitoSans-Italic.ttf", "NunitoSans-Italic");
                fonts.AddFont("Roboto-Regular.ttf", "Roboto");
                fonts.AddFont("Roboto-Light.ttf", "Roboto-Light");
                fonts.AddFont("Roboto-Medium.ttf", "Roboto-Medium");
                fonts.AddFont("Roboto-Black.ttf", "Roboto-Black");
                fonts.AddFont("RobotoCondensed.ttf", "RobotoCondensed");
                fonts.AddFont("Montserrat-Regular.ttf","Montserrat");
                fonts.AddFont("Montserrat-Medium.ttf","Montserrat-Medium");
                fonts.AddFont("Montserrat-Bold.ttf","Montserrat-Bold");
                fonts.AddFont("Montserrat-ExtraBold.ttf","Montserrat-ExtraBold");
            });
        
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#endif
        });

        //Views
        builder.Services.AddScoped<NewsPage>();
        builder.Services.AddScoped<WeatherPage>();
        builder.Services.AddScoped<DetailNewsPage>();
        builder.Services.AddScoped<StartPage>();
        builder.Services.AddScoped<LoginPage>();
        builder.Services.AddScoped<RegisterPage>();
        
        //ViewModels
        builder.Services.AddScoped<NewsPageViewModel>();
        builder.Services.AddScoped<WeatherPageViewModel>();
        builder.Services.AddScoped<DetailNewsPageViewModel>();
        builder.Services.AddScoped<LoginPageViewModel>();
        builder.Services.AddScoped<RegisterPageViewModel>();
        
        //Services
        builder.Services.AddScoped<INewsService, NewsService>();
        builder.Services.AddScoped<IWeatherService, WeatherService>();
        builder.Services.AddScoped<ILocationService, LocationService>();
        builder.Services.AddScoped<IUserServices, UserServices>();
        builder.Services.AddScoped<ICategoriesService, CategoriesService>();
        
        
        #if DEBUG
        builder.Logging.AddDebug();
        #endif

        return builder.Build();
    }
}