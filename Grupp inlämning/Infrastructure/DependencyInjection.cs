using Application.Interfaces.AzureStorageIntefaces;
using Application.Interfaces.RepositoryInterfaces;
using Azure.Storage.Blobs;
using Infrastructure.Configurations;
using Infrastructure.Databases;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionstring, string azureStorageConnectionString)
        {
            services.AddDbContext<Database>(options =>
            {
                options.UseSqlServer(connectionstring);
            });
            
            services.AddScoped<ICvRepository, CvRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Registrera Azure BlobServiceClient
            services.AddSingleton(x =>
            {
                return new BlobServiceClient(azureStorageConnectionString);
            });

            // Registrera din egen AzureStorageService
            services.AddScoped<IAzureStorageService, AzureStorageService>();

            return services;
        }
    }
}
