using Tasker.Application.Features.Tasks.CreateProject;
using Tasker.Domain.Entities;
using Tasker.Domain.Interfaces;

namespace Tasker.Application.Features.Projects.CreateProject
{
    public class CreateProjectHandler
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public CreateProjectHandler(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }
        public async Task<Guid> Handle(CreateProjectCommand command, Guid ownerId, CancellationToken ct)
        {
            var user = await _userRepository.GetByIdAsync(ownerId, ct);
            if (user == null)
                throw new Exception("Owner not found");

            var project = new Project(command.Name, command.Description, ownerId);
            await _projectRepository.AddAsync(project, ct);

            return project.Id;
        }
    }
}
