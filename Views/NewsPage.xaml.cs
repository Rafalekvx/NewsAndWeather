using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAndWeather.ViewModels;

namespace NewsAndWeather.Views;

public partial class NewsPage : ContentPage
{
    private NewsPageViewModel _viewmodel;
    public NewsPage()
    {
        InitializeComponent();
        _viewmodel = new NewsPageViewModel();
        this.BindingContext = _viewmodel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewmodel.GetList();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        base.OnAppearing();
        _viewmodel.OnItemSelected(_viewmodel.LastestPost);
    }
}