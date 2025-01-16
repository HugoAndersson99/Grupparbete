using Application.Interfaces.RepositoryInterfaces;
using Infrastructure.Databases;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionstring)
        {
            services.AddDbContext<Database>(options =>
            {
                options.UseSqlServer(connectionstring);
            });
            
            services.AddScoped<ICvRepository, CvRepository>();
            
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}
