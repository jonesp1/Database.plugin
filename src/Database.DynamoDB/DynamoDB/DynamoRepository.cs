using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Database.plugin.DynamoDB
{
    public class DynamoDBRepository<T> : IDynamoDbRepository<T> where T : IEntity
    {
        private readonly Table _table;

        public DynamoDBRepository(IAmazonDynamoDB dynamoDbClient, string tableName)
        {
            _table = Table.LoadTable(dynamoDbClient, tableName);
        }

        public async Task CreateAsync(T entity)
        {
            var document = EntityToDocument(entity); // Implement this method to convert your entity to a Document
            await _table.PutItemAsync(document);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            var scan = _table.Scan(new ScanOperationConfig());
            var documents = await scan.GetNextSetAsync();

            return documents.Select(ConvertToEntity).ToList();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            var scanFilter = new ScanFilter();
            // Implement the filter logic based on the provided expression here

            var scan = _table.Scan(scanFilter);
            var documents = await scan.GetNextSetAsync();

            return documents.Select(ConvertToEntity).ToList();
        }

        public async Task<T> GetAsync(string id)
        {
            var document = await _table.GetItemAsync(id);
            return ConvertToEntity(document);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            var scanFilter = new ScanFilter();
            // Implement the filter logic based on the provided expression here

            var scan = _table.Scan(scanFilter);
            var documents = await scan.GetNextSetAsync();
            var document = documents.FirstOrDefault();

            return ConvertToEntity(document);
        }

        public async Task RemoveAsync(string id)
        {
            await _table.DeleteItemAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            var document = EntityToDocument(entity); // Implement this method to convert your entity to a Document
            await _table.UpdateItemAsync(document);
        }

        // Helper method to convert DynamoDB document to your entity
        private T? ConvertToEntity(Document document)
        {
            if (document == null)
                return default;

            // Implement logic to convert DynamoDB document to your entity here

            return default; // Replace with actual conversion logic
        }

        private Document EntityToDocument(T entity)
    {
            var document = new Document
            {
                ["Id"] = entity.Id // Assuming your entity has an "Id" property of type string
            };

            // Map other properties of your entity to DynamoDB attributes here
            // Example: document["PropertyName"] = entity.PropertyName;

            return document;
    }
    }
}