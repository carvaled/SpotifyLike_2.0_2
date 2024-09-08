using Spotify.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace Spotify.Application.Streaming.Profile
{
    public class BandaProfile : AutoMapper.Profile
    {
        public BandaProfile()
        {
            CreateMap<BandaDto, Banda>()
                .ReverseMap();
        }
    }
}
