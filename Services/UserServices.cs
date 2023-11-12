using System.Security.Claims;
using System.Text;
using NewsAndWeather.Models;
using NewsAndWeather.Services;
using Newtonsoft.Json;


namespace NewsAndWeather.Services
{
    
    public class UserServices : IUserServices
    {

        private HttpClient _client = new HttpClient();

        private string url ="https://newsandweatherapi1.azurewebsites.net";
        
        
        public async Task<UserDto> GetUserById(int id)
        {
            string requestUrl = $"/api/user/{id}";
            try
            {
                var responseMsg = _client.GetAsync(url + requestUrl).Result;
                
                string responseBody = await responseMsg.Content.ReadAsStringAsync();

                UserDto user = JsonConvert.DeserializeObject<UserDto>(responseBody);
            
                return user;
            }

            catch (Exception ex)
            {
                return new UserDto() { ID  = 0, Name = "Api is off", Email = "Ciul@wp.pl"};
            }
            
            
        }
        
        
    }
}
