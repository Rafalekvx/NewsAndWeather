using System.Net.Mail;
using System.Text.RegularExpressions;

namespace NewsAndWeather.ViewModels.User;

public partial class UserBaseViewModel : BaseViewModel
{
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
}