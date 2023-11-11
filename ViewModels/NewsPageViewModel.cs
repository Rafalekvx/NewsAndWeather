using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NewsAndWeather.Models;
using NewsAndWeather.Services;
using NewsAndWeather.Views;

namespace NewsAndWeather.ViewModels;

public partial class NewsPageViewModel : BaseViewModel
{
    public ObservableCollection<Post> Posts { get; }

    [ObservableProperty] 
    public Post _lastestPost;

    public Post lastestPost
    {
        get { return LastestPost;}
        set { LastestPost = value; }
    }

    public INewsService _newsService => DependencyService.Get<INewsService>();
    
    public Command<Post> ItemTapped { get; }
    
    public NewsPageViewModel()
    {
        Posts = new ObservableCollection<Post>();
        ItemTapped = new Command<Post>(OnItemSelected);
    }

    [RelayCommand]
    public async void GetList()
    {
        List<Post> helperList = await _newsService.GetAllNews();
        
        if (helperList?.Count > 0)
        {
            helperList = helperList.OrderByDescending(a=> a.ID).ToList();
            Posts.Clear();
            foreach (Post post in helperList)
            {
                Posts.Add(post);
            }
        }

        lastestPost = helperList.Last();

    }
    
    async void OnItemSelected(Post post)
    {
        if (post == null)
        {
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(DetailNewsPage)}?{nameof(DetailNewsPageViewModel.ItemId)}={post.ID}");

    }
    
    
    
}