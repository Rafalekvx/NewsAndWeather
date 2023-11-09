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
        try
        {
            var responseMsg = _client.GetAsync(url + requestUrl).Result;

            responseMsg.EnsureSuccessStatusCode();
            string responseBody = await responseMsg.Content.ReadAsStringAsync();

            Post News = JsonConvert.DeserializeObject<Post>(responseBody);

            return News;
        }

        catch (Exception ex)
        {
            return new Post() { ID  = 0, Title = "Api is off", Description = "Sadeg", ImageLink = "https://i.imgur.com/Lg8jnn6.png"};
        }
    }

    public async Task<List<Post>> GetAllNews()
    {
        string requestUrl = "/api/news";

        try
        {
            var responseMsg = _client.GetAsync(url + requestUrl).Result;

            string responseBody = await responseMsg.Content.ReadAsStringAsync();


            List<Post> listOfNews = JsonConvert.DeserializeObject<List<Post>>(responseBody);

            return listOfNews;

        }

        catch
        {
            
            List<Post> posts = new List<Post>() { new Post() { ID = 1, Title = "Jeden", Description="Wiecej niz jeden", ImageLink="Linkacz" },
                new Post() { ID = 2, Title = "Dwa", Description = "Wiecej niz dwa", ImageLink = "Linkacz" } ,
                new Post() { ID = 3, Title = "Trzy", Description="Wiecej niz trzy", ImageLink="Linkacz" } };

            return posts;
            
        }

    }
    
}