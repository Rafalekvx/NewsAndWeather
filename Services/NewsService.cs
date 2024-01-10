using System.Text.Json.Serialization;
using NewsAndWeather.Models;
using NewsAndWeather.Services.Sup;
using Newtonsoft.Json;

namespace NewsAndWeather.Services;

public class NewsService : INewsService
{
    private HttpClient _client = new HttpClient();

    private string url =ApiUrls.NewsApi();
    
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

                if (!(Uri.IsWellFormedUriString(newsView.ImageLink, UriKind.Absolute)))
                {
                    newsView.ImageLink =
                        "https://newsandweatherstorage.blob.core.windows.net/newsandweatherimages/Noimage.png";
                }
        
                listOfNews.Add(newsView);
            }

            foreach (var item in listOfNews)
            {
                                
                if (item.Categories.Count == 0)
                {
                    item.CategoriesToNews.Add(new LinkCategoryToNews(){PostID = item.ID, CategoryID = 1, ID = 2137});
                }

                foreach (var categoriesTo in item.CategoriesToNews)
                {
                    item.Categories.Add(_categoriesService.GetById(categoriesTo.CategoryID).Result);
                }


            }
            
            
            
            return listOfNews;

        }

        catch
        {
            
            List<NewsView> posts = new List<NewsView>() { new NewsView() { ID = 1, Title = "Jeden", Description="Wiecej niz jeden", ImageLink="https://newsandweatherstorage.blob.core.windows.net/newsandweatherimages/Noimage.png" },
                new NewsView() { ID = 2, Title = "Dwa", Description = "Wiecej niz dwa", ImageLink = "https://newsandweatherstorage.blob.core.windows.net/newsandweatherimages/Noimage.png" } ,
                new NewsView() { ID = 3, Title = "Trzy", Description="Wiecej niz trzy", ImageLink="https://newsandweatherstorage.blob.core.windows.net/newsandweatherimages/Noimage.png" } };

            return posts;
            
        }

    }
    
}