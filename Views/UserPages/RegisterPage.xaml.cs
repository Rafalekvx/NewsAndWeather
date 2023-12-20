using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsAndWeather.ViewModels.User;

namespace NewsAndWeather.Views.UserPages;

public partial class RegisterPage : ContentPage
{
    private RegisterPageViewModel _viewmodel;
    public RegisterPage()
    {
        InitializeComponent();
        _viewmodel = new RegisterPageViewModel();
        this.BindingContext = _viewmodel;
    }
}