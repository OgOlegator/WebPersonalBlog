using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Blog.Services.Identity
{
    public static class SD
    {

        public const string Admin = "Admin";
        public const string User = "User";

        public static IEnumerable<IdentityResource> IdentityResources
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes
            => new List<ApiScope> 
            { 
                new ApiScope("Blog", "Blog Server"),
                new ApiScope("read", "Read your data."),
                new ApiScope("write", "Write your data."),
                new ApiScope("delete", "Delete your data.")
            };

        public static IEnumerable<Client> Clients
            => new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) }, // При создании реального приложения нужно добавить
                                                                       // вместо строки "secret" реальный ключ
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile" }
                },
                new Client
                {
                    ClientId = "blog",
                    ClientSecrets = { new Secret("secret".Sha256()) }, 
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44371/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44371/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "blog"
                    }
                }
            };
    }
}
