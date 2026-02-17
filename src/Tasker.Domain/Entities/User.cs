namespace Tasker.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    public ICollection<TaskItem> Tasks { get; private set; } = new List<TaskItem>();

    private User() { }

    public User(string email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty");

        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
    }
}
