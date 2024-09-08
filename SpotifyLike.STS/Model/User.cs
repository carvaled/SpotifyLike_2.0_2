namespace SpotifyLike.STS.Model
{
    internal class User
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Nome { get; set; }
        public string? Senha { get; set; }
    }
}