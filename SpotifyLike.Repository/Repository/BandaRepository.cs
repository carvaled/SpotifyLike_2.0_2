using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Repository.Repository
{
    public class BandaRepository : RepositoryBase<Banda>
    {
        public BandaRepository(SpotifyLikeContext context) : base(context)
        {
        }
    }
}
