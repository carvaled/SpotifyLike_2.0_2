using Domain.Admin.ValueObject;

namespace Domain.Admin.Agreggates;
public class ContaAdmin 
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public virtual Perfil? PerfilType { get; set; }
    public virtual DateTime DataCricao { get; set; } = DateTime.Now;
}