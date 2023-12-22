using System.Net;
using CommunityToolkit.Maui.Views;
using NewsAndWeather.Views.UserPages;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace NewsAndWeather.PopUps;

public partial class LoginPopUp : Popup
{

    string ApiKey = Preferences.Default.Get("ApiKey", "");
    public LoginPopUp()
    {
        InitializeComponent();
        if (!(string.IsNullOrWhiteSpace(ApiKey)))
        {
            LoginButton.IsVisible = false;
            RegisterButton.IsVisible = false;
            SignoutButton.IsVisible = true;
        }
        else
        {
            LoginButton.IsVisible = true;
            RegisterButton.IsVisible = true;
            SignoutButton.IsVisible = false;
        }
    }

    private void LoginButton_OnClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new LoginPage());
        this.Close();
    }
    private void RegisterButton_OnClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RegisterPage());
        this.Close();
    }
    private void SignoutButton_OnClicked(object sender, EventArgs e)
    {
        Preferences.Default.Remove("ApiKey");
        Preferences.Default.Remove("LoginDate");
        Preferences.Default.Remove("Email");
        this.Close();
    }
    private void CancelButton_OnClicked(object sender, EventArgs e)
    {
        this.Close();
    }
}