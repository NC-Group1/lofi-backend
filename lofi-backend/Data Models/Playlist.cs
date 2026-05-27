using System.ComponentModel.DataAnnotations;
using lofi_backend.Data_Models.Enums;

namespace lofi_backend.Data_Models
{
    public class Playlist
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public Mood Mood { get; set; } = Mood.Chill;
        [Required]
        public Genre Genre { get; set; } = Genre.LoFi;
        public List<Music> Songs { get; set; } = new List<Music>();
    }
}
