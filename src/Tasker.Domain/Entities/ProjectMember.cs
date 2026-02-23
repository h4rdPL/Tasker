using Tasker.Domain.Enums;

namespace Tasker.Domain.Entities
{
    public class ProjectMember
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public ProjectRole Role { get; set; }

        protected ProjectMember() { }

        public ProjectMember(Guid userId, ProjectRole role)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Role = role;
        }

        public void ChangeRole(ProjectRole role)
        {
            Role = role;
        }
    }
}
