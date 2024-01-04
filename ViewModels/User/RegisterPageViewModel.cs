using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
        if (!string.IsNullOrWhiteSpace(DisplayName) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
        {
            if (IsValidEmail(Email) && IsValidPassword(Password))
            {
                var toast = Toast.Make("Registering...", ToastDuration.Short, 12D);
                await toast.Show();
                string register = await _userService.RegisterUser(new RegisterDto()
                    { name = DisplayName, email = Email, password = Password });
                if (register == "created")
                {
                    Application.Current.MainPage.DisplayAlert("Registration was successful",
                        "Now you can log in to your account.", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Something went wrong",
                        "Something went wrong durning registration.", "OK");
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Something went wrong",
                    "Check the correctness of the email address entered or adjust the password to meet the password security requirements - The password must have one capital letter, one number and have a minimum of 8 characters.", "OK");
            }
        }
        else
        {
            Application.Current.MainPage.DisplayAlert("Something went wrong",
                "All fields cannot be empty!", "OK");
        }
    }
    
    public bool IsValidEmail(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);

            if (emailaddress.EndsWith(".")) {
                return false; // suggested by @TK-421
            }
            
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    
    public bool IsValidPassword(string password)
    {
        Regex hasNumber = new Regex(@"[0-9]+");
        Regex hasUpperChar = new Regex(@"[A-Z]+");
        Regex hasMinimum8Chars = new Regex(@".{8,}");
        bool isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
        
        return isValidated;
    }

    [RelayCommand]
    public async void Back()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}