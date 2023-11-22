using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services;

public interface INewsService
{
    Post GetByID(int id);
    List<Post> GetAll();
    void AddNews(NewsAddDto news, int UserID);
}