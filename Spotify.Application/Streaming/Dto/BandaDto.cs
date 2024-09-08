using System.ComponentModel.DataAnnotations;

namespace Spotify.Application.Streaming.Dto
{
    public class BandaDto
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public String? Nome { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public String? Descricao { get; set; }        

        public String? Backdrop { get; set; }

    }
}
