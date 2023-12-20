using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAndWeather.ViewModels.User;

namespace NewsAndWeather.Views.UserPages;

public partial class LoginPage : ContentPage
{
    private LoginPageViewModel _viewmodel;
    public LoginPage()
    {
        InitializeComponent();
        _viewmodel = new LoginPageViewModel();
        this.BindingContext = _viewmodel;
    }
}