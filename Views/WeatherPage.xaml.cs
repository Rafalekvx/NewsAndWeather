﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAndWeather.ViewModels;

namespace NewsAndWeather.Views;

public partial class WeatherPage : ContentPage
{
    private WeatherPageViewModel _viewmodel;

    public WeatherPage()
    {
        InitializeComponent();
        var inputView = LocationPicker.Children[1] as Entry;
        inputView.HorizontalTextAlignment = TextAlignment.Center;
        _viewmodel = new WeatherPageViewModel();
        this.BindingContext = _viewmodel;
        LocationPicker.SelectedIndex = _viewmodel._locationIndex;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewmodel.GetList();
    }

    private void LocationPicker_OnSelectedIndexChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        base.OnAppearing();
        _viewmodel.GetList();
    }
}