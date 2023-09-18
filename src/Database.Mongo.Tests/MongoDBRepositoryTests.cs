using Database.plugin;
using Database.plugin.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using Moq;
using NSubstitute;
using NUnit.Framework;
namespace Database.Mongo.Tests;

public class MongoDBRepositoryTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Constructor_SetsDbCollection()
    {
        // Arrange
        var mockDatabase = new Mock<IMongoDatabase>();
        var mockCollection = new Mock<IMongoCollection<MockEntity>>();

        mockDatabase
            .Setup(db => db.GetCollection<MockEntity>("myCollection", It.IsAny<MongoCollectionSettings>()))
            .Returns(mockCollection.Object);

        // Act
        var repository = new MongoRepository<MockEntity>(mockDatabase.Object, "myCollection");

        // Assert
        Assert.That(repository.dbCollection, Is.SameAs(mockCollection.Object));
    }  

}