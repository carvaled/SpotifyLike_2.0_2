using SpotifyLike.STS.Model;

namespace SpotifyLike.STS.Data.Interfaces {

    internal interface IIdentityRepository
    {
        Task<User> FindByEmail(string email);
        Task<User> FindByIdAsync(Guid Id);
    }
}