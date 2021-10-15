using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class CreatePublicationDto
    {
        [Required]
        public string Title { get; set; }
        public int ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Isbn { get; set; }

        public int LibraryId { get; set; }
    }
}
