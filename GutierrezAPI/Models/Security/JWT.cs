namespace GutierrezAPI.Models.Security
{
    public class JWT
    {
        public string Audience { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
    }
}
