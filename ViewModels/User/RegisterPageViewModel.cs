using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NewsAndWeather.Models;
using NewsAndWeather.Services;
using NewsAndWeather.Views.UserPages;

namespace NewsAndWeather.ViewModels.User;

public partial class RegisterPageViewModel : BaseViewModel
{
    
    private IUserServices _userService => DependencyService.Get<IUserServices>();
    
    private string _email;
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }
    
    private string _displayName;
    public string DisplayName
    {
        get { return _displayName; }
        set { _displayName = value; }
    }
    
    private string _password;
    public string Password
    {
        get { return _password; }
        set { _password = value; }
    }

    [RelayCommand]
    public async void Register()
    {
        string register = await _userService.RegisterUser(new RegisterDto() { name = DisplayName, email = Email, password = Password});
        if (register == "created")
        {
            Application.Current.MainPage.DisplayAlert("registration was successful",
                "Now log in to the account you created.", "OK");
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }

    [RelayCommand]
    public async void Back()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}