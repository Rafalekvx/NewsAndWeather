using NewsAndWeather.Models;

namespace NewsAndWeather.ViewModels;

public class NewsPageViewModel : BaseViewModel
{
    public List<Post> Posts { get; set; }
}