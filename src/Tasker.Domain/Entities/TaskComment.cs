namespace Tasker.Domain.Entities
{ 
public class TaskComment
{
    public Guid Id { get; private set; }
    public Guid TaskId { get; private set; }
    public Guid AuthorId { get; private set; }

    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected TaskComment() { }

    public TaskComment(Guid taskId, Guid authorId, string content)
    {
        Id = Guid.NewGuid();
        TaskId = taskId;
        AuthorId = authorId;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }
}
}
