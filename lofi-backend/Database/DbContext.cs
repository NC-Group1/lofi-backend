using lofi_backend.Data_Models;
using lofi_backend.Data_Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace lofi_backend.Database
{
    public class LoFiDbContext : DbContext
    {
        public DbSet<Music> Music { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<TaskTimer> Timers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserData> Users { get; set; }

        public LoFiDbContext(DbContextOptions<LoFiDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is "Development")
            {
                modelBuilder.Entity<UserData>().HasData(
                    new UserData
                    {
                        Id = "test1", Username = "studyowl",
                        FirstName = "Emma", LastName = "Thompson",
                        Email = "emma.thompson@example.com", DateOfBirth = new DateTime(1988, 4, 12), Gender = Gender.Female
                    },
                    new UserData
                    {
                        Id = "test2", Username = "lofilover",
                        FirstName = "Matthew", LastName = "Painter",
                        Email = "matthew.p@example.com", DateOfBirth = new DateTime(1995, 11, 3), Gender = Gender.Male
                    },
                    new UserData
                    {
                        Id = "test3", Username = "nightwave", 
                        FirstName = "Sofia", LastName = "Nguyen",
                        Email = "s.nguyen@example.com", DateOfBirth = new DateTime(2000, 9, 5), Gender = Gender.NonBinary
                    }
                );

                modelBuilder.Entity<Project>().HasData(
                    new Project
                    {
                        Id = 1, UserId = "test1", Name = "Portfolio Website",
                        StartDate = new DateTime(2026, 1, 6, 13, 0, 0), 
                        EndDate = new DateTime(2026, 1, 7, 13, 0, 0), Timers = new List<TaskTimer>()
                    },
                    new Project
                    {
                        Id = 2, UserId = "test2", Name = "English Essay",
                        StartDate = new DateTime(2026, 6, 16, 12, 0, 0),
                        EndDate = new DateTime(2026, 6, 20, 16, 0, 0), Timers = new List<TaskTimer>()
                    },
                    new Project
                    {
                        Id = 3, UserId = "test3", Name = "Sewing skirt",
                        StartDate = new DateTime(2026, 6, 17, 10, 0, 0),
                        EndDate = new DateTime(2026, 6, 17, 17, 0, 0), Timers = new List<TaskTimer>()
                    },
                    new Project
                    {
                        Id = 4, UserId = "test1", Name = "Apply for job",
                        StartDate = new DateTime(2026, 6, 22, 13, 0, 0),
                        EndDate = new DateTime(2026, 6, 22, 14, 0, 0), Timers = new List<TaskTimer>()
                    }
                );
            }
        }
    }
}
