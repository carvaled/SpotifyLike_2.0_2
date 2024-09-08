using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.IdentityModel.Tokens;

namespace SpotifyLike.STS
{
    internal class IdentityServerConfigurations
    {
        const string API_RESOURCE_NAME = "spotify-like-api-resource";
        const string DISPLAY_API_RESOURCE_NAME = "LiteStreamingResource";
        static readonly string[] USER_CLAIMS = { "openid", "profile", "email", "userid", "role" };
        const string SCOPE_NAME = "spotify-like-scope";
        const string DISPLAY_NAME_SCOPE = "LiteStreamingScope";
        const string SECRET = "spotify-like-secret";
        const string CLIENT_ID = "client-angular-spotify-like";
        const string CLIENT_NAME = "Frontend Angular Application";

        public static IEnumerable<IdentityResource> GetIdentityResource()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
        {
            new ApiResource(API_RESOURCE_NAME , DISPLAY_API_RESOURCE_NAME, USER_CLAIMS )
            {
                ApiSecrets =
                {
                    new Secret(SECRET.Sha256())
                },
                Scopes = { "spotify-like-scope" },
                AllowedAccessTokenSigningAlgorithms = { SecurityAlgorithms.RsaSha256 }
            }
        };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return
            [
                new ApiScope()
            {
                Name = SCOPE_NAME,
                DisplayName = DISPLAY_NAME_SCOPE,
                UserClaims = USER_CLAIMS
            }
            ];
        }


        public static IEnumerable<Client> GetClients()
        {
            return
            [
                new Client()
            {
                ClientId = CLIENT_ID,
                ClientName = CLIENT_NAME,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets =
                {
                    new Secret(SECRET.Sha256())
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.AccessTokenAudience,
                    SCOPE_NAME
                },
                RefreshTokenExpiration = TokenExpiration.Sliding,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true,
            }
            ];
        }
    }
}