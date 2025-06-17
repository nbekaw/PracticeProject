using Application.Dtos;
using FluentValidation;
using System;

namespace Application.Validators;

public class TaskValidator : AbstractValidator<TaskDto>
{
    public TaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Deadline).GreaterThan(DateTime.Now);
    }
}
