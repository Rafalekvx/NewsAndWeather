using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using NewsAndWeather.Services;

namespace NewsAndWeather.ViewModels;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class DetailNewsPageViewModel : BaseViewModel
{
    public INewsService _newsService => DependencyService.Get<INewsService>();
    
    private int itemId;

    public int ItemId
    {
        get
        {
            return itemId;
        }
        set
        {
            itemId = value;
            LoadItemId(value);
        }
    }
    
    [ObservableProperty]
    private string _title;
    public string title
    {
        get { return Title; }
        set { Title = value; }
    }
    [ObservableProperty]
    private string _description;
    public string description
    {
        get { return Description; }
        set { Description = value; }
    }

    [ObservableProperty]
    private string _imageLink;
    public string imageLink
    {
        get { return ImageLink; }
        set { ImageLink = value; }
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
        }
        catch (Exception)
        {
            Debug.WriteLine("Failed to Load Item");
        }
    }
}