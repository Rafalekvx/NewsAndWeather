using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using NewsAndWeather.Models;
using NewsAndWeather.Services;

namespace NewsAndWeather.ViewModels;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class DetailNewsPageViewModel : BaseViewModel
{
    public INewsService _newsService => DependencyService.Get<INewsService>();

    private int itemId;

    public int ItemId
    {
        get { return itemId; }
        set
        {
            itemId = value;
            LoadItemId(value);
        }
    }

    [ObservableProperty] private string _title;

    public string title
    {
        get { return Title; }
        set { Title = value; }
    }

    [ObservableProperty] private string _description;

    public string description
    {
        get { return Description; }
        set { Description = value; }
    }

    [ObservableProperty] private string _imageLink;

    public string imageLink
    {
        get { return ImageLink; }
        set { ImageLink = value; }
    }

    [ObservableProperty] private DateTime _createdDate;

    public DateTime createdDate
    {
        get { return CreatedDate; }
        set { CreatedDate = value; }
    }

    [ObservableProperty] private int _createdById;

    public int createdById
    {
        get {return CreatedById; }
        set { CreatedById = value;}
    }

    [ObservableProperty] private UserDto _createdBy;
    
    public  UserDto createdBy
    {
        get { return CreatedBy;}
        set { CreatedBy = value; }
    }

    [ObservableProperty]
    private List<LinkCategoryToNews> _categoriesToNews;

    public List<LinkCategoryToNews> categoriesToNews
    {
        get { return CategoriesToNews; }
        set { CategoriesToNews = value; }
    }

    [ObservableProperty] private List<Category> _categories;
    
    
    public  List<Category> categories
    {
        get { return Categories;}
        set { value = Categories; }
    }
    
    public int ID { get; set; }
    
    
    
    public async void LoadItemId(int itemId)
    {
        try
        {
            var item = await _newsService.GetOneNews(itemId);
            ID = item.ID;
            Title = item.Title;
            ImageLink = item.ImageLink;
            Description = item.Description;
            CreatedDate = item.CreatedDate;
            CreatedBy = item.CreatedBy;
            CreatedById = item.CreatedById;
            CategoriesToNews = item.CategoriesToNews;
            Categories = item.Categories;

        }
        catch (Exception)
        {
            Debug.WriteLine("Failed to Load Item");
        }
    }
}