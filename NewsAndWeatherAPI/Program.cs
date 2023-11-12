using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NewsAndWeatherAPI;
using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;
using NewsAndWeatherAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<NAWDBContext>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var authenticationSetting = new AuthenticationSettings();

builder.Services.AddSingleton(authenticationSetting);

builder.Configuration.GetSection("Authentication").Bind(authenticationSetting);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSetting.JwtIssuer,
        ValidAudience = authenticationSetting.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSetting.JwtKey)),
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
