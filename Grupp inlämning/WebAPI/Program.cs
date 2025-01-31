using Application;
using Infrastructure;
using Infrastructure.Databases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Infrastructure.Configurations;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "https://grupparbete-topaz.vercel.app")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            // Add services to the container.
            //var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            //byte[] secretkey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]!);
            //var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            //string secretKey = jwtSettings["SecretKey"] ?? throw new Exception("JWT SecretKey saknas i konfigurationen!");


            var secretKey = Environment.GetEnvironmentVariable("JwtSettings__SecretKey") // Hämta från Azure miljövariabel
                 ?? builder.Configuration["JwtSettings:SecretKey"] // Fallback till appsettings.json
                 ?? throw new Exception("JWT SecretKey saknas i konfigurationen!");

            byte[] secretkey = Encoding.ASCII.GetBytes(secretKey);
            
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey)
                };
            });

             builder.Services.AddAuthorization(options =>
             {
                 options.AddPolicy("Admin", policy =>
                 {
                     policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                     policy.RequireAuthenticatedUser();
                 });
             });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Grupp Tre", Version = "v1" });

                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     Description = "Authorize with your bearer token that generates when you login",
                     Type = SecuritySchemeType.Http,
                     Scheme = "bearer"
                 });
                
                 c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id= "Bearer"
                             }
                         },
                         Array.Empty<string>()
                     }
                 });
            });

            builder.Services.AddControllers(options =>
            {
                options.CacheProfiles.Add("DefaultCache", new CacheProfile()
                {
                    Duration = 60, // Cache i 60 sekunder
                    Location = ResponseCacheLocation.Any,
                });
            });

            builder.Services.AddMemoryCache();
            builder.Services.AddHttpClient();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            //var azureStorageConnectionString = builder.Configuration.GetConnectionString("AzureStorage");
            var azureStorageConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

            var azureDbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(azureDbConnectionString))
            {
                azureDbConnectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_GRUPP_DB");
            }
            builder.Services.AddApplication().AddInfrastructure(azureDbConnectionString, azureStorageConnectionString);
            builder.Services.AddDbContext<Database>();

            builder.Services.Configure<BlobSettings>(builder.Configuration.GetSection("BlobSettings"));

            builder.Services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            var app = builder.Build();

            // Konfigurera CORS i pipeline
            app.UseCors("AllowFrontend");

            // Configure the HTTP request pipeline.

            //    app.UseSwagger();
            //    app.UseSwaggerUI();



            //app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Grupp Tre v1");
            });

            app.UseRouting();
            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
