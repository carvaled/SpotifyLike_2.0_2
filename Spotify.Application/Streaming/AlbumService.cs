using AutoMapper;
using Spotify.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Repository;

namespace Spotify.Application.Streaming
{
    public class AlbumService
    {
        private AlbumRepository Repository { get; set; }
        private IMapper Mapper { get; set; }


        public AlbumService(AlbumRepository albumRepository, IMapper mapper)
        {
            Repository = albumRepository;
            Mapper = mapper;
        }

        public AlbumDto Create(AlbumDto dto)
        {
            Album album = this.Mapper.Map<Album>(dto);
            this.Repository.Save(album);

            return this.Mapper.Map<AlbumDto>(album);
        }

        public AlbumDto FindById(Guid id)
        {
            var album = this.Repository.GetById(id);
            var result = this.Mapper.Map<AlbumDto>(album);
            return result;
        }

        public IEnumerable<AlbumDto> FindAll(Guid userId)
        {
            var albums = this.Repository.GetAll().Where(c => c.Id == userId).ToList();
            var result = this.Mapper.Map<IEnumerable<AlbumDto>>(albums);
            return result;
        }

        public IEnumerable<AlbumDto> FindAll()
        {
            var result = this.Mapper.Map<IEnumerable<AlbumDto>>(this.Repository.GetAll());
            return result;
        }

        public AlbumDto Update(AlbumDto dto)
        {
            var album = this.Mapper.Map<Album>(dto);
            this.Repository.Update(album);
            return this.Mapper.Map<AlbumDto>(album);
        }

        public bool Delete(AlbumDto dto)
        {
            var album = this.Mapper.Map<Album>(dto);
            this.Repository.Delete(album);
            return true;
        }
    }
}
