using NewsAndWeather.Models;

namespace NewsAndWeather.Services
{
    public interface IUserServices
    {
        Task<UserDto> GetUserById(int id);
        Task<string> RegisterUser(RegisterDto registerDto);
        Task<string> LoginUser(LoginDto loginDto);
        Task<bool> CheckIsTokenExpired(string token);
    }
}
