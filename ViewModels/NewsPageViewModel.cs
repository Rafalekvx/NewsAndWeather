using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NewsAndWeather.Models;
using NewsAndWeather.Services;
using NewsAndWeather.Views;

namespace NewsAndWeather.ViewModels;

public partial class NewsPageViewModel : BaseViewModel
{
    public List<NewsView> PostsOneTake { get; set; }
    public ObservableCollection<NewsView> Posts { get; }
    
    public ObservableCollection<Category> CategoriesList { get; }

    [ObservableProperty] 
    public NewsView _lastestPost;

    public NewsView lastestPost
    {
        get { return LastestPost;}
        set { LastestPost = value; }
    }

    public INewsService _newsService => DependencyService.Get<INewsService>();
    public ICategoriesService _categoriesService => DependencyService.Get<ICategoriesService>();
    public Command<NewsView> ItemTapped { get; }
    public Command<Category> CategoryTapped { get; }
    
    public NewsPageViewModel()
    {
        Posts = new ObservableCollection<NewsView>();
        CategoriesList = new ObservableCollection<Category>();
        ItemTapped = new Command<NewsView>(OnItemSelected);
        CategoryTapped = new Command<Category>(FiltrBy);
        PostsOneTake = _newsService.GetAllNews().Result.OrderByDescending(a=> a.CreatedDate).ToList();
    }

    [RelayCommand]
    public async void GetList()
    {
        List<NewsView> helperList = await _newsService.GetAllNews();
        
        if (helperList?.Count > 0)
        {
            helperList = helperList.OrderByDescending(a=> a.CreatedDate).ToList();
            Posts.Clear();
            foreach (NewsView post in helperList)
            {
                Posts.Add(post);
            }
            lastestPost = helperList.First();
            Posts.Remove(Posts.First());

        }

    }

    [RelayCommand]
    public async void GetCategoriesList()
    {
        List<Category> helperList = await _categoriesService.GetAllCategories();
        
        if (helperList?.Count > 0)
        {
            helperList = helperList.OrderByDescending(a=> a.Name).ToList();
            CategoriesList.Clear();
            foreach (Category category in helperList)
            {
                CategoriesList.Add(category);
            }
        }
        
    }
    
    [RelayCommand]
    public async void FiltrBy(Category category)
    {
        List<NewsView> newsHelperList = PostsOneTake;
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
            helperList = helperList.OrderByDescending(a=> a.CreatedDate).ToList();
            Posts.Clear();
            foreach (NewsView post in helperList)
            {
                Posts.Add(post);
            }
            lastestPost = helperList.First();
            Posts.Remove(Posts.First());
        }
        

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