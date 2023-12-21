using CommunityToolkit.Mvvm.Input;
using NewsAndWeather.Models;
using NewsAndWeather.Services;
using NewsAndWeather.Views.UserPages;

namespace NewsAndWeather.ViewModels.User;

public partial class LoginPageViewModel : BaseViewModel
{
    private IUserServices _userService => DependencyService.Get<IUserServices>();
    private string _email;
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }
    
    private string _password;
    public string Password
    {
        get { return _password; }
        set { _password = value; }
    }
    
    [RelayCommand]
    public async void Back()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }

    [RelayCommand]
    public async void GoToRegister()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
    }

    [RelayCommand]
    public async void Login()
    { 
        string login = await _userService.LoginUser(new LoginDto() { email = Email, password = Password});
        if (!(string.IsNullOrWhiteSpace(login)))
        {
            Application.Current.MainPage.DisplayAlert("Login was successful",
                $"{login}", "OK");
           Application.Current.MainPage = new AppShell();

           if (Preferences.Default.ContainsKey("ApiKey") || Preferences.Default.ContainsKey("LoginDate") || Preferences.Default.ContainsKey("Email"))
           {
               Preferences.Default.Remove("ApiKey");
               Preferences.Default.Remove("LoginDate");
               Preferences.Default.Remove("Email");
           }
           Preferences.Default.Set("ApiKey", login);
           Preferences.Default.Set("LoginDate", DateTime.Now);
           Preferences.Default.Set("Email", Email);
        }
        else
        {
            Application.Current.MainPage.DisplayAlert("Something went wrong",
                "Something went wrong durning registration.", "OK");
        }
    }
}