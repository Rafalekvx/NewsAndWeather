using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAndWeatherAPI.Entities;
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

    [Route("new")]
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult AddNews([FromBody]NewsAddDto dto)
    {
        string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

        int UserID = int.Parse(userId);
        
        _newsService.AddNews(dto, UserID);

        return Ok();
    }
    
}