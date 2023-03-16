using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Blog.Services.Identity
{
    public class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("BlogWebAPI", "Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("BlogWebAPI", "Web API", new []
                    { JwtClaimTypes.Name})
                {
                    Scopes = {"BlogWebAPI"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "blog-web-app",
                    ClientName = "Blog Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =                  //Указан uri куда происходит переадресация после аутентификации. Заполнить!!!
                    {
                        "https://localhost:7217/signin-oidc"
                    },
                    AllowedCorsOrigins =    //Набор Uri адресов, которые будут использовать (позволено) IdentityServer 
                    {
                        "https://localhost:7217"
                    },  
                    PostLogoutRedirectUris =    //Набор Uri куда может происходить переадрисация после выхода. Заполнить!!!
                    {
                        "https://localhost:7217/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "BlogWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true,

                    
                }
            };

    }
}
