using System.Text.Json.Serialization;
using NewsAndWeather.Models;
using Newtonsoft.Json;

namespace NewsAndWeather.Services;

public class NewsService : INewsService
{
    private HttpClient _client = new HttpClient();

    private string url ="https://newsandweatherapi1.azurewebsites.net";
    
    private IUserServices _userServices => DependencyService.Get<IUserServices>();
    private ICategoriesService _categoriesService => DependencyService.Get<ICategoriesService>();
    
    public async Task<NewsView> GetOneNews(int id)
    {
        string requestUrl = $"/api/news/{id}";
        try
        {
            var responseMsg = _client.GetAsync(url + requestUrl).Result;

            responseMsg.EnsureSuccessStatusCode();
            string responseBody = await responseMsg.Content.ReadAsStringAsync();

            Post News = JsonConvert.DeserializeObject<Post>(responseBody);

            NewsView newsView = new NewsView()
            {
                Title = News.Title,
                Description = News.Description,
                ImageLink = News.ImageLink,
                CreatedDate = News.CreatedDate,
                CreatedById = News.CreatedById,
                CreatedBy = _userServices.GetUserById(News.CreatedById).Result,
                CategoriesToNews = News.CategoriesToNews,
                Categories = new List<Category>()
            };

            foreach (var categoriesTo in newsView.CategoriesToNews)
            {
                newsView.Categories.Add(_categoriesService.GetById(categoriesTo.CategoryID).Result);
            }
            
            return newsView;
        }

        catch (Exception ex)
        {
            return new NewsView() { ID  = 0, Title = "Api is off", Description = "Sadeg", ImageLink = "https://i.imgur.com/Lg8jnn6.png"};
        }
    }

    public async Task<List<NewsView>> GetAllNews()
    {
        string requestUrl = "/api/news";

        try
        {
            var responseMsg = _client.GetAsync(url + requestUrl).Result;

            string responseBody = await responseMsg.Content.ReadAsStringAsync();


            List<Post> listOfPost = JsonConvert.DeserializeObject<List<Post>>(responseBody);

            List<NewsView> listOfNews = new List<NewsView>();
            foreach (var News in listOfPost)
            {
                
                NewsView newsView = new NewsView()
                {
                    ID = News.ID,
                    Title = News.Title,
                    Description = News.Description,
                    ImageLink = News.ImageLink,
                    CreatedDate = News.CreatedDate,
                    CreatedById = News.CreatedById,
                    CreatedBy = await _userServices.GetUserById(News.CreatedById),
                    CategoriesToNews = News.CategoriesToNews,
                    Categories = new List<Category>()
                };
                
                
                foreach (var categoriesTo in newsView.CategoriesToNews)
                {
                    newsView.Categories.Add(_categoriesService.GetById(categoriesTo.CategoryID).Result);
                }
                
                listOfNews.Add(newsView);
            }
            
            return listOfNews;

        }

        catch
        {
            
            List<NewsView> posts = new List<NewsView>() { new NewsView() { ID = 1, Title = "Jeden", Description="Wiecej niz jeden", ImageLink="Linkacz" },
                new NewsView() { ID = 2, Title = "Dwa", Description = "Wiecej niz dwa", ImageLink = "Linkacz" } ,
                new NewsView() { ID = 3, Title = "Trzy", Description="Wiecej niz trzy", ImageLink="Linkacz" } };

            return posts;
            
        }

    }
    
}