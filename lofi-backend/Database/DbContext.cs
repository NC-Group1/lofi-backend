using lofi_backend.Data_Models;
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

        public LoFiDbContext(DbContextOptions<LoFiDbContext> options) : base(options){ }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
