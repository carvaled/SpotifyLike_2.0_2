using IdentityModel;
using IdentityServer4.Validation;
using SpotifyLike.STS.Data.Interfaces;

namespace SpotifyLike.STS.GrantType
{

    internal class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IIdentityRepository repository;

        public ResourceOwnerPasswordValidator(IIdentityRepository repository)
        {
            this.repository = repository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var email = context.UserName;
            var user = await this.repository.FindByEmail(email);

            if (user is not null && context.Password.Equals(user.Senha))
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
            }
        }
    }
}