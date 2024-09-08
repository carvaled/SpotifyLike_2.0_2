using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Streaming.ValueObject;

namespace SpotifyLike.Repository.Mapping.Streaming
{
    public class MusicaMapping : IEntityTypeConfiguration<Musica>
    {
        public void Configure(EntityTypeBuilder<Musica> builder)
        {
            builder.ToTable(nameof(Musica));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Url).IsRequired();

            builder.OwnsOne<Duracao>(d => d.Duracao, c =>
            {
                c.Property(x => x.Valor).IsRequired().HasMaxLength(50);
            });


        }
    }
}
