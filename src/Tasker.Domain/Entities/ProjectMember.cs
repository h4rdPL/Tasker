using Tasker.Domain.Enums;

namespace Tasker.Domain.Entities
{
    public class ProjectMember : UserRoleAssignment
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public Role Role { get; set; }

        protected ProjectMember() { }

        public ProjectMember(Guid userId, Guid id, Role role) : base(userId, role)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Id = id;
            Role = role;
        }

        public void ChangeRole(Role role)
        {
            Role = role;
        }
    }
}
