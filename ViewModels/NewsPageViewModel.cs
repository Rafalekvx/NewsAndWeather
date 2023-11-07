using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using NewsAndWeather.Models;
using NewsAndWeather.Services;

namespace NewsAndWeather.ViewModels;

public partial class NewsPageViewModel : BaseViewModel
{
    public ObservableCollection<Post> Posts { get; }

    public INewsService _newsService => DependencyService.Get<INewsService>();
    
    public NewsPageViewModel()
    {
        Posts = new ObservableCollection<Post>();
    }

    [RelayCommand]
    public async void GetList()
    {
        // List<Post> posts = new List<Post>() { new Post() { ID = 1, Title = "Jeden", Description="Wiecej niz jeden", ImageLink="Linkacz" },
        //     new Post() { ID = 2, Title = "Dwa", Description = "Wiecej niz dwa", ImageLink = "Linkacz" } ,
        //     new Post() { ID = 3, Title = "Trzy", Description="Wiecej niz trzy", ImageLink="Linkacz" } };
        //
        // Posts.Clear();
        // foreach (Post post in posts)
        // {
        //     Posts.Add(post);
        // }

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