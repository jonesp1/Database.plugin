using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Database.plugin.Settings;
using Database.plugin.DynamoDB;

namespace Database.plugin.DynamoDB
{
    public static class Extensions
    {
        public static IServiceCollection AddDynamoDB(this IServiceCollection services)
        {
            services.AddDefaultAWSOptions(new AWSOptions
            {
                // Configure AWS options here if needed
            });

            services.AddSingleton<IAmazonDynamoDB>(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var dynamoDbSettings = configuration.GetSection(nameof(DynamoDbSettings)).Get<DynamoDbSettings>();

                var clientConfig = new AmazonDynamoDBConfig
                {
                    ServiceURL = dynamoDbSettings.ServiceUrl
                };

                return new AmazonDynamoDBClient(clientConfig);
            });

            return services;
        }

        public static IServiceCollection AddDynamoDBRepository<T>(this IServiceCollection services, string tableNamePrefix) where T : IEntity
        {
            services.AddSingleton<IRepository<T>>(serviceProvider =>
            {
                var dynamoDBClient = serviceProvider.GetService<IAmazonDynamoDB>();
                return new DynamoDBRepository(dynamoDBClient, tableNamePrefix);
            });

            return services;
        }
    }
}