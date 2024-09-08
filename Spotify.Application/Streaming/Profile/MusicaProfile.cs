using Spotify.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace Spotify.Application.Streaming.Profile
{
    public class MusicaProfile : AutoMapper.Profile
    {
        public MusicaProfile()
        {
            CreateMap<MusicaDto, Musica>()
                .ReverseMap();

            CreateMap<Musica, MusicaDto>()
                .ReverseMap();

            CreateMap<AlbumDto, Album>()
                .ReverseMap();
            CreateMap<BandaDto, Banda>()
                .ReverseMap();
        }
    }
}