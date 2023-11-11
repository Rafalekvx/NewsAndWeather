namespace NewsAndWeatherAPI.Models;

public class LinkCategoryToNews
{
    public int ID { get; set; }
    public int PostID { get; set; }
    public int CategoryID { get; set; }
    public Post Post { get; set; } = null;
    public Category Category { get; set; } = null;
}