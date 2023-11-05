using Microsoft.CodeAnalysis.Elfie.Serialization;
using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services;

public class NewsService : INewsService
{
    private readonly PostDBContext _dbContext;
    
    public NewsService(PostDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Post GetByID(int id)
    {
        Post returnPost = _dbContext.Posts.FirstOrDefault(i => i.ID == id);
        
        return returnPost;
    }

    public List<Post> GetAll()
    {
        List<Post> listOfPosts = _dbContext.Posts.ToList();
        
        
        return listOfPosts;
    }
    
    
}