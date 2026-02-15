namespace Tasker.Domain.Entities;

public class TaskItem 
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime CreatedAt { get; set; }

    private TaskItem() {}
    
    public TaskItem(string title, string? description, DateTime? deadline)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Deadline = deadline;
        CreatedAt = DateTime.UtcNow;
    }
}