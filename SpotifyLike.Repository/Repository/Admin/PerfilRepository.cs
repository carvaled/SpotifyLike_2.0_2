using Domain.Admin.ValueObject;

namespace SpotifyLike.Repository.Admin
{
    public class PerfilRepository : RepositoryBase<Perfil>
    {
        public SpotifyLikeContextAdmin Context { get; }
        public PerfilRepository(SpotifyLikeContextAdmin context) : base(context)
        {
            Context = context;
        }
        public Perfil GetById(int id)
        {
            return Context.Set<Perfil>().Find(id) ?? new();
        }
    }
}