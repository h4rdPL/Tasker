using Tasker.Domain.Enums;

namespace Tasker.Domain.Entities
{
    public abstract class UserRoleAssignment
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public Role Role { get; protected set; }

        protected UserRoleAssignment() { }

        protected UserRoleAssignment(Guid userId, Role role)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Role = role;
        }

        public void ChangeRole(Role role)
        {
            Role = role;
        }

        public bool CanEdit()
        {
            return Role == Role.Owner || Role == Role.Admin;
        }
    }
}