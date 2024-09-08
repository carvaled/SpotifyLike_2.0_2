using SpotifyLike.Domain.Conta.Agreggates;
using SpotifyLike.Domain.Streaming.ValueObject;

namespace SpotifyLike.Domain.Streaming.Aggregates
{
    public class Musica
    {
        public Guid Id { get; set; }
        public String? Nome { get; set; }
        public Duracao Duracao { get; set; }
        public String? Url { get; set; }

        public virtual IList<Playlist> Playlists { get; set; } = new List<Playlist>();

    }
}
