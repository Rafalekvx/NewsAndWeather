using Microsoft.AspNetCore.Mvc;
using NewsAndWeatherAPI.Models;
using NewsAndWeatherAPI.Services;

namespace NewsAndWeatherAPI.Controllers;


[Route("api/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;
    
    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }
    
    [HttpGet]
    public ActionResult<List<Category>> GetAll()
    {
        List<Category> listOfLocations = _categoriesService.GetAll();

        return Ok(listOfLocations);
    }

    [Route("{id}")]
    [HttpGet]
    public ActionResult<Category> GetById([FromRoute]int id)
    {
        Category category = _categoriesService.GetById(id);

        return Ok(category);
    }
}