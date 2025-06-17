using Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ITaskService
{
    Task<List<TaskDto>> GetAll(string userId);
    Task<TaskDto> GetById(string id, string userId);
    Task Create(TaskDto dto, string userId);
    Task Update(string id, TaskDto dto, string userId);
    Task Delete(string id, string userId);
}
