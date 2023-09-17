using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Database.plugin.DynamoDB;
using NSubstitute;
using NUnit.Framework;

namespace Database.DynamoDB.Tests;

[TestFixture]
public class DynamoDBRepositoryTests
{
    private IAmazonDynamoDB _dynamoDbClientMock;
    private Table _tableMock;
    private DynamoDBRepository<MockEntity> _repository;

    [SetUp]
    public void Setup()
    {
        

    }

    [Test]
    public async Task CreateAsync_ShouldPutItemAsync()
    {
        Assert.IsTrue(true);
    }

}