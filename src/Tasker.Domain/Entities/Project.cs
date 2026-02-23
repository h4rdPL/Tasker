using Tasker.Domain.Entities;
using Tasker.Domain.Enums;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid OwnerId { get; set; }
    public List<ProjectMember> Members { get; set; } = new(); 

    protected Project() { }

    public Project(string name, string? description, Guid ownerId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        OwnerId = ownerId;

        Members.Add(new ProjectMember(ownerId, ProjectRole.Owner));
    }

    public bool CanEdit(Guid userId)
    {
        return Members.Any(m =>
            m.UserId == userId &&
            (m.Role == ProjectRole.Owner || m.Role == ProjectRole.Admin));
    }
}