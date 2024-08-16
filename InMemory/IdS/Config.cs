using Duende.IdentityServer.Models;

namespace PasswordlessIdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("api1"),
            new ApiScope("scope2"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "mvc",
                ClientName = "MVC Client",

                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RequirePkce = true,
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                RedirectUris = { "https://localhost:5003/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:5003/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:5003/signout-callback-oidc" },

                RequireConsent = false,
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "api1" }
            },
        };
};