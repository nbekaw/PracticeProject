using Domain.Entities;
using Domain.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb.Repositories
{
    public class UserRepository
    {
        private readonly MongoDbContext _context;

        public UserRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _context.Users.Find(_ => true).ToListAsync();
        }

        public async Task<UserEntity> GetByIdAsync(string id)
        {
            return await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserEntity> GetByLoginAsync(string login)
        {
            return await _context.Users.Find(u => u.Login == login).FirstOrDefaultAsync();
        }

        public async Task<UserEntity> CreateAsync(UserEntity user)
        {
            await _context.Users.InsertOneAsync(user);
            return user;
        }

        public async Task<UserEntity> UpdateAsync(string id, UserEntity userIn)
        {
            await _context.Users.ReplaceOneAsync(u => u.Id == id, userIn);
            return userIn;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _context.Users.DeleteOneAsync(u => u.Id == id);
            return result.DeletedCount > 0;
        }
    }
}