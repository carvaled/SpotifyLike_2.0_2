using Domain.Admin.Agreggates;

namespace Domain.Admin.ValueObject;
public record Perfil
{
    public virtual IList<ContaAdmin>? Usuarios { get; set; }

    public Perfil() : base() { }

    public enum TipoUsuario
    {
        Invalid = 0,
        Admin = 1,
        Normal = 2
    }

    public virtual int Id { get; set; }
    public virtual string? Descricao { get; set; }    

    public Perfil(TipoUsuario type)
    {
        Id = (int)type;
        Descricao = GetDescription(type);
    }

    private static string GetDescription(TipoUsuario userType)
    {
        return userType switch
        {
            TipoUsuario.Admin => "Administrador",
            TipoUsuario.Normal => "Normal",
            _ => throw new ArgumentException("Tipo de usuário inválido.")
        };
    }
}
