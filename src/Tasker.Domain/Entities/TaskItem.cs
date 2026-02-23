using System;
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

    public List<TaskComment> Comments { get; private set; } = new();

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid? ProjectId { get; set; }
    public Guid? AssignedToUserId { get; private set; }
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

    public void AssignTo(Guid userId)
    {
        AssignedToUserId = userId;
    }

    public void AddComment(Guid authorId, string content)
    {
        var comment = new TaskComment(this.Id, authorId, content);
        Comments.Add(comment);
    }
}