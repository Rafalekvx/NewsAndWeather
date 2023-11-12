using NewsAndWeather.Models;
using Newtonsoft.Json;

namespace NewsAndWeather.Services;

public class CategoriesService : ICategoriesService
{
    private HttpClient _client = new HttpClient();

    private string url ="https://newsandweatherapi1.azurewebsites.net";
    
    public async Task<List<Category>> GetAllCategories()
    {
        string requestUrl = "/api/categories";
        try
        {
            var responseMsg = _client.GetAsync(url + requestUrl).Result;
            
            string responseBody = await responseMsg.Content.ReadAsStringAsync();
        
            if (!(responseMsg.IsSuccessStatusCode))
            {
                List<Category> secondList = new List<Category>() { new Category() { ID = 1, Name = "Api off" } };

                return secondList;
            }

            List<Category> listOfLocations = JsonConvert.DeserializeObject<List<Category>>(responseBody);

            return listOfLocations;
        }

        catch(Exception ex)
        {
            List<Category> secondList = new List<Category>() { new Category() { ID = 1, Name = "Api off" } };

            return secondList;
        }
        
    }
    
    public async Task<Category> GetById(int id)
    {
        string requestUrl = $"/api/categories/{id}";
        try
        {
            var responseMsg = _client.GetAsync(url + requestUrl).Result;
            
            string responseBody = await responseMsg.Content.ReadAsStringAsync();
        
            if (!(responseMsg.IsSuccessStatusCode))
            {
                Category secondCategory = new Category() { ID = 1, Name = "Api off" };

                return secondCategory;
            }

            Category category = JsonConvert.DeserializeObject<Category>(responseBody);

            return category;
        }

        catch(Exception ex)
        {
            Category secondCategory = new Category() { ID = 1, Name = "Api off" };

            return secondCategory;
        }
        
    }
}