using Domain.Enums;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public class TaskEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Domain.Enums.TaskStatus Status { get; set; }
    public DateTime Deadline { get; set; }
    public string Assignee { get; set; }
    public string UserId { get; set; }
}
