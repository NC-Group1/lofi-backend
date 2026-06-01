using lofi_backend.Database;
using lofi_backend.HealthChecks;
using lofi_backend.Repository;
using lofi_backend.Repository.Authentication;
using lofi_backend.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Supabase;
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

            //builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = false)
            //    .AddEntityFrameworkStores<LoFiDbContext>()
            //    .AddDefaultTokenProviders();

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            //})
            //    .AddCookie()

            //    .AddGoogle(options =>
            //    {
            //        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            //        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            //        options.CallbackPath = "/signing-google";
            //        options.Events.OnCreatingTicket = ctx =>
            //        {
            //            var identity = (ClaimsIdentity)ctx.Principal.Identity;
            //            var email = ctx.User.GetProperty("email").GetString();
            //            var name = ctx.User.GetProperty("name").GetString();
            //            identity.AddClaim(new Claim(ClaimTypes.Email, email));
            //            identity.AddClaim(new Claim(ClaimTypes.Name, name));
            //            return Task.CompletedTask;
            //        };
            //    });
            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //    options.Cookie.SameSite = SameSiteMode.Strict;
            //});

            var bytes = Encoding.UTF8.GetBytes(builder.Configuration["Authentication: JwtSecret"]);

            builder.Services.AddAuthentication().AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(bytes),
                    ValidAudience = builder.Configuration["Authentication: ValidAudience"],
                    ValidIssuer = builder.Configuration["Authentication: ValidIssuer"]
                };
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
