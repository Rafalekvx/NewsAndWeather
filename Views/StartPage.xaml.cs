using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace NewsAndWeather.Views;

public partial class StartPage : ContentPage
{
    private int counter = 0;
    public StartPage()
    {
        InitializeComponent();
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

    private void LoginButton_OnClicked(object sender, EventArgs e)
    {
        
    }
}