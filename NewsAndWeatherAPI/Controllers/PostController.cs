using Microsoft.AspNetCore.Mvc;
using NewsAndWeatherAPI.Models;
using NewsAndWeatherAPI.Services;

namespace NewsAndWeatherAPI.Controllers;

[Route("api/news")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly INewsService _newsService;
    
    public PostController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet("{id}")]
    public ActionResult<Post> GetByID([FromRoute] int id)
    {
        Post onePost = _newsService.GetByID(id);

        if (onePost is null)
        {
            return NotFound(onePost);
        }
        
        return Ok(onePost);
    }
    
    [HttpGet]
    public ActionResult<List<Post>> GetAll()
    {
        List<Post> listOfPosts = _newsService.GetAll();

        return Ok(listOfPosts);
    }
    
}