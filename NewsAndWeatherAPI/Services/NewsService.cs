using Microsoft.CodeAnalysis.Elfie.Serialization;
using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services;

public class NewsService : INewsService
{
    private readonly NAWDBContext _dbContext;
    
    public NewsService(NAWDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Post GetByID(int id)
    {
        Post returnPost = _dbContext.Posts.FirstOrDefault(i => i.ID == id);

        returnPost.CategoriesToNews = _dbContext.CategoriesToNews.Where(r => r.PostID == returnPost.ID).ToList();
        
        
        return returnPost;
    }

    public List<Post> GetAll()
    {
        List<Post> listOfPosts = _dbContext.Posts.ToList();

        foreach (var post in listOfPosts)
        {
            post.CategoriesToNews = _dbContext.CategoriesToNews.Where(r => r.PostID == post.ID).ToList();
        }
        
        return listOfPosts;
    }

    public void AddNews(NewsAddDto news)
    {
        Post Added = new Post()
        {
            Title = news.Title,
            Description = news.Description,
            CategoriesToNews = news.CategoriesToNews,
            CreatedById = news.CreatedById,
            CreatedDate = news.CreatedDate,
            ImageLink = news.ImageLink
        };
        
        _dbContext.Posts.Add(Added);
        _dbContext.SaveChanges();
    }
    
    
}