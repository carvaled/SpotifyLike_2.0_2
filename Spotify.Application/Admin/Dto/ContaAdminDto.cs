using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spotify.Application.Admin.Dto
{
    public class ContaAdminDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório!")]
        [EmailAddress(ErrorMessage = "O campo email é inválido!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        [PasswordPropertyText]
        public string? Senha { get; set; }

        [Required]
        public PerfilDto PerfilType { get; set; }

    }
}