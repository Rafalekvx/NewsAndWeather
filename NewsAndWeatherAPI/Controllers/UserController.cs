using Microsoft.AspNetCore.Mvc;
using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;
using NewsAndWeatherAPI.Services;

namespace NewsAndWeatherAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("{id}")]
        public ActionResult<UserGetDto> GetUserById([FromRoute]int id)
        {
            UserGetDto user = _userServices.GetUserById(id);
            return Ok(user);
        }
        [HttpPost]
        [Route("register")]
        public ActionResult<User> RegisterUser([FromBody]UserRegisterDto dto)
        {
            _userServices.RegisterUser(dto);
            return Ok();
        }
        [HttpPost]
        [Route("login")]
        public ActionResult<User> LoginUser([FromBody]UserLoginDto dto)
        {
            string result = _userServices.LoginUser(dto);
            return Ok(result);
        }
        [HttpPost("token/expired")]
        public ActionResult<User> CheckTokenExpired([FromBody]string token)
        {
            bool expire = _userServices.CheckTokenExpired(token);
            return Ok(!expire);
        }

    }
}
