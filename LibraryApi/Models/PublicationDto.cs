using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models
{
    public class PublicationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Isbn { get; set; }
    }
}
