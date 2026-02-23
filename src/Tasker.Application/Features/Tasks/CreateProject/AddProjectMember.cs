using Tasker.Domain.Entities;
using Tasker.Domain.Enums;
using Tasker.Domain.Interfaces;

namespace Tasker.Application.Features.CreateProject
{
    public class AddProjectMemberHandler
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public AddProjectMemberHandler(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(Guid projectId, Guid userId, Role role, CancellationToken ct)
        {
            var project = await _projectRepository.GetByIdAsync(projectId, ct);
            if (project == null)
                throw new Exception("Project not found");

            var user = await _userRepository.GetByIdAsync(userId, ct);
            if (user == null)
                throw new Exception("User not found");

            var member = new ProjectMember(projectId, user.Id, role);
            project.Members.Add(member);

            await _projectRepository.SaveChangesAsync(ct);

            return member.Id;
        }
    }
}