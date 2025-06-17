using Domain.Enums;
using System;

namespace Application.Dtos;

public class TaskDto
{
    public string? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Domain.Enums.TaskStatus Status { get; set; }
    public DateTime Deadline { get; set; }
    public string Assignee { get; set; }
}
