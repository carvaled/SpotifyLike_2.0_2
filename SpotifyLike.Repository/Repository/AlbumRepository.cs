using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Repository.Repository
{
    public class AlbumRepository : RepositoryBase<Album>
    {
        public AlbumRepository(SpotifyLikeContext context) : base(context)
        {
        }
    }
}
