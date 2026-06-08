using System.ComponentModel.DataAnnotations;
using lofi_backend.Data_Models.Enums;

namespace lofi_backend.Models
{
    public class Playlist
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public Mood Mood { get; set; } = Mood.Chill;
        [Required]
        public Genre Genre { get; set; } = Genre.LoFi;
        public List<Music> Songs { get; set; } = new List<Music>();
    }
}
