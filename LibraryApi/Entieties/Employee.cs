namespace LibraryApi.Entieties
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Pesel { get; set; }

        public virtual Library Library { get; set; }
    }
}
