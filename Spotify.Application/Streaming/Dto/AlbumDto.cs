using System.ComponentModel.DataAnnotations;

namespace Spotify.Application.Streaming.Dto
{
    public class AlbumDto
    {
        public Guid Id { get; set; }

        [Required]
        public Guid BandaId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string? Nome { get; set; }
        public string? Backdrop { get; set; }
        public List<MusicaDto> Musicas { get; set; } = new List<MusicaDto>();
    }
}
