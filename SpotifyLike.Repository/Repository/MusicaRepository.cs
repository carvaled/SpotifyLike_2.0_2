using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Repository.Repository
{
    public class MusicaRepository : RepositoryBase<Musica>
    {
        public MusicaRepository(SpotifyLikeContext context) : base(context)
        {
        }
    }
}
