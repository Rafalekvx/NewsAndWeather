﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services
{
    [Route("api/user")]
    public class UserServices : IUserServices
    {
        private readonly NAWDBContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserServices(NAWDBContext dbContext,IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public static long GetTokenExpirationTime(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var ticks = long.Parse(tokenExp);
            return ticks;
        }

        public bool CheckTokenExpired(string token)
        {
            var tokenTicks = GetTokenExpirationTime(token);
            var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;

            var now = DateTime.Now.ToUniversalTime();

            var valid = tokenDate >= now;

            return valid;
        }


        public UserGetDto GetUserById([FromRoute]int id)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.ID == id);

            UserGetDto usetDto = new UserGetDto()
            {
                ID = user.ID,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
                
            };
            
            return usetDto;
            
        }
        public string LoginUser(UserLoginDto login)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.Email == login.Email);

            if (user is null)
            {
                throw new Exception("Something went wrong");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, login.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid Email");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name,$"{user.Name}"),
                new Claim(ClaimTypes.Email,$"{user.Email}"),
                new Claim(ClaimTypes.DateOfBirth, $"{user.DateOfBirth}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return tokenHandler.WriteToken(token);

            return string.Empty;

        }

        public void RegisterUser(UserRegisterDto register)
        {
            User newUser = new User() 
            { 
                Name = register.Name, 
                Email = register.Email, 
                Password = register.Password,
            };

            var HashedPassword = _passwordHasher.HashPassword(newUser,newUser.Password);

            newUser.Password = HashedPassword;


           _dbContext.Add(newUser);
           _dbContext.SaveChanges();
        }
        
    }
}
