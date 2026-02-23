namespace Tasker.Domain.Entities
{
    public class ProjectInvitation
    {
        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Email { get; private set; }

        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }
    }
}
