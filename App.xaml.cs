﻿using NewsAndWeather.Services;

namespace NewsAndWeather;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        DependencyService.Register<NewsService>();
        DependencyService.Register<WeatherService>();
        MainPage = new AppShell();
    }
}