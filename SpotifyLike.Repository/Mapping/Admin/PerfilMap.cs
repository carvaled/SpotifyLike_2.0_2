using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Admin.ValueObject;

namespace SpotifyLike.Repository.Mapping.Admin
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("Perfil");

            builder.HasKey(perfil => perfil.Id);
            builder.Property(perfil => perfil.Id).IsRequired().HasConversion<int>();
            builder.Property(perfil => perfil.Descricao).IsRequired();

            builder.HasData
            (
                new Perfil(Perfil.TipoUsuario.Admin),
                new Perfil(Perfil.TipoUsuario.Normal)
            );
        }
    }
}