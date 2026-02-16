using Tasker.Domain.Enums;
namespace Tasker.Domain.Entities;

public class TaskItem 
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime CreatedAt { get; set; }
    public TaskState Status {get; private set; }
    public TaskPriority Priority {get; private set; }

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();


    private TaskItem() {}
    
    public TaskItem(string title, string? description, DateTime? deadline, TaskPriority priority)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Deadline = deadline;
        CreatedAt = DateTime.UtcNow;
        Priority = priority;
        Status = TaskState.Pending;
    }

    public void AddTag(Tag tag)
    {
        if (!Tags.Any(t => t.Name == tag.Name))
        {
            Tags.Add(tag);
            tag.Tasks.Add(this);
        }
    }
}