using System.Net;
using System.Security.Claims;
using System.Text;
using NewsAndWeather.Models;
using NewsAndWeather.Services;
using NewsAndWeather.Services.Sup;
using Newtonsoft.Json;


namespace NewsAndWeather.Services
{
    
    public class UserServices : IUserServices
    {

        private HttpClient _client = new HttpClient();

        private string url =ApiUrls.NewsApi();
        
        
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

                var responseMsg = await _client.PostAsync(fullUrl, content);

                string responseBody = responseMsg.StatusCode.ToString();

                return responseBody.ToLower();
            }
            catch (Exception ex)
            {
                return "error";
            }
        } 
        public async Task<string> LoginUser(LoginDto loginDto)
        {
            string requestUrl = $"/api/user/login";
            Uri fullUrl = new Uri(url + requestUrl);
            try
            {
                LoginDto dto = new LoginDto()
                    {email = loginDto.email, password = loginDto.password };

                string json = JsonConvert.SerializeObject(dto);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMsg = await _client.PostAsync(fullUrl, content);

                if (responseMsg.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await responseMsg.Content.ReadAsStringAsync();
                    return responseBody;
                }

                return "";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public async Task<bool> CheckIsTokenExpired(string token)
        {
            string requestUrl = $"/api/user/token/expired";
            Uri fullUrl = new Uri(url + requestUrl);
            try
            {
                string dto = token;
                string json = JsonConvert.SerializeObject(dto);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMsg = await _client.PostAsync(fullUrl, content);

                string responseBody = await responseMsg.Content.ReadAsStringAsync();

                bool result = bool.TryParse(responseBody, out result);
                
                return result;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
