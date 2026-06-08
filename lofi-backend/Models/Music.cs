using System.ComponentModel.DataAnnotations;
using lofi_backend.Data_Models.Enums;

namespace lofi_backend.Models
{
    public class Music
    {
        [Required] 
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = "";
        [Required] 
        public string Artist { get; set; } = "";
        [Required]
        public string Channel { get; set; } = "";
        public Mood Mood { get; set; } = Mood.Chill;
        public Genre Genre { get; set; } = Genre.LoFi;
        public string URL { get; set; } = "";
        public string Thumbnail { get; set; } = "";
    }
}
