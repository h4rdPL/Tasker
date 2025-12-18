using System.ComponentModel.DataAnnotations;
using Tasker.Domain.Entities;

namespace Tasker.Application.Tasks;

public record CreateTaskRequest(
    [Required, MinLength(3)] string Title,
    string Description,
    TaskPriority Priority,
    DateTime? Deadline
);
