using Domain.Admin.Agreggates;
using Domain.Admin.ValueObject;

namespace SpotifyLike.Repository.Admin
{
    public class ContaAdminRepository : RepositoryBase<ContaAdmin>
    {
        public SpotifyLikeContextAdmin Context { get; }
        public ContaAdminRepository(SpotifyLikeContextAdmin context) : base(context)
        {
            Context = context;
        }

        public override void Save(ContaAdmin entity)
        {
            entity.PerfilType = Context.Set<Perfil>().Find(entity.PerfilType.Id) ?? throw new ArgumentNullException();
            Context.Add(entity);
            Context.SaveChanges();
        }

        public override void Update(ContaAdmin entity)
        {
            entity.PerfilType = Context.Set<Perfil>().Find(entity.PerfilType.Id) ?? throw new ArgumentNullException();
            Context.Update(entity);
            Context.SaveChanges();
        }
    }
}