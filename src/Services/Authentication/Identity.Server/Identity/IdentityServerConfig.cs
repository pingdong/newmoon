using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace PingDong.Newmoon.IdentityServer.Identity
{
    public class IdentityServerConfig
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            // http://docs.identityserver.io/en/release/reference/identity_resource.html
            return new List<IdentityResource>
                {
                    new IdentityResources.Email(),
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile()
                };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            // http://docs.identityserver.io/en/release/reference/api_resource.html
            return new List<ApiResource>
                {
                    new ApiResource (name: "events.api", displayName: "Events API"),
                    new ApiResource(name: "postman_api", displayName: "Postman")
                };
        }

        // http://docs.identityserver.io/en/release/reference/client.html
        public static IEnumerable<Client> GetClients()
        {
            var eventsApiUri = "http://localhost:5001";
            var postmanUri = "http://localhost:5001";

            // client credentials client
            return new List<Client>
                {
                    new Client
                    {
                        ClientId = "postman",
                        ClientName = "Postman",
                        ClientSecrets = { new Secret("postman-secret".Sha256()) },
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowAccessTokensViaBrowser = true,
                        RequireConsent = false,
                        EnableLocalLogin = true,
                        RedirectUris = { $"{postmanUri}" },
                        PostLogoutRedirectUris = { $"{postmanUri}" },
                        AllowedCorsOrigins = { $"{postmanUri}" },
                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Email,
                            IdentityServerConstants.StandardScopes.Profile,
                            "postman_api"
                        }
                    },
                    new Client
                    {
                        ClientId = "eventsclient",
                        ClientName = "EventsClient",
                        ClientSecrets = { new Secret("events_api-secret".Sha256()) },
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowAccessTokensViaBrowser = true,
                        EnableLocalLogin = true,
                        RedirectUris = { $"{eventsApiUri}" },
                        PostLogoutRedirectUris = { $"{eventsApiUri}" },
                        AllowedCorsOrigins = { $"{eventsApiUri}" },
                        AllowedScopes =
                            {
                                IdentityServerConstants.StandardScopes.OpenId,
                                IdentityServerConstants.StandardScopes.Profile,
                                "events.api"
                            }
                    }
                };
        }
    }
}
