using lofi_backend.Data_Models;
using lofi_backend.Database;
using Microsoft.EntityFrameworkCore;

namespace lofi_backend.Repository
{

    public interface IPlaylistRepository
    {
        public IEnumerable<Playlist> GetAllPlaylists();
        public Playlist GetPlaylistById(string id);
        public Playlist CreatePlaylist(Playlist playlist);
        public Playlist EditPlaylist(Playlist playlist);
        public Playlist DeletePlaylist(string id);
    }

    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly LoFiDbContext _db;

        public PlaylistRepository(LoFiDbContext dbContext)
        {
            _db = dbContext;
        }

        public IEnumerable<Playlist> GetAllPlaylists()
        { 
            return _db.Playlists.ToList();
        }
        public Playlist GetPlaylistById(string id)
        {
            return _db.Playlists.FirstOrDefault(p => p.Id == id);
        }
        public Playlist CreatePlaylist(Playlist playlist)

        public Playlist EditPlaylist(Playlist playlist);
        public Playlist DeletePlaylist(string id);
    }
}
