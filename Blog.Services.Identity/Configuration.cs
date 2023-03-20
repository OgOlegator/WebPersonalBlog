using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Blog.Services.Identity
{
    public class Configuration
    {
        public const string Admin = "Admin";
        public const string Client = "User";

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("BlogWebAPI", "Web API"),
                //new ApiScope(name: "read", displayName: "Read your data."),
                //new ApiScope(name: "create", displayName: "Create your data."),
                //new ApiScope(name: "delete", displayName: "Delete your data."),
                //new ApiScope(name: "edit", displayName: "Edit your data."),
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
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
                    
                },
            };

    }
}
