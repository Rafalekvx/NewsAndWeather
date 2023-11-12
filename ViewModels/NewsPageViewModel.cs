using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NewsAndWeather.Models;
using NewsAndWeather.Services;
using NewsAndWeather.Views;

namespace NewsAndWeather.ViewModels;

public partial class NewsPageViewModel : BaseViewModel
{
    public ObservableCollection<NewsView> Posts { get; }

    [ObservableProperty] 
    public NewsView _lastestPost;

    public NewsView lastestPost
    {
        get { return LastestPost;}
        set { LastestPost = value; }
    }

    public INewsService _newsService => DependencyService.Get<INewsService>();
    
    public Command<NewsView> ItemTapped { get; }
    
    public NewsPageViewModel()
    {
        Posts = new ObservableCollection<NewsView>();
        ItemTapped = new Command<NewsView>(OnItemSelected);
    }

    [RelayCommand]
    public async void GetList()
    {
        List<NewsView> helperList = await _newsService.GetAllNews();
        
        if (helperList?.Count > 0)
        {
            helperList = helperList.OrderByDescending(a=> a.ID).ToList();
            Posts.Clear();
            foreach (NewsView post in helperList)
            {
                Posts.Add(post);
            }
        }

        lastestPost = helperList.Last();

    }
    
    public async void OnItemSelected(NewsView post)
    {
        if (post == null)
        {
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(DetailNewsPage)}?{nameof(DetailNewsPageViewModel.ItemId)}={post.ID}");

    }
    
    
    
    
}