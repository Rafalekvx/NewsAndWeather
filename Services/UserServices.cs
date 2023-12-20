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

        public async Task<string> RegisterUser(RegisterDto registerDto)
        {
            string requestUrl = $"/api/user/register";
            Uri fullUrl = new Uri(url + requestUrl);
            try
            {
                RegisterDto dto = new RegisterDto()
                    { name = registerDto.name, email = registerDto.email, password = registerDto.password };

                string json = JsonConvert.SerializeObject(dto);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMsg = _client.PostAsync(fullUrl, content).Result;

                string responseBody = responseMsg.StatusCode.ToString();

                return responseBody.ToLower();
            }
            catch (Exception ex)
            {
                return "error";
            }
        } 
        
        
    }
}
