using System.ComponentModel.DataAnnotations;
using Azure.Identity;
using lofi_backend.Data_Models.Enums;

namespace lofi_backend.Data_Models
{
    public class User(int id, string username, string firstName, string lastName, string email, int age, Gender gender = Gender.PreferNotToSay)
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = username;
        [Required]
        public string FirstName { get; set; } = firstName;
        [Required]
        public string LastName { get; set; } = lastName;
        [Required]
        public string Email { get; set; } = email;
        [Required]
        public int Age { get; set; } = age;
        [Required]
        public Gender Gender { get; set; } = gender;
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
