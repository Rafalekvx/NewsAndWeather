using NewsAndWeather.Models;
using NewsAndWeather.Services.Sup;
using Newtonsoft.Json;

namespace NewsAndWeather.Services;

public class CategoriesService : ICategoriesService
{
    private HttpClient _client = new HttpClient();

    private string url = ApiUrls.NewsApi();
    
    public async Task<List<Category>> GetAllCategories()
    {
        string requestUrl = "/api/categories";
        try
        { 
            List<Category> listOfCategories = new List<Category>();
            using (Stream s = _client.GetStreamAsync($"{ApiUrls.NewsApi()+requestUrl}").Result)
            using (StreamReader sr = new StreamReader(s))
            
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                listOfCategories = serializer.Deserialize<List<Category>>(reader);
            }
        
            if (listOfCategories.Count == 0)
            {
                List<Category> secondList = new List<Category>() { new Category() { ID = 1, Name = "Api off" } };

                return secondList;
            }

            return listOfCategories;
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