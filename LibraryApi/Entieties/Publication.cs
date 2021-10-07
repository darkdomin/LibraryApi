namespace LibraryApi.Entieties
{
    public class Publication
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Isbn { get; set; }

        public int LibraryId { get; set; }
        public virtual Library Library { get; set; }
    }
}