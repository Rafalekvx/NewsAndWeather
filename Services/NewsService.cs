using System.Text.Json.Serialization;
using NewsAndWeather.Models;
using Newtonsoft.Json;

namespace NewsAndWeather.Services;

public class NewsService : INewsService
{
    private HttpClient _client = new HttpClient();

    private string url ="http://10.0.2.2:5011";
    
    
    public async Task<Post> GetOneNews(int id)
    {
        string one = "/api/news/"+ id;
        
        var responseMsg = _client.GetAsync(url + one).Result;
        
        responseMsg.EnsureSuccessStatusCode();
        string responseBody = await responseMsg.Content.ReadAsStringAsync();
        
        Post News = JsonConvert.DeserializeObject<Post>(responseBody);

        return News;
        
    }

    public async Task<List<Post>> GetAllNews()
    {
        string all = "/api/news";
        
        var responseMsg = _client.GetAsync(url + all).Result;
        
        responseMsg.EnsureSuccessStatusCode();
        string responseBody = await responseMsg.Content.ReadAsStringAsync();

        List<Post> listOfNews = JsonConvert.DeserializeObject<List<Post>>(responseBody);

        return listOfNews;
    }
    
}