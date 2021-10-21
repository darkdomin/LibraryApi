using System.Collections.Generic;

namespace LibraryApi.Entieties
{
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Publication> Publications { get; set; }
    }
}
