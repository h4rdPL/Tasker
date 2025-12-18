namespace Tasker.Domain.Entities;

public class TaskItem
{
    private TaskItem() { } 

    public TaskItem(
        string title,
        string description,
        TaskPriority priority,
        DateTime? deadline,
        Guid ownerUserId)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Priority = priority;
        Deadline = deadline;
        Status = TaskStatus.Todo;
        OwnerUserId = ownerUserId;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public TaskPriority Priority { get; private set; }
    public TaskStatus Status { get; private set; }
    public DateTime? Deadline { get; private set; }
    public Guid OwnerUserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
}
