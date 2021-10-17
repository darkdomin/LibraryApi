using System.Collections.Generic;

namespace LibraryApi.Entieties
{
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int? EmployeeId { get; set; }
        public virtual Employee Employees { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Publication> Publications { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
