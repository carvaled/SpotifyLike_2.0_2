using Domain.Admin.Agreggates;
using Domain.Admin.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpotifyLike.Repository.Mapping.Admin;

namespace SpotifyLike.Repository {
    public class SpotifyLikeContextAdmin : DbContext
    {

        public SpotifyLikeContextAdmin(DbContextOptions<SpotifyLikeContextAdmin> options) : base(options) { }
        public DbSet<ContaAdmin> Admin { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContaAdminMap());
            modelBuilder.ApplyConfiguration(new PerfilMap());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));
            base.OnConfiguring(optionsBuilder);
        }
    } 
}