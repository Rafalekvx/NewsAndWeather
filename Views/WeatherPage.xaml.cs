using System;
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
        _viewmodel = new WeatherPageViewModel();
        this.BindingContext = _viewmodel;
        LocationPicker.SelectedIndex = 0;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        _viewmodel.GetList();
    }

    private void LocationPicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        _viewmodel.SelectedLocationID = int.Parse(LocationPicker.SelectedValue.ToString());
        base.OnAppearing();
    }
}