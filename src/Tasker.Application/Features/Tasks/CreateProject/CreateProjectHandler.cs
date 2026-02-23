using Tasker.Domain.Entities;
using Tasker.Domain.Enums;
using Tasker.Domain.Interfaces;

namespace Tasker.Application.Features.Tasks.CreateProject
{
    public record ChangeProjectMemberRoleCommand(
        Guid ProjectId,
        Guid MemberId,
        Role NewRole
    );

    public class CreateProjectHandler
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Guid> Handle(CreateProjectCommand command, Guid ownerId, CancellationToken ct)
        {
            var project = new Project(command.Name, command.Description, ownerId);

            var ownerMember = new ProjectMember(ownerId, Guid.NewGuid(), Role.Owner);
            project.Members.Add(ownerMember);

            await _projectRepository.AddAsync(project, ct);

            return project.Id;
        }
    }
}