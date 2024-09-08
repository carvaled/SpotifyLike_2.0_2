using AutoMapper;
using Spotify.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Repository;

namespace Spotify.Application.Streaming
{
    public class MusicaService
    {
        private MusicaRepository Repository { get; set; }
        private IMapper Mapper { get; set; }

        public MusicaService(MusicaRepository musicaRepository, IMapper mapper)
        {
            Repository = musicaRepository;
            Mapper = mapper;
        }

        public MusicaDto Create(MusicaDto dto)
        {
            Musica musica = this.Mapper.Map<Musica>(dto);
            this.Repository.Save(musica);

            return this.Mapper.Map<MusicaDto>(musica);
        }

        public MusicaDto FindById(Guid id)
        {
            var musica = this.Repository.GetById(id);
            var result = this.Mapper.Map<MusicaDto>(musica);
            return result;
        }

        public IEnumerable<MusicaDto> FindAll(Guid userId)
        {
            var musicas = this.Repository.GetAll().Where(c => c.Id == userId).ToList();
            var result = this.Mapper.Map<IEnumerable<MusicaDto>>(musicas);
            return result;
        }

        public IEnumerable<MusicaDto> FindAll()
        {
            var result = this.Mapper.Map<IEnumerable<MusicaDto>>(this.Repository.GetAll());
            return result;
        }

        public MusicaDto Update(MusicaDto dto)
        {
            var musica = this.Mapper.Map<Musica>(dto);
            this.Repository.Update(musica);
            return this.Mapper.Map<MusicaDto>(musica);
        }

        public bool Delete(MusicaDto dto)
        {
            var musica = this.Mapper.Map<Musica>(dto);
            this.Repository.Delete(musica);
            return true;
        }
    }
}
