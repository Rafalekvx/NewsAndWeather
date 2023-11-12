using NewsAndWeather.Models;

namespace NewsAndWeather.Services;

public interface ICategoriesService
{
    Task<List<Category>> GetAllCategories();
    Task<Category> GetById(int id);
}