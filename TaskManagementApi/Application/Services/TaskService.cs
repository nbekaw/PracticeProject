using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.MongoDb;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services;

public class TaskService : ITaskService
{
    private readonly MongoDbContext _context;
    private readonly IMapper _mapper;

    public TaskService(MongoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TaskDto>> GetAll(string userId)
    {
        var tasks = await _context.Tasks.Find(t => t.UserId == userId).ToListAsync();
        return _mapper.Map<List<TaskDto>>(tasks);
    }

    public async Task<TaskDto> GetById(string id, string userId)
    {
        var task = await _context.Tasks.Find(t => t.Id == id && t.UserId == userId).FirstOrDefaultAsync();
        return _mapper.Map<TaskDto>(task);
    }

    public async Task Create(TaskDto dto, string userId)
    {
        var task = _mapper.Map<TaskEntity>(dto);
        task.UserId = userId;
        await _context.Tasks.InsertOneAsync(task);
    }

    public async Task Update(string id, TaskDto dto, string userId)
    {
        var task = _mapper.Map<TaskEntity>(dto);
        task.Id = id;
        task.UserId = userId;
        await _context.Tasks.ReplaceOneAsync(t => t.Id == id && t.UserId == userId, task);
    }

    public async Task Delete(string id, string userId)
    {
        await _context.Tasks.DeleteOneAsync(t => t.Id == id && t.UserId == userId);
    }
}
