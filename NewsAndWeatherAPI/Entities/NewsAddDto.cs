using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Entities;

public class NewsAddDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageLink { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public int? CreatedById { get; set; }
    public List<LinkCategoryToNews> CategoriesToNews { get; set; }
}