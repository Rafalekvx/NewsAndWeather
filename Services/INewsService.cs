using NewsAndWeather.Models;

namespace NewsAndWeather.Services;

public interface INewsService
{
    Task<NewsView> GetOneNews(int id);
    Task<List<NewsView>> GetAllNews();
}