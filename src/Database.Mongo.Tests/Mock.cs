using Database.plugin;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MockEntity : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }
}

