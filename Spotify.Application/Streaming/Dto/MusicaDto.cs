using System.ComponentModel.DataAnnotations;

namespace Spotify.Application.Streaming.Dto
{
    public class MusicaDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid AlbumId { get; set; }
        
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public String? Nome { get; set; }

        [Required(ErrorMessage = "O campo Url é obrigatório.")]
        [Url(ErrorMessage = "Está não é uma url válida.")]
        public String? Url { get; set; }
        public int Duracao { get; set; }

    }
}
