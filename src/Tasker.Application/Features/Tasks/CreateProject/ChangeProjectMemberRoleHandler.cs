using Tasker.Domain.Enums;
using Tasker.Domain.Interfaces;

namespace Tasker.Application.Features.Tasks.CreateProject
{
    public class ChangeProjectMemberRoleHandler
    {
        private readonly IProjectRepository _projectRepository;

        public ChangeProjectMemberRoleHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(ChangeProjectMemberRoleCommand command, Guid currentUserId, CancellationToken ct)
        {
            var project = await _projectRepository.GetByIdAsync(command.ProjectId, ct);
            if (project == null)
                throw new Exception("Project not found");

            var currentMember = project.Members
                .FirstOrDefault(m => m.UserId == currentUserId);

            if (currentMember == null ||
                (currentMember.Role != Role.Owner && currentMember.Role != Role.Admin))
                throw new Exception("No permission");

            var member = project.Members
                .FirstOrDefault(m => m.UserId == command.MemberId);

            if (member == null)
                throw new Exception("Member not found");

            member.ChangeRole(command.NewRole);

            await _projectRepository.SaveChangesAsync(ct);
        }
    }
}
