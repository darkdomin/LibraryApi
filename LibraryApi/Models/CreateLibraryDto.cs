using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class CreateLibraryDto
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

    }
}
