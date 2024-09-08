using AutoMapper;
using Spotify.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Repository;

namespace Spotify.Application.Streaming
{
    public class BandaService
    {
        private BandaRepository Repository { get; set; }
        private IMapper Mapper { get; set; }


        public BandaService(BandaRepository bandaRepository, IMapper mapper)
        {
            Repository = bandaRepository;
            Mapper = mapper;
        }

        public BandaDto Criar(BandaDto dto)
        {
            Banda banda = this.Mapper.Map<Banda>(dto);
            this.Repository.Save(banda);

            return this.Mapper.Map<BandaDto>(banda);
        }

        public BandaDto Obter(Guid id)
        {
            var banda = this.Repository.GetById(id);
            return this.Mapper.Map<BandaDto>(banda);
        }

        public IEnumerable<BandaDto> Obter()
        {
            var banda = this.Repository.GetAll();
            return this.Mapper.Map<IEnumerable<BandaDto>>(banda);
        }

        public AlbumDto AssociarAlbum(AlbumDto dto)
        {
            var banda = this.Repository.GetById(dto.BandaId);

            if (banda == null)
            {
                throw new Exception("Banda não encontrada");
            }

            var novoAlbum = this.AlbumDtoParaAlbum(dto);

            banda.AdicionarAlbum(novoAlbum);

            this.Repository.Update(banda);

            var result = this.AlbumParaAlbumDto(novoAlbum);

            return result;

        }

        public AlbumDto ObterAlbum(Guid idBanda, Guid id)
        {
            var banda = this.Repository.GetById(idBanda);

            if (banda == null)
            {
                throw new Exception("Banda não encontrada");
            }

            var album = banda.Albums.FirstOrDefault(x => x.Id == id);

            var result = AlbumParaAlbumDto(album);
            result.BandaId = banda.Id;

            return result;

        }

        private Album AlbumDtoParaAlbum(AlbumDto dto)
        {
            Album album = new Album()
            {
                Nome = dto.Nome
            };

            foreach (MusicaDto item in dto.Musicas)
            {
                album.AdicionarMusica(new Musica
                {
                    Nome = item.Nome,
                    Duracao = new SpotifyLike.Domain.Streaming.ValueObject.Duracao(item.Duracao)
                });
            }

            return album;
        }

        private AlbumDto AlbumParaAlbumDto(Album album)
        {
            AlbumDto dto = new AlbumDto(); 
            dto.Id = album.Id;
            dto.Nome = album.Nome;

            foreach (var item in album.Musica)
            {
                var musicaDto = new MusicaDto()
                {
                    Id = item.Id,
                    Duracao = item.Duracao.Valor,
                    Nome = item.Nome
                };

                dto.Musicas.Add(musicaDto);
            }

            return dto;
        }

        public BandaDto FindById(Guid id)
        {
            var banda = this.Repository.GetById(id);
            var result = this.Mapper.Map<BandaDto>(banda);
            return result;
        }

        public IEnumerable<BandaDto> FindAll(Guid userId)
        {
            var bandas = this.Repository.GetAll().Where(c => c.Id == userId).ToList();
            var result = this.Mapper.Map<IEnumerable<BandaDto>>(bandas);
            return result;
        }

        public IEnumerable<BandaDto> FindAll()
        {
            var result = this.Mapper.Map<IEnumerable<BandaDto>>(this.Repository.GetAll());
            return result;
        }

        public BandaDto Update(BandaDto dto)
        {
            var banda = this.Mapper.Map<Banda>(dto);
            this.Repository.Update(banda);
            return this.Mapper.Map<BandaDto>(banda);
        }

        public bool Delete(BandaDto dto)
        {
            var banda = this.Mapper.Map<Banda>(dto);
            this.Repository.Delete(banda);
            return true;
        }
    }
}
