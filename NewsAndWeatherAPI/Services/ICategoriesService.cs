using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services;

public interface ICategoriesService
{
    List<Category> GetAll();
    
    Category GetById(int id);

}