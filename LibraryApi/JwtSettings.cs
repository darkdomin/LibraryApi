namespace LibraryApi
{
    public class JwtSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpiresDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
