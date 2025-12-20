namespace Tasker.Domain.Entities;

public class User 
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; private set; } 
    public DateTime CreatedAt { get; set; }

    private User() {}

    public User(string email, string username, string password)
    {
        Id = Guid.NewGuid();
        Email = email;
        Username = username;
        PasswordHash = password;
        CreatedAt = DateTime.UtcNow;
    }

}

