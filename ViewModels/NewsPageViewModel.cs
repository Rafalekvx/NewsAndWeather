using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using NewsAndWeather.Models;
using NewsAndWeather.Services;

namespace NewsAndWeather.ViewModels;

public partial class NewsPageViewModel : BaseViewModel
{
    public ObservableCollection<Post> Posts { get; set; }

    public INewsService _newsService => DependencyService.Get<INewsService>();
    
    public NewsPageViewModel()
    {
    }

    [RelayCommand]
    public async void GetList()
    {
        List<Post> helperList = await _newsService.GetAllNews();
    
        if (helperList?.Count > 0)
        {
            Posts.Clear();
            foreach (Post post in helperList)
            {
                Posts.Add(post);
            }
        }
    }
    
    
    
}