using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using NewsAndWeather.Models;
using NewsAndWeather.ViewModels.User;
using NewsAndWeather.Views.UserPages;

namespace NewsAndWeather.Views;

public partial class StartPage : ContentPage
{
    private int counter = 0;

    public StartPage()
    {
        InitializeComponent();
        string ApiKey = Preferences.Default.Get("ApiKey", "");
        DateTime LoginDate = Preferences.Default.Get("LoginDate", DateTime.Now.Subtract(TimeSpan.FromDays(91))); 
        if (!(string.IsNullOrWhiteSpace(ApiKey)))
        {
            if (DateTime.Now < LoginDate.AddDays(89))
            {
                LoginButton.IsVisible = false;
                LoginLabel.IsVisible = false;
                HeaderLabel.Text = "WELCOME BACK!";
                SecondLineLabel.Text = $"{Preferences.Default.Get("Email","")} Nice to see you coming back to us!";
            }
            else
            {
                Preferences.Default.Remove("ApiKey");
                Preferences.Default.Remove("LoginDate");
                Preferences.Default.Remove("Email");
            }
        }
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AppShell();
    }
    private async void CloseButton_OnClicked(object sender, EventArgs e)
    {
        var toast = Toast.Make("News are loading", ToastDuration.Long, 12D);
        await toast.Show();
        counter++;
        if (counter == 1)
        {
            Application.Current.MainPage = new AppShell();
        }
    }

    private async void LoginButton_OnClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
    }
}