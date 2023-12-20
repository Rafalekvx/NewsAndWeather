using NewsAndWeather.Views;
using NewsAndWeather.Views.UserPages;

namespace NewsAndWeather;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(DetailNewsPage), typeof(DetailNewsPage));
    }
}