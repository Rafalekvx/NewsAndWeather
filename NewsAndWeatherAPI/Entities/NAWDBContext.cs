using Microsoft.EntityFrameworkCore;
using NewsAndWeather.Models;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Entities;

public class NAWDBContext : DbContext
{
    private string _connectionString = "***REMOVED***";
    
    public DbSet<Post> Posts { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Location> Locations { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<LinkCategoryToNews> CategoriesToNews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>().Property(r => r.Name).IsRequired();
        modelBuilder.Entity<User>().Property(r => r.Email).IsRequired();
        modelBuilder.Entity<User>().Property(r => r.Password).IsRequired();
        
        modelBuilder.Entity<Post>().Property(r => r.Title).IsRequired();
        modelBuilder.Entity<Post>().Property(r => r.ID).IsRequired();

        
        modelBuilder.Entity<Location>().Property(r => r.Id).IsRequired().ValueGeneratedNever();
        modelBuilder.Entity<Location>().Property(r => r.Name).IsRequired();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}