using System.Text.Json.Serialization;
using NewsAndWeather.Models;
using Newtonsoft.Json;

namespace NewsAndWeather.Services;

public class NewsService : INewsService
{
    private HttpClient _client = new HttpClient();

    private string url ="http://localhost:5011";
    
    
    public async Task<Post> GetOneNews(int id)
    {
        string requestUrl = $"/api/news/{id}";
        
        var responseMsg = _client.GetAsync(url + requestUrl).Result;
        
        responseMsg.EnsureSuccessStatusCode();
        string responseBody = await responseMsg.Content.ReadAsStringAsync();
        
        Post News = JsonConvert.DeserializeObject<Post>(responseBody);

        return News;
        
    }

    public async Task<List<Post>> GetAllNews()
    {
        string requestUrl = "/api/news";
        
        var responseMsg = _client.GetAsync(url + requestUrl).Result;
        
        responseMsg.EnsureSuccessStatusCode();
        string responseBody = await responseMsg.Content.ReadAsStringAsync();

        List<Post> listOfNews = JsonConvert.DeserializeObject<List<Post>>(responseBody);

        return listOfNews;
    }
    
}