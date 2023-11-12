namespace NewsAndWeather.Models;

public class NewsView
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageLink { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatedById { get; set; }
    public  UserDto CreatedBy { get; set; }
    public  List<LinkCategoryToNews> CategoriesToNews { get; set; }
    public  List<Category> Categories { get; set; }
}