using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
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

            services.AddScoped<IRepository<About>, GenericRepository<About>>();
            services.AddScoped<IRepository<ContactDetail>, GenericRepository<ContactDetail>>();
            services.AddScoped<IRepository<CV>, GenericRepository<CV>>();
            services.AddScoped<IRepository<Education>, GenericRepository<Education>>();
            services.AddScoped<IRepository<Skill>, GenericRepository<Skill>>();
            services.AddScoped<IRepository<User>, GenericRepository<User>>();
            services.AddScoped<IRepository<WorkExperience>, GenericRepository<WorkExperience>>();

            return services;
        }
    }
}
