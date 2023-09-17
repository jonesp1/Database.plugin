using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;
using Database.plugin.Settings;

namespace Database.plugin.DynamoDB
{
    public static class Extensions
    {
        public static IServiceCollection AddDynamoDB(this IServiceCollection services)
        {
            services.AddSingleton(serviceProvider =>
            {
                var client = new AmazonDynamoDBClient();
                return client;
            });

            return services;
        }

        public static IServiceCollection AddDynamoDBRepository<T>(this IServiceCollection services, string tableName) where T : IEntity
        {
            services.AddSingleton<IRepository<T>>(ServiceProvider =>
            {
                var client = ServiceProvider.GetService<AmazonDynamoDBClient>();
                return new DynamoDBRepository<T>(client, tableName);
            });

            return services;
        }
    }
}
