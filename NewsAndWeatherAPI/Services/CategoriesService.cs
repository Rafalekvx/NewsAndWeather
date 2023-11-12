using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services;

public class CategoriesService : ICategoriesService
{
    private readonly NAWDBContext _dbContext;
    
    public CategoriesService(NAWDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Category GetById(int id)
    {
        Category category = _dbContext.Categories.FirstOrDefault(r => r.ID == id);

        category.CategoriesToNews = _dbContext.CategoriesToNews.Where(r => r.CategoryID == category.ID).ToList();
        
        return category;
    }

    public List<Category> GetAll()
    {
        List<Category> listOfCategories = _dbContext.Categories.ToList();
        
        foreach (var category in listOfCategories)
        {
            category.CategoriesToNews = _dbContext.CategoriesToNews.Where(r => r.CategoryID == category.ID).ToList();
        }
        
        return listOfCategories;
    }
}