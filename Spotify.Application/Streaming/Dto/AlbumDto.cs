using System.ComponentModel.DataAnnotations;

namespace Spotify.Application.Streaming.Dto
{
    public class AlbumDto
    {
        public Guid Id { get; set; }

        [Required]
        public Guid BandaId { get; set; }

        [Required]
        public string? Nome { get; set; }
        public string? Url { get; set; }
        public List<MusicaDto> Musicas { get; set; } = new List<MusicaDto>();

    }


    public class MusicaDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid AlbumId { get; set; }
        public String? Nome { get; set; }
        public String? Url { get; set; }
        public int Duracao { get; set; }

    }
}
