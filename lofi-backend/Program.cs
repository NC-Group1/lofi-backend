using lofi_backend.Database;
using lofi_backend.HealthChecks;
using lofi_backend.Repository;
using lofi_backend.Repository.Authentication;
using lofi_backend.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Supabase;
using System.Security.Claims;
using System.Text;

namespace lofi_backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("customsettings.json");

            var supabaseUrl = builder.Configuration["Supabase:Url"]!;
            var supabaseKey = builder.Configuration["Supabase:Key"]!;
            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            var supabaseSignatureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(supabaseKey));
            var validIssuers = supabaseUrl + "/auth/v1";
            List<string> validAudiences = ["authenticated"];

            builder.Services.AddAuthorization();

            builder.Services.AddAuthentication().AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = supabaseSignatureKey,
                    ValidIssuer = validIssuers
                };
            });

            builder.Services.AddSingleton(provider => 
                new Supabase.Client(supabaseUrl, supabaseKey, options));

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITaskTimerRepository, TaskTimerRepository>();
            builder.Services.AddScoped<ITaskTimerService, TaskTimerService>();
            builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            builder.Services.AddHealthChecks().AddCheck<ApiHealthCheck>("api_health_check",
                failureStatus: HealthStatus.Unhealthy, tags: new[] { "api", "users" }).AddCheck<DatabaseHealthCheck>("database_health_check",
                failureStatus: HealthStatus.Unhealthy, tags: new[] {"database", "users" });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<LoFiDbContext>(options =>
            {
                var _connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is "Development")
                {
                    var connection = new SqliteConnection(_connectionString);
                    connection.Open();
                    options.UseSqlite(connection);
                }
                else
                {
                    Console.WriteLine($"Connection: ${_connectionString}");
                    options.UseSqlServer(_connectionString);
                }
            });

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
            }
            var app = builder.Build();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<LoFiDbContext>();

                if (app.Environment.IsDevelopment()) db.Database.EnsureCreated();
                else db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHealthChecks("/health");


            app.MapControllers();

            app.Run();
        }
    }
}
