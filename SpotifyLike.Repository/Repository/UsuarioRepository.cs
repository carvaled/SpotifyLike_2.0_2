using SpotifyLike.Domain.Conta.Agreggates;

namespace SpotifyLike.Repository.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>
    {
        public SpotifyLikeContext Context { get; set; }

        public UsuarioRepository(SpotifyLikeContext context) : base(context)
        {
            Context = context;
        }

        public Usuario GetById(Guid id)
        {
            return this.Context.Usuarios
                       .FirstOrDefault(x => x.Id == id);
        }


    }
}
