using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.MongoDb;

public class MongoDbContext
{
    private readonly IMongoDatabase _db;

    public MongoDbContext(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDb:ConnectionString"]);
        _db = client.GetDatabase(config["MongoDb:DatabaseName"]);
    }

    public IMongoCollection<UserEntity> Users => _db.GetCollection<UserEntity>("Users");
    public IMongoCollection<TaskEntity> Tasks => _db.GetCollection<TaskEntity>("Tasks");
}
