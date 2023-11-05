using Microsoft.EntityFrameworkCore;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Entities;

public class PostDBContext : DbContext
{
    private string _connectionString = "Server=DESKTOP-THG5B7H;Database=NewsDb; Trusted_Connection=True; Encrypt=False";
    
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().Property(r => r.Title).IsRequired();
        modelBuilder.Entity<Post>().Property(r => r.ID).IsRequired();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}