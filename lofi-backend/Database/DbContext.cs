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

        public LoFiDbContext(DbContextOptions<LoFiDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is "Development")
            {
                modelBuilder.Entity<UserData>().HasData(
                    new UserData
                    {
                        Id = "test1", 
                        Username = "studyowl",
                        FirstName = "Emma", 
                        LastName = "Thompson",
                        Email = "emma.thompson@example.com", 
                        DateOfBirth = new DateTime(1988, 4, 12), 
                        Gender = Gender.Female, 
                    },
                    new UserData
                    {
                        Id = "test2", 
                        Username = "lofilover",
                        FirstName = "Matthew", 
                        LastName = "Painter",
                        Email = "matthew.p@example.com", 
                        DateOfBirth = new DateTime(1995, 11, 3), 
                        Gender = Gender.Male
                    },
                    new UserData
                    {
                        Id = "test3", 
                        Username = "nightwave", 
                        FirstName = "Sofia", 
                        LastName = "Nguyen",
                        Email = "s.nguyen@example.com", 
                        DateOfBirth = new DateTime(2000, 9, 5), 
                        Gender = Gender.NonBinary
                    }
                );

                modelBuilder.Entity<Project>().HasData(
                    new Project
                    {
                        Id = 1, 
                        UserId = "test1", 
                        Name = "Portfolio Website",
                        StartDate = new DateTime(2026, 1, 6, 13, 0, 0), 
                        EndDate = new DateTime(2026, 1, 7, 13, 0, 0), 
                        Timers = new List<TaskTimer>()
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

                modelBuilder.Entity<TaskTimer>().HasData(
                    new TaskTimer
                    {
                        Id = 1, ProjectId = 1, DateCreated = new DateTime(2026, 1, 6, 13, 0, 0),
                        DateUpdated = new DateTime(2026, 1, 6, 14, 0, 0), Duration = 3600, IsActive = false
                    },
                    new TaskTimer
                    {
                        Id = 2, ProjectId = 2, DateCreated = new DateTime(2026, 6, 16, 12, 0, 0),
                        DateUpdated = new DateTime(2026, 6, 16, 14, 30, 0), Duration = 9000, IsActive = false
                    },
                    new TaskTimer
                    {
                        Id = 3, ProjectId = 3, DateCreated = new DateTime(2026, 6, 17, 10, 0, 0),
                        DateUpdated = new DateTime(2026, 6, 17, 12, 15, 0), Duration = 8100, IsActive = false
                    },
                    new TaskTimer
                    {
                        Id = 4, ProjectId = 4, DateCreated = new DateTime(2026, 6, 22, 13, 0, 0),
                        DateUpdated = new DateTime(2026, 6, 22, 13, 45, 0), Duration = 2700, IsActive = false
                    }
                );

                modelBuilder.Entity<Music>().HasData(
                    new Music
                    {
                        Id = 1, Title = "Chillhop Essentials - Spring 2026",
                        Artist = "Various Artists", Channel = "Chillhop Essentials",
                        Mood = Mood.Relax, Genre = Genre.LoFi, URL = "https://example.com/music/chillhop-spring-2026.mp3"
                    },
                    new Music
                    {
                        Id = 2, Title = "Lofi Study Beats",
                        Artist = "DJ Sleepy", Channel = "Lofi Vibes",
                        Mood = Mood.Study, Genre = Genre.Chill, URL = "https://example.com/music/lofi-study-beats.mp3"
                    },
                    new Music
                    {
                        Id = 3, Title = "Late Night Lo-Fi",
                        Artist = "Night Owl", Channel = "Midnight Moods",
                        Mood = Mood.Sleep, Genre = Genre.LoFi, URL = "https://example.com/music/late-night-lofi.mp3"
                    }
                );

                modelBuilder.Entity<Playlist>().HasData(
                    new Playlist
                    {
                        Id = "playlist1",
                        Name = "Morning Focus",
                        Mood = Mood.Focus,
                        Genre = Genre.LoFi,
                        Songs = new List<Music>()

                    },

                    new Playlist
                    {
                        Id = "playlist2",
                        Name = "Relaxing Evening",
                        Mood = Mood.Relax,
                        Genre = Genre.Chill,
                        Songs = new List<Music>()
                    },

                    new Playlist
                    {
                        Id = "playlist3",
                        Name = "Sleepy Time",
                        Mood = Mood.Sleep,
                        Genre = Genre.LoFi,
                        Songs = new List<Music>()
                    }
                );
            }
        }
    }
}
