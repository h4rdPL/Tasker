namespace Tasker.Domain.Entities
{
    public class TaskMention
    {
        public Guid Id { get; private set; }
        public Guid TaskId { get; private set; }
        public Guid MentionedUserId { get; private set; }

        public DateTime CreatedAt { get; private set; }
    }
}
