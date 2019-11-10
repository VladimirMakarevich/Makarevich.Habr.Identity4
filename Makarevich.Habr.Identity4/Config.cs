using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Makarevich.Habr.Identity4 {
    public static class Config {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource> {
                new ApiResource("demo-api", "Demo Api")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client> {
                new Client() {
                    ClientId = "angularClient",
                    ClientUri = "/",
                    ClientName = "angularClient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = false,
                    RequireClientSecret = false,
                    RequireConsent = false,
//                    RedirectUris = {
//                        "https://localhost:44308/account/login",
//                        "https://localhost:44308/home",
//                        "https://localhost:44308/forbidden",
//                        "https://localhost:44308/unauthorized",
//                    },
//                    PostLogoutRedirectUris = {
//                        "https://localhost:44308/account/login",
//                        "https://localhost:44308/home",
//                        "https://localhost:44308/forbidden",
//                        "https://localhost:44308/unauthorized",
//                        "https://localhost:44308"
//                    },
//                    AllowedCorsOrigins = {
//                        "https://localhost:44308"
//                    },
                    IdentityTokenLifetime = 180,
                    AccessTokenLifetime = 900,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                }
                // machine to machine client
                //new Client
                //{
                //    ClientId = "angularClient",
                //    ClientName = "angularClient",
                //    //ClientSecrets = { new Secret("secret".Sha256()) },

                //    AllowedGrantTypes = GrantTypes.Code,
                //    // scopes that client has access to
                //    AllowedScopes = {
                //      IdentityServerConstants.StandardScopes.OpenId,
                //      IdentityServerConstants.StandardScopes.Profile,
                //    },
                //    AllowedCorsOrigins = {
                //        "http://localhost:4200",
                //        "https://localhost:44308"
                //    },
                //},
                //// interactive ASP.NET Core MVC client
                //new Client
                //{
                //    ClientId = "mvc",
                //    ClientSecrets = { new Secret("secret".Sha256()) },

                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequireConsent = false,
                //    RequirePkce = true,

                //    // where to redirect to after login
                //    RedirectUris = { "http://localhost:5002/signin-oidc" },

                //    // where to redirect to after logout
                //    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                //    AllowedScopes = new List<string>
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile
                //    }
                //}
            };
    }
}