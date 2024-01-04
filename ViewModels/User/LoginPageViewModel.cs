using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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

    private bool _isntBusy = true;
    
    public bool IsntBusy
    {
        get { return _isntBusy; }
        set { _isntBusy = value; }
    }
    
    [RelayCommand]
    public async void Back()
    {
        if (IsntBusy)
        {
            if (Application.Current.MainPage.Navigation.NavigationStack.Count <= 2)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                Application.Current.MainPage = new AppShell();
            }
        }
    }

    [RelayCommand]
    public async void GoToRegister()
    {
        if (IsntBusy)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }

    [RelayCommand]
    public async void Login()
    {
        if (IsntBusy)
        {
            var toast = Toast.Make("Logging to your account...", ToastDuration.Long, 12D);
            await toast.Show();
            IsntBusy = false;
            string login = await _userService.LoginUser(new LoginDto() { email = Email, password = Password });
            if (!(string.IsNullOrWhiteSpace(login)))
            {
                Application.Current.MainPage = new AppShell();

                if (Preferences.Default.ContainsKey("ApiKey") || Preferences.Default.ContainsKey("LoginDate") ||
                    Preferences.Default.ContainsKey("Email"))
                {
                    Preferences.Default.Remove("ApiKey");
                    Preferences.Default.Remove("LoginDate");
                    Preferences.Default.Remove("Email");
                }

                Preferences.Default.Set("ApiKey", login);
                Preferences.Default.Set("LoginDate", DateTime.Now);
                Preferences.Default.Set("Email", Email);
                
                var secondToast = Toast.Make("Successful login!", ToastDuration.Short, 12D);
                await secondToast.Show();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Something went wrong",
                    "Something went wrong durning login.", "OK");
            }

            IsntBusy = true;
        }
    }
}