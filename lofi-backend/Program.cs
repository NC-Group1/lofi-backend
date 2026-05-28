using Microsoft.EntityFrameworkCore;
using lofi_backend.Database;
using Microsoft.Data.Sqlite;
using lofi_backend.Repository;
using lofi_backend.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;



namespace lofi_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<LoFiDbContext>(options =>
            {
                var _connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is "Development")
                {
                    Console.WriteLine($"Connection: ${_connectionString}");
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

            //builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = false)
            //    .AddEntityFrameworkStores<LoFiDbContext>()
            //    .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
                .AddCookie()

                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                    options.CallbackPath = "/signing-google";
                    options.Events.OnCreatingTicket = ctx =>
                    {
                        var identity = (ClaimsIdentity)ctx.Principal.Identity;
                        var email = ctx.User.GetProperty("email").GetString();
                        var name = ctx.User.GetProperty("name").GetString();
                        identity.AddClaim(new Claim(ClaimTypes.Email, email));
                        identity.AddClaim(new Claim(ClaimTypes.Name, name));
                        return Task.CompletedTask;
                    };
                });
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });

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


            app.MapControllers();

            app.Run();
        }
    }
}
