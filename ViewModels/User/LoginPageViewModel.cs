using CommunityToolkit.Mvvm.Input;
using NewsAndWeather.Views.UserPages;

namespace NewsAndWeather.ViewModels.User;

public partial class LoginPageViewModel : BaseViewModel
{
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
    public void Login()
    { 
        Application.Current.MainPage = new AppShell();
    }
}