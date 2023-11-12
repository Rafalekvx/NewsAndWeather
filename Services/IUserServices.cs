using NewsAndWeather.Models;

namespace NewsAndWeather.Services
{
    public interface IUserServices
    {
        Task<UserDto> GetUserById(int id);
    }
}
