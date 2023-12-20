using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using NewsAndWeather.Models;
using NewsAndWeather.Services;
using NewsAndWeather.Views;

namespace NewsAndWeather.ViewModels;

public partial class NewsPageViewModel : BaseViewModel
{
    public static NewsView SelectedItem { get; set; }  
    public List<NewsView> PostsOneTake { get; set; }
    public ObservableRangeCollection<NewsView> Posts { get; }
    
    public ObservableRangeCollection<Category> CategoriesList { get; } 

    private DateTime lastestUpdate;
    
    [ObservableProperty] 
    public NewsView _lastestPost;

    public NewsView lastestPost
    {
        get { return LastestPost;}
        set { LastestPost = value; }
    }

    private INewsService _newsService => DependencyService.Get<INewsService>();
    private ICategoriesService _categoriesService => DependencyService.Get<ICategoriesService>();
    public Command<NewsView> ItemTapped { get; }
    public Command<Category> CategoryTapped { get; }
    
    public NewsPageViewModel()
    {
        Posts = new ObservableRangeCollection<NewsView>();
        CategoriesList = new ObservableRangeCollection<Category>();
        ItemTapped = new Command<NewsView>(OnItemSelected);
        CategoryTapped = new Command<Category>(FiltrBy);
        PostsOneTake = new List<NewsView>();
        lastestUpdate = DateTime.Now.Subtract(TimeSpan.FromMinutes(15));
    }

    [RelayCommand]
    public async void GetList()
    {
        List<NewsView> helperList = await CheckForUpdateNews();
        if (helperList?.Count > 0)
        {
            UpdateNews(helperList);
        }
    }

    [RelayCommand]
    public async void GetCategoriesList()
    {
        List<Category> helperList = await _categoriesService.GetAllCategories();
        
        if (helperList?.Count > 0)
        {
            helperList.Sort(delegate(Category x, Category y)
            {
                if (x.ID == null && y.ID == null) return 0;
                else if (x.ID == null) return -1;
                else if (y.ID == null) return 1;
                else return x.ID.CompareTo(y.ID);
            });
            CategoriesList.Clear();
            CategoriesList.AddRange(helperList);
        }
        
    }
    
    [RelayCommand]
    public async void FiltrBy(Category category)
    {
        List<NewsView> newsHelperList = await CheckForUpdateNews();
        List<NewsView> helperList = new List<NewsView>();
        foreach (var news in newsHelperList)
        {
            bool contains = false;
            foreach(var categ in news.Categories) 
            {
                if (categ.ID == category.ID)
                {
                    contains = true;
                }
            }
            if (contains)
            {
                helperList.Add( news);
            }
        }
        if (helperList?.Count > 0)
        {
            UpdateNews(helperList);
        }
    }
    
    
    public async void OnItemSelected(NewsView post)
    {
        if (post == null)
        {
            return;
        }

        if (!IsBusy)
        {
            IsBusy = true;
            SelectedItem = post;
            await Shell.Current.GoToAsync(
                $"{nameof(DetailNewsPage)}?{nameof(DetailNewsPageViewModel.ItemId)}={post.ID}");
        }
    }


    private async void UpdateNews(List<NewsView> helperList)
    {
        helperList.Sort(delegate(NewsView x, NewsView y)
        {
            if (x.CreatedDate == null && y.CreatedDate == null) return 0;
            else if (x.CreatedDate == null) return -1;
            else if (y.CreatedDate == null) return 1;
            else return y.CreatedDate.CompareTo(x.CreatedDate);
        });
        Posts.Clear();
        Posts.AddRange(helperList);
        lastestPost = helperList[0];
        Posts.Remove(Posts.First());
    }

    private async Task<List<NewsView>> CheckForUpdateNews()
    {
        List<NewsView> newsList = new List<NewsView>();
        if(lastestUpdate.AddMinutes(10) <= DateTime.Now){
            newsList = await _newsService.GetAllNews();
            PostsOneTake = newsList;
            lastestUpdate = DateTime.Now;
        }
        else
        { 
            newsList = PostsOneTake;
        }
        
        return newsList;
    }
    
}