using System.ComponentModel.DataAnnotations;
using lofi_backend.Data_Models.Enums;

namespace lofi_backend.Data_Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public int Age { get; set; } = 0;
        [Required]
        public Gender Gender { get; set; } = Gender.PreferNotToSay;
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
