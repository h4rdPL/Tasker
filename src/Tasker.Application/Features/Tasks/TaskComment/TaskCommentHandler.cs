using Tasker.Domain.Entities;
using Tasker.Domain.Interfaces;

namespace Tasker.Application.Features.TaskComments

{
    public class TaskCommentHandler
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITaskCommentRepository _commentRepository;

        public TaskCommentHandler(
            ITaskRepository taskRepository,
            IUserRepository userRepository,
            ITaskCommentRepository commentRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        public async Task<Guid> AddCommentAsync(
            AddTaskCommentCommand command,
            Guid userId,
            CancellationToken ct)
        {
            var task = await _taskRepository.GetByIdAsync(command.TaskId, ct)
                ?? throw new Exception("Task not found");

            var user = await _userRepository.GetByIdAsync(userId, ct)
                ?? throw new Exception("User not found");

            var comment = new TaskComment(task.Id, user.Id, command.Content);

            await _commentRepository.AddAsync(comment, ct);

            return comment.Id;
        }


        public async Task<List<TaskCommentDto>> GetCommentsAsync(
            GetTaskCommentsQuery query,
            CancellationToken ct)
        {
            var task = await _taskRepository.GetByIdAsync(query.TaskId, ct);
            if (task == null)
                throw new Exception("Task not found");

            var comments = await _commentRepository.GetByTaskIdAsync(query.TaskId, ct);

            return comments
                .OrderBy(c => c.CreatedAt)
                .Select(c => new TaskCommentDto(
                    c.Id,
                    c.AuthorId,
                    c.Content,
                    c.CreatedAt))
                .ToList();
        }
    }


    public record AddTaskCommentCommand(
        Guid TaskId,
        string Content
    );


    public record GetTaskCommentsQuery(
        Guid TaskId
    );


    public record TaskCommentDto(
        Guid Id,
        Guid UserId,
        string Content,
        DateTime CreatedAt
    );
}

