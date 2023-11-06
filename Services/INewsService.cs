using NewsAndWeather.Models;

namespace NewsAndWeather.Services;

public interface INewsService
{
    Task<Post> GetOneNews(int id);
    Task<List<Post>> GetAllNews();
}