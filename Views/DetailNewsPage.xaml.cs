using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAndWeather.ViewModels;

namespace NewsAndWeather.Views;

public partial class DetailNewsPage : ContentPage
{
    private DetailNewsPageViewModel _viewmodel;
    public DetailNewsPage()
    {
        InitializeComponent();

        _viewmodel = new DetailNewsPageViewModel();
        this.BindingContext = _viewmodel;
    }
}