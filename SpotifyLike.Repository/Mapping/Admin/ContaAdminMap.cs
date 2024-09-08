using Domain.Admin.Agreggates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SpotifyLike.Repository.Mapping.Admin
{
    public class ContaAdminMap : IEntityTypeConfiguration<ContaAdmin>
    {
        public void Configure(EntityTypeBuilder<ContaAdmin> builder)
        {
            builder.ToTable("ContaAdmin");
            builder.HasKey(account => account.Id);
            builder.Property(account => account.Id).ValueGeneratedOnAdd();
            builder.Property(account => account.Nome).IsRequired().HasMaxLength(100);
            builder.Property(account => account.Senha).IsRequired();
            builder.Property(account => account.DataCricao).IsRequired();
            builder.HasOne(account => account.PerfilType).WithMany(perfil => perfil.Usuarios).IsRequired();
            
        }
    }
}