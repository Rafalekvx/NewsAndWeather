namespace NewsAndWeatherAPI.Entities;

public class UserGetDto
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.Today;
}