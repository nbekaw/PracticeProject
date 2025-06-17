using Domain.Entities;
using Domain.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb.Repositories
{
    public class TaskRepository
    {
        private readonly MongoDbContext _context;

        public TaskRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _context.Tasks.Find(_ => true).ToListAsync();
        }

        public async Task<TaskEntity> GetByIdAsync(string id)
        {
            return await _context.Tasks.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetByUserIdAsync(string userId)
        {
            return await _context.Tasks.Find(t => t.UserId == userId).ToListAsync();
        }

        public async Task<TaskEntity> CreateAsync(TaskEntity task)
        {
            await _context.Tasks.InsertOneAsync(task);
            return task;
        }

        public async Task<TaskEntity> UpdateAsync(string id, TaskEntity taskIn)
        {
            await _context.Tasks.ReplaceOneAsync(t => t.Id == id, taskIn);
            return taskIn;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _context.Tasks.DeleteOneAsync(t => t.Id == id);
            return result.DeletedCount > 0;
        }
    }
}